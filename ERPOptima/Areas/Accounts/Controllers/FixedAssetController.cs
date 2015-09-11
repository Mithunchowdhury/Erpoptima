using ERPOptima.Data.Common.Repository;
using ERPOptima.Data.Accounts.Repository;
using ERPOptima.Data.Hrm.Repository;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Data.Infrastructure;
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
using Optima.Areas.Sales.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Globalization;
using ERPOptima.Helper;
using System.Web.Hosting;

namespace Optima.Areas.Accounts.Controllers
{
    public class FixedAssetController : Controller
    {
        //
        // GET: /Accounts/FixedAsset/

        private IAnFFixedAssetService _AnFFixedAssetService;
        private IAnFFixedAssetRevalueService _AnFFixedAssetRevalueService;
        private IOfficeService _officeService;
        private IHrmEmployeeService _hrmEmployeeService;
        private ISecCompanyService _SecCompanyService;
        public FixedAssetController()
        {
            var dbfactory = new DatabaseFactory();
            _SecCompanyService = new SecCompanyService(new SecCompanyRepository(dbfactory), new UnitOfWork(dbfactory));
            _officeService = new OfficeService(new OfficeRepository(dbfactory), new UnitOfWork(dbfactory));
            _hrmEmployeeService = new HrmEmployeeService(new HrmEmployeeRepository(dbfactory), new UnitOfWork(dbfactory));
            _AnFFixedAssetService = new AnFFixedAssetService(new AnFFixedAssetRepository(dbfactory), new AnFFixedAcquisitionRepository(dbfactory), new AnFFixedAssetRevalueRepository(dbfactory), new UnitOfWork(dbfactory));
            _AnFFixedAssetRevalueService = new AnFFixedAssetRevalueService(new AnFFixedAssetRevalueRepository(dbfactory), new UnitOfWork(dbfactory));
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Index()
        {
            return View();
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult FixedAssetEntry()
        {
            return View();
        }

        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult RateEntry()
        {
            return View();
        }

        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult DepreciationProcess()
        {
            return View();
        }

        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Disposal()
        {
            return View();
        }

        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Revaluation()
        {
            return View();
        }

        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult WriteOff()
        {
            return View();
        }

        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult FixedAssetsRegister()
        {
            return View();
        }

        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult FixedAssetsScheduleReport()
        {
            return View();
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult DepreciationRate()
        {
            return View();
        }

        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult FixedAssetsRevaluation()
        {
            return View();
        }

        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult DepreciationReport()
        {
            return View();
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult RevaluationReport()
        {
            return View();
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult DisposalReport()
        {
            return View();
        }

        //auto generate code for RefNo
        public string GetCode(int companyId, int employeeId)
        {
            companyId = Convert.ToInt32(Session["companyId"]);
            employeeId = Convert.ToInt32(Session["userId"]);
            SecCompany objCmnCompany = _SecCompanyService.GetById(companyId);
            SlsOffice office = _officeService.GetById((int)_hrmEmployeeService.GetById(employeeId).SlsOfficeId);
            string code = _AnFFixedAssetService.GetLastCode(companyId, objCmnCompany.Prefix, office.Code);
            return code;

        }

        [HttpGet]
        public ActionResult GetAll()
        {
            int companyId = Convert.ToInt32(Session["companyId"]);
            var list = _AnFFixedAssetService.GetAll(companyId).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetFxdNAcquisition()
        {
            int companyId = Convert.ToInt32(Session["companyId"]);
            
            IList<FxdAcquisition> list = new List<FxdAcquisition>();

            list = _AnFFixedAssetRevalueService.GetFxdNAcquisition(companyId).ToList();
            var Relist = list.Select(p => new
            {
                Id = p.Id,
                //AssetName = p.FxdAsset.Name,
                //AcquisitionName = p.Name,
                //model = p.ModelOrBatchNo,
                //code = p.Code,
                Name = p.FxdAsset.Name + "-" + p.Name + "-" + p.ModelOrBatchNo + "-" + p.Code,
                Cost = p.AcquisitionCost

            }).ToList();
            
            return Json(Relist, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetAllAcquisition()
        {
            int companyId = Convert.ToInt32(Session["companyId"]);
            //var list = _AnFFixedAssetService.GetAllAcquisition(companyId).ToList();
            //return Json(list, JsonRequestBehavior.AllowGet);
            IList<AnFFixedAssetViewModel> acquisitions = null;
            DataTable dt = _AnFFixedAssetService.GetAllAcquisition(companyId);
            if (dt != null)
            {
                acquisitions = new List<AnFFixedAssetViewModel>();
                foreach (DataRow row in dt.Rows)
                {
                    acquisitions.Add((AnFFixedAssetViewModel)ERPOptima.Lib.Utilities.Helper.FillTo(row, typeof(AnFFixedAssetViewModel)));
                }
            }
            acquisitions = acquisitions.OrderBy(t => t.Id).ToList();
            return Json(acquisitions, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SaveAcquisition(FxdAcquisition fixedAssetEntry)
        {

            int companyId = Convert.ToInt32(Session["companyId"]);
            int userid = Convert.ToInt32(Session["userId"]);
            int financialYearId = Convert.ToInt32(Session["financialYear"]);
            decimal cost = fixedAssetEntry.AcquisitionCost;
            int quantity = fixedAssetEntry.Quantity;


            Operation objOperation = new Operation();
            if (ModelState.IsValid)
            {
                if (fixedAssetEntry.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        fixedAssetEntry.SecCompanyId = companyId;
                        fixedAssetEntry.CmnFinancialYearId = financialYearId;
                        fixedAssetEntry.TotalAcquisitionCost = cost * quantity;
                        //fixedAssetEntry.AnfVoucherId = 1;
                        fixedAssetEntry.CreatedBy = userid;
                        fixedAssetEntry.CreatedDate = DateTime.Now.Date;
                        objOperation = _AnFFixedAssetService.SaveAcquisition(fixedAssetEntry);                       
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
                        fixedAssetEntry.SecCompanyId = companyId;
                        fixedAssetEntry.CmnFinancialYearId = financialYearId;
                        fixedAssetEntry.TotalAcquisitionCost = cost * quantity;
                        //fixedAssetEntry.AnfVoucherId = 1;
                        fixedAssetEntry.ModifiedBy = userid;
                        fixedAssetEntry.ModifiedDate = DateTime.Now.Date;
                        //objOperation = _AnFFixedAssetService.UpdateAcquisition(fixedAssetEntry);
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
        //fixed asset revaluation start
        //*****************************
        //fixed asset revaluation start
        [HttpPost]
        public ActionResult RevaluationList()
        {
            IList<FxdRevaluation> list = new List<FxdRevaluation>();
            IList<FxdRevaluationViewModel> newlist = new List<FxdRevaluationViewModel>();

            list = _AnFFixedAssetRevalueService.RevaluationList();
            newlist = list.Select(t => new FxdRevaluationViewModel
            {
                Id = t.Id,
                RefNo = t.RefNo,
                Date = t.Date,
                FxdAcquisitionId = t.FxdAcquisitionId,
                Presentvalue = t.Presentvalue,
                Revalue = t.Revalue,
                AnfVoucherId = t.AnfVoucherId,
                CmnFinancialYearId = t.CmnFinancialYearId,
                SecCompanyId = t.SecCompanyId,
                Remarks = t.Remarks,
                AssetName = t.FxdAcquisition.FxdAsset.Name
            }).ToList();

            return Json(newlist, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult SaveFxdRevalue(FxdRevaluation FxdRevaluationEntry)
        {
            decimal Revaluenew = 0;
            decimal PresentValue = FxdRevaluationEntry.Presentvalue;
            int AcquitionValue=0;
            var Acquisitionval = new Object();
            int companyId = Convert.ToInt32(Session["companyId"]);
            int userid = Convert.ToInt32(Session["userId"]);
            int financialYearId = Convert.ToInt32(Session["financialYear"]);
            //int Acquisitionvalue = FxdRevaluationEntry.Acquisitionvalue;
            //int pvalue = FxdRevaluationEntry.Presentvalue;

            Acquisitionval = _AnFFixedAssetRevalueService.GetByIdAcquisition(FxdRevaluationEntry.FxdAcquisitionId);

            //AcquitionValue = Acquisitionval.AcquisitionCost;
            //AcquitionValue = Acquisitionval.n
            Revaluenew = PresentValue - AcquitionValue;

            Operation objOperation = new Operation();
            if (ModelState.IsValid)
            {
                if (FxdRevaluationEntry.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        FxdRevaluationEntry.SecCompanyId = companyId;
                        FxdRevaluationEntry.CmnFinancialYearId = financialYearId;
                        FxdRevaluationEntry.Revalue = Revaluenew;
                        //fixedAssetEntry.TotalAcquisitionCost = cost * quantity;
                        //fixedAssetEntry.AnfVoucherId = 1;
                        FxdRevaluationEntry.CreatedBy = userid;
                        FxdRevaluationEntry.CreatedDate = DateTime.Now.Date;
                        objOperation = _AnFFixedAssetRevalueService.SaveFxdRevalue(FxdRevaluationEntry);
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
                        FxdRevaluationEntry.SecCompanyId = companyId;
                        FxdRevaluationEntry.CmnFinancialYearId = financialYearId;
                        //fixedAssetEntry.TotalAcquisitionCost = cost * quantity;
                        //fixedAssetEntry.AnfVoucherId = 1;
                        FxdRevaluationEntry.ModifiedBy = userid;
                        FxdRevaluationEntry.ModifiedDate = DateTime.Now.Date;
                        //objOperation = _AnFFixedAssetRevalueService.UpdateAcquisition(fixedAssetEntry);
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
        public ActionResult DeleteRevaluation(int Id)
        {
            Operation objOperation = new Operation { Success = false };
            if (Id != 0)
            {
                FxdRevaluation obj = _AnFFixedAssetRevalueService.GetById(Id);
                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _AnFFixedAssetRevalueService.Delete(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }//End delete
        //fixed asset revaluation end
    }
}
