using ERPOptima.Data.Common.Repository;
using ERPOptima.Data.Accounts.Repository;
using ERPOptima.Data.Inventory.Repository;
using ERPOptima.Data.Hrm.Repository;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Inventory;
using ERPOptima.Model.Accounts;
using ERPOptima.Model.Sales;
using ERPOptima.Model.Security;
using ERPOptima.Service.Accounts;
using ERPOptima.Service.Hrm;
using ERPOptima.Service.Inventory;
using ERPOptima.Service.Sales;
using ERPOptima.Service.Security;
using ERPOptima.Web.Filters;
using Optima.Areas.Accounts.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using System.IO;
using System.Globalization;
using ERPOptima.Helper;
using System.Web.Hosting;
//using Tutorial.FileUpload.Models;


namespace Optima.Areas.Accounts.Controllers
{
    [Authorize]
    public class ExpenseController : Controller
    {
        private IAnFExpenseService _anfExpenseService;
        private ISecCompanyService _SecCompanyService;
        private IOfficeService _officeService;
        private IHrmEmployeeService _hrmEmployeeService;
        private IAnFCostCenterService _ccService;  // Add by Bably
       
        int companyId;
        int financialYearId;
        int userId;
        int moduleId;
        public ExpenseController()
        {
            var dbfactory = new DatabaseFactory();
            _anfExpenseService = new AnFExpenseService(new ERPOptima.Data.Accounts.Repository.AnFExpenseRepository(dbfactory), new UnitOfWork(dbfactory));
            _SecCompanyService = new SecCompanyService(new SecCompanyRepository(dbfactory), new UnitOfWork(dbfactory));
            _officeService = new OfficeService(new OfficeRepository(dbfactory), new UnitOfWork(dbfactory));
            _hrmEmployeeService = new HrmEmployeeService(new HrmEmployeeRepository(dbfactory), new UnitOfWork(dbfactory));
            _ccService = new AnFCostCenterService(new AnFCostCenterRepository(dbfactory), new UnitOfWork(dbfactory)); //Add by Bably
        }

        #region Private

        private string GetReportPath(string fileName)
        {
            string basePath = HostingEnvironment.MapPath("~/Areas/Accounts/CrystalReport");

            return Path.Combine(basePath, fileName);
        }

