using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Sales;
using ERPOptima.Model.ViewModel;
using ERPOptima.Service.Sales;
using ERPOptima.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Optima.Areas.Sales.Controllers
{
    public class CommissionPaymentController : Controller
    {
        //
        // GET: /Sales/CommissionPayment/
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Index()
        {
            return View();
        }

        private ISalesOrderService<SlsSalesOrderApproval, SlsSalesOrderViewModel> _salesOrderService;
        private ICommissionPackageService _CommissionPackageService;
        private ICommissionService _CommissionService;
        public CommissionPaymentController()
        {
            var dbfactory = new DatabaseFactory();
            _salesOrderService = new SalesOrderService(new SalesOrderRepository(dbfactory),
                new SalesOrderDetailRepository(dbfactory),
                new SalesOrderApprovalRepository(dbfactory),
                new NotificationRepository(dbfactory),
                new NotificationDetailRepository(dbfactory),
                new UnitOfWork(dbfactory));
            _CommissionPackageService = new CommissionPackageService(new CommissionPackageRepository(dbfactory),
                new UnitOfWork(dbfactory));
            _CommissionService = new CommissionService(new CommissionRepository(dbfactory),
                new UnitOfWork(dbfactory));
        }
        [HttpGet]
        public ActionResult GetNetSales(DateTime from, DateTime to, int partyType, int party)
        {                       
            var lastDayOfMonth = to.AddMonths(1).AddDays(-to.Day);
            //change from date to start from first date of month
            from = new DateTime(from.Year, from.Month, 1);
            //change to date to end to last date of month
            to = new DateTime(to.Year, to.Month, lastDayOfMonth.Day);

            //test
            decimal netSales = _salesOrderService.GetNetSales(from, to, partyType, party);
            return Json(new { result = netSales }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetCommissionRate(DateTime from, DateTime to, int partyType, int party, decimal netSales)
        {
            var lastDayOfMonth = to.AddMonths(1).AddDays(-to.Day);
            //change from date to start from first date of month
            from = new DateTime(from.Year, from.Month, 1);
            //change to date to end to last date of month
            to = new DateTime(to.Year, to.Month, lastDayOfMonth.Day);

            //test            
            decimal commission = _CommissionPackageService.GetCommissionRate(from, to, netSales, partyType, party);
            return Json(new { result = commission }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Save(SlsCommissionViewModel obj)
        {
            int userId = Convert.ToInt32(Session["userId"]);            
            Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid)
            {   
                if (obj.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {   
                        obj.CreatedBy = userId;
                        obj.CreatedDate = DateTime.Now.Date;
                        objOperation = _CommissionService.Save(obj);
                    }
                    else { objOperation.OperationId = -1; }

                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        obj.ModifiedBy = userId;
                        obj.ModifiedDate = DateTime.Now.Date;
                        objOperation = _CommissionService.Update(obj);
                    }
                    else { objOperation.OperationId = -2; }
                }
            }
            else
            {
                var errors = ModelState
                          .Where(x => x.Value.Errors.Count > 0)
                          .Select(x => new { x.Key, x.Value.Errors })
                          .ToArray();
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            Operation objOperation = new Operation { Success = false };

            if (Id > 0)
            {
                objOperation = _CommissionService.Delete(Id);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var list = _CommissionService.GetAllVM();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

       
    }
}
