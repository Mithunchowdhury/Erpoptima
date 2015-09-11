using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.HRM;
using ERPOptima.Model.Sales;
using ERPOptima.Model.Security;
using ERPOptima.Service.Sales;
using ERPOptima.Web.Filters;
using Optima.Areas.Hrm.ViewModel;
using Optima.Areas.Sales.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPOptima.Data;
using Optima.Areas.Common.Controllers;
using ERPOptima.Service.Security;
using ERPOptima.Service.Hrm;
using ERPOptima.Data.Common.Repository;
using ERPOptima.Data.Hrm.Repository;
using ERPOptima.Model.ViewModel;

namespace Optima.Areas.Sales.Controllers
{
    public class CorporateSalesController : Controller
    {
        //
        // GET: /Sales/CorporateSales/
        private ISalesOrderService<SlsSalesOrderApproval, SlsSalesOrderViewModel> _salesOrderService;
        private ICorporateSalesService<SlsCorporateSalesApproval, SlsCorporateSalesApplication> _service;
        private ApprovalController<SlsCorporateSalesApproval, SlsCorporateSalesApplication> _approvalController;
        private ISecCompanyService _SecCompanyService;
        private IOfficeService _officeService;
        private IHrmEmployeeService _hrmEmployeeService;
        private ICorporateClientService _CorporateClientService;
       
