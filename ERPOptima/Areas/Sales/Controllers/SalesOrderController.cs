using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Model.Sales;
using ERPOptima.Model.ViewModel;
using ERPOptima.Service.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Optima.Areas.Sales.Controllers
{
    public class SalesOrderController : Controller
    {
        //
        // GET: /Sales/SalesOrder/
        private ISalesOrderService<SlsSalesOrderApproval, SlsSalesOrderViewModel> _salesOrderService;
        public SalesOrderController()
        {
            var dbfactory = new DatabaseFactory();
            var unitOfWork = new UnitOfWork(dbfactory);
            _salesOrderService = new SalesOrderService(new SalesOrderRepository(dbfactory),
                new SalesOrderDetailRepository(dbfactory),
                new SalesOrderApprovalRepository(dbfactory),
                new NotificationRepository(dbfactory),
                new NotificationDetailRepository(dbfactory),
                unitOfWork);
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetAllRegularSales()
        {
            try
            {
                var list = _salesOrderService.GetAll().OrderByDescending(i => i.Id).ToList();
                 
                //1=Regular,2=Corporate,3=Retail
                //Load all regular sales order list
                list = list.Where(i => i.SalesType == 1).ToList();

                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

            }

            return Json(new List<SlsSalesOrderViewModel>(), JsonRequestBehavior.AllowGet);
        }

    }
}
