using ERPOptima.Data.Common.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Helper;
using ERPOptima.Lib.Model;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Common;
using ERPOptima.Service.Common;
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

namespace Optima.Areas.Common.Controllers
{
    public class CurrencyController : Controller
    {
        //
        // GET: /Common/Business/
        private ICmnCurrencyService _ccService;

        public CurrencyController()
        {
            var dbfactory = new DatabaseFactory();
            _ccService = new CmnCurrencyService(new CmnCurrencyRepository(dbfactory), new UnitOfWork(dbfactory));
           
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]

        public ActionResult Index()
        {
            return View();
        }
        #region Currency

        [HttpGet]
        public ActionResult GetCurrencies()
        {
            var list = _ccService.GetCmnCurrencies().ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }       
           
        [HttpPost]
        public ActionResult SaveCmnCurrency(CmnCurrency currency)
        {
                        Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid)
            {
                int userId = Convert.ToInt32(Session["userId"].ToString());
                currency.CreatedBy = userId;
                if (currency.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        objOperation = _ccService.SaveCmnCurrency(currency);
                    }
                    else { objOperation.OperationId = -1; }
                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        currency.ModifiedBy = userId;
                        objOperation = _ccService.UpdateCmnCurrency(currency);
                    }
                    else { objOperation.OperationId = -2; }
                }
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult DeleteCmnCurrency(int Id)
        {
            Operation objOperation = new Operation { Success = false };
            if (Id != 0)
            {
                CmnCurrency obj = _ccService.GetById(Id);

                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _ccService.DeleteCmnCurrency(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }        

        #endregion
    }
}
