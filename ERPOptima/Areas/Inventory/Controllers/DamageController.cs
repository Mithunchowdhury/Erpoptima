using ERPOptima.Data.Common.Repository;
using ERPOptima.Data.Hrm.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Inventory.Repository;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Inventory;
using ERPOptima.Model.Security;
using ERPOptima.Service.Hrm;
using ERPOptima.Service.Inventory;
using ERPOptima.Service.Sales;
using ERPOptima.Service.Security;
using ERPOptima.Web.Filters;
using Optima.Areas.Common.Controllers;
using Optima.Areas.Inventory.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Optima.Areas.Inventory.Controllers
{
    public class DamageController : Controller
    {
        private IDamageService<InvDamageApproval, InvDamage> _DamageService;
        private ApprovalController<InvDamageApproval, InvDamage> _approvalController;
        private IDamageDetailService _DamageDetailService; 
        private ISecCompanyService _SecCompanyService;
       

        int userId = 0;
        int companyId = 0;

        public DamageController()
        {
            var dbfactory = new DatabaseFactory();
            _DamageService = new DamageService(new InvDamageRepository(dbfactory), 
                new NotificationRepository(dbfactory),
                new NotificationDetailRepository(dbfactory), 
                new InvDamageApprovalRepository(dbfactory), new UnitOfWork(dbfactory));
            _approvalController = new ApprovalController<InvDamageApproval, InvDamage>(_DamageService);

            _SecCompanyService = new SecCompanyService(new SecCompanyRepository(dbfactory), new UnitOfWork(dbfactory));
            _DamageDetailService = new DamageDetailService(new InvDamageDetailRepository(dbfactory), new UnitOfWork(dbfactory));
          
            //approval controller - to control all approvals
        }

        //
        // GET: /Inventory/Damage/
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Index()
        {
            return View();
        }

        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Approval()
        {
            int? damId = null;
            if (TempData["DamageNumber"] != null)
            {
                damId = (int)TempData["DamageNumber"];
                ViewData["DamageNumber"] = damId;
            }

            return View();
        }
        public ActionResult ApprovalById(int DamageNumber)
        {
            TempData["DamageNumber"] = DamageNumber;
            return RedirectToAction("Approval");
        }
        public string GetCode(int companyId)
        {
            SecCompany objCmnCompany = _SecCompanyService.GetById(companyId);
            string RefNo = "";// _DamageService.GetLastCode(companyId, objCmnCompany.Prefix);
            return RefNo; 

        }
        public ActionResult GetByDamageId(int damageId)
        {
            var list = _DamageDetailService.GetByDamageId(damageId).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //[AuthorizeUser(Permission = PermissionEnum.Add, ResourceTag = "RequisitionIndex")]
        public ActionResult Save(InvDamage objInvDamage)
        {
            Operation objOperation = new Operation();
            userId = Convert.ToInt32(Session["userId"]);
            companyId = Convert.ToInt32(Session["companyId"]);
            if (ModelState.IsValid)
            {
              
                    objInvDamage.SecCompanyId = companyId;
                   objInvDamage.CreatedBy = userId;
                   objInvDamage.CreatedDate = DateTime.Now;
                   objInvDamage.ModifiedBy = userId;
                   objInvDamage.ModifiedDate = DateTime.Now;

                if (objInvDamage.Id == 0)
                {
                    objOperation = _DamageService.Save(objInvDamage);
                }
                else
                {
                    objOperation = _DamageService.Update(objInvDamage);
                }
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }


        public ActionResult SaveDetail(List<DamageDetail> DamageDetail)
        {
            Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid && DamageDetail != null)
            {
                int Id = _DamageDetailService.GetLastId();
                foreach (var item in DamageDetail)
                {
                    InvDamageDetail objInvDamageDetail = _DamageDetailService.GetById(item.Id);
                    if (objInvDamageDetail != null)
                    {
                        objInvDamageDetail.SlsProductId = item.SlsProductId;
                        objInvDamageDetail.Quantity = item.Quantity;
                        objInvDamageDetail.SlsUnitsId = item.SlsUnitsId;
                        _DamageDetailService.Update(objInvDamageDetail);
                    }
                    else
                    {
                        objInvDamageDetail = new InvDamageDetail();
                        objInvDamageDetail.Id = Id;
                        objInvDamageDetail.SlsProductId = item.SlsProductId;
                        objInvDamageDetail.Quantity = item.Quantity;
                        objInvDamageDetail.SlsUnitsId = item.SlsUnitsId;
                        _DamageDetailService.Add(objInvDamageDetail);
                        Id = Id + 1;
                    }

                }

                objOperation = _DamageDetailService.Commit();
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        #region Approval
        [HttpGet]
        public ActionResult GetAllForApproval()
        {
            int userId = Convert.ToInt32(Session["userId"]);
            var list = _approvalController.GetAllForApproval(userId);
            list = list.OrderByDescending(i => i.CreatedDate).ToList(); // order by created date
            var result = list.Select(i => new
            {
                Id = i.Id, RefNo = i.RefNo, SecCompanyId = i.SecCompanyId, InvStoreId = i.InvStoreId, Status = i.Status, ApprovalStatus = i.ApprovalStatus,
                CreatedBy = i.CreatedBy, CreatedDate = i.CreatedDate, ModifiedBy = i.ModifiedBy, ModifiedDate = i.ModifiedDate
            }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetAllForApprovalByDate(DateTime fromDate, DateTime toDate)
        {
            int userId = Convert.ToInt32(Session["userId"]);
            var list = _approvalController.GetAllForApprovalByDate(userId, fromDate, toDate);
            var result = list.Select(i => new
            {
                Id = i.Id,
                RefNo = i.RefNo,
                SecCompanyId = i.SecCompanyId,
                InvStoreId = i.InvStoreId,
                Status = i.Status,
                ApprovalStatus = i.ApprovalStatus,
                CreatedBy = i.CreatedBy,
                CreatedDate = i.CreatedDate,
                ModifiedBy = i.ModifiedBy,
                ModifiedDate = i.ModifiedDate
            }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetApprovalById(int appId)
        {
            var obj = _approvalController.GetApprovalById(appId);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Approve(InvDamageApproval obj, string newComment)
        {
            //According to DB column description: 1=New,2=Approve,3=Pass,4=Reject
            return UpdateToApproval(obj, 2, newComment);
        }

        [HttpPost]
        public ActionResult Reject(InvDamageApproval obj, string newComment)
        {
            //According to DB column description: 1=New,2=Approve,3=Pass,4=Reject
            return UpdateToApproval(obj, 4, newComment);
        }
        [HttpPost]
        public ActionResult Pass(InvDamageApproval obj, string newComment)
        {
            //According to DB column description: 1=New,2=Approve,3=Pass,4=Reject
            return UpdateToApproval(obj, 3, newComment);
        }
        public ActionResult UpdateToApproval(InvDamageApproval obj, int action, string newComment)
        {
            int userId = Convert.ToInt32(Session["userId"]);
            Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid)
            {
                if (obj.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        //Sales Order Approval is created internally during sales order creation.
                        objOperation.Success = false;
                    }
                    else { objOperation.OperationId = -1; }

                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        //According to DB column description: 1=New,2=Approve,3=Pass,4=Reject
                        obj.Action = action;
                        obj.ModifiedBy = userId;
                        obj.ModifiedDate = DateTime.Now.Date;
                        objOperation = _approvalController.UpdateApproval(obj, newComment);
                    }
                    else { objOperation.OperationId = -2; }
                }
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }
        
        #endregion
    }
}
