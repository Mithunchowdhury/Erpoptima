using ERPOptima.Data.Common.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Helper;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Security;
using ERPOptima.Model.Sales;
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
using ERPOptima.Data.Sales;
using ERPOptima.Data.Inventory.Repository;

namespace Optima.Areas.Sales.Controllers
{
    public class SalesReportController : Controller
    {
        private ICollectionEntryService _objCollectionEntryMoneyReceiptReportService; 

        private IPartyListReportService _objPartyListReportService;
        private ISecCompanyService _SecCompanyService;
        private IChartOfProductService _ChartOfProductService;
        private IStockBalanceReportService _objStockBalanceReportService;
        private IProductPriceReportService _objProductPriceReportService;
        private IFreeProductReportService _objFreeProductReportService;
        private IDailySalesReportService _objDailySalesReportService;
        private IChallenReportService _objChallenReportService;
        private ITargetNAchievementReportService _objTargetNAchievementReportService;
        private IRptInvoiceService _objRptInvoiceService;
        private ISalesOrderListReportService _objSalesOrderListReportService;
        private IProductReportService _objProductService;
        private ISalesCommissionReportService _objSalesCommissionReportService;
        private IPartyCreditReportService _objPartyCreditReportService;
        private ISalesReturnListReportService _objSalesReturnListReportService;
        private ICollectionReportService _objCollectionReportService;
        private IPartyLedgerReportService _objPartyLedgerReportService;
        private IRptSalesTargetService _objSalesTargetReportService;
        public SalesReportController()
        {
            var dbfactory = new DatabaseFactory();
            _objCollectionEntryMoneyReceiptReportService = new CollectionEntryService(new CollectionEntryRepository(dbfactory), new UnitOfWork(dbfactory));
            _objPartyListReportService = new PartyListReportService(new DistributorRepository(dbfactory), new UnitOfWork(dbfactory));
            _SecCompanyService = new SecCompanyService(new SecCompanyRepository(dbfactory), new UnitOfWork(dbfactory));
            _ChartOfProductService = new ChartOfProductService(new ChartOfProductRepository(dbfactory), new UnitOfWork(dbfactory));
            _objProductPriceReportService = new ProductPriceReportService(new ProductPriceRepository(dbfactory), new UnitOfWork(dbfactory));
            _objFreeProductReportService = new FreeProductReportService(new FreeProductRepository(dbfactory), new UnitOfWork(dbfactory));
            _objDailySalesReportService = new DailySalesReportService(new SalesOrderRepository(dbfactory), new UnitOfWork(dbfactory));
            _objChallenReportService = new ChallenReportService(new DistributorRepository(dbfactory), new UnitOfWork(dbfactory));
            _objTargetNAchievementReportService = new TargetNAchievementReportService(new SalesOrderRepository(dbfactory), new UnitOfWork(dbfactory));
            _objRptInvoiceService = new RptInvoiceService(new DistributorRepository(dbfactory), new UnitOfWork(dbfactory));
            _objSalesOrderListReportService = new SalesOrderListReportService(new SalesOrderRepository(dbfactory), new UnitOfWork(dbfactory));
            _objStockBalanceReportService = new StockBalanceReportService(new InvStoreOpeningRepository(dbfactory), new UnitOfWork(dbfactory));
            _objSalesCommissionReportService = new SalesCommissionReportService(new InvStoreOpeningRepository(dbfactory), new UnitOfWork(dbfactory));
            _objPartyCreditReportService = new PartyCreditReportService(new InvStoreOpeningRepository(dbfactory), new UnitOfWork(dbfactory));
            _objSalesReturnListReportService = new SalesReturnListReportService(new SalesReturnRepository(dbfactory), new UnitOfWork(dbfactory));
            _objProductService = new ProductReportService(new ProductRepository(dbfactory), new UnitOfWork(dbfactory));
            _objCollectionReportService = new CollectionReportService(new CollectionReportRepository(dbfactory), new UnitOfWork(dbfactory));
            _objPartyLedgerReportService = new PartyLedgerReportService(new InvStoreOpeningRepository(dbfactory), new UnitOfWork(dbfactory));
            _objSalesTargetReportService = new RptSalesTargetService(new RptSalesTargetRepository(dbfactory), new UnitOfWork(dbfactory));
        }
        private string GetReportPath(string fileName)
        {
            string basePath = HostingEnvironment.MapPath("~/Areas/Sales/Reports");

            return Path.Combine(basePath, fileName);
        }


