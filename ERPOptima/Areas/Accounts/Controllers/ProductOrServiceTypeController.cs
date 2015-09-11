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
    public class ProductOrServiceTypeController : Controller
    {
        //
        // GET: /Accounts/ProductOrServiceType/
        private IAnFProductOrServiceTypeService _ccService;

        public ProductOrServiceTypeController()
        {
            var dbfactory = new DatabaseFactory();
            _ccService = new AnFProductOrServiceTypeService(new AnFProductOrServiceTypeRepository(dbfactory), new UnitOfWork(dbfactory));

        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]

        public ActionResult Index()
        {
            return View();
        }

        #region ProductOrServiceType

        [HttpGet]
        public ActionResult GetProductOrServiceTypes()
        {
            var list = _ccService.GetProductOrServiceTypes().ToList();//.Select(ac => new

            return Json(list, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult SaveProductOrServiceType(AnFProductOrServiceType anFProductOrServiceType)
        {
            Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid)
            {

                if (anFProductOrServiceType.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        objOperation = _ccService.SaveAnFProductOrServiceType(anFProductOrServiceType);
                    }
                    else { objOperation.OperationId = -1; }
                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        objOperation = _ccService.UpdateAnFProductOrServiceType(anFProductOrServiceType);
                    }
                    else { objOperation.OperationId = -2; }
                }
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult DeleteAnFProductOrServiceType(int Id)
        {
            Operation objOperation = new Operation { Success = false };

            if (Id != 0)
            {
                AnFProductOrServiceType obj = _ccService.GetById(Id);

                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _ccService.DeleteAnFProductOrServiceType(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }


        #endregion
    }
}