using ERPOptima.Data.Accounts.Repository;
using ERPOptima.Data.Common.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Security.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Accounts;
using ERPOptima.Model.Common;
using ERPOptima.Model.Security;
using ERPOptima.Service.Accounts;
using ERPOptima.Service.Common;
using ERPOptima.Service.Security;
using ERPOptima.Web.Accounts.ViewModel;
using ERPOptima.Web.Common.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPOptima.Lib.Utilities;
using Optima.Areas.Accounts.ViewModel;
using ERPOptima.Web.Filters;

namespace Optima.Areas.Accounts.Controllers
{
    public class VoucherApprovalController : Controller
    {
        //
        // GET: /Accounts/VoucherApproval/

        private IVoucherApprovalService _IVoucherApprovalService;
        private ICmnApprovalCommentService _ICmnApprovalCommentService;
        private ICmnApprovalService _ICmnApprovalService;
        private ISecUserService _su;
        private IAnFVoucherService _IAnFVoucherService;
        private ICmnApprovalProcessLevelService _cmnApprovalProcessLevelService;
        private IAnFVoucherDetailsService _anfVoucherDetailsService;

        public VoucherApprovalController()
        {
            var dbfactory = new DatabaseFactory();
            _IVoucherApprovalService = new VoucherApprovalService(new AnFVoucherApprovalRepository(dbfactory), new UnitOfWork(dbfactory));
            _su = new SecUserService(new SecUserRepository(dbfactory), new UnitOfWork(dbfactory));
            _ICmnApprovalCommentService = new CmnApprovalCommentService(new CmnApprovalCommentRepository(dbfactory), new UnitOfWork(dbfactory));
            _ICmnApprovalService = new CmnApprovalService(new CmnApprovalRepository(dbfactory), new UnitOfWork(dbfactory));
            _IAnFVoucherService = new AnFVoucherService(new AnFVoucherRepository(dbfactory), new UnitOfWork(dbfactory));
            _cmnApprovalProcessLevelService = new CmnApprovalProcessLevelService(new CmnApprovalProcessLevelRepository(dbfactory), new UnitOfWork(dbfactory));
            _anfVoucherDetailsService = new AnFVoucherDetailsService(new AnFVoucherDetailsRepository(dbfactory), new UnitOfWork(dbfactory));
        }

        [AuthorizeUser]

        public ActionResult Index()
        {
            return View();
        }
        [AuthorizeUser]

        public ActionResult Payment()
        {
            return View();
        }
        [AuthorizeUser]
        public ActionResult Receipt()
        {
            return View();
        }
        [AuthorizeUser]
        public ActionResult Journal()
        {
            return View();
        }


        #region Private Methods



