using ERPOptima.Data.Common.Repository;
using ERPOptima.Data.Hrm.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Helper;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Sales;
using ERPOptima.Model.Security;
using ERPOptima.Model.ViewModel;
using ERPOptima.Service.Hrm;
using ERPOptima.Service.Sales;
using ERPOptima.Service.Security;
using Optima.Areas.Sales.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Optima.Areas.Sales.Controllers
{
    public class DeliveryController : Controller
    {
        private IDeliveryService _DeliveryService;
        private IDeliveryDetailsService _DeliveryDetailsService;
        private ISecCompanyService _SecCompanyService;
        private ISalesOrderService<SlsSalesOrderApproval, SlsSalesOrderViewModel> _salesOrderService;
        private IOfficeService _officeService;
        private IHrmEmployeeService _hrmEmployeeService;
        private IRptInvoiceService _objRptInvoiceService;
        private IChallenReportService _objChallenReportService;

        ////
        // GET: /Sales/Delivery/
        public DeliveryController()
        {
            var dbfactory = new DatabaseFactory();
            _DeliveryService = new DeliveryService(new DeliveryRepository(dbfactory), new DeliveryDetailRepository(dbfactory), new SalesOrderDetailRepository(dbfactory),
                new UnitOfWork(dbfactory));
            _DeliveryDetailsService = new DeliveryDetailsService(new DeliveryDetailsRepository(dbfactory), new ChartOfProductRepository(dbfactory),
                new UnitOfMeasurementRepository(dbfactory), new DeliveryRepository(dbfactory), new SalesOrderDetailRepository(dbfactory),
                new UnitOfWork(dbfactory));
            _SecCompanyService = new SecCompanyService(new SecCompanyRepository(dbfactory), new UnitOfWork(dbfactory));
            _salesOrderService = new SalesOrderService(new SalesOrderRepository(dbfactory),
               new SalesOrderDetailRepository(dbfactory),
               new SalesOrderApprovalRepository(dbfactory),
               new NotificationRepository(dbfactory),
               new NotificationDetailRepository(dbfactory),
               new UnitOfWork(dbfactory));
            _officeService = new OfficeService(new OfficeRepository(dbfactory), new UnitOfWork(dbfactory));
            _hrmEmployeeService = new HrmEmployeeService(new HrmEmployeeRepository(dbfactory), new UnitOfWork(dbfactory));
            _objRptInvoiceService = new RptInvoiceService(new DistributorRepository(dbfactory), new UnitOfWork(dbfactory));
            _objChallenReportService = new ChallenReportService(new DistributorRepository(dbfactory), new UnitOfWork(dbfactory));
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetAllChallan()
        {
            var list = _DeliveryService.GetAll().ToList();
            var challanNos = list.Select(i => new { Id = i.Id, ChallanNo = i.ChallanNo }).Distinct().ToList();
            return Json(challanNos, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetChallanList()
        {
            var list = _DeliveryService.GetChallanList().Select(c => new { Id = c.Id, ChallanNo = c.ChallanNo }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetInvoiceList()
        {
            var list = _DeliveryService.GetChallanList().Select(i => new { Id = i.Id, InvoiceNo = i.InvoiceNo }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]

        public string GetChallanNo(int companyId, int employeeId)
        {
            SecCompany objCmnCompany = _SecCompanyService.GetById(companyId);
            SlsOffice office = _officeService.GetById((int)_hrmEmployeeService.GetById(employeeId).SlsOfficeId);
            string challanNo = _DeliveryService.GetChallanNo(objCmnCompany.Prefix, office.Code);
            return challanNo;

        }
        public string GetInvoiceNo(int companyId, int employeeId)
        {
            SecCompany objCmnCompany = _SecCompanyService.GetById(companyId);
            SlsOffice office = _officeService.GetById((int)_hrmEmployeeService.GetById(employeeId).SlsOfficeId);
            string invoiceNo = _DeliveryService.GetInvoiceNo(objCmnCompany.Prefix, office.Code);
            return invoiceNo;

        }
        public ActionResult GetAll()
        {
            try
            {
                var list = _DeliveryService.GetAllVM().ToList();
                list = list.OrderByDescending(i => i.DeliveryDate).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }            
            catch (Exception ex)
            {

            }

            return Json(new List<SlsDeliveryViewModel>(), JsonRequestBehavior.AllowGet); 
        }
        [HttpGet]
        public ActionResult GetAllSORefNo()
        {            
            var delList = _DeliveryService.GetAll().Where(j => j.ReceivedStatus == 1).ToList();
            var deliveryProcessing = delList.Select(i => i.SlsSalesOrderId).ToList();
            //1=New,2=Approve,3=Pass,4=Reject: Only approved sales orders are ready to deliver.
            var list = _salesOrderService.GetAll().Where(i=>i.ApprovalStatus==2).ToList();
            list = list.Where(i => !deliveryProcessing.Contains(i.Id)).ToList();

            var refNos = list.Select(i => new { Id = i.Id, RefNo = i.RefNo + " (" + i.PartyName + ")" }).Distinct().ToList();
            return Json(refNos, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult DetailsByOrderID(int deliveryId)
        {
            int CompanyId = Convert.ToInt32(Session["companyId"]);
            var list = _DeliveryDetailsService.GetAll(CompanyId, deliveryId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetOrderDetailsByOrderId(int orderId)
        {

            var list = _salesOrderService.GetById(orderId);
            List<SlsDeliverDetailViewModel> orderDetail = null;

            if (list != null && list.SalesOrderDetails != null && list.SalesOrderDetails.Count() > 0)
            {

                orderDetail = list.SalesOrderDetails.Select(o => new SlsDeliverDetailViewModel
                {
                    Id = 0,
                    SlsDeliveryId = 0,
                    SlsProductId = o.SlsProductId,
                    Quantity = 0,

                    SlsUnitId = o.SlsUnitId,
                    Rate = o.Rate,
                    Price = o.Price,
                    Discount = o.Discount,
                    Total = o.Total,
                    SalesOrderQuantity = o.SalesOrderQuantity,
                    SlsProductName = o.SlsProductName,
                    SlsUnitName = o.SlsUnitName

                }).ToList();


            }


            return Json(orderDetail, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetAllByOrderID(int orderId)
        {
            SlsDelivery obj = _DeliveryService.GetAllByOrderID(orderId);
            return Json(obj, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetAllOrders()
        {
            Collection<GetOrderViewModel> records = null;

            DataTable dt = _DeliveryService.GetOrderViewModel();
            if (dt != null)
            {
                records = new Collection<GetOrderViewModel>();
                foreach (DataRow row in dt.Rows)
                {
                    records.Add((GetOrderViewModel)ERPOptima.Lib.Utilities.Helper.FillTo(row, typeof(GetOrderViewModel)));
                }
            }
            return Json(records, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDetailsByOrderID(int orderId)
        {
            Collection<GetDeliveryDetailsByOrderViewModel> records = null;

            DataTable dt = _DeliveryService.GetDetailsByOrderIDVM(orderId);
            if (dt != null)
            {
                records = new Collection<GetDeliveryDetailsByOrderViewModel>();
                foreach (DataRow row in dt.Rows)
                {
                    records.Add((GetDeliveryDetailsByOrderViewModel)ERPOptima.Lib.Utilities.Helper.FillTo(row, typeof(GetDeliveryDetailsByOrderViewModel)));
                }
            }
            return Json(records, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveSalesDelivery(SlsDeliveryViewModel objT)
        {
            int userId = Convert.ToInt32(Session["userId"]);
            Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid)
            {
                if (objT.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        objT.CreatedBy = userId;
                        objT.CreatedDate = DateTime.Now.Date;
                        objOperation = _DeliveryService.Save(objT);
                    }
                    else { objOperation.OperationId = -1; }

                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        objT.ModifiedBy = userId;
                        objT.ModifiedDate = DateTime.Now.Date;
                        objOperation = _DeliveryService.Update(objT);
                    }
                    else { objOperation.OperationId = -2; }
                }
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        public ActionResult Update(SlsDelivery objSalesReceive)
        {
            int userId = Convert.ToInt32(Session["userId"]);
            Operation objOperation = new Operation { Success = false };

            if ((bool)Session["Edit"])
            {

                objSalesReceive.ModifiedBy = userId;
                objSalesReceive.ModifiedDate = DateTime.Now;
                objOperation = _DeliveryService.Update(objSalesReceive);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }


        [HttpPost]
        public ActionResult SaveSalesDeliveryInternal(SlsSalesOrderViewModel objS)
        {
            int soId = objS.Id;
            decimal discount = objS.Discount;
            decimal total = objS.Total;

            SlsDeliveryViewModel objT = new SlsDeliveryViewModel();

            int userId = Convert.ToInt32(Session["userId"]);
            int companyId = Convert.ToInt32(Session["companyId"]);
            int employeeId = Convert.ToInt32(Session["employeeId"]);
            SecCompany objCmnCompany = _SecCompanyService.GetById(companyId);
            SlsOffice office = _officeService.GetById((int)_hrmEmployeeService.GetById(employeeId).SlsOfficeId);
            string challanNo = _DeliveryService.GetChallanNo(objCmnCompany.Prefix, office.Code);
            string invoiceNo = _DeliveryService.GetInvoiceNo(objCmnCompany.Prefix, office.Code);

            Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid)
            {
                if (objT.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        objT.SlsSalesOrderId = soId;
                        objT.DeliveryDate = DateTime.Now;                        
                        objT.ChallanNo = challanNo;
                        objT.InvoiceNo = invoiceNo;
                        objT.VehicleNo = "";
                        objT.Remarks = "";
                        objT.Discount = discount;
                        objT.Total = total;
                        objT.ReceivedStatus = 1;
                        objT.ReceivedDate = DateTime.Now;
                        objT.ReceivedRemarks = "";
                        objT.CreatedBy = userId;
                        objT.CreatedDate = DateTime.Now.Date;

                        //map details of sales items to delivery items.
                        IList<SlsDeliverDetailViewModel> details = new List<SlsDeliverDetailViewModel>();
                       
                        foreach(var det in objS.SalesOrderDetails)
                        {
                            SlsDeliverDetailViewModel dObj = new SlsDeliverDetailViewModel();

                            dObj.SlsProductId = det.SlsProductId;
                            dObj.Quantity = det.SalesOrderQuantity;
                            dObj.SlsUnitId = det.SlsUnitId;
                            dObj.Rate = det.Rate;
                            dObj.Price = det.Price;
                            dObj.Discount = det.Discount;
                            dObj.Total = det.Total;
                            dObj.SalesOrderQuantity = det.SalesOrderQuantity;
                            dObj.SlsProductName = det.SlsProductName;
                            dObj.SlsUnitName = det.SlsUnitName;

                            details.Add(dObj);
                        }

                        objT.SlsDeliverDetails = details;

                        objOperation = _DeliveryService.Save(objT);
                    }
                    else { objOperation.OperationId = -1; }

                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        //Normally not required till now - as this will perform once upon a time
                        objT.ModifiedBy = userId;
                        objT.ModifiedDate = DateTime.Now.Date;
                        objOperation = _DeliveryService.Update(objT);
                    }
                    else { objOperation.OperationId = -2; }
                }
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        [HttpGet]
        public ActionResult GetInvoiceReport(string invoiceNo)
        {
            if (ModelState.IsValid)
            {
                DataTable dt = null;
                string filepath = string.Empty;


                int CompanyId = Convert.ToInt32(Session["companyId"]);

                SecCompany objCmnCompany = _SecCompanyService.GetById(CompanyId);

                string companyName = objCmnCompany.Name;
                string companyAddress = objCmnCompany.Address;
                string reportName = "Invoice Report";

                List<ReportParameter> paramList = new List<ReportParameter>();


                #region ParameterListPreperation

                ReportParameter objcmpName = new ReportParameter();
                objcmpName.Name = "CompanyName";
                objcmpName.Value = companyName;
                paramList.Add(objcmpName);

                ReportParameter objcmpAddress = new ReportParameter();
                objcmpAddress.Name = "Address";
                objcmpAddress.Value = companyAddress;
                paramList.Add(objcmpAddress);

                ReportParameter objReportName = new ReportParameter();
                objReportName.Name = "ReportName";
                objReportName.Value = reportName;
                paramList.Add(objReportName);


                #endregion ParameterListPreperation

                if (invoiceNo.Length > 0)
                {
                    dt = _objRptInvoiceService.GetInvoiceList(invoiceNo);

                    filepath = GetReportPath("RptInvoice.rpt");

                    if (dt != null && filepath != string.Empty)
                    {
                        return new CrystalReportResult(filepath, dt, paramList);
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }

        [HttpGet]
        public ActionResult GetChallenList(int DeliveryId)
        {
            if (ModelState.IsValid)
            {
                DataTable dt = null;
                string filepath = string.Empty;


                int CompanyId = Convert.ToInt32(Session["companyId"]);

                SecCompany objCmnCompany = _SecCompanyService.GetById(CompanyId);

                string companyName = objCmnCompany.Name;
                string companyAddress = objCmnCompany.Address;
                string reportName = "Challen Report";

                List<ReportParameter> paramList = new List<ReportParameter>();


                #region ParameterListPreperation

                ReportParameter objcmpName = new ReportParameter();
                objcmpName.Name = "CompanyName";
                objcmpName.Value = companyName;
                paramList.Add(objcmpName);

                ReportParameter objcmpAddress = new ReportParameter();
                objcmpAddress.Name = "Address";
                objcmpAddress.Value = companyAddress;
                paramList.Add(objcmpAddress);

                ReportParameter objReportName = new ReportParameter();
                objReportName.Name = "ReportName";
                objReportName.Value = reportName;
                paramList.Add(objReportName);


                #endregion ParameterListPreperation


                dt = _objChallenReportService.GetChallenList(DeliveryId);

                filepath = GetReportPath("RptChallen.rpt");
                dt = null;
                if (dt != null && filepath != string.Empty)
                {
                    return new CrystalReportResult(filepath, dt, paramList);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }

        private string GetReportPath(string fileName)
        {
            string basePath = HostingEnvironment.MapPath("~/Areas/Sales/Reports");

            return Path.Combine(basePath, fileName);
        }

    }
}