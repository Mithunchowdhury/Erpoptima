using ERPOptima.Data.Accounts.Repository;
using ERPOptima.Data.Common.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Lib.Model;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Accounts;
using ERPOptima.Model.Common;
using ERPOptima.Service.Accounts;
using ERPOptima.Service.Common;
using ERPOptima.Web.Accounts.ViewModel;
using ERPOptima.Web.Filters;
using Optima.Areas.Accounts.ViewModel;
using Optima.Areas.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using System.Web.Routing;
using System.Linq;
using ERPOptima.Service.Hrm;
using ERPOptima.Data.Hrm.Repository;

namespace Optima.Areas.Accounts.Controllers
{
    [Authorize]
    public class VoucherController : Controller
    {
        //
        // GET: /Accounts/Voucher/

        private IAnFVoucherService _anfVoucherService;
        private IAnFVoucherDetailsService _anfVoucherDetailsService;
        private ICmnApprovalProcessService _cmnApprovalProcessService;
        private ICmnApprovalService _cmnApprovalService;
        private IVoucherService _VoucherService;
        private IHrmEmployeeService _HrmEmployeeService;

        int companyId;
        int financialYearId;
        int userId;
        int moduleId;
        int officeId;

        [AuthorizeUser]
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            companyId = Convert.ToInt32(Session["companyId"].ToString());
            financialYearId = Convert.ToInt32(Session["financialYear"].ToString());
            userId = Convert.ToInt32(Session["userId"].ToString());
            moduleId = Convert.ToInt32(Session["moduleId"].ToString());
            //Need to get office id from session
            //officeId = Convert.ToInt32(Session["officeId"].ToString());
            officeId = _HrmEmployeeService.GetEmployeeOfficeId(userId);
        }

        public VoucherController()
        {
            var dbfactory = new DatabaseFactory();
            _anfVoucherService = new AnFVoucherService(new AnFVoucherRepository(dbfactory), new UnitOfWork(dbfactory));

            _anfVoucherDetailsService = new AnFVoucherDetailsService(new AnFVoucherDetailsRepository(dbfactory), new UnitOfWork(dbfactory));

            _cmnApprovalProcessService = new CmnApprovalProcessService(new CmnApprovalProcessRepository(dbfactory), new UnitOfWork(dbfactory));

            _cmnApprovalService = new CmnApprovalService(new CmnApprovalRepository(dbfactory), new CmnProcessLevelRepository(dbfactory), new UnitOfWork(dbfactory));
            _VoucherService = new VoucherService(new VoucherRepository(dbfactory), new VoucherDetailRepository(dbfactory), new UnitOfWork(dbfactory));
            _HrmEmployeeService = new HrmEmployeeService(new HrmEmployeeRepository(dbfactory), new UnitOfWork(dbfactory));
        }

        [AuthorizeUser]

        public ActionResult Index()
        {
            return View();
        }
        [AuthorizeUser]

        public ActionResult CashPayment()
        {
            return View();
        }
        [AuthorizeUser]
        public ActionResult JournalVoucher()
        {
            return View();
        }
        [AuthorizeUser]
        public ActionResult BankReceive()
        {
            return View();
        }

        [AuthorizeUser]
        public ActionResult ContraVoucher()
        {
            return View();
        }


        [AuthorizeUser]
        public ActionResult VoucherSearch()
        {
            return View();
        }

        [AuthorizeUser]
        public ActionResult BankPayment()
        {
            return View();
        }

        [AuthorizeUser]
        public ActionResult CashReceive()
        {
            return View();
        }


