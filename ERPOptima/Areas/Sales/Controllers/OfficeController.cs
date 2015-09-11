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
    public class OfficeController : Controller
    {
        //
        // GET: /Sales/Office/
        [AuthorizeUser]
        [ResourcePermissionAttribute]

        public ActionResult Index()
        {
            return View();
        }
         private IOfficeService _officeService;

        public OfficeController()
        {
            var dbfactory = new DatabaseFactory();
            _officeService = new OfficeService(new OfficeRepository(dbfactory), new UnitOfWork(dbfactory));
        }
        #region Office

        [HttpGet]
        public ActionResult GetAll()
        {
            var list = _officeService.GetAll();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetByRegionId(int regionId)
        {
            var list = _officeService.GetAll().Where(i=>i.SlsRegionId == regionId).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetOfficeByEmployee(int? employeeId,int regionId) 
        {
            Collection<AreaConfigurationViewModel> areaConfigurations = null;
            DataTable dt = _officeService.GetOfficeByEmployee(employeeId,regionId);
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
        public ActionResult Save(SlsOffice office)
        {
            int userId = Convert.ToInt32(Session["userId"]);
            Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid)
            {
                if (office.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        office.CreatedBy = userId;
                        office.CreatedDate = DateTime.Now.Date;
                        objOperation = _officeService.Save(office);
                    }
                    else { objOperation.OperationId = -1; }

                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        office.ModifiedBy = userId;
                        office.ModifiedDate = DateTime.Now.Date;
                        objOperation = _officeService.Update(office);
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
                SlsOffice obj = _officeService.GetById(Id);

                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _officeService.Delete(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        [AllowAnonymous]
        public JsonResult IsOfficeCodeAvailable(string officeCode)
        {
            var list = _officeService.GetAll();
            bool status = true;
            
            if (officeCode.Trim().Length > 0 &&
                list != null && list.Count() > 0)
            {
                var existobj = list.Where(i => i.Code == officeCode).FirstOrDefault();
                if (existobj != null && existobj.Id > 0)
                    status = false;
            }
            return Json(new { result = status});
        }

        public ActionResult RegionIdOfThisOffice(int officeId)
        {
            var list = _officeService.GetAll().Where(i => i.Id == officeId).ToList();
            if (list == null || list.Count() == 0)
                return Json(new { result = 0, status = false }, JsonRequestBehavior.AllowGet);
            var regionId = list.First().SlsRegionId;
            return Json(new { result = regionId, status = true }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
