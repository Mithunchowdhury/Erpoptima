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
    public class CorporateClientController : Controller
    {
        private ICorporateClientService _corporateClientService;
        public CorporateClientController()
        {
            var dbfactory = new DatabaseFactory();
            _corporateClientService = new CorporateClientService(new CorporateClientRepository(dbfactory), new UnitOfWork(dbfactory));
        }
        //
        // GET: /Sales/CorporateClient/
        [AuthorizeUser]
        [ResourcePermissionAttribute]

        public ActionResult Index()
        {
            return View();
        }

        #region Corporate

        [HttpGet]        
        public ActionResult GetAll()
        {
            var list = _corporateClientService.GetAll();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetCorporateName()
        {
            var list = _corporateClientService.GetAll();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
       
        //public ActionResult GetById(int Id)
        //{
        //    var list = _corporateClientService.GetAll().Where(i => i.ExecutiveName == Id).ToList();
        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}

         [HttpGet]

        public ActionResult GetById(int distId)
        {
            var corporateClient = _corporateClientService.GetAll().Where(i => i.Id == distId).First();
            return Json(corporateClient, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult GetCorporateClientById(int? employeeId, int districtId)
        //{
        //    Collection<AreaConfigurationViewModel> areaConfigurations = null;
        //    DataTable dt = _corporateClientService.GetCorporateClientById(employeeId, districtId);
        //    if (dt != null)
        //    {
        //        areaConfigurations = new Collection<AreaConfigurationViewModel>();
        //        foreach (DataRow row in dt.Rows)
        //        {
        //            areaConfigurations.Add((AreaConfigurationViewModel)ERPOptima.Lib.Utilities.Helper.FillTo(row, typeof(AreaConfigurationViewModel)));
        //        }
        //    }
        //    return Json(areaConfigurations, JsonRequestBehavior.AllowGet);
        //}
        //public ActionResult GetByDistrictId(int districtId)
        //{
        //    var list = _corporateClientService.GetAll().Where(i => i.SlsDistrictId == districtId).ToList();
        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}
      


        [HttpPost]
        public ActionResult Save(SlsCorporateClient slsCorporateClient)
        {
            int userId = Convert.ToInt32(Session["userId"]);
            Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid)
            {
                if (slsCorporateClient.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        slsCorporateClient.CreatedBy = userId;
                        slsCorporateClient.CreatedDate = DateTime.Now.Date;
                        objOperation = _corporateClientService.Save(slsCorporateClient);
                    }
                    else { objOperation.OperationId = -1; }

                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        slsCorporateClient.ModifiedBy = userId;
                        slsCorporateClient.ModifiedDate = DateTime.Now.Date;
                        objOperation = _corporateClientService.Update(slsCorporateClient);
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
                SlsCorporateClient obj = _corporateClientService.GetById(Id);

                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _corporateClientService.Delete(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        #endregion

        [AllowAnonymous]
        public JsonResult IsNameAvailable(string name)
        {
            var list = _corporateClientService.GetAll();
            bool status = true;

            if (name.Trim().Length > 0 &&
                list != null && list.Count() > 0)
            {
                var existobj = list.Where(i => i.Name == name).FirstOrDefault();
                if (existobj != null && existobj.Id > 0)
                    status = false;
            }
            return Json(new { result = status });
        }

        [AllowAnonymous]
        public JsonResult IsCodeAvailable(string code)
        {
            var list = _corporateClientService.GetAll();
            bool status = true;

            if (code.Trim().Length > 0 &&
                list != null && list.Count() > 0)
            {
                var existobj = list.Where(i => i.Code == code).FirstOrDefault();
                if (existobj != null && existobj.Id > 0)
                    status = false;
            }
            return Json(new { result = status });
        }

    }
}