        public ActionResult GetAnfTransactionalHeadsByCompanyId()
        {
            DataTable dt = null;
            List<PaymentVoucherAccountNameAndCode> list = new List<PaymentVoucherAccountNameAndCode>();
            try
            {
                dt = _anfVoucherService.GetTranasactionalHeadsByCompanyId(companyId);
                list = dt.DataTableToList<PaymentVoucherAccountNameAndCode>();
            }
            catch (Exception)
            {
                throw new NullReferenceException();
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }


        public string GetVoucherNo(int ParamCmnCompanyId, int ParamType, string ParamTypeCode, int ParamSlsOfficeId, int ParamCmnFinancialYearId, DateTime ParamDate)
        {
            string voucherNo = _VoucherService.GenerateVoucherNo(ParamCmnCompanyId, ParamType, ParamTypeCode, ParamSlsOfficeId, ParamCmnFinancialYearId, ParamDate);

            return voucherNo;
        }


        public ActionResult SearchByItems(VoucherSearchForGridViewModel voucher)
        {


            DataTable dt = _anfVoucherService.GetByParameters(companyId, financialYearId, voucher.VoucherTypeId, voucher.ProjectId, voucher.DateFrom, voucher.ToDate, true);

            List<VoucherSearchResultViewModel> list = dt.DataTableToList<VoucherSearchResultViewModel>();
            foreach (VoucherSearchResultViewModel item in list)
            {
                item.DateString = item.Date.ToShortDateString();
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /*
          Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid)
            {
                
                if (anFCostCenter.Id == 0)
                {
                    anFCostCenter.CmnCompanyId = Convert.ToInt32(Session["companyId"].ToString());
                    objOperation = _ccService.SaveAnFCostCenter(anFCostCenter);
                }
                else
                {
                    anFCostCenter.CmnCompanyId = Convert.ToInt32(Session["companyId"].ToString());
                    objOperation = _ccService.UpdateAnFCostCenter(anFCostCenter);
                }
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
         */
        [HttpPost]
        public ActionResult InsertVoucher(VoucherViewModel voucher)
        {
            //Operation objOperation = new Operation { Success = false };
            //int ret = 0;

            //voucher.CmnCompanyId = companyId;
            //voucher.CmnFinancialYearId = financialYearId;
            //AnFVoucher v = new AnFVoucher();
            //v.Id = voucher.Id;
            //v.Type = voucher.Type;

            //v.VoucherNumber = voucher.VoucherNumber;
            //if (voucher.Id <= 0)
            //{
            //    v.VoucherNumber = GetVoucherNo(new VoucherDateAndType() { Date = voucher.Date, CmnBusinessId = voucher.CmnBusinessId, Type = voucher.Type, Prefix = Enum.GetName(typeof(AnFVoucherTypes), voucher.Type) });
            //}
            //v.Date = voucher.Date;
            //v.Naration = voucher.Naration;
            //v.TotalAmount = voucher.TotalAmount;
            //v.CmnCompanyId = voucher.CmnCompanyId;
            //v.CmnFinancialYearId = voucher.CmnFinancialYearId;
            //v.Status = true;
            //v.IsPosted = false;
            //v.IsCancel = false;
            //v.CreatedBy = userId;
            //v.CreatedDate = DateTime.Now;
            ////if (ModelState.IsValid)
            ////{
            //if (voucher.Id <= 0)
            //{
            //    ret = _anfVoucherService.SaveVoucher(v);
            //}
            //else
            //{
            //    ret = _anfVoucherService.UpdateVoucher(v);
            //}

            //if (ret != 0)
            //{
            //    _anfVoucherDetailsService.DeleteDetailsByVoucherId(ret);

            //    long voucherdetailsId = _anfVoucherDetailsService.GetLastId();
            //    if (voucher.VouchrerDetails != null)
            //    {
            //        foreach (VoucherDetailsViewModel details in voucher.VouchrerDetails)
            //        {
            //            AnFVoucherDetail det = new AnFVoucherDetail();
            //            det.Id = voucherdetailsId;
            //            det.AnFVoucherId = ret;
            //            det.AnFChartOfAccountId = details.AnFChartOfAccountId;
            //            //det.AnFCostCenterId = details.AnFCostCenterId;
            //            //det.CmnProjectId = details.CmnProjectId;

            //            det.Debit = details.Debit;
            //            det.Credit = details.Credit;
            //            det.ShortNarration = details.ShortNaration;
            //            det.VoucherSerial = details.VoucherSerial;
            //            if (voucher.Id == 0)
            //            {
            //                det.SubVoucherNumber = v.VoucherNumber + "-" + details.VoucherSerial;
            //            }
            //            else { det.SubVoucherNumber = details.SubVoucherNumber; }

            //            _anfVoucherDetailsService.InsertVoucherDetails(det);
            //            voucherdetailsId = voucherdetailsId + 1;
            //        }
            //    }

            //    if (voucher.Id <= 0 && voucherdetailsId > 0)
            //    {
            //        string prefix = v.VoucherNumber.Split('-')[0];
            //        CmnApprovalProcess process = _cmnApprovalProcessService.GetByShortName(prefix, moduleId);

            //        _cmnApprovalService.AutoGenerate(companyId, (int)process.Id, userId, ret);
            //    }

            //    _anfVoucherDetailsService.Commit();
            //    objOperation.Success = true; objOperation.OperationId = v.VoucherNumber;

            //}
            ////}
            ////return ret;
            //return Json(objOperation, JsonRequestBehavior.DenyGet);
            return Json(new { }, JsonRequestBehavior.DenyGet);
        }



        [HttpPost]
        public ActionResult GetVoucherDetailsbyVoucherId(VoucherIdViewModel v)
        {
            DataTable dt = _anfVoucherDetailsService.GetVoucherDetailsbyVoucherId(v.id);

            List<VoucherSearchResultById> list = dt.DataTableToList<VoucherSearchResultById>();

            return Json(list, JsonRequestBehavior.AllowGet);

        }

        //For Voucher Search

        public ActionResult AnFVoucherSearch(DateTime? dateFrom, DateTime? toDate, int? VoucherTypeId, int? CostcenterId)
        {
            IList<AnFVoucher> list = new List<AnFVoucher>();

            list = _anfVoucherService.SearchVoucher(companyId, financialYearId, dateFrom, toDate, VoucherTypeId, CostcenterId);
            var newList = list.Select(t => new
            {
                Id = t.Id,
                CmnCompanyId = t.CmnCompanyId,
                Type = t.Type,
                VoucherNumber = t.VoucherNumber,
                Date = t.Date,
                CmnFinancialYearId = t.CmnFinancialYearId,
                TotalAmount = t.TotalAmount,
                Naration = t.Naration,
                Status = t.Status,
                IsPosted = t.IsPosted,
                PostedBy = t.PostedBy,
                PostedDate = t.PostedDate,
                IsCancel = t.IsCancel,
                CancelledBy = t.CancelledBy,
                CancelledDate = t.CancelledDate,
                CancelReason = t.CancelReason,
                CreatedBy = t.CreatedBy,
                CreatedDate = t.CreatedDate,
                ModifiedBy = t.ModifiedBy,
                ModifiedDate = t.ModifiedDate,
                DateStr = t.Date.ToString("dd-MM-yyyy"),
                TypeName = t.Type == 1 ? "CR" : t.Type == 2 ? "BR" : t.Type == 3 ? "CP" : t.Type == 4 ? "BP" : t.Type == 5 ? "JV" : "CT"
            }).ToList();

            return Json(newList, JsonRequestBehavior.AllowGet);

        }

        #region Voucher - code added by hasan
        //[AuthorizeUser]
        //[ResourcePermissionAttribute]
        public ActionResult Manage()
        {
            string type = "";
            if (TempData["type"] != null)
            {
                type = TempData["type"].ToString();
                ViewData["type"] = type;
            }
            ViewData["financialyear"] = Convert.ToInt32(financialYearId.ToString());
            ViewData["companyid"] = Convert.ToInt32(companyId.ToString());
            ViewData["officeid"] = Convert.ToInt32(officeId.ToString());
            return View();
        }
        public ActionResult CashRecieveVoucher()
        {
            TempData["type"] = "CR";
            return RedirectToAction("Manage");
        }
        public ActionResult BankRecieveVoucher()
        {
            TempData["type"] = "BR";
            return RedirectToAction("Manage");
        }
        public ActionResult CashPaymentVoucher()
        {
            TempData["type"] = "CP";
            return RedirectToAction("Manage");
        }
        public ActionResult BankPaymentVoucher()
        {
            TempData["type"] = "BP";
            return RedirectToAction("Manage");
        }
        public ActionResult Journal()
        {
            TempData["type"] = "JV";
            return RedirectToAction("Manage");
        }
        public ActionResult Contra()
        {
            TempData["type"] = "CT";
            return RedirectToAction("Manage");
        }

        [HttpPost]
        public ActionResult SaveVoucher(AnFVoucher obj)
        {
            Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid)
            {
                //obj.Id = 332;
                //obj.VoucherNumber = "MLP-JV-001-2/06-14";
                if (obj.Id == 0)
                {
                    //if ((bool)Session["Add"])
                    //{
                    int officeId = obj.SlsOfficeId != null ? (int)obj.SlsOfficeId : 0;
                    string typeStr = Enum.GetName(typeof(VoucherType), obj.Type);
                    obj.VoucherNumber = GetVoucherNo(obj.CmnCompanyId, obj.Type, typeStr, officeId, obj.CmnFinancialYearId, obj.Date);
                    foreach (var detail in obj.AnFVoucherDetails)
                    {
                        detail.SubVoucherNumber = obj.VoucherNumber + "-" + detail.VoucherSerial;
                    }
                    obj.IsPosted = false;
                    obj.PostedBy = userId;
                    obj.PostedDate = DateTime.Now.Date;
                    obj.CreatedBy = userId;
                    obj.CreatedDate = DateTime.Now.Date;

                    objOperation = _VoucherService.Save(obj);
                    //}
                    //else { objOperation.OperationId = -1; }

                }
                else
                {
                    //if ((bool)Session["Edit"])
                    //{
                    obj.ModifiedBy = userId;
                    obj.ModifiedDate = DateTime.Now.Date;
                    foreach (var detail in obj.AnFVoucherDetails)
                    {
                        detail.SubVoucherNumber = obj.VoucherNumber + "-" + detail.VoucherSerial;
                    }

                    objOperation = _VoucherService.Save(obj);
                    //}
                    //else { objOperation.OperationId = -2; }
                }
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);

            //return Json(new { Success= true }, JsonRequestBehavior.DenyGet);

        }

        #endregion
    }
}