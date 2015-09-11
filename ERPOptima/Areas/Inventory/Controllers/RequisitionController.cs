using ERPOptima.Data.Common.Repository;
using ERPOptima.Data.Hrm.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Inventory.Repository;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Inventory;
using ERPOptima.Model.Sales;
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
    public class RequisitionController : Controller
    {

        private IRequisitionService<InvRequisitionApproval, InvRequisitionViewModel> _RequisitionService;
        private IRequisitionDetailService _RequisitionDetailService;
        private ISecCompanyService _SecCompanyService;
        private IOfficeService _officeService;
        private IHrmEmployeeService _hrmEmployeeService;
        private ApprovalController<InvRequisitionApproval, InvRequisitionViewModel> _approvalController;
        int userId = 0;


        #region ApprovalView

        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Approval()
        {
            int? reqId = null;
            if (TempData["RequisitionNumber"] != null)
            {
                reqId = (int)TempData["RequisitionNumber"];
                ViewData["RequisitionNumber"] = reqId;
            }

            return View();
        }
        #endregion

        public RequisitionController()
        {
            var dbfactory = new DatabaseFactory();
            _RequisitionService = new RequisitionService(new RequisitionRepository(dbfactory),
                new RequisitionApprovalRepository(dbfactory),
                new NotificationRepository(dbfactory), new NotificationDetailRepository(dbfactory),
                new UnitOfWork(dbfactory));
            _SecCompanyService = new SecCompanyService(new SecCompanyRepository(dbfactory), new UnitOfWork(dbfactory));
            _officeService = new OfficeService(new OfficeRepository(dbfactory), new UnitOfWork(dbfactory));
            _hrmEmployeeService = new HrmEmployeeService(new HrmEmployeeRepository(dbfactory), new UnitOfWork(dbfactory));
            _RequisitionDetailService = new RequisitionDetailService(new RequisitionDetailRepository(dbfactory), new UnitOfWork(dbfactory));
            //approval controller - to control all approvals
            _approvalController = new ApprovalController<InvRequisitionApproval, InvRequisitionViewModel>(_RequisitionService);
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult List()
        {
            return View();
        }
        //
        // GET: /Inventory/Requisition/

        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Index()
        {
            int? soId = null;
            if (TempData["RequisitionNumber"] != null)
            {
                soId = (int)TempData["RequisitionNumber"];
                ViewData["RequisitionNumber"] = soId;
            }

            return View();
        }
       

        [AllowAnonymous]
        public ActionResult Edit(int RequisitionNumber)
        {
            TempData["RequisitionNumber"] = RequisitionNumber;
            return RedirectToAction("Index");
        }

        #region Requisition
        public string GetCode(int companyId, int employeeId)
        {            
            SecCompany objCmnCompany = _SecCompanyService.GetById(companyId);
            SlsOffice office = _officeService.GetById((int)_hrmEmployeeService.GetById(employeeId).SlsOfficeId);
            string code = _RequisitionService.GetLastCode(companyId, objCmnCompany.Prefix, office.Code);                
            return code;

        }
        

        [HttpGet]
        public ActionResult Show(DateTime? StartDate = null, DateTime? EndDate = null, int? ApprovalStatus = 0)
        {
            var list = _RequisitionService.GetAll(int.Parse(Session["companyId"].ToString()));

                list = list.OrderByDescending(i => i.CreatedDate).ToList();  // order by createdDate


                list = list.Where(i => i.CreatedDate != null && (StartDate <= i.CreatedDate)).ToList();

            if (EndDate != null)
                list = list.Where(i => i.CreatedDate != null && (EndDate >= i.CreatedDate)).ToList();
            if (ApprovalStatus > 0)
                list = list.Where(i => i.ApprovalStatus != null && (ApprovalStatus == i.ApprovalStatus)).ToList();

            var retList = list.Select(p => new
            {
                Id = p.Id,
                RequisitionCode = p.RequisitionCode,
                PreferredDeliveryDate = p.PreferredDeliveryDate,
                Status = p.Status == 0 ? "New" : p.Status == 1 ? "Inprogress" : "Complated",
                ApprovalStatus = p.ApprovalStatus == 1 ? "New" : p.ApprovalStatus == 2 ? "Approve" : p.ApprovalStatus == 3 ? "Pass" : "Reject",
                CreatedDate = p.CreatedDate

            }).ToList();

            //list = list.Where(i => i.TransactionDate != null && (DateFrom <= i.TransactionDate.Value && DateTo >= i.TransactionDate.Value)).ToList();
            return Json(retList, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult GetAll(int companyId = 1)
        {

            var list = _RequisitionService.GetAll(companyId).Select(p => new
            {
                Id = p.Id,
                RequisitionCode = p.RequisitionCode,
                PreferredDeliveryDate = p.PreferredDeliveryDate,
                SecCompanyId = p.SecCompanyId,
                Status = p.Status,
                ApprovalStatus = p.ApprovalStatus,
                Remarks = p.Remarks,
                CreatedDate = p.CreatedDate,
                CreatedBy = p.CreatedBy,
                ModifiedBy = p.ModifiedBy,
                ModifiedDate = p.ModifiedDate
            }).ToList();
            list = list.OrderByDescending(t => t.CreatedDate).ToList();  // order by created date
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetByRequisitionId(int requisitionId)
        {
            var list = _RequisitionDetailService.GetByRequisitionId(requisitionId).Select(p => new
            {
                Id = p.Id,
                InvRequisitionId = p.InvRequisitionId,
                SlsProductId = p.SlsProductId,
                RequiredQuantity = p.RequiredQuantity,
                SlsUnitId = p.SlsUnitId,
                ProductName = p.ProductName,
                UnitName = p.UnitName
               
            }).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetByRequisitionIdForIssue(int requisitionId)
        {
            var list = _RequisitionDetailService.GetByRequisitionId(requisitionId).Select(p => new
            {
                Id = p.Id,
                InvRequisitionId = p.InvRequisitionId,
                SlsProductId = p.SlsProductId,
                RequiredQuantity = p.RequiredQuantity,
                IssedQuantity=0,
                PendingQuantity = p.RequiredQuantity,
                IssueQuantity=0,
                SlsUnitId = p.SlsUnitId,
                ProductName = p.ProductName,
                UnitName = p.UnitName

            }).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }



        public InvRequisition GetById(int Id)
        {
            InvRequisition list = _RequisitionService.GetById(Id);
            return list;
        }

        [HttpPost]
        [AuthorizeUser(Permission = PermissionEnum.Add, ResourceTag = "RequisitionIndex")]
        public ActionResult Save(RequisitionViewModel objRequisitionViewModel, int companyId = 1)
        {
            Operation objOperation = new Operation();
            userId = Convert.ToInt32(Session["userId"]);

            if (ModelState.IsValid)
            {
                InvRequisition objInvRequisition = new InvRequisition
                {
                    Id = objRequisitionViewModel.Id,
                    RequisitionCode = objRequisitionViewModel.RequisitionCode,
                    PreferredDeliveryDate = objRequisitionViewModel.PreferredDeliveryDate,
                    Remarks = objRequisitionViewModel.Remarks, 
                    SecCompanyId = companyId,                                 
                    CreatedBy = userId,
                    CreatedDate = DateTime.Now,
                    ModifiedBy = userId,
                    ModifiedDate = DateTime.Now,
                  
                    
                };
                if (objInvRequisition.Id == 0)
                {
                    objOperation = _RequisitionService.Save(objInvRequisition);
                }
                else
                {
                    objOperation = _RequisitionService.Update(objInvRequisition);
                }
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }


        public ActionResult SaveDetail(List<RequisitionDetailViewModel> reqDetail)
        {
            Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid && reqDetail != null)
            {
                int Id = _RequisitionDetailService.GetLastId();
                foreach (var item in reqDetail)
                {
                    InvRequisitionDetail objInvRequisitionDetail = _RequisitionDetailService.GetById(item.Id);
                    if (objInvRequisitionDetail != null)
                    {
                        objInvRequisitionDetail.InvRequisitionId = item.InvRequisitionId;
                        objInvRequisitionDetail.SlsProductId = item.SlsProductId;
                        objInvRequisitionDetail.RequiredQuantity = item.RequiredQuantity;
                        objInvRequisitionDetail.SlsUnitId = item.SlsUnitId;                      
                        _RequisitionDetailService.Update(objInvRequisitionDetail);
                    }
                    else
                    {
                        objInvRequisitionDetail = new InvRequisitionDetail();
                        objInvRequisitionDetail.Id = Id;
                        objInvRequisitionDetail.InvRequisitionId = item.InvRequisitionId;
                        objInvRequisitionDetail.SlsProductId = item.SlsProductId;
                        objInvRequisitionDetail.RequiredQuantity = item.RequiredQuantity;
                        objInvRequisitionDetail.SlsUnitId = item.SlsUnitId;    
                        _RequisitionDetailService.Add(objInvRequisitionDetail);
                        Id = Id + 1;
                    }

                }

                objOperation = _RequisitionDetailService.Commit();
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }


        public ActionResult Delete(int Id = 0)
        {
            Operation objOperation = null;

            if (Id != 0)
            {
                InvRequisition obj = _RequisitionService.GetById(Id);

                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _RequisitionService.Delete(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        public ActionResult DeleteDetail(int Id = 0)
        {
            Operation objOperation = null;

            if (Id != 0)
            {
                InvRequisitionDetail obj = _RequisitionDetailService.GetById(Id);

                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _RequisitionDetailService.Delete(obj);
            }
            else
            {
                objOperation = new Operation();
                objOperation.Success = true;               
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        #endregion

        #region Approval
        public ActionResult ApprovalById(int RequisitionNumber)
        {
            TempData["RequisitionNumber"] = RequisitionNumber;
            return RedirectToAction("Approval");
        }
        [HttpGet]
        public ActionResult GetAllReqForApproval()
        {
            int userId = Convert.ToInt32(Session["userId"]);
            var list = _approvalController.GetAllForApproval(userId);
            list = list.OrderByDescending(i => i.CreatedDate).ToList(); // order by created date
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetAllReqForApprovalByDate(DateTime fromDate, DateTime toDate)
        {
            int userId = Convert.ToInt32(Session["userId"]);
            var list = _approvalController.GetAllForApprovalByDate(userId, fromDate, toDate);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetReqApprovalByReqId(int reqId)
        {
            var obj = _approvalController.GetApprovalById(reqId);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ApproveRequisition(InvRequisitionApproval obj, string newComment)
        {
            //According to DB column description: 1=New,2=Approve,3=Pass,4=Reject
            return UpdateRequisitionApproval(obj, 2, newComment);
        }

        [HttpPost]
        public ActionResult RejectRequisition(InvRequisitionApproval obj, string newComment)
        {
            //According to DB column description: 1=New,2=Approve,3=Pass,4=Reject
            return UpdateRequisitionApproval(obj, 4, newComment);
        }
        [HttpPost]
        public ActionResult PassRequisition(InvRequisitionApproval obj, string newComment)
        {
            //According to DB column description: 1=New,2=Approve,3=Pass,4=Reject
            return UpdateRequisitionApproval(obj, 3, newComment);
        }
        public ActionResult UpdateRequisitionApproval(InvRequisitionApproval obj, int action, string newComment)
        {
            int userId = Convert.ToInt32(Session["userId"]);
            Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid)
            {
                if (obj.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        //Approval is created internally during sales order creation.
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
