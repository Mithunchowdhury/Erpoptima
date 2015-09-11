using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Service.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Optima.Areas.Sales.Controllers
{
    public class ReportInvoiceController : Controller
    {
        private IReportInvoiceService _ReportInvoiceService;
        //
        // GET: /Sales/ReportInvoice/
        public ReportInvoiceController()
        {
            var dbfactory = new DatabaseFactory();
            _ReportInvoiceService = new ReportInvoiceService(new ReportInvoiceRepository(dbfactory), new UnitOfWork(dbfactory));
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAll()
        {
            string cmpny = Session["CompanyId"].ToString();
            var list = _ReportInvoiceService.GetAll(int.Parse(cmpny)).ToList();
            var invoiceNos = list.Select(i => new { Id = i.Id, InvoiceNo = i.InvoiceNo }).Distinct().ToList();
            return Json(invoiceNos, JsonRequestBehavior.AllowGet);
        }
    }
}