        //
        // GET: /Sales/SalesReport/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductIndex()
        {
            return View();
        }

        public ActionResult ProductPrice()
        {
            return View();
        }

        public ActionResult FreeProductIndex()
        {
            return View();
        }

        public ActionResult DailySales()
        {
            return View();
        }
        public ActionResult ChallanStatement()
        {
            return View();
        }
        public ActionResult TargetNAchievement()
        {
            return View();
        }
        public ActionResult InvoiceReport()
        {
            return View();
        }


        public ActionResult MoneyReceiptReport()
        {
            return View();
        }
        public ActionResult SalesOrderListReport()
        {
            return View();
        }

        public ActionResult StockBalanceReport()
        {
            return View();
        }

        public ActionResult SalesCommissionReport()
        {
            return View();
        }
        public ActionResult PartyCreditReport()
        {
            return View();
        }
        public ActionResult SalesReturnReport()
        {
            return View();
        }
        public ActionResult CollectionReport()
        {
            return View();
        }

        public ActionResult PartyLedgerReport()
        {
            return View();
        }
        public ActionResult SalesTargetReport()
        {
            return View();
        }
        public ActionResult SelfSalesTargetReport()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetSelfSalesTargetReports()
        {

            if (ModelState.IsValid)
            {
                int EmployeeId = Convert.ToInt32(Session["employeeId"]);
                return GetDataTableForSalesCollectionTarget(EmployeeId, "SelfSalesTargetReport", "Self Sales & Collection Target Report");

            }
            else
            {
                return RedirectToAction("SelfSalesTargetReport");
            }

        }

        
        public string DisplayMonthName(int month) 
        {
            string name = "";
            switch(month)
            {  
                case 1:
                name = "January";
                    break;
                case 2:
                    name = "February";
                    break;
                case 3:
                    name = "March";
                    break;
                case 4:
                    name = "April";
                    break;
                case 5:
                    name = "May";
                    break;

                case 6:
                    name = "June";
                    break;

                case 7:
                    name = "July";
                    break;

                case 8:
                    name = "August";
                    break;

                case 9:
                    name = "September";
                    break;

                case 10:
                    name = "October";
                    break;

                case 11:
                    name = "November";
                    break;

                case 12:
                    name = "December";
                    break;
                default:
                    name = "";
                    break;

            }
            return name;

        }
        public ActionResult GetSalesTargetReports(int? EmployeeId)
        {

            if (ModelState.IsValid)
            {
                return GetDataTableForSalesCollectionTarget(EmployeeId, "SalesTargetReport", "Sales & Collection Target Report");
                
            }
            else
            {
                return RedirectToAction("SalesTargetReport");
            }

        }

        public ActionResult GetDataTableForSalesCollectionTarget(int? EmployeeId, string defaultView, string reportTitle)
        {
            DataTable dt = null;
            dt = _objSalesTargetReportService.GetSalesTargetReport(EmployeeId);

            string filepath = string.Empty;
            filepath = GetReportPath("RptSalesTargetCollectiont1.rpt");

            List<ReportParameter> paramList = new List<ReportParameter>();
            int companyId = Convert.ToInt32(Session["companyId"]);
            SecCompany objCmnCompany = _SecCompanyService.GetById(companyId);
            string companyName = objCmnCompany.Name;
            string companyAddress = objCmnCompany.Address;
            string reportName = reportTitle;
            #region ParameterListPreperation

            ReportParameter objcmpName = new ReportParameter();
            objcmpName.Name = "Company";
            objcmpName.Value = companyName;
            paramList.Add(objcmpName);

            ReportParameter objcmpAddress = new ReportParameter();
            objcmpAddress.Name = "CompanyAddress";
            objcmpAddress.Value = companyAddress;
            paramList.Add(objcmpAddress);

            ReportParameter objReportName = new ReportParameter();
            objReportName.Name = "ReportName";
            objReportName.Value = reportName;
            paramList.Add(objReportName);

            ReportParameter objMonthName = new ReportParameter();
            objMonthName.Name = "MonthName";
            objMonthName.Value = DisplayMonthName(DateTime.Now.Month);
            paramList.Add(objMonthName);

            //ReportParameter objYear = new ReportParameter();
            //objYear.Name = "Year";
            //objYear.Value = Year.ToString();
            //paramList.Add(objYear);

            //ReportParameter objMonth = new ReportParameter();
            //objMonth.Name = "Month";
            //objMonth.Value = Month.ToString();
            //paramList.Add(objMonth);


            #endregion ParameterListPreperation


            if (dt != null && filepath != string.Empty)
            {
                return new CrystalReportResult(filepath, dt, paramList);
            }
            else
            {
                return RedirectToAction(defaultView);
            }
        }

