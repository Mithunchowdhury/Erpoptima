using ERPOptima.Data.Common.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Inventory.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Inventory;
using ERPOptima.Model.Security;
using ERPOptima.Service.Inventory;
using ERPOptima.Service.Sales;
using ERPOptima.Service.Security;
using ERPOptima.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Optima.Areas.Sales.Controllers
{
    public class StockInController : Controller
    {

        private IStockInService _stockInService;
        private IStoreService _storeService;
       

        public StockInController()
        {
            var dbfactory = new DatabaseFactory();
            _stockInService = new StockInService(new StockInRepository(dbfactory), new UnitOfWork(dbfactory));
            _storeService = new StoreService(new InvStoreRepository(dbfactory), new UnitOfWork(dbfactory));
        
        }

        //
        // GET: /Sales/StockIn/
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]       
        public ActionResult GetAll()
        {
            var list = _stockInService.GetAll();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Search(DateTime? DateFrom = null, DateTime? DateTo = null)
        {
            var list = _stockInService.GetAll();

            if (DateFrom != null)
                list = list.Where(i => i.TransactionDate != null && (DateFrom <= i.TransactionDate.Value)).ToList();

            if (DateTo != null)
                list = list.Where(i => i.TransactionDate != null && (DateTo >= i.TransactionDate.Value)).ToList();

            //list = list.Where(i => i.TransactionDate != null && (DateFrom <= i.TransactionDate.Value && DateTo >= i.TransactionDate.Value)).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        //public ActionResult GetAll()
        //{
        //    var list = _regionService.GetAll();
        //    JsonResult jresult = new JsonResult();

        //    jresult = Json(list, JsonRequestBehavior.AllowGet);

        //    return jresult;
        //}

        [HttpGet]

        public ActionResult GetAllStores()
        {
            int userId = Convert.ToInt32(Session["userId"]);
            var list = _storeService.GetStoresForOffice(1);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //[HttpGet]
        //public string GetRefNo(int companyId)
        //{
        //    SecCompany objCmnCompany = _SecCompanyService.GetById(companyId);
        //    string refNo = _stockInService.GetRefNo(companyId, objCmnCompany.Prefix);
        //    return refNo;
        //    //SecCompany objCmnCompany = _SecCompanyService.GetById(companyId);
        //    //string code = _IssueService.GetLastCode(companyId, objCmnCompany.Prefix);
        //    //return code;

        //}

        [HttpPost]
        public ActionResult Save(InvStockInOut invStockInOut)
        {
            int userId = Convert.ToInt32(Session["userId"]);
            Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid)
            {
                if (invStockInOut.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {

                        objOperation = _stockInService.Save(invStockInOut);
                    }
                    else { objOperation.OperationId = -1; }

                }
                else
                {
                    if ((bool)Session["Edit"])
                    {

                        objOperation = _stockInService.Update(invStockInOut);
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
                InvStockInOut obj = _stockInService.GetById(Id);

                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _stockInService.Delete(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }


        
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult History()
        {
            return View();
        }
       

    }
}