        #endregion Private
        //
        // GET: /Accounts/Expense/
        [AuthorizeUser]
        public ActionResult Index()
        {
            return View();
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Expense()
        {
            return View();
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult ExpenseReport()
        {
            return View();
        }

        //for Expenses List view
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult ExpenseList()
        {
            return View();
        }

        //For Expens List Search

        public ActionResult Search(DateTime? fromDate, DateTime? toDate, bool? status)
        {
            int companyId = Convert.ToInt32(Session["companyId"].ToString());
            //financialYearId = Convert.ToInt32(Session["financialYearId"].ToString());
            int financialYearId = Convert.ToInt32(Session["financialYear"].ToString());
            IList<AnFExpens> list = new List<AnFExpens>();

            list = _anfExpenseService.Search(companyId, financialYearId, fromDate, toDate, status);

            return Json(list, JsonRequestBehavior.AllowGet);
           

        }
        //public ActionResult Update(AnFExpens expense)
        //{
        //    Operation operation = new Operation { Success = false };
        //    if (ModelState.IsValid)
        //    {
        //        var obj = _anfExpenseService.GetAll(AnFExpens.Id);
        //        obj.LockingStatus = AnFExpens.LockStatus;
        //        operation = _anfExpenseService.Update(obj);
        //    }

        //    return Json(operation, JsonRequestBehavior.DenyGet);
        //}

        // Save Method

        public string GetCode(int companyId, int employeeId)
        {
            companyId = Convert.ToInt32(Session["companyId"]);
            employeeId = Convert.ToInt32(Session["userId"]);
            SecCompany objCmnCompany = _SecCompanyService.GetById(companyId);
            SlsOffice office = _officeService.GetById((int)_hrmEmployeeService.GetById(employeeId).SlsOfficeId);
            string code = _anfExpenseService.GetLastCode(companyId, objCmnCompany.Prefix, office.Code);
            return code;

        }
        public ActionResult GetByCurrentFinancialYearId()
        {
            int financialYearId = Convert.ToInt32(Session["financialYear"]);
            var list = _anfExpenseService.GetByfinancialYearId(financialYearId).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //save file start
        //public JsonResult SaveFiles()
        //{
        //    string Message, fileName, actualFileName;
        //    Message = fileName = actualFileName = string.Empty;
        //    bool flag = false;
        //    if (Request.Files != null)
        //    {
        //        var file = Request.Files[0];
        //        actualFileName = file.FileName;
        //        fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                
        //        try
        //        {
        //            file.SaveAs(Path.Combine(Server.MapPath("~/Expense"), fileName));

        //            UploadedFile f = new UploadedFile
        //            {
        //                FileName = actualFileName,
        //                FilePath = fileName
                        
        //            };
        //        }
        //        catch (Exception)
        //        {
        //            Message = "File upload failed! Please try again";
        //        }

        //    }
        //    return new JsonResult { Data = new { Message = Message, Status = flag } };
        //}

        //save file end


        [HttpPost]
        public ActionResult Save(AnFExpens expense)
        {
            //System.IO.Directory.CreateDirectory("Expense");
            if(!System.IO.Directory.Exists(Server.MapPath("~/Expense")))
            {
                System.IO.Directory.CreateDirectory(Server.MapPath(@"~/Expense"));
            }
            //expense.FileLocation.s
            //expense.FileLocation.SaveAs(Server.MapPath("~/Expense" + expense.FileLocation.FileName));
            //return Json("Tutorial Saved", JsonRequestBehavior.AllowGet);
            

            
            int companyId = Convert.ToInt32(Session["companyId"]);
            int userid = Convert.ToInt32(Session["userId"]);
            int financialYearId = Convert.ToInt32(Session["financialYear"]);
            //SaveFiles();
            expense.CmnFinancialYearId = financialYearId;

            Operation objOperation = new Operation();
            if (ModelState.IsValid)
            {
                if (expense.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        expense.SecCompanyId = companyId;
                        expense.CmnFinancialYearId = financialYearId;
                        expense.CreatedBy = userid;
                        expense.IsPosted = false;
                        //expense.FileLocation = filelocat;

                        expense.CreatedDate = DateTime.Now.Date;
                        objOperation = _anfExpenseService.Save(expense);
                    }
                    else
                    {
                        objOperation.OperationId = -2;
                        objOperation.Success = false;
                    }
                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        expense.SecCompanyId = companyId;
                        expense.CmnFinancialYearId = financialYearId;
                        expense.ModifiedBy = userid;
                        expense.IsPosted = false;
                        expense.ModifiedDate = DateTime.Now.Date;
                        objOperation = _anfExpenseService.Update(expense);
                    }
                    else
                    {
                        objOperation.OperationId = -2;
                        objOperation.Success = false;
                    }
                }
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }
        //Delete Method
        [HttpPost]
        public ActionResult Delete(int Id)
        {
            //ff
            //Operation objOperation = new Operation { Success = false };
            //if (Id != 0)
            //{
            //    AnFAdjustment obj = _advancetAdjustmentService.GetById(Id);
            //    if (obj == null)
            //    {
            //        objOperation.Success = false;
            //        return Json(objOperation, JsonRequestBehavior.DenyGet);
            //    }
            //    objOperation = _advancetAdjustmentService.Delete(obj);
            //}
            //return Json(objOperation, JsonRequestBehavior.DenyGet);
            //ff
            Operation objOperation = new Operation { Success = false };
            if (Id != 0)
            {
                AnFExpens obj = _anfExpenseService.GetById(Id);
                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _anfExpenseService.Delete(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        // Add by Bably

        #region ExpenseReport

        public ActionResult GetAnfExpenseReport(ExpenseSearchViewModel objExpenseSearchViewModel)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<ReportAnFExpenseViewModel> list = null;
                IList<AnFExpens> expList = null;
                List<ReportParameter> paramList = new List<ReportParameter>();
                string filepath = string.Empty;

                //string projectName = string.Empty;

                string costCenterName = string.Empty;

                string dateFrom = string.Empty;
                string dateTo = string.Empty;

                int companyId = Convert.ToInt32(Session["companyId"]);
                int financialYearId = Convert.ToInt32(Session["financialYear"]);

                SecCompany objCmnCompany = _SecCompanyService.GetById(companyId);

                string companyName = objCmnCompany.Name;
                string companyAddress = objCmnCompany.Address;


                dateFrom = objExpenseSearchViewModel.DateFrom.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                dateTo = objExpenseSearchViewModel.ToDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                if (objExpenseSearchViewModel.CostcenterId != null)
                {
                    costCenterName = _ccService.GetById(objExpenseSearchViewModel.CostcenterId.GetValueOrDefault()).Name;
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

                //ReportParameter objCostCenter = new ReportParameter();
                //objCostCenter.Name = "CostCenter";
                //objCostCenter.Value = costCenterName;
                //paramList.Add(objCostCenter);

                ReportParameter objDateFrom = new ReportParameter();
                objDateFrom.Name = "DateFrom";
                objDateFrom.Value = dateFrom;
                paramList.Add(objDateFrom);

                ReportParameter objDateTo = new ReportParameter();
                objDateTo.Name = "DateTo";
                objDateTo.Value = dateTo;
                paramList.Add(objDateTo);


                //ReportParameter objAnfId = new ReportParameter();
                //objAnfId.Name = "anfId";
                //objAnfId.Value = objGeneralLedgerSearchViewModel.AnFChartOfAccountId.ToString();
                //paramList.Add(objAnfId);
                #endregion ParameterListPreperation

                expList = _anfExpenseService.Search(companyId, financialYearId, objExpenseSearchViewModel.DateFrom, objExpenseSearchViewModel.ToDate, null).ToList();
                                                                //objExpenseSearchViewModel.AnFChartOfAccountId,
                                                                //objExpenseSearchViewModel.Status);
                //var newlist = expList.Select(t => new
                //{
                //    Id = t.Id,
                //    RefNo = t.RefNo,
                //    Particular = t.Particular == null ? "" : t.Particular,
                //    AnFChartOfAccountId = t.AnFChartOfAccountId,
                //    AnFCostCenterId = t.AnFCostCenterId == null ? 0 : (int)t.AnFCostCenterId,
                //    Date = t.Date,
                //    BillNo = t.BillNo,
                //    Mode = t.Mode,
                //    Narration = t.Narration,
                //    FileLocation = t.FileLocation == null ? "" : t.FileLocation,
                //    CmnFinancialYearId = t.CmnFinancialYearId,
                //    SecCompanyId = t.SecCompanyId,
                //    AnFVoucherId = t.AnFVoucherId == null ? 0 : (int)t.AnFVoucherId,
                //    Amount = t.Amount.Value,
                //    IsPosted = t.IsPosted
                //}).ToList();
                foreach (var obj in expList)
                {

                }
                filepath = GetReportPath("ExpenseReport.rpt");
   

                if (list != null && filepath != string.Empty)
                {
                    //return RedirectToAction("ExpenseReport");
                    return new CrystalReportResult(filepath, list, paramList);
                }
                else
                {
                    return RedirectToAction("ExpenseReport");
                }
            }
            else
            {
                return RedirectToAction("ExpenseReport");
            }
        }
        #endregion ExpenseReport
    }
}