        [HttpPost]
        public ActionResult BindVoucherList(VoucherSearchViewModel obj, int companyId, int financialYear)
        {
            int processId = 0;
            int levelId = 0;
            string urlData = Request.UrlReferrer.Query;
            string[] array = urlData.Split('=');
            if (array.Length > 0)
            {
                processId = Convert.ToInt32(array[1]);
                levelId = Convert.ToInt32(array[2]);
            }
            //var list = "";
            try
            {
                DataTable dt = _cmnApprovalProcessLevelService.GetByCompanyModuleApprovalProcessId(companyId, processId);
                var records = dt.DataTableToList<CmnApprovalProcessLevelMappingViewModel>();
                Collection<CmnProcessLevel> collection = new Collection<CmnProcessLevel>();
                foreach (CmnApprovalProcessLevelMappingViewModel objModel in records)
                {
                    CmnProcessLevel objLevel = new CmnProcessLevel();
                    objLevel.Id = objModel.ProcessLevelId;
                    objLevel.Name = objModel.Name;
                    collection.Add(objLevel);
                }

                Collection<AnFVoucher> list = _IAnFVoucherService.GetForApproval(companyId, financialYear, processId, levelId, obj.DateFrom, obj.ToDate, obj.BusinessId, obj.ProjectId, collection);
                Collection<VoucherSearchResultViewModel> collections = new Collection<VoucherSearchResultViewModel>();
                if (list != null)
                {
                    foreach (AnFVoucher objVoucher in list)
                    {
                        VoucherSearchResultViewModel objValue = new VoucherSearchResultViewModel();
                        objValue.id = objVoucher.Id;
                        objValue.VoucherNumber = objVoucher.VoucherNumber;
                        objValue.DateString = objVoucher.Date.ToString();
                        collections.Add(objValue);
                    }
                }
                return Json(collections, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw;
            }
            //Get the vouchers that are waiting for levels(check/pass/arrove)


        }

        public ActionResult GetVoucherDetailsByVoucherId(long id)
        {
            DataTable dt = _anfVoucherDetailsService.GetVoucherDetailsbyVoucherId(id);

            List<VoucherSearchResultById> list = dt.DataTableToList<VoucherSearchResultById>();

            return Json(list, JsonRequestBehavior.AllowGet);

        }

        private void ShowActionStatus(string msg, int status)
        {
            string caption = string.Empty;
            switch (status)
            {
                case 0:
                    caption = "Successful";
                    break;
                case 1:
                    caption = "Failed";
                    break;
            }

        }


        public ActionResult SaveApprovalComment(VoucherViewModel comment, int userId, int companyId)
        {
            int processId = 0;
            int levelId = 0;
            string urlData = Request.UrlReferrer.Query;
            string[] array = urlData.Split('=');
            if (array.Length > 0)
            {
                processId = Convert.ToInt32(array[1]);
                levelId = Convert.ToInt32(array[2]);
            }

            Operation objOperation = new Operation();
            Int64 ret = 0;

            CmnApproval apr = _ICmnApprovalService.GetApproval(comment.Id, processId, levelId);
            if (apr != null)
            {

                apr.Value = true;
                apr.DoneBy = userId;
                apr.DoneDateTime = DateTime.Now;
                //apr.CmnApprovalComments = comment;
                objOperation = _ICmnApprovalService.UpdatewithComment(apr);
                if (objOperation.Success)
                {
                    CmnApprovalComment obj = new CmnApprovalComment();
                    obj.CmnApprovalId = Convert.ToInt32(objOperation.OperationId);
                    obj.Comments = comment.Naration;
                    obj.Commentator = userId;
                    obj.CommentDate = DateTime.Now;
                    objOperation = _ICmnApprovalCommentService.SaveApprovalComment(obj);
                }
                int i = Convert.ToInt32(objOperation.OperationId);
                if (i > 0)
                {

                    //This means all is checked so make the PStatus=true
                    if (array[3] == "Approve")
                    {
                        AnFVoucher vr = _IAnFVoucherService.GetAnfVoucherById(comment.Id);
                        if (vr != null)
                        {
                            vr.IsPosted = true;
                            _IAnFVoucherService.UpdateVoucher(vr);
                        }
                    }
                }

            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);

        }

        public ActionResult Comments(VoucherViewModel comment, int userId, int companyId)
        {
            int processId = 0;
            int levelId = 0;
            string urlData = Request.UrlReferrer.Query;
            string[] array = urlData.Split('=');
            if (array.Length > 0)
            {
                processId = Convert.ToInt32(array[1]);
                levelId = Convert.ToInt32(array[2]);
            }
            Operation objOperation = new Operation();

            CmnApproval apr = _ICmnApprovalService.GetApproval(comment.Id, processId, levelId);
            if (apr != null)
            {
                CmnApprovalComment obj = new CmnApprovalComment();
                obj.CmnApprovalId = apr.Id;
                obj.Comments = comment.Naration;
                obj.Commentator = userId;
                obj.CommentDate = DateTime.Now;
                objOperation = _ICmnApprovalCommentService.SaveApprovalComment(obj);

            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }


        public ActionResult Reject(VoucherViewModel comment, int userId, int companyId)
        {

            int processId = 0;
            int levelId = 0;
            string urlData = Request.UrlReferrer.Query;
            string[] array = urlData.Split('=');
            if (array.Length > 0)
            {
                processId = Convert.ToInt32(array[1]);
                levelId = Convert.ToInt32(array[2]);
            }
            Operation objOperation = new Operation();

            CmnApproval apr = _ICmnApprovalService.GetApproval(comment.Id, processId, levelId);
            if (apr != null)
            {
                apr.Value = false;
                apr.DoneBy = userId;
                apr.DoneDateTime = DateTime.Now;
                //apr.Comment = comment.Naration;
                //Int64 i = 0;
                objOperation = _ICmnApprovalService.UpdatewithComment(apr);
                if (objOperation.Success)
                {
                    CmnApprovalComment obj = new CmnApprovalComment();
                    obj.CmnApprovalId = Convert.ToInt32(objOperation.OperationId);
                    obj.Comments = comment.Naration;
                    obj.Commentator = userId;
                    obj.CommentDate = DateTime.Now;
                    objOperation = _ICmnApprovalCommentService.SaveApprovalComment(obj);
                }

            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }


        [HttpGet]
        public ActionResult GetApprovalComment(long id)
        {
            int processId = 0;
            int levelId = 0;
            string urlData = Request.UrlReferrer.Query;
            string[] array = urlData.Split('=');
            if (array.Length > 0)
            {
                processId = Convert.ToInt32(array[1]);
                levelId = Convert.ToInt32(array[2]);
            }
            Collection<CmnApprovalComment> list = _ICmnApprovalCommentService.GetApprovalComments(processId, id);
            Collection<CmnApprovalComment> collection = new Collection<CmnApprovalComment>();

            foreach (CmnApprovalComment c in list)
            {
                SecUser secUser = _su.GetById((int)c.Commentator);
                c.Comments = c.Comments + " Done by " + secUser.LoginName + " On " + c.CommentDate.ToString();

                collection.Add(c);
            }
            return Json(collection, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region New Code added by Hasan
        [HttpGet]
        public ActionResult Approve(string type, string level)
        {
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}
