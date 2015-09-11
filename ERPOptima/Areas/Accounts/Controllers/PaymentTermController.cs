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
    public class PaymentTermController : Controller
    {
        //
        // GET: /Accounts/PaymentTerm/
        private IAnFPaymentTermService _ptService;

         public PaymentTermController()
        {
            var dbfactory = new DatabaseFactory();
            _ptService = new AnFPaymentTermService(new AnFPaymentTermRepository(dbfactory), new UnitOfWork(dbfactory));
           
        }
         [AuthorizeUser]
         [ResourcePermissionAttribute]

        public ActionResult Index()
        {
            return View();
        }

        #region Payment  Term

        [HttpGet]
        public ActionResult GetPaymentTerms()
        {
            var list = _ptService.GetAnFPaymentTerms().ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }
       
        [HttpPost]
        public ActionResult SavePaymentTerm(AnFPaymentTerm anFPaymentTerm)
        {
            Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid)
            {
                if (anFPaymentTerm.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        objOperation = _ptService.SaveAnFPaymentTerm(anFPaymentTerm);
                    }
                    else { objOperation.OperationId = -1; }
                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        objOperation = _ptService.UpdateAnFPaymentTerm(anFPaymentTerm);
                    }
                    else { objOperation.OperationId = -2; }
                }
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult DeletePaymentTerm(int Id)
        {
           Operation objOperation = new Operation { Success = false };
            if (Id != 0)
            {
                AnFPaymentTerm obj = _ptService.GetById(Id);
                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _ptService.DeleteAnFPaymentTerm(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }
     

        #endregion
    }
}