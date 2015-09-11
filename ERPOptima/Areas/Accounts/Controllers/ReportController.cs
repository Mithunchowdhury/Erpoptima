
using ERPOptima.Data.Accounts;
using ERPOptima.Data.Accounts.Repository;
using ERPOptima.Data.Common.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Helper;
using ERPOptima.Lib.Model;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Accounts;
using ERPOptima.Model.Common;
using ERPOptima.Model.Security;
using ERPOptima.Service.Accounts;
using ERPOptima.Service.Common;
using ERPOptima.Service.Security;
using ERPOptima.Web.Accounts.ViewModel;
using ERPOptima.Web.Filters;
using Optima.Areas.Accounts.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Optima.Areas.Accounts.Controllers
{
    public partial class ReportController : Controller
    {
        private IAnFVoucherService _anfVoucherService;
        private ISecCompanyService _CmnCompanyService;
        //private ICmnProjectService _CmnProjectService;
        private IAnFCostCenterService _ccService;
        private IChartOfAccountService _coaService;
        //private IAnFOpeningBalanceService _opBalanceService;

        public ReportController()
        {
            var dbfactory = new DatabaseFactory();
            _anfVoucherService = new AnFVoucherService(new AnFVoucherRepository(dbfactory), new UnitOfWork(dbfactory));
            _CmnCompanyService = new SecCompanyService(new SecCompanyRepository(dbfactory), new UnitOfWork(dbfactory));
            //_CmnProjectService = new CmnProjectService(new CmnProjectRepository(dbfactory), new UnitOfWork(dbfactory));
            _ccService = new AnFCostCenterService(new AnFCostCenterRepository(dbfactory), new UnitOfWork(dbfactory));
            _coaService = new ChartOfAccountService(new AnFChartOfAccountRepository(dbfactory), new UnitOfWork(dbfactory));
            //_opBalanceService = new AnFOpeningBalanceService(new AnFOpeningBalanceRepository(dbfactory), new UnitOfWork(dbfactory));
        }

        #region Private

        private string GetReportPath(string fileName)
        {
            string basePath = HostingEnvironment.MapPath("~/Areas/Accounts/CrystalReport");

            return Path.Combine(basePath, fileName);
        }

        #endregion Private

        //
        // GET: /Accounts/Report/
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Index()
        {
            return View();
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult DepositAndPrepaymentReport()
        {
            return View();
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Voucher()
        {
            return View();
            //string filepath = GetReportPath("AnFVouchers.rpt");

            //return new CrystalReportResult(filepath, null);
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult GeneralLedger()
        {
            return View();
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult CostCenterReport()
        {
            return View();
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult NoteSchedule()
        {
            return View();
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult ProfitLoss()
        {
            return View();
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult BalanceSheet()
        {
            return View();
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult CostOfSoldGoods()
        {
            return View();
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult TrialBalanceDetails()
        {
            return View();
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult TiralBalanceProjectWise()
        {
            return View();
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Receive()
        {
            return View();
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Payment()
        {
            return View();
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Employee()
        {
            return View();
        }
        [AuthorizeUser]
        public ActionResult ProductOrService()
        {
            return View();
        }

        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult StatementOfCashFlow()
        {
            return View();
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult RatioAnalysis()
        {
            return View();
        }

        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Receivable()
        {
            return View();
        }

        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Payable()
        {
            return View();
        }

        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult StatementOfChangesEquity()
        {
            return View();
        }


        [HttpGet]
        public ActionResult GetAnFTrialBalanceProjectWiseReport(ReportAnFTrialbalanceProjectWiseSearchViewModel obj)
        {
            DataTable dt = null;
            string filepath = string.Empty;

            int CompanyId = Convert.ToInt32(Session["companyId"].ToString());
            CompanyId = 1;
            long financilaYearId = Convert.ToInt64(Session["financialYear"].ToString());
            SecCompany objCmnCompany = _CmnCompanyService.GetById(CompanyId);

            string companyName = objCmnCompany.Name;
            string companyAddress = objCmnCompany.Address;

            List<ReportParameter> paramList = new List<ReportParameter>();

            #region ParameterListPreperation

            ReportParameter objcmpName = new ReportParameter();
            objcmpName.Name = "Company";
            objcmpName.Value = companyName;
            paramList.Add(objcmpName);

            ReportParameter objcmpDateTo = new ReportParameter();
            objcmpDateTo.Name = "ToDate";
            objcmpDateTo.Value = obj.ToDate.ToString("dd/MM/yyyy");
            paramList.Add(objcmpDateTo);

            ReportParameter objcmpDateFrom = new ReportParameter();
            objcmpDateFrom.Name = "DateFrom";
            objcmpDateFrom.Value = obj.FromDate.ToString("dd/MM/yyyy");
            paramList.Add(objcmpDateFrom);

            #endregion ParameterListPreperation

            //dt = _ccService.GetCostOfGoodSoldReport(CompanyId, financilaYearId, obj.FromDate, obj.ToDate, obj.CmnProjectId);

            dt = _ccService.GetTrialBalanceProjectWiseReport(CompanyId, financilaYearId, obj.FromDate, obj.ToDate, obj.BusinessId);

            filepath = GetReportPath("AnFTrialBalanceProject.rpt");

            if (dt != null && filepath != string.Empty)
            {
                return new CrystalReportResult(filepath, dt, paramList);
            }
            else
            {
                return RedirectToAction("CostOfSoldGoods");
            }
        }

        [HttpGet]
        public ActionResult GetAnFTrialBalanceDetailsReport(ReportAnFTrialBalanceDetalisSearchViewModel obj)
        {
            DataTable dt = null;
            string filepath = string.Empty;

            int CompanyId = Convert.ToInt32(Session["companyId"].ToString());
            //CompanyId = 1;
            int financilaYearId = Convert.ToInt32(Session["financialYear"].ToString());
            SecCompany objCmnCompany = _CmnCompanyService.GetById(CompanyId);

            AnFCostCenter costCenter = _ccService.GetCostCenterById(obj.AnFCostCeterId);

            string companyName = objCmnCompany.Name;
            string companyAddress = objCmnCompany.Address;

            List<ReportParameter> paramList = new List<ReportParameter>();

            #region ParameterListPreperation

            ReportParameter objcmpName = new ReportParameter();
            objcmpName.Name = "company";
            objcmpName.Value = companyName;
            paramList.Add(objcmpName);

            ReportParameter objPsID = new ReportParameter();
            objPsID.Name = "prmCostCenter";
            objPsID.Value = costCenter == null ? "" : costCenter.Name;
            paramList.Add(objPsID);

            ReportParameter objcmpDateTo = new ReportParameter();
            objcmpDateTo.Name = "toDate";
            objcmpDateTo.Value = obj.ToDate.ToString("dd/MM/yyyy");
            paramList.Add(objcmpDateTo);

            ReportParameter objcmpDateFrom = new ReportParameter();
            objcmpDateFrom.Name = "fromDate";
            objcmpDateFrom.Value = obj.FromDate.ToString("dd/MM/yyyy");
            paramList.Add(objcmpDateFrom);

            #endregion ParameterListPreperation

            dt = _ccService.GetTrialBalanceDetailsReport(CompanyId, financilaYearId, obj.FromDate, obj.ToDate,obj.AnFCostCeterId, false);

            filepath = GetReportPath("AnFTrialBalanceDetails.rpt");

            if (dt != null && filepath != string.Empty)
            {
                return new CrystalReportResult(filepath, dt, paramList);
            }
            else
            {
                return RedirectToAction("TrialBalanceDetails");
            }
        }

        [HttpGet]
        public ActionResult GetAnFCostOfGoodSoldReport(ReportAnFCostOfGoodSoldSearchVIewModel obj)
        {
            //new
            List<ReportParameter> paramList = new List<ReportParameter>();
            string filepath = string.Empty;
            int financilaYearId = Convert.ToInt32(Session["financialYear"].ToString());
            string companyName = string.Empty;
            int? CompanyId = obj.SecCompanyId;
            if (CompanyId != 0)
            {
                SecCompany objCmnCompany = _CmnCompanyService.GetById(CompanyId.Value);
                companyName = objCmnCompany.Name;
                string companyAddress = objCmnCompany.Address;
            }
            else
            {
                CompanyId = null;
                companyName = "MEP Group";
            }
            DataTable dt = null;

            #region ParameterListPreperation

            ReportParameter objcmpName = new ReportParameter();
            objcmpName.Name = "company";
            objcmpName.Value = companyName;
            paramList.Add(objcmpName);

            ReportParameter objcmpDateTo = new ReportParameter();
            objcmpDateTo.Name = "dateto";
            objcmpDateTo.Value = obj.ToDate.ToString("dd/MM/yyyy");
            paramList.Add(objcmpDateTo);

            ReportParameter objcmpDateFrom = new ReportParameter();
            objcmpDateFrom.Name = "datefrom";
            objcmpDateFrom.Value = obj.FromDate.ToString("dd/MM/yyyy");
            paramList.Add(objcmpDateFrom);

            #endregion ParameterListPreperation
            //new
            

            dt = _ccService.GetCostOfGoodSoldReport(CompanyId, financilaYearId, obj.FromDate, obj.ToDate);

            filepath = GetReportPath("AnFCostOfGoodsSold.rpt");

            if (dt != null && filepath != string.Empty)
            {
                return new CrystalReportResult(filepath, dt, paramList);
            }
            else
            {
                return RedirectToAction("CostOfSoldGoods");
            }
        }

        [HttpGet]
        public ActionResult GetCompany()
        {
            //int companyId = Convert.ToInt32(Session["companyId"]);
            //Convert.ToInt32(Session["companyId"]);
            var list = _CmnCompanyService.GetCmnCompanies();//.Select(ac => new
            //{
            //    Id = ac.Id,
            //    Name = ac.Name,
            //    Code = ac.Code,
            //    Location = ac.Location
            //});

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetAnFBalanceSheetReport(ReportAnFBalanceSheetSearchViewModel obj)
        {
            DataTable dt = null;
            string filepath = string.Empty;

            int financilaYearId = Convert.ToInt32(Session["financialYear"].ToString());
            // List<AnFChartOfAccount> list = _coaService.GetByCompanyId(CompanyId);
            
            List<ReportParameter> paramList = new List<ReportParameter>();

            #region ParameterListPreperation
            //int CompanyId = Convert.ToInt32(Session["companyId"].ToString());
            //ReportParameter objcmpSecCompanyId = new ReportParameter();
            string companyName = string.Empty;
            int? CompanyId = obj.SecCompanyId;
            if (CompanyId != 0)
            {
                SecCompany objCmnCompany = _CmnCompanyService.GetById(CompanyId.Value);
                companyName = objCmnCompany.Name;
                string companyAddress = objCmnCompany.Address;
            }
            else 
            {
                CompanyId = null;
                companyName = "MEP Group";
            }
            
            ReportParameter objcmpName = new ReportParameter();
            objcmpName.Name = "company";
            objcmpName.Value = companyName;
            paramList.Add(objcmpName);

            ReportParameter objcmpDateTo = new ReportParameter();
            objcmpDateTo.Name = "dateto";
            objcmpDateTo.Value = obj.ToDate.ToString("dd/MM/yyyy");
            paramList.Add(objcmpDateTo);

            ReportParameter objcmpDateFrom = new ReportParameter();
            objcmpDateFrom.Name = "datefrom";
            objcmpDateFrom.Value = obj.FromDate.ToString("dd/MM/yyyy");
            paramList.Add(objcmpDateFrom);

            //ReportParameter objcmpBid = new ReportParameter();
            //objcmpBid.Name = "bsid";
            //objcmpBid.Value = obj.CmnBusinessId.ToString();
            //paramList.Add(objcmpBid);

            //ReportParameter objcmpPid = new ReportParameter();
            //objcmpPid.Name = "psid";
            //objcmpPid.Value = obj.CmnProjectId.ToString();
            //paramList.Add(objcmpPid);

            #endregion ParameterListPreperation

            //dt = _coaService.GetAnFBalanceSheetReport(CompanyId, financilaYearId, obj.FromDate, obj.ToDate, obj.CmnBusinessId, obj.CmnProjectId);
            dt = _coaService.GetAnFBalanceSheetReport(CompanyId, financilaYearId, obj.FromDate, obj.ToDate);

            filepath = GetReportPath("AnFBalanceSheet.rpt");

            if (dt != null && filepath != string.Empty)
            {
                return new CrystalReportResult(filepath, dt, paramList);
            }
            else
            {
                return RedirectToAction("BalanceSheet");
            }
        }

        #region ChartOfAccounReport

        public ActionResult ChartOfAccountReport()
        {
            return View();
        }

        public ActionResult GetAnFChartOfAccountReport()
        {
            if (ModelState.IsValid)
            {
                DataTable dt = null;
                string filepath = string.Empty;

                string projectName = string.Empty;

                string costCenterName = string.Empty;

                string dateFrom = string.Empty;
                string dateTo = string.Empty;

                int CompanyId = Convert.ToInt32(Session["companyId"].ToString());

                // List<AnFChartOfAccount> list = _coaService.GetByCompanyId(CompanyId);

                SecCompany objCmnCompany = _CmnCompanyService.GetById(CompanyId);

                string companyName = objCmnCompany.Name;
                string companyAddress = objCmnCompany.Address;

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

                #endregion ParameterListPreperation

                dt = _coaService.GetAnFChartAccountsForReportByCompanyId(CompanyId);

                filepath = GetReportPath("AnFChartsofAccount.rpt");

                if (dt != null && filepath != string.Empty)
                {
                    return new CrystalReportResult(filepath, dt, paramList);
                }
                else
                {
                    return RedirectToAction("ChartOfAccount");
                }
            }
            else
            {
                return RedirectToAction("ChartOfAccount");
            }
        }
        public ActionResult OpeningBalanceReport()
        {
            return View();
        }


        public ActionResult GetAnFChartOfAccountWIthOpeningbalanceReport(int? YearId)
        {
            if (YearId == null)
            {
                return RedirectToAction("OpeningBalanceReport");
            }

            DataTable dt = null;
            string filepath = string.Empty;

            //string projectName = string.Empty;

            //string costCenterName = string.Empty;

            //string dateFrom = string.Empty;
            //string dateTo = string.Empty;

            int CompanyId = Convert.ToInt32(Session["companyId"].ToString());
            CompanyId = 1;
            //long FinancilaYearId = Convert.ToInt64(Session["financialYear"].ToString());

            SecCompany objCmnCompany = _CmnCompanyService.GetById(CompanyId);

            string companyName = objCmnCompany.Name;
            string companyAddress = objCmnCompany.Address;

            List<ReportParameter> paramList = new List<ReportParameter>();

            #region ParameterListPreperation

            ReportParameter objcmpName = new ReportParameter();
            objcmpName.Name = "company";
            objcmpName.Value = companyName;
            paramList.Add(objcmpName);

            ReportParameter objcmpAddress = new ReportParameter();
            objcmpAddress.Name = "companyAddress";
            objcmpAddress.Value = companyAddress;
            paramList.Add(objcmpAddress);

            #endregion ParameterListPreperation

            dt = _coaService.GetAnFChartAccountsWithOpeningBalanceForReportByCompanyId(CompanyId, YearId);
            //dt = _opBalanceService.GetOpeningBalanceReportById(CompanyId, YearId);

            filepath = GetReportPath("AnFOpenningBalance.rpt");

            if (dt != null && filepath != string.Empty)
            {
                return new CrystalReportResult(filepath, dt, paramList);
            }
            else
            {
                return RedirectToAction("OpeningBalanceReport");
            }
        }

        #endregion ChartOfAccounReport

        #region GeneralLedgerReport

        public ActionResult GetAnfGeneralLedger(GeneralLedgerSearchViewModel objGeneralLedgerSearchViewModel)
        {
            if (ModelState.IsValid)
            {
                DataTable dt = null;
                List<ReportParameter> paramList = new List<ReportParameter>();
                string filepath = string.Empty;

                //string projectName = string.Empty;

                string costCenterName = string.Empty;

                string dateFrom = string.Empty;
                string dateTo = string.Empty;

                int companyId = Convert.ToInt32(Session["companyId"]);
                int financialYearId = Convert.ToInt32(Session["financialYear"]);

                SecCompany objCmnCompany = _CmnCompanyService.GetById(companyId);

                string companyName = objCmnCompany.Name;
                string companyAddress = objCmnCompany.Address;


                dateFrom = objGeneralLedgerSearchViewModel.DateFrom.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                dateTo = objGeneralLedgerSearchViewModel.ToDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                //if (objGeneralLedgerSearchViewModel.Type == 1)
                //{
                //if (objGeneralLedgerSearchViewModel.ProjectId != null)
                //{
                //    projectName = _CmnProjectService.GetById(objGeneralLedgerSearchViewModel.ProjectId.GetValueOrDefault()).Name;
                //}
                if (objGeneralLedgerSearchViewModel.CostcenterId != null)
                {
                    costCenterName = _ccService.GetById(objGeneralLedgerSearchViewModel.CostcenterId.GetValueOrDefault()).Name;
                }

                #region ParameterListPreperation

                ReportParameter objcmpName = new ReportParameter();
                objcmpName.Name = "Company";
                objcmpName.Value = companyName;
                paramList.Add(objcmpName);

                ReportParameter objcmpAddress = new ReportParameter();
                objcmpAddress.Name = "CompanyAddress";
                objcmpAddress.Value = companyAddress;
                paramList.Add(objcmpAddress);

                //ReportParameter objProjecName = new ReportParameter();
                //objProjecName.Name = "Project";
                //objProjecName.Value = projectName;
                //paramList.Add(objProjecName);

                ReportParameter objCostCenter = new ReportParameter();
                objCostCenter.Name = "CostCenter";
                objCostCenter.Value = costCenterName;
                paramList.Add(objCostCenter);

                ReportParameter objDateFrom = new ReportParameter();
                objDateFrom.Name = "DateFrom";
                objDateFrom.Value = dateFrom;
                paramList.Add(objDateFrom);

                ReportParameter objDateTo = new ReportParameter();
                objDateTo.Name = "DateTo";
                objDateTo.Value = dateTo;
                paramList.Add(objDateTo);


                ReportParameter objAnfId = new ReportParameter();
                objAnfId.Name = "anfId";
                objAnfId.Value = objGeneralLedgerSearchViewModel.AnFChartOfAccountId.ToString();
                paramList.Add(objAnfId);
                #endregion ParameterListPreperation

                dt = _anfVoucherService.GetGeneralLedgerForReport(financialYearId, companyId,
                                                                objGeneralLedgerSearchViewModel.DateFrom.ToString("dd/MMM/yyyy"),
                                                                objGeneralLedgerSearchViewModel.ToDate.ToString("dd/MMM/yyyy"),
                                                                objGeneralLedgerSearchViewModel.CostcenterId,
                                                                objGeneralLedgerSearchViewModel.AnFChartOfAccountId,
                                                                objGeneralLedgerSearchViewModel.Status);

                filepath = GetReportPath("AnFGeneralLedger.rpt");
                // }
                //else if (objGeneralLedgerSearchViewModel.Type == 2)
                //{
                //    ReportParameter objDateFrom = new ReportParameter();
                //    objDateFrom.Name = "DateFrom";
                //    objDateFrom.Value = dateFrom;
                //    paramList.Add(objDateFrom);

                //    ReportParameter objDateTo = new ReportParameter();
                //    objDateTo.Name = "DateTo";
                //    objDateTo.Value = dateTo;
                //    paramList.Add(objDateTo);

                //    ReportParameter objCompany = new ReportParameter();
                //    objCompany.Name = "Company";
                //    objCompany.Value = companyName;
                //    paramList.Add(objCompany);

                //    dt = _anfVoucherService.GetProjectGeneralLedgerForReport(financialYearId, companyId, objGeneralLedgerSearchViewModel.DateFrom.ToString("dd/MMM/yyyy"),
                //        objGeneralLedgerSearchViewModel.ToDate.ToString("dd/MMM/yyyy"), objGeneralLedgerSearchViewModel.BusinessId, objGeneralLedgerSearchViewModel.ProjectId,
                //        objGeneralLedgerSearchViewModel.AnFChartOfAccountId, objGeneralLedgerSearchViewModel.Status);

                //    filepath = GetReportPath("AnFGeneralLedgerProject.rpt");
                //}

                if (dt != null && filepath != string.Empty)
                {
                    return new CrystalReportResult(filepath, dt, paramList);
                }
                else
                {
                    return RedirectToAction("GeneralLedger");
                }
            }
            else
            {
                return RedirectToAction("GeneralLedger");
            }
        }

        #endregion GeneralLedgerReport

        #region Cost Center Report

        public ActionResult GetAnFCostCenterReport()
        {
            DataTable dt = null;
            string filepath = string.Empty;

            int companyId = Convert.ToInt32(Session["companyId"].ToString());
            SecCompany objCmnCompany = _CmnCompanyService.GetById(companyId);
            IList<ReportAnFCostCentreViewModel> collection = new List<ReportAnFCostCentreViewModel>();
            List<ReportParameter> paramList = new List<ReportParameter>();

            ReportParameter objcmpName = new ReportParameter();
            objcmpName.Name = "Company";
            objcmpName.Value = objCmnCompany.Name;
            paramList.Add(objcmpName);

            ReportParameter objcmpAddress = new ReportParameter();
            objcmpAddress.Name = "CompanyAddress";
            objcmpAddress.Value = objCmnCompany.Address;
            paramList.Add(objcmpAddress);
            if (ModelState.IsValid)
            {
                dt = new DataTable();
                dt = _ccService.GetCostCentersByCompanyId(companyId);

                filepath = GetReportPath("AnFCostCenter.rpt");

                if (dt != null && filepath != string.Empty)
                {
                    return new CrystalReportResult(filepath, dt, paramList);
                }
                else
                {
                    return RedirectToAction("CostCenterReport", new { id = 1 });
                    //return RedirectToAction("CostCenterReport");
                }
            }
            else
            {
                return RedirectToAction("CostCenterReport");
            }
        }

        #endregion Cost Center Report

        #region NoteSchedule

        public ActionResult GetNoteSchedule(NoteScheduleSearchViewModel objNoteScheduleSearchViewModel)
        {
            if (ModelState.IsValid)
            {
                List<ReportParameter> paramList = new List<ReportParameter>();
                string filepath = string.Empty;           

                string costCenterName = string.Empty;

                string dateFrom = string.Empty;
                string dateTo = string.Empty;

                int companyId = Convert.ToInt32(Session["companyId"]);
                int financialYearId = Convert.ToInt32(Session["financialYear"]);
                SecCompany objCmnCompany = _CmnCompanyService.GetById(companyId);

                string companyName = objCmnCompany.Name;
                string companyAddress = objCmnCompany.Address;

                DataTable dt = null;
                dateFrom = objNoteScheduleSearchViewModel.DateFrom.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                dateTo = objNoteScheduleSearchViewModel.ToDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

              
                if (objNoteScheduleSearchViewModel.CostcenterId != null)
                {
                    costCenterName = _ccService.GetById(objNoteScheduleSearchViewModel.CostcenterId.GetValueOrDefault()).Name;
                }

                #region ParameterListPreperation

                ReportParameter objcmpName = new ReportParameter();
                objcmpName.Name = "Company";
                objcmpName.Value = companyName;
                paramList.Add(objcmpName);

                ReportParameter objcmpAddress = new ReportParameter();
                objcmpAddress.Name = "CompanyAddress";
                objcmpAddress.Value = companyAddress;
                paramList.Add(objcmpAddress);

                ReportParameter objDateFrom = new ReportParameter();
                objDateFrom.Name = "DateFrom";
                objDateFrom.Value = dateFrom;
                paramList.Add(objDateFrom);

                ReportParameter objDateTo = new ReportParameter();
                objDateTo.Name = "DateTo";
                objDateTo.Value = dateTo;
                paramList.Add(objDateTo);

                #endregion ParameterListPreperation

                //if (objNoteScheduleSearchViewModel.Type == 1)
                //{ 
                dt = _coaService.GetReportForNoteSchedule(financialYearId, companyId, 
                                                           objNoteScheduleSearchViewModel.DateFrom.ToString("dd/MMM/yyyy"),
                                                           objNoteScheduleSearchViewModel.ToDate.ToString("dd/MMM/yyyy"),                                                        
                                                           objNoteScheduleSearchViewModel.CostcenterId, 
                                                           objNoteScheduleSearchViewModel.AnFChartOfAccountId);

               

                filepath = GetReportPath("AnFNoteSchedule.rpt");
                //}
                //else if (objNoteScheduleSearchViewModel.Type == 2)
                //{
                //    //dt = _coaService.GetReportForNoteScheduleProject(financialYear, companyId, objNoteScheduleSearchViewModel.DateFrom.ToString("dd/MMM/yyyy"),
                //    // objNoteScheduleSearchViewModel.ToDate.ToString("dd/MMM/yyyy"), objNoteScheduleSearchViewModel.AnFChartOfAccountId);

                //    #region Demo

                //    dt = _coaService.GetReportForNoteScheduleProject(financialYear, companyId,objNoteScheduleSearchViewModel.BusinessId, objNoteScheduleSearchViewModel.DateFrom.ToString("dd/MMM/yyyy"),
                //     objNoteScheduleSearchViewModel.ToDate.ToString("dd/MMM/yyyy"), objNoteScheduleSearchViewModel.AnFChartOfAccountId);

                //    #endregion Demo

                //    filepath = GetReportPath("AnFNoteScheduleProject.rpt");
                //}

                if (dt != null && filepath != string.Empty)
                {
                    return new CrystalReportResult(filepath, dt, paramList);
                }
                else
                {
                    return RedirectToAction("NoteSchedule");
                }
            }
            else
            {
                return RedirectToAction("NoteSchedule");
            }
        }

        #endregion NoteSchedule

        #region ProfitLosst

        public ActionResult GetProfitLoss(ProfitLossSearchViewModel obj)
        {
                List<ReportParameter> paramList = new List<ReportParameter>();
                string filepath = string.Empty;
                int financilaYearId = Convert.ToInt32(Session["financialYear"].ToString());


                string companyName = string.Empty;
                int? CompanyId = obj.SecCompanyId;
                if (CompanyId != 0)
                {
                    SecCompany objCmnCompany = _CmnCompanyService.GetById(CompanyId.Value);
                    companyName = objCmnCompany.Name;
                    string companyAddress = objCmnCompany.Address;
                }
                else
                {
                    CompanyId = null;
                    companyName = "MEP Group";
                }
                DataTable dt = null;
                
                #region ParameterListPreperation

                ReportParameter objcmpName = new ReportParameter();
                objcmpName.Name = "company";
                objcmpName.Value = companyName;
                paramList.Add(objcmpName);

                ReportParameter objcmpDateTo = new ReportParameter();
                objcmpDateTo.Name = "dateto";
                objcmpDateTo.Value = obj.ToDate.ToString("dd/MM/yyyy");
                paramList.Add(objcmpDateTo);

                ReportParameter objcmpDateFrom = new ReportParameter();
                objcmpDateFrom.Name = "datefrom";
                objcmpDateFrom.Value = obj.FromDate.ToString("dd/MM/yyyy");
                paramList.Add(objcmpDateFrom);

                #endregion ParameterListPreperation

                dt = _anfVoucherService.GetProfitLossForReport(CompanyId, financilaYearId, obj.FromDate, obj.ToDate);
                
                filepath = GetReportPath("AnFProfitLoss.rpt");

                if (dt != null && filepath != string.Empty)
                {
                    return new CrystalReportResult(filepath, dt, paramList);
                }
                else
                {
                    return RedirectToAction("ProfitLoss");
                }
        }

        #endregion ProfitLosst
    }
}