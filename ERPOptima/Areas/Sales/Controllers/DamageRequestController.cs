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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Optima.Areas.Sales.Controllers
{
    public class DamageRequestController : Controller
    {
        private IDamageService<InvDamageApproval, InvDamage> _IDamageService;         
         private IDamageDetailService _IDamageDetailService;
         private ISecCompanyService _SecCompanyService;
         private IOfficeService _officeService;
         private IHrmEmployeeService _hrmEmployeeService;
         
        int userId = 0;
        int companyId = 0;
        public DamageRequestController()
        {
             var dbfactory = new DatabaseFactory();
             _IDamageService = new DamageService(new InvDamageRepository(dbfactory),
                new NotificationRepository(dbfactory),
                new NotificationDetailRepository(dbfactory),
                new InvDamageApprovalRepository(dbfactory), new UnitOfWork(dbfactory));
            _IDamageDetailService = new DamageDetailService(new InvDamageDetailRepository(dbfactory), new UnitOfWork(dbfactory));
            _SecCompanyService = new SecCompanyService(new SecCompanyRepository(dbfactory), new UnitOfWork(dbfactory));
            _officeService = new OfficeService(new OfficeRepository(dbfactory), new UnitOfWork(dbfactory));
            _hrmEmployeeService = new HrmEmployeeService(new HrmEmployeeRepository(dbfactory), new UnitOfWork(dbfactory));
        }

        //
        // GET: /Sales/DamageRequest/
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
        [HttpGet]
        public ActionResult GetAll()
        {
            
            var list = _IDamageService.GetAll();
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult Show(DateTime? StartDate = null, DateTime? EndDate = null, int? ApprovalStatus = 0)
        {
            var list = _IDamageService.GetAll(int.Parse(Session["companyId"].ToString()));
            list = list.OrderByDescending(i => i.CreatedDate).ToList();  //order by date
            if (StartDate != null)
                list = list.Where(i => i.CreatedDate != null && (StartDate <= i.CreatedDate)).ToList();

            if (EndDate != null)
                list = list.Where(i => i.CreatedDate != null && (EndDate >= i.CreatedDate)).ToList();

            if (ApprovalStatus > 0)
                list = list.Where(i => i.ApprovalStatus != null && (ApprovalStatus == i.ApprovalStatus)).ToList();

            var retList = list.Select(p => new
            {
                Id = p.Id,
                RefNo = p.RefNo,
                Status = p.Status == 0 ? "New" : p.Status == 1 ? "Inprogress" : "Complated",
                ApprovalStatus = p.ApprovalStatus == 1 ? "New" : p.ApprovalStatus == 2 ? "Approve" : p.ApprovalStatus == 3 ? "Pass" : "Reject",
                InvStoreId = p.InvStoreId,
                CreatedDate = p.CreatedDate

            }).ToList();

            //list = list.Where(i => i.TransactionDate != null && (DateFrom <= i.TransactionDate.Value && DateTo >= i.TransactionDate.Value)).ToList();
            return Json(retList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetAutoNumber(int companyId, int employeeId)
        {           
            SecCompany objCmnCompany = _SecCompanyService.GetById(companyId);
            SlsOffice office = _officeService.GetById((int)_hrmEmployeeService.GetById(employeeId).SlsOfficeId);
            string autoNumber = _IDamageService.GetLastCode(companyId,objCmnCompany.Prefix, office.Code);
            
            return Json(new { Refno = autoNumber }, JsonRequestBehavior.AllowGet);
        }

         [HttpPost]
        //public ActionResult Save(InvDamage obj)
        //{
        //    Operation objOperation = new Operation();
        //    userId = Convert.ToInt32(Session["userId"]);
        //    companyId = Convert.ToInt32(Session["companyId"]);
        //    if (ModelState.IsValid)
        //    {

        //        obj.SecCompanyId = companyId;
        //        obj.CreatedBy = userId;
        //        obj.CreatedDate = DateTime.Now;
        //        obj.ModifiedBy = userId;
        //        obj.ModifiedDate = DateTime.Now;
                
        //        if (obj.Id == 0)
        //        {
        //            objOperation = _DamageService.Save(obj);
        //        }
        //        else
        //        {
        //            objOperation = _DamageService.Update(obj);
        //        }
        //    }

        //    return Json(objOperation, JsonRequestBehavior.DenyGet);
        //}
        public ActionResult Save(InvDamage invDamage)
        {
            int companyId = Convert.ToInt32(Session["companyId"]);
            int userId = Convert.ToInt32(Session["userId"]);
            
            Operation objOperation = new Operation { Success = false };
            if (ModelState.IsValid && invDamage != null)
            {
                if (invDamage.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {                        
                        invDamage.SecCompanyId = companyId;
                        invDamage.CreatedBy = userId;
                        invDamage.CreatedDate = DateTime.Now;
                        //invDamage.InvDamageDetails = null;
                        objOperation = _IDamageService.Save(invDamage);

                        //int DamageId = Convert.ToInt32(objOperation.OperationId);

                        //foreach (var item in Detail)
                        //{
                        //    InvDamageDetail objInvDamageDetail = _IDamageDetailService.GetById(item.Id);
                        //    if (objInvDamageDetail != null)
                        //    {
                        //        objInvDamageDetail.InvDamageId= invDamage.Id;
                        //        objInvDamageDetail.SlsProductId = item.SlsProductId;
                        //        objInvDamageDetail.Quantity = item.Quantity;
                        //        objInvDamageDetail.Reason = item.Reason;
                        //        objInvDamageDetail.SlsUnitsId = item.SlsUnitsId;
                        //        _IDamageDetailService.Update(objInvDamageDetail);
                        //    }
                        //    else
                        //    {
                        //        objInvDamageDetail = new InvDamageDetail();
                        //        objInvDamageDetail.InvDamageId = DamageId;
                        //        objInvDamageDetail.SlsProductId = item.SlsProductId;
                        //        objInvDamageDetail.Quantity = item.Quantity;
                        //        objInvDamageDetail.Reason = item.Reason;
                        //        objInvDamageDetail.SlsUnitsId = item.SlsUnitsId;
                        //        _IDamageDetailService.Save(objInvDamageDetail);
                        //    }

                        //}

                    }

                }
                else
                {
                    //if ((bool)Session["Edit"])
                    //{
                    //    invDamage.SecCompanyId = companyId;
                    //    invDamage.ModifiedBy = userId;
                    //    invDamage.ModifiedDate = DateTime.Now;
                    //    objOperation = _IDamageService.Update(invDamage);

                    //    foreach (var item in Detail)
                    //    {
                    //        InvDamageDetail objInvDamageDetail = _IDamageDetailService.GetById(item.Id);
                    //        if (objInvDamageDetail != null)
                    //        {
                    //            objInvDamageDetail.InvDamageId = invDamage.Id;
                    //            objInvDamageDetail.SlsProductId = item.SlsProductId;
                    //            objInvDamageDetail.Quantity = item.Quantity;
                    //            objInvDamageDetail.Reason = item.Reason;
                    //            objInvDamageDetail.SlsUnitsId = item.SlsUnitsId;
                    //            _IDamageDetailService.Update(objInvDamageDetail);
                    //        }
                    //        else
                    //        {
                    //            objInvDamageDetail = new InvDamageDetail();
                    //            objInvDamageDetail.InvDamageId = invDamage.Id;
                    //            objInvDamageDetail.SlsProductId = item.SlsProductId;
                    //            objInvDamageDetail.Quantity = item.Quantity;
                    //            objInvDamageDetail.Reason = item.Reason;
                    //            objInvDamageDetail.SlsUnitsId = item.SlsUnitsId;
                    //            _IDamageDetailService.Save(objInvDamageDetail);
                    //        }

                    //    }

                    //}

                }

                //objOperation = _IDamageService.Commit();
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }
         public ActionResult Delete(int Id)
         {
             Operation objOperation = new Operation { Success = false };
             if (Id != 0)
             {
                 InvDamage obj = _IDamageService.GetById(Id);
                 if (obj == null)
                 {
                     objOperation.Success = false;
                     return Json(objOperation, JsonRequestBehavior.DenyGet);
                 }
                 objOperation = _IDamageService.Delete(obj);
             }
             return Json(objOperation, JsonRequestBehavior.DenyGet);
         }
         public ActionResult DeleteDetail(int Id)
         {
             Operation objOperation = new Operation { Success = false };
             if (Id != 0)
             {
                 InvDamageDetail obj = _IDamageDetailService.GetById(Id);
                 if (obj == null)
                 {
                     objOperation.Success = false;
                     return Json(objOperation, JsonRequestBehavior.DenyGet);
                 }
                 objOperation = _IDamageDetailService.Delete(obj);
             }
             return Json(objOperation, JsonRequestBehavior.DenyGet);
         }

         
    }
}
