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
    public class DeliveryMethodController : Controller
    {
        //
        // GET: /Accounts/DeliveryMethod/
        private IAnFDeliveryMethodService _dmService;

        public DeliveryMethodController()
        {
            var dbfactory = new DatabaseFactory();
            _dmService = new AnFDeliveryMethodService(new AnFDeliveryMethodRepository(dbfactory), new UnitOfWork(dbfactory));
           
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]

        public ActionResult Index()
        {
            return View();
        }

        #region Delivery Method

        [HttpGet]
        public ActionResult GetAnFDeliveryMethods()
        {
            var list = _dmService.GetAnFDeliveryMethods().ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }
       
        [HttpPost]
        public ActionResult SaveDeliveryMethod(AnFDeliveryMethod anFDeliveryMethod)
        {
            Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid)
            {
                if (anFDeliveryMethod.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        objOperation = _dmService.SaveAnFDeliveryMethod(anFDeliveryMethod);
                    }
                    else { objOperation.OperationId = -1; }
                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        objOperation = _dmService.UpdateAnFDeliveryMethod(anFDeliveryMethod);
                    }
                    else { objOperation.OperationId = -2; }
                }
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult DeleteAnFDeliveryMethod(int Id)
        {
           Operation objOperation = new Operation { Success = false };
            if (Id != 0)
            {
                AnFDeliveryMethod obj = _dmService.GetById(Id);
                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _dmService.DeleteAnFDeliveryMethod(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }
     

        #endregion
    }
}