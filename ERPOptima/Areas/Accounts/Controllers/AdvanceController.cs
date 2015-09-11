using ERPOptima.Data.Accounts.Repository;
using ERPOptima.Data.Common.Repository;
using ERPOptima.Data.Hrm.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Accounts;
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
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Optima.Areas.Accounts.Controllers
{
    public partial class AdvanceController : Controller
    {
        private IAnfAdvancetListService _advanceListService;
        private IAnfAdvancetAdjustmentService _advancetAdjustmentService;
        private ISecCompanyService _SecCompanyService;      //Add by Bably
        private IHrmEmployeeService _hrmEmployeeService;   //Add by Bably
        private IOfficeService _officeService;            //Add by Bably   
        public AdvanceController() 
        {
            var dbfactory = new DatabaseFactory();
            _advanceListService = new AnfAdvancetListService(new AnfAdvancetListepository(dbfactory),new UnitOfWork(dbfactory));
            _advancetAdjustmentService = new AnfAdvancetAdjustmentService(new AnfAdvanceAdjustmentRepository(dbfactory), new UnitOfWork(dbfactory));
            _SecCompanyService = new SecCompanyService(new SecCompanyRepository(dbfactory), new UnitOfWork(dbfactory));
            _officeService = new OfficeService(new OfficeRepository(dbfactory), new UnitOfWork(dbfactory));
            _hrmEmployeeService = new HrmEmployeeService(new HrmEmployeeRepository(dbfactory), new UnitOfWork(dbfactory));
        }

        #region Private

        private string GetReportPath(string fileName)
        {
            string basePath = HostingEnvironment.MapPath("~/Areas/Accounts/CrystalReport");

            return Path.Combine(basePath, fileName);
        }

        #endregion Private
        //
        // GET: /Accounts/Advance/
        [AuthorizeUser]
        public ActionResult Index()
        {
            return View();
        }

        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult AdvanceReport()
        {
            return View();
        }

        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult AdvanceAdjustmentReport()
        {
            return View();
        }

        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult AdvanceEntry()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdjustmentListSearch(int AnFAdvanceId)
        {
            IList<AnFAdjustment> list = new List<AnFAdjustment>();
            IList<AnfAdjustmentsViewModel> newlist = new List<AnfAdjustmentsViewModel>();

            list = _advancetAdjustmentService.AdjustmentListSearch(AnFAdvanceId);
            newlist = list.Select(t => new AnfAdjustmentsViewModel
            {
                Id = t.Id,
                AnFAdvanceId = t.AnFAdvanceId,
                AdjustedAmount=t.AdjustedAmount,
                Date=t.Date,
                CmnFinancialYearId=t.CmnFinancialYearId,
                SecCompanyId=t.SecCompanyId,
                AnFVoucherId=t.AnFVoucherId,
                AdvanceRefNo=t.AnFAdvance.RefNo,
                EmployeeName = t.AnFAdvance.HrmEmployee.Name
            }).ToList();

            return Json(newlist, JsonRequestBehavior.AllowGet);       

        }
        [HttpPost]
        public ActionResult AdjustmentGridView()
        {
            IList<AnFAdjustment> list = new List<AnFAdjustment>();
            IList<AnfAdjustmentsViewModel> newlist = new List<AnfAdjustmentsViewModel>();

            list = _advancetAdjustmentService.AdjustmentList();
            newlist = list.Select(t => new AnfAdjustmentsViewModel
            {
                Id = t.Id,
                AnFAdvanceId = t.AnFAdvanceId,
                AdjustedAmount = t.AdjustedAmount,
                Date = t.Date,
                CmnFinancialYearId = t.CmnFinancialYearId,
                SecCompanyId = t.SecCompanyId,
                AnFVoucherId = t.AnFVoucherId,
                AdvanceRefNo = t.AnFAdvance.RefNo,
                EmployeeName = t.AnFAdvance.HrmEmployee.Name
            }).ToList();

            return Json(newlist, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        ActionResult GetAllAdvance()
        {
            int companyId = Convert.ToInt32(Session["companyId"]);
            var list = _advanceListService.GetAllAdvance(companyId).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTransactionalHeadByCompanyId(int companyId = 1)
        {
            List<AnFAdvance> list = _advanceListService.GetTransactionalHeadByCompanyId(companyId).ToList();

            var newVM = list.Select(coa => new
            {
                Id = coa.Id,
                RefNo = coa.RefNo
                
            }).ToList();

            return Json(newVM, JsonRequestBehavior.AllowGet);
        }

        
        
        // Add by Bably
        public string GetRefNo(int companyId, int employeeId)
        
        {
            //companyId = Convert.ToInt32(Session["companyId"]);
            //employeeId = Convert.ToInt32(Session["userId"]);
            SecCompany objCmnCompany = _SecCompanyService.GetById(companyId);
            SlsOffice office = _officeService.GetById((int)_hrmEmployeeService.GetById(employeeId).SlsOfficeId);
            string refno = _advanceListService.GetRefNo(companyId, objCmnCompany.Prefix, office.Code);
            return refno;

        }
        
        //---------------------------Save Advance--------------------
        public ActionResult Save(AnFAdvance advanceEntry)
        {

            int companyId = Convert.ToInt32(Session["companyId"]);
            int userid = Convert.ToInt32(Session["userId"]);
            int financialYearId = Convert.ToInt32(Session["financialYear"]);
     
            Operation objOperation = new Operation();
            if (ModelState.IsValid)
            {
                if (advanceEntry.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        advanceEntry.SecCompanyId = companyId;
                        advanceEntry.CmnFinancialYearId = financialYearId;
                        advanceEntry.CreatedBy = userid;
                        //advanceEntry.IsPosted = false;

                        advanceEntry.CreatedDate = DateTime.Now.Date;
                        objOperation = _advanceListService.Save(advanceEntry);
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
                        advanceEntry.SecCompanyId = companyId;
                        advanceEntry.ModifiedBy = userid;
                        advanceEntry.ModifiedDate = DateTime.Now.Date;
                        objOperation = _advanceListService.Update(advanceEntry);
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

        public ActionResult SaveAdjustment(AnFAdjustment adjustmentEntry)
        {

            int companyId = Convert.ToInt32(Session["companyId"]);
            int userid = Convert.ToInt32(Session["userId"]);
            int financialYearId = Convert.ToInt32(Session["financialYear"]);
     
            Operation objOperation = new Operation();
            if (ModelState.IsValid)
            {
                if (adjustmentEntry.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        adjustmentEntry.SecCompanyId = companyId;
                        adjustmentEntry.CmnFinancialYearId = financialYearId;
                        adjustmentEntry.CreatedBy = userid;
                        adjustmentEntry.CreatedDate = DateTime.Now.Date;
                        objOperation = _advancetAdjustmentService.Save(adjustmentEntry);
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
                        adjustmentEntry.SecCompanyId = companyId;
                        adjustmentEntry.CmnFinancialYearId = financialYearId;
                        adjustmentEntry.ModifiedBy = userid;
                        adjustmentEntry.ModifiedDate = DateTime.Now.Date;
                        objOperation = _advancetAdjustmentService.Update(adjustmentEntry);
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

        //---------------------Delete Advance---------------------------
        public ActionResult Delete(int Id)
        {
            Operation objOperation = new Operation { Success = false };
            if (Id != 0)
            {
                AnFAdvance obj = _advanceListService.GetById(Id);
                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _advanceListService.Delete(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }//End delete

        //Delete Method
        [HttpPost]
        public ActionResult DeleteAdjustment(int Id)
        {
            Operation objOperation = new Operation { Success = false };
            if (Id != 0)
            {
                AnFAdjustment obj = _advancetAdjustmentService.GetById(Id);
                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _advancetAdjustmentService.Delete(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

     

        
        [HttpPost]
        //public ActionResult SaveCostCenter(AnFCostCenter anFCostCenter)
        //{
        //    Operation objOperation = new Operation { Success = false };

        //    if (ModelState.IsValid)
        //    {
        //        if (anFCostCenter.Id == 0)
        //        {
        //            if ((bool)Session["Add"])
        //            {
        //                anFCostCenter.CmnCompanyId = Convert.ToInt32(Session["companyId"].ToString());
        //                objOperation = _ccService.SaveAnFCostCenter(anFCostCenter);
        //            }
        //            else { objOperation.OperationId = -1; }
        //        }
        //        else
        //        {
        //            if ((bool)Session["Edit"])
        //            {
        //                anFCostCenter.CmnCompanyId = Convert.ToInt32(Session["companyId"].ToString());
        //                objOperation = _ccService.UpdateAnFCostCenter(anFCostCenter);
        //            }
        //            else { objOperation.OperationId = -2; }
        //        }
        //    }

        //    return Json(objOperation, JsonRequestBehavior.DenyGet);
        //}
        ////[HttpPost]
        //public ActionResult DeleteAnFCostCenter(int Id)
        //{
        //    Operation objOperation = new Operation { Success = false };

        //    if (Id != 0)
        //    {
        //        AnFCostCenter obj = _ccService.GetById(Id);

        //        if (obj == null)
        //        {
        //            objOperation.Success = false;
        //            return Json(objOperation, JsonRequestBehavior.DenyGet);
        //        }
        //        objOperation = _ccService.DeleteAnFCostCenter(obj);
        //    }
        //    return Json(objOperation, JsonRequestBehavior.DenyGet);
        //}
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult AdvanceList()
        {
            return View();
        }

        public ActionResult GetAllAdvanceList(int employeeId, DateTime? startDate, DateTime? endDate)
        {
            int companyid = int.Parse(Session["companyId"].ToString());
            var list = _advanceListService.GetAdvanceList(employeeId,companyid, startDate, endDate).ToList();

            var Relist = list.Select(p=>new
            {
                Id= p.Id,
                RefNo=p.RefNo,
                hrmEmployeeId=p.HrmEmployeeId,
                EmpName=p.HrmEmployee.Name,
                Date=p.Date,
                cmnYearId=p.CmnFinancialYearId,
                anfCostCenter=p.AnFCostCenterId,
                fy=p.CmnFinancialYear.Name,
                cc=p.AnFCostCenter.Name

            });
            return Json(Relist, JsonRequestBehavior.AllowGet);
        }

        //for Adjustment List view
        //[HttpGet]
        //public ActionResult GetAdvanceRef()
        //{
        //    int companyId = Convert.ToInt32(Session["companyId"]);
        //    //Convert.ToInt32(Session["companyId"]);
        //    var list = _ccService.GetCostCenters(companyId).ToList();//.Select(ac => new
        //    //{
        //    //    Id = ac.Id,
        //    //    Name = ac.Name,
        //    //    Code = ac.Code,
        //    //    Location = ac.Location
        //    //});

        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}

        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult AdjustmentList()
        {
            return View();
        }

        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult AdjustmentEntry()
        {
            return View();
        }

        //Add by Bably  for AdvanceAdjustment Report
        public ActionResult GetRefNoByEmpId(int employeeID)
        {
            IList<AnFAdvance> list = new List<AnFAdvance>();
            //IList<AnfAdjustmentsViewModel> newlist = new List<AnfAdjustmentsViewModel>();

            list = _advancetAdjustmentService.GetRefNoByEmpId(employeeID);
             var  newlist = list.Select(t => new AnFAdvance
                {
                    Id = t.Id,
                    RefNo = t.RefNo
                }).ToList();

             return Json(newlist, JsonRequestBehavior.AllowGet);

        }


    }
}
