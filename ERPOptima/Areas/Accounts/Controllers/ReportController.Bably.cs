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

        #region VoucherReport

        [HttpGet]
        public ActionResult GetAnfVoucherList(VoucherSearchViewModel objVoucherSearchViewModel)
        {
            if (ModelState.IsValid)
            {
                DataTable dt = null;
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

                List<ReportParameter> paramList = new List<ReportParameter>();

                if (objVoucherSearchViewModel.CostcenterId != null)
                {
                    costCenterName = _ccService.GetById(objVoucherSearchViewModel.CostcenterId.GetValueOrDefault()).Name;
                }

                dateFrom = objVoucherSearchViewModel.DateFrom.GetValueOrDefault().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                dateTo = objVoucherSearchViewModel.ToDate.GetValueOrDefault().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                #region ParameterListPreperation

                ReportParameter objcmpName = new ReportParameter();
                objcmpName.Name = "Company";
                objcmpName.Value = companyName;
                paramList.Add(objcmpName);

                ReportParameter objcmpAddress = new ReportParameter();
                objcmpAddress.Name = "CompanyAddress";
                objcmpAddress.Value = companyAddress;
                paramList.Add(objcmpAddress);

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

                #endregion ParameterListPreperation

                if (objVoucherSearchViewModel.ReportType == 2)
                {
                    dt = _anfVoucherService.GetAnfVoucherList(financialYearId, companyId,
                                             objVoucherSearchViewModel.VoucherTypeId,
                                              objVoucherSearchViewModel.DateFrom,
                                             objVoucherSearchViewModel.ToDate,
                                             objVoucherSearchViewModel.CostcenterId);

                    filepath = GetReportPath("AnFVoucherList.rpt");
                }

                if (objVoucherSearchViewModel.ReportType == 3)
                {
                    dt = _anfVoucherService.GetShortDetailAnFVoucherList(financialYearId, companyId,
                                             objVoucherSearchViewModel.VoucherTypeId,
                                              objVoucherSearchViewModel.DateFrom,
                                             objVoucherSearchViewModel.ToDate,
                                             objVoucherSearchViewModel.CostcenterId);
                    filepath = GetReportPath("AnFShortVouchersList.rpt");
                }

                if (objVoucherSearchViewModel.ReportType == 4)
                {
                    dt = _anfVoucherService.GetShortDetailAnFVoucherList(financialYearId, companyId,
                                             objVoucherSearchViewModel.VoucherTypeId,
                                              objVoucherSearchViewModel.DateFrom,
                                             objVoucherSearchViewModel.ToDate,
                                             objVoucherSearchViewModel.CostcenterId);
                    filepath = GetReportPath("AnFDetailVouchers.rpt");
                }
                if (objVoucherSearchViewModel.ReportType == 5)
                {
                    //Payment
                    dt = _anfVoucherService.GetPaymentVouchers(financialYearId, companyId,
                                             objVoucherSearchViewModel.VoucherTypeId,
                                              objVoucherSearchViewModel.DateFrom,
                                             objVoucherSearchViewModel.ToDate,
                                             objVoucherSearchViewModel.CostcenterId);
                    ReportParameter PaymentOrRecieve = new ReportParameter();
                    PaymentOrRecieve.Name = "PaymentOrRecieve";
                    PaymentOrRecieve.Value = "Payment Report";
                    paramList.Add(PaymentOrRecieve);
                    //PaymentOrRecieve
                    filepath = GetReportPath("AnFPayment.rpt");
                }
                if (objVoucherSearchViewModel.ReportType == 6)
                {
                    //Receipt  
                    dt = _anfVoucherService.GetPaymentVouchers(financialYearId, companyId,
                                            objVoucherSearchViewModel.VoucherTypeId,
                                             objVoucherSearchViewModel.DateFrom,
                                            objVoucherSearchViewModel.ToDate,
                                            objVoucherSearchViewModel.CostcenterId);
                    ReportParameter PaymentOrRecieve = new ReportParameter();
                    PaymentOrRecieve.Name = "PaymentOrRecieve";
                    PaymentOrRecieve.Value = "Receive Report";
                    paramList.Add(PaymentOrRecieve);
                    //PaymentOrRecieve
                    filepath = GetReportPath("AnFPayment.rpt");
                }
                if (dt != null && filepath != string.Empty)
                {
                    return new CrystalReportResult(filepath, dt, paramList);
                }
                else
                {
                    return RedirectToAction("Voucher");
                }
            }
            else
            {
                return RedirectToAction("Voucher");
            }
        }

        public ActionResult GetAnfVoucher(long Id)
        {
            DataTable dt = _anfVoucherService.GetAnfVoucher(Id);

            int companyId = Convert.ToInt32(Session["companyId"]);
            int financialYearId = Convert.ToInt32(Session["financialYear"]);

            SecCompany objCmnCompany = _CmnCompanyService.GetById(companyId);

            string companyName = objCmnCompany.Name;
            string companyAddress = objCmnCompany.Address;

            List<ReportAnFVoucherViewModel> list = dt.DataTableToList<ReportAnFVoucherViewModel>();

            List<ReportParameter> paramList = new List<ReportParameter>();
            ReportParameter objcmpName = new ReportParameter();
            objcmpName.Name = "Company";
            objcmpName.Value = companyName;
            paramList.Add(objcmpName);

            ReportParameter objcmpAddress = new ReportParameter();
            objcmpAddress.Name = "CompanyAddress";
            objcmpAddress.Value = companyAddress;
            paramList.Add(objcmpAddress);

            string filepath = GetReportPath("AnFVouchers.rpt");

            return new CrystalReportResult(filepath, list, paramList);
        }

        public ActionResult SearchAnFVoucher(VoucherSearchViewModel objVoucherSearchViewModel)
        {
            if (ModelState.IsValid)
            {
                int companyId = Convert.ToInt32(Session["companyId"].ToString());
                int financialYearId = Convert.ToInt32(Session["financialYear"].ToString());

                DataTable dt = null;
                dt = _anfVoucherService.GetAnfVoucherList(financialYearId, companyId,
                                             objVoucherSearchViewModel.VoucherTypeId,
                                              objVoucherSearchViewModel.DateFrom,
                                             objVoucherSearchViewModel.ToDate,
                                             objVoucherSearchViewModel.CostcenterId);
                List<ReportVoucherListViewModel> list = dt.DataTableToList<ReportVoucherListViewModel>();

                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return RedirectToAction("Voucher");
            }
        }

        #endregion VoucherReport

        #region ReceivableReport
        public ActionResult GetAnFReceivableReport(int? costCenterId, int ReportType)
        {
            if (ModelState.IsValid)
            {
                DataTable dt = null;
                string filepath = string.Empty;

                string costCenterName = string.Empty;

                int companyId = Convert.ToInt32(Session["companyId"]);
                int financialYearId = Convert.ToInt32(Session["financialYear"]);

                SecCompany objCmnCompany = _CmnCompanyService.GetById(companyId);

                string companyName = objCmnCompany.Name;
                string companyAddress = objCmnCompany.Address;

                List<ReportParameter> paramList = new List<ReportParameter>();

                if (costCenterId != null)
                {
                    costCenterName = _ccService.GetById(costCenterId.GetValueOrDefault()).Name;
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

                ReportParameter objCostCenter = new ReportParameter();
                objCostCenter.Name = "CostCenter";
                objCostCenter.Value = costCenterName;
                paramList.Add(objCostCenter);

                #endregion ParameterListPreperation


                if (ReportType == 1)
                {
                    //Receivable
                dt = _anfVoucherService.GetAnFReceivableReport(financialYearId, companyId, costCenterId);
                    ReportParameter ReceivableOrPayable = new ReportParameter();
                    ReceivableOrPayable.Name = "ReceivableOrPayable";
                    ReceivableOrPayable.Value = "Receivable Report";
                    paramList.Add(ReceivableOrPayable);
                    //ReceivableOrPayable
                    filepath = GetReportPath("AnFReceivableReport.rpt");
                }
                if (ReportType == 2)
                {
                    //Payable  
                    dt = _anfVoucherService.GetAnFPayableReport(financialYearId, companyId, costCenterId);
                    ReportParameter ReceivableOrPayable = new ReportParameter();
                    ReceivableOrPayable.Name = "ReceivableOrPayable";
                    ReceivableOrPayable.Value = "Payable Report";
                    paramList.Add(ReceivableOrPayable);
                    //ReceivableOrPayable
                    filepath = GetReportPath("AnFReceivableReport.rpt");
                }
                if (dt != null && filepath != string.Empty)
                {
                    return new CrystalReportResult(filepath, dt, paramList);
                }
                else
                {
                    return RedirectToAction("ReceivableReport");
                }
            }
            else
            {
                return RedirectToAction("ReceivableReport");
            }
        }
        #endregion ReceivableReport
    }
}