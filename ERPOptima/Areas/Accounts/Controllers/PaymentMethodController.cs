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
    public class PaymentMethodController : Controller
    {
        //
        // GET: /Accounts/PaymentMethod/
        private IAnFPaymentMethodService _pmService;

         public PaymentMethodController()
        {
            var dbfactory = new DatabaseFactory();
            _pmService = new AnFPaymentMethodService(new AnFPaymentMethodRepository(dbfactory), new UnitOfWork(dbfactory));
           
        }
         [AuthorizeUser]
         [ResourcePermissionAttribute]

        public ActionResult Index()
        {
            return View();
        }

        #region Payment Method

        [HttpGet]
        public ActionResult GetPaymentMethods()
        {
            var list = _pmService.GetAnFPaymentMethods().ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }
       
        [HttpPost]
        public ActionResult SavePaymentMethod(AnFPaymentMethod anFPaymentMethod)
        {
            Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid)
            {
                if (anFPaymentMethod.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        objOperation = _pmService.SaveAnFPaymentMethod(anFPaymentMethod);
                    }
                    else { objOperation.OperationId = -1; }
                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        objOperation = _pmService.UpdateAnFPaymentMethod(anFPaymentMethod);
                    }
                    else { objOperation.OperationId = -2; }
                }
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult DeletePaymentMethod(int Id)
        {
           Operation objOperation = new Operation { Success = false };
            if (Id != 0)
            {
                AnFPaymentMethod obj = _pmService.GetById(Id);
                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _pmService.DeleteAnFPaymentMethod(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }
     

        #endregion
    }
}