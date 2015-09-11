using ERPOptima.Data.Accounts.Repository;
using ERPOptima.Data.Common.Repository;
using ERPOptima.Data.Hrm.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Helper;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Accounts;
using ERPOptima.Model.HRM;
using ERPOptima.Model.Sales;
using ERPOptima.Model.Security;
using ERPOptima.Service.Accounts;
using ERPOptima.Service.Hrm;
using ERPOptima.Service.Sales;
using ERPOptima.Service.Security;
using ERPOptima.Web.Filters;
using Optima.Areas.Accounts.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Optima.Areas.Accounts.Controllers
{
    public partial class AdvanceController : Controller 
    {
        public ActionResult GetByCurrentFinancialYearId()
        {
            int financialYearId = Convert.ToInt32(Session["financialYear"]);
            var list = _advanceListService.GetByfinancialYearId(financialYearId).ToList();

            var newList = list.Select(t => new
            {
                Id = t.Id,
                RefNo = t.RefNo,
                CmnFinancialYearId = t.CmnFinancialYearId,
                AnFVoucherId = t.AnFVoucherId,
                HrmEmployeeId = t.HrmEmployeeId,
                EmpName = t.HrmEmployee.Name,
                SecCompanyId = t.SecCompanyId,
                AnFCostCenterId = t.AnFCostCenterId,
                Date = t.Date,
                // DateStr = t.Date.ToString("dd-MM-yyyy"),
                Advance = t.Advance,
                Purpose = t.Purpose,
                ProposedReturnDate = t.ProposedReturnDate,
                Status = t.Status,
                CreatedBy = t.CreatedBy,
                CreatedDate = t.CreatedDate,
                ModifiedBy = t.ModifiedBy,
                ModifiedDate = t.ModifiedDate

            }).ToList();
            return Json(newList, JsonRequestBehavior.AllowGet);

            //return Json(list, JsonRequestBehavior.AllowGet);

        }


        #region AdvanceAdjustmentReport
        public ActionResult GetAnFAdvanceAdjustmentReport(int anfAdvanceId)
        {
            if (ModelState.IsValid)
            {
                DataTable dt = null;
                string filepath = string.Empty;             

                AnFAdvance objAnfAdvance = _advanceListService.GetById(anfAdvanceId);

                HrmEmployee objHrmEmployee = _hrmEmployeeService.GetById(objAnfAdvance.HrmEmployeeId);

                string employeeName = objAnfAdvance.HrmEmployee.Name;
                string advance = objAnfAdvance.RefNo;

                List<ReportParameter> paramList = new List<ReportParameter>();

                #region ParameterListPreperation

                ReportParameter objcmpName = new ReportParameter();
                objcmpName.Name = "Employee";
                objcmpName.Value = employeeName;
                paramList.Add(objcmpName);

                ReportParameter objcmpAddress = new ReportParameter();
                objcmpAddress.Name = "Advance";
                objcmpAddress.Value = advance;
                paramList.Add(objcmpAddress);

                #endregion ParameterListPreperation


               // AdvanceAdjustmentReport
                dt = _advanceListService.GetAnFAdvanceAdjustmentReport(anfAdvanceId);
         
                    filepath = GetReportPath("AnFAdvanceAdjustmentReport.rpt");
                
               
                if (dt != null && filepath != string.Empty)
                {
                    return new CrystalReportResult(filepath, dt, paramList);
                }
                else
                {
                    return RedirectToAction("AdvanceAdjustmentReport");
                }
            }
            else
            {
                return RedirectToAction("AdvanceAdjustmentReport");
            }
        }
        #endregion AdvanceAdjustmentReport

    }
}