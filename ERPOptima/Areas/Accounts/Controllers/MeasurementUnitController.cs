using ERPOptima.Data.Accounts.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Helper;
using ERPOptima.Lib.Model;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Accounts;
using ERPOptima.Service.Accounts;
using ERPOptima.Web.Accounts.ViewModel;
using ERPOptima.Web.Filters;
using Optima.Areas.Accounts.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Optima.Areas.Accounts.Controllers
{
    public class MeasurementUnitController : Controller
    {
        //
        // GET: /Accounts/MeasurementUnit/
        private IAnFMeasurementUnitService _pmService;

         public MeasurementUnitController()
        {
            var dbfactory = new DatabaseFactory();
            _pmService = new AnFMeasurementUnitService(new AnFMeasurementUnitRepository(dbfactory), new UnitOfWork(dbfactory));
           
        }
         [AuthorizeUser]
         [ResourcePermissionAttribute]

        public ActionResult Index()
        {
            return View();
        }

        #region Measurement Unit

        [HttpGet]
        public ActionResult GetMeasurementUnits()
        {
            var list = _pmService.GetAnFMeasurementUnits().ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }
       
        [HttpPost]
        public ActionResult SaveMeasurementUnit(AnFMeasurementUnit anFMeasurementUnit)
        {
            Operation objOperation = new Operation { Success = false };
            int companyId = Convert.ToInt32(Session["companyId"]);
            if (ModelState.IsValid)
            {
                anFMeasurementUnit.CmnCompanyId = companyId;
                if (anFMeasurementUnit.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        objOperation = _pmService.SaveAnFMeasurementUnit(anFMeasurementUnit);
                    }
                    else { objOperation.OperationId = -1; }
                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        objOperation = _pmService.UpdateAnFMeasurementUnit(anFMeasurementUnit);
                    }
                    else { objOperation.OperationId = -2; }
                }
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult DeleteMeasurementUnit(int Id)
        {
           Operation objOperation = new Operation { Success = false };
            if (Id != 0)
            {
                AnFMeasurementUnit obj = _pmService.GetById(Id);
                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _pmService.DeleteAnFMeasurementUnit(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }
     

        #endregion
    }
}