        public ActionResult GetPartyLedgerReport(DateTime dateFrom, DateTime dateTo, int type, int partyId)
        {
            if (ModelState.IsValid)
            {
                DataTable dt = null;
                string filepath = string.Empty;


                int companyId = Convert.ToInt32(Session["companyId"]);

                SecCompany objCmnCompany = _SecCompanyService.GetById(companyId);

                string companyName = objCmnCompany.Name;
                string companyAddress = objCmnCompany.Address;
                string reportName = "Party Ledger Report";

                List<ReportParameter> paramList = new List<ReportParameter>();


                #region ParameterListPreperation

                ReportParameter objcmpName = new ReportParameter();
                objcmpName.Name = "Company";
                objcmpName.Value = companyName;
                paramList.Add(objcmpName);

                ReportParameter objcmpAddress = new ReportParameter();
                objcmpAddress.Name = "CompanyAddress";
                objcmpAddress.Value = companyAddress;
                paramList.Add(objcmpAddress);

                ReportParameter objReportName = new ReportParameter();
                objReportName.Name = "ReportName";
                objReportName.Value = reportName;
                paramList.Add(objReportName);

                ReportParameter objDateFrom = new ReportParameter();
                objDateFrom.Name = "DateFrom";
                objDateFrom.Value = dateFrom.ToString("dd/MM/yyyy");
                paramList.Add(objDateFrom);

                ReportParameter objDateTo = new ReportParameter();
                objDateTo.Name = "DateTo";
                objDateTo.Value = dateTo.ToString("dd/MM/yyyy");
                paramList.Add(objDateTo);


                #endregion ParameterListPreperation


                dt = _objPartyLedgerReportService.GetPartyLedgerReport(dateFrom, dateTo, type, partyId, companyId);

                filepath = GetReportPath("PartyLedger.rpt");

                if (dt != null && filepath != string.Empty)
                {
                    return new CrystalReportResult(filepath, dt, paramList);
                }
                else
                {
                    return RedirectToAction("PartyLedgerReport");
                }
            }
            else
            {
                return RedirectToAction("PartyLedgerReport");
            }

        }
        public ActionResult GetCollectionReports(int? nmPartytype, int? nmParty, int? OfficeId, DateTime StartDate, DateTime EndDate)
        {

            if (ModelState.IsValid)
            {
                DataTable dt = null;
                string filepath = string.Empty;

                if (Session["companyId"] != null)
                {

                    int companyId = Convert.ToInt32(Session["companyId"]);

                    SecCompany objCmnCompany = _SecCompanyService.GetById(companyId);

                    string companyName = objCmnCompany.Name;
                    string companyAddress = objCmnCompany.Address;
                    string reportName = "Collection Report";

                    List<ReportParameter> paramList = new List<ReportParameter>();


                    #region ParameterListPreperation

                    ReportParameter objcmpName = new ReportParameter();
                    objcmpName.Name = "Company";
                    objcmpName.Value = companyName;
                    paramList.Add(objcmpName);

                    ReportParameter objcmpAddress = new ReportParameter();
                    objcmpAddress.Name = "CompanyAddress";
                    objcmpAddress.Value = companyAddress;
                    paramList.Add(objcmpAddress);

                    ReportParameter objReportName = new ReportParameter();
                    objReportName.Name = "ReportName";
                    objReportName.Value = reportName;
                    paramList.Add(objReportName);

                    ReportParameter objDateFrom = new ReportParameter();
                    objDateFrom.Name = "DateFrom";
                    objDateFrom.Value = StartDate.ToString("dd-MMM-yyyy");
                    paramList.Add(objDateFrom);

                    ReportParameter objDateTo = new ReportParameter();
                    objDateTo.Name = "DateTo";
                    objDateTo.Value = EndDate.ToString("dd-MMM-yyyy");
                    paramList.Add(objDateTo);


                    #endregion ParameterListPreperation

                    dt = _objCollectionReportService.GetCollectionReport(nmPartytype, nmParty, OfficeId, StartDate, EndDate);

                    filepath = GetReportPath("CollectionReport1.rpt");

                    if (dt != null && filepath != string.Empty)
                    {
                        return new CrystalReportResult(filepath, dt, paramList);
                    }
                    else
                    {
                        return RedirectToAction("GetCollectionReports");
                    }
                }
                else
                {
                    return RedirectToAction("GetCollectionReports");
                }
                //
            }
            else
            {
                return RedirectToAction("GetCollectionReports");
            }

        }
        public ActionResult GetSalesReturnReport(int id)
        {

            if (ModelState.IsValid)
            {
                DataTable dt = null;
                string filepath = string.Empty;


                int companyId = Convert.ToInt32(Session["companyId"]);

                SecCompany objCmnCompany = _SecCompanyService.GetById(companyId);

                string companyName = objCmnCompany.Name;
                string companyAddress = objCmnCompany.Address;
                string reportName = "Sales Return Report";

                List<ReportParameter> paramList = new List<ReportParameter>();


                #region ParameterListPreperation

                ReportParameter objcmpName = new ReportParameter();
                objcmpName.Name = "Company";
                objcmpName.Value = companyName;
                paramList.Add(objcmpName);

                ReportParameter objcmpAddress = new ReportParameter();
                objcmpAddress.Name = "CompanyAddress";
                objcmpAddress.Value = companyAddress;
                paramList.Add(objcmpAddress);

                ReportParameter objReportName = new ReportParameter();
                objReportName.Name = "ReportName";
                objReportName.Value = reportName;
                paramList.Add(objReportName);


                #endregion ParameterListPreperation


                dt = _objSalesReturnListReportService.GetSalesReturnListReport(id);

                filepath = GetReportPath("RptSalesReturn.rpt");

                if (dt != null && filepath != string.Empty)
                {
                    return new CrystalReportResult(filepath, dt, paramList);
                }
                else
                {
                    return RedirectToAction("GetSalesReturnListReport");
                }
            }
            else
            {
                return RedirectToAction("GetSalesReturnListReport");
            }

        }
        public ActionResult GetPartyCreditReport(int? type, int? partyId)
        {
            if (ModelState.IsValid)
            {
                DataTable dt = null;
                string filepath = string.Empty;


                int companyId = Convert.ToInt32(Session["companyId"]);

                SecCompany objCmnCompany = _SecCompanyService.GetById(companyId);

                string companyName = objCmnCompany.Name;
                string companyAddress = objCmnCompany.Address;
                string reportName = "Party Credit Report";

                List<ReportParameter> paramList = new List<ReportParameter>();


                #region ParameterListPreperation

                ReportParameter objcmpName = new ReportParameter();
                objcmpName.Name = "Company";
                objcmpName.Value = companyName;
                paramList.Add(objcmpName);

                ReportParameter objcmpAddress = new ReportParameter();
                objcmpAddress.Name = "CompanyAddress";
                objcmpAddress.Value = companyAddress;
                paramList.Add(objcmpAddress);

                ReportParameter objReportName = new ReportParameter();
                objReportName.Name = "ReportName";
                objReportName.Value = reportName;
                paramList.Add(objReportName);


                #endregion ParameterListPreperation


                dt = _objPartyCreditReportService.GetPartyCreditReport(type, partyId, companyId);

                filepath = GetReportPath("PartyCredit.rpt");

                if (dt != null && filepath != string.Empty)
                {
                    return new CrystalReportResult(filepath, dt, paramList);
                }
                else
                {
                    return RedirectToAction("GetPartyCreditReport");
                }
            }
            else
            {
                return RedirectToAction("GetPartyCreditReport");
            }

        }
        public ActionResult GetSalesCommissionReport(DateTime? DateFrom, DateTime? DateTo)
        {
            if (ModelState.IsValid)
            {
                DataTable dt = null;
                string filepath = string.Empty;


                int companyId = Convert.ToInt32(Session["companyId"]);

                SecCompany objCmnCompany = _SecCompanyService.GetById(companyId);

                string companyName = objCmnCompany.Name;
                string companyAddress = objCmnCompany.Address;
                string reportName = "Sales Commission Report";

                List<ReportParameter> paramList = new List<ReportParameter>();


                #region ParameterListPreperation

                ReportParameter objcmpName = new ReportParameter();
                objcmpName.Name = "Company";
                objcmpName.Value = companyName;
                paramList.Add(objcmpName);

                ReportParameter objcmpAddress = new ReportParameter();
                objcmpAddress.Name = "CompanyAddress";
                objcmpAddress.Value = companyAddress;
                paramList.Add(objcmpAddress);

                ReportParameter objReportName = new ReportParameter();
                objReportName.Name = "ReportName";
                objReportName.Value = reportName;
                paramList.Add(objReportName);


                #endregion ParameterListPreperation


                dt = _objSalesCommissionReportService.GetSalesCommissionReport(DateFrom, DateTo, companyId);

                filepath = GetReportPath("SalesCommission.rpt");

                if (dt != null && filepath != string.Empty)
                {
                    return new CrystalReportResult(filepath, dt, paramList);
                }
                else
                {
                    return RedirectToAction("SalesCommissionReport");
                }
            }
            else
            {
                return RedirectToAction("SalesCommissionReport");
            }

        }
        public ActionResult GetStockBalanceReport(int? StoreId)
        {
            if (ModelState.IsValid)
            {
                DataTable dt = null;
                string filepath = string.Empty;


                int companyId = Convert.ToInt32(Session["companyId"]);

                SecCompany objCmnCompany = _SecCompanyService.GetById(companyId);

                string companyName = objCmnCompany.Name;
                string companyAddress = objCmnCompany.Address;
                string reportName = "Store Stock Balance Report";

                List<ReportParameter> paramList = new List<ReportParameter>();


                #region ParameterListPreperation

                ReportParameter objcmpName = new ReportParameter();
                objcmpName.Name = "Company";
                objcmpName.Value = companyName;
                paramList.Add(objcmpName);

                ReportParameter objcmpAddress = new ReportParameter();
                objcmpAddress.Name = "CompanyAddress";
                objcmpAddress.Value = companyAddress;
                paramList.Add(objcmpAddress);

                ReportParameter objReportName = new ReportParameter();
                objReportName.Name = "ReportName";
                objReportName.Value = reportName;
                paramList.Add(objReportName);


                #endregion ParameterListPreperation


                dt = _objStockBalanceReportService.GetStockBalanceReport(companyId, StoreId);

                filepath = GetReportPath("StockBalance.rpt");

                if (dt != null && filepath != string.Empty)
                {
                    return new CrystalReportResult(filepath, dt, paramList);
                }
                else
                {
                    return RedirectToAction("StockBalanceReport");
                }
            }
            else
            {
                return RedirectToAction("StockBalanceReport");
            }

        }
        public ActionResult GetSalesOrderListReport(int Status, DateTime StartDate, DateTime EndDate)
        {

            if (ModelState.IsValid)
            {
                DataTable dt = null;
                string filepath = string.Empty;


                int companyId = Convert.ToInt32(Session["companyId"]);

                SecCompany objCmnCompany = _SecCompanyService.GetById(companyId);
                string CompanyName = objCmnCompany.Name;
                string Address = objCmnCompany.Address;
                string ReportName = "Sales Order List Report";

                List<ReportParameter> paramList = new List<ReportParameter>();


                #region ParameterListPreperation

                ReportParameter objcmpName = new ReportParameter();
                objcmpName.Name = "Company";
                objcmpName.Value = CompanyName;
                paramList.Add(objcmpName);

                ReportParameter objcmpAddress = new ReportParameter();
                objcmpAddress.Name = "Address";
                objcmpAddress.Value = Address;
                paramList.Add(objcmpAddress);

                ReportParameter objReportName = new ReportParameter();
                objReportName.Name = "ReportName";
                objReportName.Value = ReportName;
                paramList.Add(objReportName);

                



                #endregion ParameterListPreperation


                dt = _objSalesOrderListReportService.GetSalesOrderReport(companyId, Status, StartDate, EndDate);

                filepath = GetReportPath("RptSalesOrderList.rpt");

                if (dt != null && filepath != string.Empty)
                {

                    return new CrystalReportResult(filepath, dt, paramList);
                }
                else
                {
                    return RedirectToAction("SalesOrderListReport");
                }
            }
            else
            {
                return RedirectToAction("SalesOrderListReport");
            }

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
                        return RedirectToAction("InvoiceReport");
                    }
                }
                else
                {
                    return RedirectToAction("InvoiceReport");
                }
            }
            else
            {
                return RedirectToAction("InvoiceReport");
            }

        }

        [HttpGet]
        public ActionResult GetInvoiceList(string InvoiceId)
        {
            string invoiceNo = "";

            try
            {
                invoiceNo = InvoiceId.Split(' ')[0];
            }
            catch (Exception ex)
            {

            }

            return GetInvoiceReport(invoiceNo);
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

                if (dt != null && filepath != string.Empty)
                {
                    return new CrystalReportResult(filepath, dt, paramList);
                }
                else
                {
                    return RedirectToAction("ChallanStatement");
                }
            }
            else
            {
                return RedirectToAction("ChallanStatement");
            }

        }
        [HttpGet]
        public ActionResult GetFreeProductList(int productId)
        {
            if (ModelState.IsValid)
            {
                DataTable dt = null;
                string filepath = string.Empty;


                int CompanyId = Convert.ToInt32(Session["companyId"]);

                SecCompany objCmnCompany = _SecCompanyService.GetById(CompanyId);

                string companyName = objCmnCompany.Name;
                string companyAddress = objCmnCompany.Address;
                string reportName = "Free Product List Report";

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


                dt = _objFreeProductReportService.GetFreeProductsReport(CompanyId, productId);

                filepath = GetReportPath("RptFreeProductList.rpt");

                if (dt != null && filepath != string.Empty)
                {
                    return new CrystalReportResult(filepath, dt, paramList);
                }
                else
                {
                    return RedirectToAction("FreeProductIndex");
                }
            }
            else
            {
                return RedirectToAction("FreeProductIndex");
            }

        }


        [HttpGet]
        public ActionResult GetPartyListReport(int? type)
        {
            if (ModelState.IsValid)
            {
                DataTable dt = null;
                string filepath = string.Empty;


                int companyId = Convert.ToInt32(Session["companyId"]);

                SecCompany objCmnCompany = _SecCompanyService.GetById(companyId);

                string companyName = objCmnCompany.Name;
                string companyAddress = objCmnCompany.Address;
                string reportName = "Party List Report";

                List<ReportParameter> paramList = new List<ReportParameter>();


                #region ParameterListPreperation

                ReportParameter objcmpName = new ReportParameter();
                objcmpName.Name = "Company";
                objcmpName.Value = companyName;
                paramList.Add(objcmpName);

                ReportParameter objcmpAddress = new ReportParameter();
                objcmpAddress.Name = "CompanyAddress";
                objcmpAddress.Value = companyAddress;
                paramList.Add(objcmpAddress);

                ReportParameter objReportName = new ReportParameter();
                objReportName.Name = "ReportName";
                objReportName.Value = reportName;
                paramList.Add(objReportName);


                #endregion ParameterListPreperation


                dt = _objPartyListReportService.GetPartyListReport(companyId, type);

                filepath = GetReportPath("PartyList.rpt");

                if (dt != null && filepath != string.Empty)
                {
                    return new CrystalReportResult(filepath, dt, paramList);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
           
        }

        [HttpGet]
        public ActionResult GetProductListReport()
        {
            if (ModelState.IsValid)
            {
                DataTable dt = null;
                string filepath = string.Empty;


                int CompanyId = Convert.ToInt32(Session["companyId"]);

                SecCompany objCmnCompany = _SecCompanyService.GetById(CompanyId);

                string companyName = objCmnCompany.Name;
                string companyAddress = objCmnCompany.Address;
                string reportName = "Product List Report";

                List<ReportParameter> paramList = new List<ReportParameter>();


                #region ParameterListPreperation

                ReportParameter objcmpName = new ReportParameter();
                objcmpName.Name = "Company";
                objcmpName.Value = companyName;
                paramList.Add(objcmpName);

                ReportParameter objcmpAddress = new ReportParameter();
                objcmpAddress.Name = "CompanyAddress";
                objcmpAddress.Value = companyAddress;
                paramList.Add(objcmpAddress);

                ReportParameter objReportName = new ReportParameter();
                objReportName.Name = "ReportName";
                objReportName.Value = reportName;
                paramList.Add(objReportName);


                #endregion ParameterListPreperation


                dt = _objProductService.GetProductListReport(CompanyId);

                filepath = GetReportPath("ProductList.rpt");

                if (dt != null && filepath != string.Empty)
                {
                    return new CrystalReportResult(filepath, dt, paramList);
                }
                else
                {
                    return RedirectToAction("ProductIndex");
                }
            }
            else
            {
                return RedirectToAction("ProductIndex");
            }

        }

        [HttpGet]
        public ActionResult MoneyReceiptprint(int CollectionEntryID = 0)
        {

            if (ModelState.IsValid)
            {
                DataTable dt = null;
                string filepath = string.Empty;


                int companyId = Convert.ToInt32(Session["companyId"]);

                SecCompany objCmnCompany = _SecCompanyService.GetById(companyId);

                string companyName = objCmnCompany.Name;
                string companyAddress = objCmnCompany.Address;
                string reportName = "Money Receipt";

                List<ReportParameter> paramList = new List<ReportParameter>();


                #region ParameterListPreperation

                ReportParameter objcmpName = new ReportParameter();
                objcmpName.Name = "Company";
                objcmpName.Value = companyName;
                paramList.Add(objcmpName);

                ReportParameter objcmpAddress = new ReportParameter();
                objcmpAddress.Name = "CompanyAddress";
                objcmpAddress.Value = companyAddress;
                paramList.Add(objcmpAddress);

                ReportParameter objReportName = new ReportParameter();
                objReportName.Name = "ReportName";
                objReportName.Value = reportName;
                paramList.Add(objReportName);


                #endregion ParameterListPreperation


                dt = _objCollectionEntryMoneyReceiptReportService.GetCollectionMoneyReceiptReport(companyId, CollectionEntryID);

                filepath = GetReportPath("MoneyReceipt.rpt");

                if (dt != null && filepath != string.Empty)
                {
                    return new CrystalReportResult(filepath, dt, paramList);
                }
                else
                {
                    return RedirectToAction("MoneyReceiptReport");
                }
            }
            else
            {
                return RedirectToAction("MoneyReceiptReport");
            }

        }

        
        [HttpGet]
        public ActionResult GetProductPriceReport(int ProductId=0)
        {
      
            if (ModelState.IsValid)
            {
                DataTable dt = null;
                string filepath = string.Empty;

                if (Session["companyId"] != null)
                {
                    int companyId = Convert.ToInt32(Session["companyId"]);

                    SecCompany objCmnCompany = _SecCompanyService.GetById(companyId);

                    string companyName = objCmnCompany.Name;
                    string companyAddress = objCmnCompany.Address;
                    string reportName = "Price List Report";

                    List<ReportParameter> paramList = new List<ReportParameter>();


                    #region ParameterListPreperation

                    ReportParameter objcmpName = new ReportParameter();
                    objcmpName.Name = "Company";
                    objcmpName.Value = companyName;
                    paramList.Add(objcmpName);

                    ReportParameter objcmpAddress = new ReportParameter();
                    objcmpAddress.Name = "CompanyAddress";
                    objcmpAddress.Value = companyAddress;
                    paramList.Add(objcmpAddress);

                    ReportParameter objReportName = new ReportParameter();
                    objReportName.Name = "ReportName";
                    objReportName.Value = reportName;
                    paramList.Add(objReportName);


                    #endregion ParameterListPreperation


                    dt = _objProductPriceReportService.GetPriceListReport(companyId, ProductId);

                    filepath = GetReportPath("PriceList.rpt");

                    if (dt != null && filepath != string.Empty)
                    {
                        return new CrystalReportResult(filepath, dt, paramList);
                    }
                    else
                    {
                        return RedirectToAction("ProductPrice");
                    }
                }
                else
                {
                    return RedirectToAction("ProductPrice");
                }
            }
            else
            {
                return RedirectToAction("ProductPrice");
            }

        }

        public ActionResult GetDailySalesReport(DateTime StartDate, DateTime EndDate, int OfficeId = 0, int CategoryId = 0, int SubCategoryId = 0)
        {

            if (ModelState.IsValid)
            {
                DataTable dt = null;
                string filepath = string.Empty;


                int companyId = Convert.ToInt32(Session["companyId"]);
                int categoryId = CategoryId;

                SecCompany objCmnCompany = _SecCompanyService.GetById(companyId);
                SlsProduct ChartOfProduct = _ChartOfProductService.GetById(categoryId);
                string productName = ChartOfProduct.Name;
                string companyName = objCmnCompany.Name;
                string companyAddress = objCmnCompany.Address;
                string reportName = "Daily Sales Report";


                List<ReportParameter> paramList = new List<ReportParameter>();


                #region ParameterListPreperation

                ReportParameter objcmpName = new ReportParameter();
                objcmpName.Name = "Company";
                objcmpName.Value = companyName;
                paramList.Add(objcmpName);

                ReportParameter objcmpAddress = new ReportParameter();
                objcmpAddress.Name = "CompanyAddress";
                objcmpAddress.Value = companyAddress;
                paramList.Add(objcmpAddress);

                ReportParameter objReportName = new ReportParameter();
                objReportName.Name = "ReportName";
                objReportName.Value = reportName;
                paramList.Add(objReportName);

                ReportParameter objStartDate = new ReportParameter();
                objStartDate.Name = "StartDate";
                String StrtDate = StartDate.ToString("dd-MM-yyyy");
                objStartDate.Value = "From :"+StrtDate;
                paramList.Add(objStartDate);

                ReportParameter objEndDate = new ReportParameter();
                objEndDate.Name = "EndDate";
                String endDate = EndDate.ToString("dd-MM-yyyy");
                objEndDate.Value = "To :" + endDate;
                paramList.Add(objEndDate);

                ReportParameter objCategory = new ReportParameter();
                objCategory.Name = "Category";
                objCategory.Value = "Group/Category Code : "+productName;
                paramList.Add(objCategory);

                #endregion ParameterListPreperation


                dt = _objDailySalesReportService.GetDailySalesReport(companyId, StartDate, EndDate, OfficeId, CategoryId, SubCategoryId);

                filepath = GetReportPath("DailySales.rpt");

                if (dt != null && filepath != string.Empty)
                {

                    return new CrystalReportResult(filepath, dt, paramList);
                }
                else
                {
                    return RedirectToAction("DailySales");
                }
            }
            else
            {
                return RedirectToAction("DailySales");
            }

        }

        public ActionResult GetTargetNAchievementReport(int CompanyId, int MonthId, int YearId, int EmployeeId = 0)
        {
                        
            if (ModelState.IsValid)
            {
                DataTable dt = null;
                string filepath = string.Empty;                
                int companyId = Convert.ToInt32(Session["companyId"]);
                SecCompany objCmnCompany = _SecCompanyService.GetById(companyId);                
                string companyName = objCmnCompany.Name;
                string companyAddress = objCmnCompany.Address;
                string reportName = "Sales Target & Achievement Report";
                

                List<ReportParameter> paramList = new List<ReportParameter>();


                #region ParameterListPreperation
               

                ReportParameter objcmpName = new ReportParameter();
                objcmpName.Name = "companyName";
                objcmpName.Value = companyName;
                paramList.Add(objcmpName);

                ReportParameter objcmpAddress = new ReportParameter();
                objcmpAddress.Name = "CompanyAddress";
                objcmpAddress.Value = companyAddress;
                paramList.Add(objcmpAddress);

                ReportParameter objReportName = new ReportParameter();
                objReportName.Name = "ReportName";
                objReportName.Value = reportName;
                paramList.Add(objReportName);

                ReportParameter objMonth = new ReportParameter();
                objMonth.Name = "Month";
                objMonth.Value = DisplayMonthName(MonthId);
                paramList.Add(objMonth);

                ReportParameter objYear = new ReportParameter();
                objYear.Name = "Year";
                String Year = YearId.ToString();
                objYear.Value = Year;
                paramList.Add(objYear);
                              

                #endregion ParameterListPreperation


                dt = _objTargetNAchievementReportService.GetTargetNAchievementReport(CompanyId, MonthId, YearId, EmployeeId);

                filepath = GetReportPath("TargetNAchievement.rpt");

                if (dt != null && filepath != string.Empty)
                {

                    return new CrystalReportResult(filepath, dt, paramList);
                }
                else
                {
                    return RedirectToAction("TargetNAchievement");
                }
            }
            else
            {
                return RedirectToAction("TargetNAchievement");
            }

        }
    }

}