        public CorporateSalesController()
        {
            var dbfactory = new DatabaseFactory();
            _service = new CorporateSalesService(new CorporateSalesRepository(dbfactory), new CorporateSalesDetailRepository(dbfactory),
                new CorporateSalesApprovalRepository(dbfactory), new NotificationRepository(dbfactory), new NotificationDetailRepository(dbfactory),
                new UnitOfWork(dbfactory));
            _approvalController = new ApprovalController<SlsCorporateSalesApproval, SlsCorporateSalesApplication>(_service);
            _SecCompanyService = new SecCompanyService(new SecCompanyRepository(dbfactory), new UnitOfWork(dbfactory));
            _officeService = new OfficeService(new OfficeRepository(dbfactory), new UnitOfWork(dbfactory));
            _hrmEmployeeService = new HrmEmployeeService(new HrmEmployeeRepository(dbfactory), new UnitOfWork(dbfactory));
            _CorporateClientService = new CorporateClientService(new CorporateClientRepository(dbfactory), new UnitOfWork(dbfactory));
            _salesOrderService = new SalesOrderService(new SalesOrderRepository(dbfactory),
                new SalesOrderDetailRepository(dbfactory),
                new SalesOrderApprovalRepository(dbfactory),
                new NotificationRepository(dbfactory),
                new NotificationDetailRepository(dbfactory),
                new UnitOfWork(dbfactory));
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Index()
        {
            return View();
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult List()
        {
            return View();
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]

        public ActionResult Approval()
        {
            int? id = null;
            if (TempData["CorporateSalesNumber"] != null)
            {
                id = (int)TempData["CorporateSalesNumber"];
                ViewData["CorporateSalesNumber"] = id;
            }
            
            return View();
        }

        [HttpGet]
        public ActionResult GetAllApproved()
        {
            var list = _service.GetAll();
            var corporateClients = _CorporateClientService.GetAll();
            var salesOrderList = _salesOrderService.GetAll();

            if(list != null && list.Count() > 0)
            {
                //0-New, 1- Inprogress or partial, 2-completed
                //1=New,2=Approve,3=Pass,4=Reject
                IList<int> salesOrderCorpApps = new List<int>();
                try
                {
                    salesOrderList = salesOrderList.Where(i => i.SlsCorporateSalesApplicationId != null).ToList();
                    salesOrderCorpApps = salesOrderList.Select(i => (int)i.SlsCorporateSalesApplicationId).Distinct().ToList();
                }
                catch(Exception ex)
                {

                }

                //list = list.Where(i => i.Status == 2 && i.ApprovalStatus == 2 && !salesOrderCorpApps.Contains(i.Id)).ToList();
                list = list.Where(i => i.ApprovalStatus == 2 && !salesOrderCorpApps.Contains(i.Id)).ToList();

                if (corporateClients != null && corporateClients.Count() > 0)
                {
                    var list2 = list.Select(i => new
                    {
                        Id = i.Id,
                        RefNo = i.RefNo + " (" + corporateClients.Where(j => j.Id == i.SlsCorporateClientId).FirstOrDefault().Name + ")"
                    }).ToList();
                    return Json(list2, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetByCorporateSalesId(int CorporateSalesId)
        {
            var list = _service.GetByCorporateSalesId(CorporateSalesId).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult GetCorpareClient(int corporateAppId)
        {
            var list = _service.GetAll();

            if (list != null && list.Count() > 0)
            {
                try
                {
                    //0-New, 1- Inprogress or partial, 2-completed
                    //1=New,2=Approve,3=Pass,4=Reject
                    var obj = list.Where(i => i.Id == corporateAppId).ToList().FirstOrDefault();
                    int id = obj.SlsCorporateClientId;                    
                    return Json(new { ClientId = id, app = obj }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {

                }
            }

            return Json(new { ClientId = 0 }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetCorpareClientDetail(int corporateAppId)
        {
            var list = _service.GetAll();

            if (list != null && list.Count() > 0)
            {
                try
                {
                    //0-New, 1- Inprogress or partial, 2-completed
                    //1=New,2=Approve,3=Pass,4=Reject
                    var obj = list.Where(i => i.Id == corporateAppId).ToList().FirstOrDefault();
                    int id = obj.SlsCorporateClientId;
                    //var details = _service.GetDetails(id);
                    var details = _service.GetDetails(corporateAppId);
                    if (details != null && details.Count() > 0)
                    {
                        var detailsCustomized = details.Select(i => new
                        {
                            Id = i.Id,
                            SlsCorporateSalesApplicationId = i.SlsCorporateSalesApplicationId,
                            SlsProductId = i.SlsProductId,
                            AppliedPercentage = i.AppliedPercentage,
                            ApprovedPercentage = i.ApprovedPercentage
                        }).ToList();
                        return Json(new { ClientId = id, details = detailsCustomized }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return Json(new { ClientId = 0 }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Show(DateTime? StartDate = null, DateTime? EndDate = null, int? ApprovalStatus = 0, int? Status = 0)
        {
            var list = _service.GetAll(int.Parse(Session["companyId"].ToString()));
            list = list.OrderByDescending(i => i.CreatedDate).ToList(); // order by created date
            if (StartDate != null)
                list = list.Where(i => i.CreatedDate != null && (StartDate <= i.CreatedDate)).ToList();

            if (EndDate != null)
                list = list.Where(i => i.CreatedDate != null && (EndDate >= i.CreatedDate)).ToList();
            if (ApprovalStatus > 0)
                list = list.Where(i => i.ApprovalStatus != null && (ApprovalStatus == i.ApprovalStatus)).ToList();
            if (Status > 0)
                list = list.Where(i => i.Status != null && (Status == i.Status)).ToList();

            var retList = list.Select(p => new
            {
                Id = p.Id,
                RefNo = p.RefNo,
                SlsCorporateClientId = p.SlsCorporateClientId,
                ApprovalStatus = p.ApprovalStatus == 1 ? "New" : p.ApprovalStatus == 2 ? "Approved" : p.ApprovalStatus == 3 ? "Passed" : "Rejected",
                Status = p.Status == 0 ? "New" : p.Status == 1 ? "Inprogress" : "Complated",
                CreatedDate = p.CreatedDate

            }).ToList();

            //list = list.Where(i => i.TransactionDate != null && (DateFrom <= i.TransactionDate.Value && DateTo >= i.TransactionDate.Value)).ToList();
            return Json(retList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public string GetRef(int companyId, int employeeId)
        {
            SecCompany objCmnCompany = _SecCompanyService.GetById(companyId);
            SlsOffice office = _officeService.GetById((int)_hrmEmployeeService.GetById(employeeId).SlsOfficeId);
            string refNo = _service.GetRef(companyId, objCmnCompany.Prefix, office.Code);
            return refNo;
           
        }

        [HttpPost]
        //[AuthorizeUser(Permission = PermissionEnum.Add, ResourceTag = "RequisitionIndex")]
        public ActionResult Save(SlsCorporateSalesApplication record)
        {
            var dbfactory = new DatabaseFactory();
            Operation objOperation = new Operation();
            int userId = Convert.ToInt32(Session["userId"]);
            int companyId = Convert.ToInt32(Session["companyId"]);
            int empid = 0;
            ERPOptima.Service.Security.SecUserService usrSrv = new ERPOptima.Service.Security.SecUserService(new ERPOptima.Data.Security.Repository.SecUserRepository(dbfactory),new UnitOfWork(dbfactory));
            SecUser usr = usrSrv.GetById(userId);

            if (usr.HrmEmployeeId != null && usr.HrmEmployeeId!=0)
            {
                if (ModelState.IsValid)
                {
                    record.HrmEmployeeId = usr.HrmEmployeeId;
                    record.SecCompanyId = companyId;
                    record.CreatedBy = userId;
                    record.CreatedDate = DateTime.Now;
                    record.ModifiedBy = null;
                    record.ModifiedDate = null;

                    if (record.Id == 0)
                    {
                        objOperation = _service.Save(record);
                    }
                    else
                    {
                        //objOperation = _service.Update(objInvDamage);
                    }
                }
                else
                {
                    var errors = ModelState
                                .Where(x => x.Value.Errors.Count > 0)
                                .Select(x => new { x.Key, x.Value.Errors })
                                .ToArray();
                }
            }
            else
            {
                objOperation.Error = "failed to save";
                objOperation.Success = false;
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
                Id = i.Id,
                RefNo = i.RefNo,
                SlsCorporateClientId = i.SlsCorporateClientId,
                SalesAmount = i.SalesAmount,
                PaymentMode = i.PaymentMode,
                CreditTerms = i.CreditTerms,
                AdvanceAmount = i.AdvanceAmount,
                ExtraExpensePerson = i.ExtraExpensePerson,
                Designation = i.Designation,
                Phone = i.Phone,
                Percentage = i.Percentage,
                Amount = i.Amount,
                HrmEmployeeId = i.HrmEmployeeId,
                Status = i.Status,
                ApprovalStatus = i.ApprovalStatus,
                SecCompanyId = i.SecCompanyId,                
                CreatedBy = i.CreatedBy,
                CreatedDate = i.CreatedDate,
                ModifiedBy = i.ModifiedBy,
                ModifiedDate = i.ModifiedDate
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
                SlsCorporateClientId = i.SlsCorporateClientId,
                SalesAmount = i.SalesAmount,
                PaymentMode = i.PaymentMode,
                CreditTerms = i.CreditTerms,
                AdvanceAmount = i.AdvanceAmount,
                ExtraExpensePerson = i.ExtraExpensePerson,
                Designation = i.Designation,
                Phone = i.Phone,
                Percentage = i.Percentage,
                Amount = i.Amount,
                HrmEmployeeId = i.HrmEmployeeId,
                Status = i.Status,
                ApprovalStatus = i.ApprovalStatus,
                SecCompanyId = i.SecCompanyId,
                CreatedBy = i.CreatedBy,
                CreatedDate = i.CreatedDate,
                ModifiedBy = i.ModifiedBy,
                ModifiedDate = i.ModifiedDate
            }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetApprovalById(int id)
        {
            var obj = _approvalController.GetApprovalById(id);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Approve(SlsCorporateSalesApproval obj, string newComment)
        {
            //According to DB column description: 1=New,2=Approve,3=Pass,4=Reject
            return UpdateSalesOrderApproval(obj, 2, newComment);
        }

        [HttpPost]
        public ActionResult Reject(SlsCorporateSalesApproval obj, string newComment)
        {
            //According to DB column description: 1=New,2=Approve,3=Pass,4=Reject
            return UpdateSalesOrderApproval(obj, 4, newComment);
        }
        [HttpPost]
        public ActionResult Pass(SlsCorporateSalesApproval obj, string newComment)
        {
            //According to DB column description: 1=New,2=Approve,3=Pass,4=Reject
            return UpdateSalesOrderApproval(obj, 3, newComment);
        }
        public ActionResult UpdateSalesOrderApproval(SlsCorporateSalesApproval obj, int action, string newComment)
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
                        obj.Event = action;
                        obj.ModifiedBy = userId;
                        obj.ModifiedDate = DateTime.Now.Date;
                        objOperation = _approvalController.UpdateApproval(obj, newComment);
                    }
                    else { objOperation.OperationId = -2; }
                }
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        public ActionResult ApprovalById(int number)
        {
            TempData["CorporateSalesNumber"] = number;
            return RedirectToAction("Approval");
        }
        #endregion
    }
}
