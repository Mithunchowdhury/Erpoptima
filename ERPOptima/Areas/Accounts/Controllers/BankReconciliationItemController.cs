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
    public class BankReconciliationItemController : Controller
    {
        //
        // GET: /Accounts/BankReconciliationItem/
        private IAnFBankReconciliationItemService _pmService;

         public BankReconciliationItemController()
        {
            var dbfactory = new DatabaseFactory();
            _pmService = new AnFBankReconciliationItemService(new AnFBankReconciliationItemRepository(dbfactory), new UnitOfWork(dbfactory));
           
        }
        [AuthorizeUser]

        public ActionResult Index()
        {
            return View();
        }

        #region Reconciliation Item

        [HttpGet]
        public ActionResult GetBankReconciliationItems()
        {
            var list = _pmService.GetAnFBankReconciliationItems().ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }
       
        [HttpPost]
        public ActionResult SaveBankReconciliationItem(AnFBankReconciliationItem anFBankReconciliationItem)
        {
            Operation objOperation = new Operation { Success = false };
            if (ModelState.IsValid)
            {
                if (anFBankReconciliationItem.Id == 0)
                {
                    objOperation = _pmService.SaveAnFBankReconciliationItem(anFBankReconciliationItem);
                }
                else
                {
                    objOperation = _pmService.UpdateAnFBankReconciliationItem(anFBankReconciliationItem);
                }
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult DeleteBankReconciliationItem(int Id)
        {
           Operation objOperation = new Operation { Success = false };
            if (Id != 0)
            {
                AnFBankReconciliationItem obj = _pmService.GetById(Id);
                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _pmService.DeleteAnFBankReconciliationItem(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }
     

        #endregion
    }
}