using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Inventory.Repository;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Service.Sales;
using ERPOptima.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Optima.Areas.Sales.Controllers
{
    public class SalesReturnListController : Controller
    {
        private ISalesReturnService _SalesReturnService;
        public SalesReturnListController()
        {
            var dbfactory = new DatabaseFactory();
            _SalesReturnService = new SalesReturnService(new SalesReturnRepository(dbfactory),
                new SalesReturnDetailRepository(dbfactory), new StockInRepository(dbfactory), new UnitOfWork(dbfactory));            
        }
        //
        // GET: /Sales/SalesReturnList/

        
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult SalesReturn()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetAll(DateTime StartDate, DateTime EndDate)
        {
            int companyid = int.Parse(Session["companyId"].ToString());
            var list = _SalesReturnService.Get(companyid, StartDate, EndDate);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}
