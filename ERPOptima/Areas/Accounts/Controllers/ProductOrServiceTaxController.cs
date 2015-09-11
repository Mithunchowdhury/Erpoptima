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
    public class ProductOrServiceTaxController : Controller
    {
        //
        // GET: /Accounts/ProductOrServiceTax/
        private IAnFProductOrServiceTaxService _ccService;

        public ProductOrServiceTaxController()
        {
            var dbfactory = new DatabaseFactory();
            _ccService = new AnFProductOrServiceTaxService(new AnFProductOrServiceTaxRepository(dbfactory), new UnitOfWork(dbfactory));
           
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]

        public ActionResult Index()
        {
            return View();
        }

        #region ProductOrServiceTax

        [HttpGet]
        public ActionResult GetProductOrServiceTaxes()
        {
            var list = _ccService.GetProductOrServiceTaxes().ToList();//.Select(ac => new

            return Json(list, JsonRequestBehavior.AllowGet);
        }
       
       
       
        [HttpPost]
        public ActionResult SaveProductOrServiceTax(AnFProductOrServiceTax anFProductOrServiceTax)
        {
            Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid)
            {
                
                if (anFProductOrServiceTax.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        objOperation = _ccService.SaveAnFProductOrServiceTax(anFProductOrServiceTax);
                    }
                    else { objOperation.OperationId = -1; }
                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        objOperation = _ccService.UpdateAnFProductOrServiceTax(anFProductOrServiceTax);
                    }
                    else { objOperation.OperationId = -2; }
                }
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult DeleteAnFProductOrServiceTax(int Id)
        {
                        Operation objOperation = new Operation { Success = false };

            if (Id != 0)
            {
                AnFProductOrServiceTax obj = _ccService.GetById(Id);

                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _ccService.DeleteAnFProductOrServiceTax(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }
      

        #endregion
    }
}