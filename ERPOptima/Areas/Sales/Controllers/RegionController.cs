using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Sales;
using ERPOptima.Service.Sales;
using ERPOptima.Web.Filters;
using Optima.Areas.Sales.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Optima.Areas.Sales.Controllers
{
    public class RegionController : Controller
    {
        //
        // GET: /Sales/Region/
        private IRegionService _regionService;

        public RegionController()
        {
            var dbfactory = new DatabaseFactory();
            var datacontext=dbfactory.Get();
           
            _regionService = new RegionService(new RegionRepository(dbfactory), new UnitOfWork(dbfactory));
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Index()
        {
            return View();
        }
         
        #region Region

        [HttpGet]
        public ActionResult GetAll()
        {
            var dbfactory = new DatabaseFactory();
            var dataContext = dbfactory.Get();
            var rawlist = _regionService.GetAll();
            var empSrv = new ERPOptima.Service.Hrm.HrmEmployeeService(new ERPOptima.Data.Hrm.Repository.HrmEmployeeRepository(dbfactory), new UnitOfWork(dbfactory));
            var list = rawlist.Select(m => new
            {
                Id=m.Id,
                Code=m.Code,
                Name=m.Name,
                Head=m.Head,
                Remarks=m.Remarks,

                HeadName = empSrv.GetById(m.Head) == null ? "" : empSrv.GetById(m.Head).Name
                
            });
            JsonResult jresult = new JsonResult();
            
            jresult = Json(list, JsonRequestBehavior.AllowGet);

            return jresult;
        }
        public ActionResult GetAllRegion()
        {
            var list = _regionService.GetAll();
            return Json(list, JsonRequestBehavior.AllowGet);
        }  
        public ActionResult GetById(int Id)
        {
            var list = _regionService.GetAll().Where(i => i.Head == Id).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetRegionByEmployee(int employeeId)
        {
            Collection<AreaConfigurationViewModel> areaConfigurations = null;
            DataTable dt = _regionService.GetRegionByEmployee(employeeId);
            if (dt != null)
            {
                areaConfigurations = new Collection<AreaConfigurationViewModel>();
                foreach (DataRow row in dt.Rows)
                {
                    areaConfigurations.Add((AreaConfigurationViewModel)ERPOptima.Lib.Utilities.Helper.FillTo(row, typeof(AreaConfigurationViewModel)));
                }
            }

            return Json(areaConfigurations, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Save(SlsRegion slsRegion)
        {
            int userId = Convert.ToInt32(Session["userId"]);
            Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid)
            {
                if (slsRegion.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {

                        slsRegion.CreatedBy = userId;
                        slsRegion.CreatedDate = DateTime.Now.Date;
                        objOperation = _regionService.Save(slsRegion);
                    }
                    else { objOperation.OperationId = -1; }

                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        slsRegion.ModifiedBy = userId;
                        slsRegion.ModifiedDate = DateTime.Now.Date;
                        objOperation = _regionService.Update(slsRegion);
                    }
                    else { objOperation.OperationId = -2; }
                }
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            Operation objOperation = new Operation { Success = false };

            if (Id != 0)
            {
                SlsRegion obj = _regionService.GetById(Id);

                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _regionService.Delete(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        #endregion

    }
}
