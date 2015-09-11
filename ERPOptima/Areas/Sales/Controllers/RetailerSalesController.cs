using ERPOptima.Data.Common.Repository;
using ERPOptima.Data.Hrm.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Inventory.Repository;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.HRM;
using ERPOptima.Model.Sales;
using ERPOptima.Model.Security;
using ERPOptima.Model.ViewModel;
using ERPOptima.Service.Hrm;
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
    public class RetailerSalesController : Controller
    {
        private ISalesOrderService<SlsSalesOrderApproval, SlsSalesOrderViewModel> _salesOrderService;
        private IOfficeService _officeService;
        private ISecCompanyService _SecCompanyService;
        private IFreeProductService _FreeProductService;
        private IHrmEmployeeService _hrmEmployeeService;
        private ISalesDiscountSettingService _salesDiscountSettingService;
        private IPartyCreditReportService _PartyCreditService;
        private IDeliveryService _DeliveryService;
        public RetailerSalesController()
        {
            var dbfactory = new DatabaseFactory();
            var unitOfWork = new UnitOfWork(dbfactory);
            _salesOrderService = new SalesOrderService(new SalesOrderRepository(dbfactory),
                new SalesOrderDetailRepository(dbfactory),
                new SalesOrderApprovalRepository(dbfactory),
                new NotificationRepository(dbfactory),
                new NotificationDetailRepository(dbfactory),
                unitOfWork);
            _officeService = new OfficeService(new OfficeRepository(dbfactory), unitOfWork);
            _SecCompanyService = new SecCompanyService(new SecCompanyRepository(dbfactory), unitOfWork);
            _FreeProductService = new FreeProductService(new FreeProductRepository(dbfactory), new ChartOfProductRepository(dbfactory),
                new UnitOfMeasurementRepository(dbfactory), unitOfWork);
            _hrmEmployeeService = new HrmEmployeeService(new HrmEmployeeRepository(dbfactory), unitOfWork);
            _salesDiscountSettingService = new SalesDiscountSettingService(new SalesDiscountSettingRepository(dbfactory), unitOfWork);
            _PartyCreditService = new PartyCreditReportService(new InvStoreOpeningRepository(dbfactory), unitOfWork);
            _DeliveryService = new DeliveryService(new DeliveryRepository(dbfactory), new DeliveryDetailRepository(dbfactory),
                new SalesOrderDetailRepository(dbfactory), new UnitOfWork(dbfactory));
        }

        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                var list = _salesOrderService.GetAll().OrderByDescending(i => i.Id).ToList();
                var deliverylist = _DeliveryService.GetAllVM().Where(i => !string.IsNullOrEmpty(i.ChallanNo) && !string.IsNullOrEmpty(i.InvoiceNo)).ToList();

                //1=Regular,2=Corporate,3=Retail
                //Load only retail sales order list
                list = list.Where(i => i.SalesType == 3).ToList();
                var result = list.Select(i => new
                {
                    Id = i.Id,
                    RefNo = i.RefNo,
                    PartyName = i.PartyName,
                    PreferredDeliveryDate = i.PreferredDeliveryDate,
                    Discount = i.Discount,
                    Total = i.Total,
                    DeliveryId = deliverylist.Where(j => j.SlsSalesOrderId == i.Id).FirstOrDefault() != null ? deliverylist.Where(j => j.SlsSalesOrderId == i.Id).FirstOrDefault().Id : 0,
                    ChallanNo = deliverylist.Where(j => j.SlsSalesOrderId == i.Id).FirstOrDefault() != null ? deliverylist.Where(j => j.SlsSalesOrderId == i.Id).FirstOrDefault().ChallanNo : "",
                    InvoiceNo = deliverylist.Where(j => j.SlsSalesOrderId == i.Id).FirstOrDefault() != null ? deliverylist.Where(j => j.SlsSalesOrderId == i.Id).FirstOrDefault().InvoiceNo : ""
                }).ToList();

                return Json(result, JsonRequestBehavior.AllowGet);                
            }
            catch (Exception ex)
            {

            }

            return Json(new List<SlsSalesOrderViewModel>(), JsonRequestBehavior.AllowGet); 
        }

        
    }
}
