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

namespace Optima.Areas.Inventory.Controllers
{
    public class IssueController : Controller
    {
         private IIssueService _IssueService;
         private IIssueDetailService _IssueDetailService;
         private ISecCompanyService _SecCompanyService;
         private IOfficeService _officeService;
         private IHrmEmployeeService _hrmEmployeeService;
         public IssueController()
         {
             var dbfactory = new DatabaseFactory();
             _IssueService = new IssueService(new IssueRepository(dbfactory), new UnitOfWork(dbfactory));
             _IssueDetailService = new IssueDetailService(new IssueDetailRepository(dbfactory), new UnitOfWork(dbfactory));
             _SecCompanyService = new SecCompanyService(new SecCompanyRepository(dbfactory), new UnitOfWork(dbfactory));
             _officeService = new OfficeService(new OfficeRepository(dbfactory), new UnitOfWork(dbfactory));
             _hrmEmployeeService = new HrmEmployeeService(new HrmEmployeeRepository(dbfactory), new UnitOfWork(dbfactory));
         }
        //
        // GET: /Inventory/Issue/
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string GetCode(int companyId, int employeeId)
        {
            SecCompany objCmnCompany = _SecCompanyService.GetById(companyId);
            SlsOffice office = _officeService.GetById((int)_hrmEmployeeService.GetById(employeeId).SlsOfficeId);
            string code = _IssueService.GetLastCode(companyId, objCmnCompany.Prefix, office.Code);
            return code;

        }

         public ActionResult GetIssueByRequisitionId(int requisitionId)
         {
             var list = _IssueService.GetIssueByRequisitionId(requisitionId);
             list = list.OrderByDescending(i => i.Date).ToList();   // order by date

             return Json(list, JsonRequestBehavior.AllowGet);
         }
         public ActionResult GetIssueDetailByIssueId(int requisitionId,int issueId)
         {
             var list = _IssueDetailService.GetIssueDetailByIssueId(requisitionId,issueId);


             return Json(list, JsonRequestBehavior.AllowGet);
         }





         [HttpPost]
         public ActionResult Save(InvIssue iss, List<InvIssueDetail> issDetail)
         {
             int companyId = Convert.ToInt32(Session["companyId"]);
             int userId = Convert.ToInt32(Session["userId"]);
             Operation objOperation = new Operation { Success = false };
             if (ModelState.IsValid && issDetail != null)
             {
                 if (iss.Id == 0)
                 {
                     if ((bool)Session["Add"])
                     {
                         iss.SecCompanyId = companyId;
                         iss.CreatedBy = userId;
                         iss.CreatedDate = DateTime.Now;
                         objOperation = _IssueService.Save(iss);

                         int IssueId = Convert.ToInt32(objOperation.OperationId);

                         foreach (var item in issDetail)
                         {
                             InvIssueDetail objInvIssueDetail = _IssueDetailService.GetById(item.Id);
                             if (objInvIssueDetail != null)
                             {
                                 objInvIssueDetail.InvIssueId = iss.Id;
                                 objInvIssueDetail.SlsProductId = item.SlsProductId;
                                 objInvIssueDetail.RequiredQuantity = item.RequiredQuantity;
                                 objInvIssueDetail.IssuedQuantity = item.IssuedQuantity;
                                 objInvIssueDetail.SlsUnitId = item.SlsUnitId;
                                 _IssueDetailService.Update(objInvIssueDetail);
                             }
                             else
                             {
                                 objInvIssueDetail = new InvIssueDetail();
                                 objInvIssueDetail.InvIssueId = IssueId;
                                 objInvIssueDetail.SlsProductId = item.SlsProductId;
                                 objInvIssueDetail.RequiredQuantity = item.RequiredQuantity;
                                 objInvIssueDetail.IssuedQuantity = item.IssuedQuantity;
                                 objInvIssueDetail.SlsUnitId = item.SlsUnitId;
                                 _IssueDetailService.Save(objInvIssueDetail);
                             }

                         }

                     }
                    
                 }
                 else
                 {
                     if ((bool)Session["Edit"])
                     {
                         iss.SecCompanyId = companyId;
                         iss.ModifiedBy = userId;
                         iss.ModifiedDate = DateTime.Now;
                         objOperation = _IssueService.Update(iss);

                         foreach (var item in issDetail)
                         {
                             InvIssueDetail objInvIssueDetail = _IssueDetailService.GetById(item.Id);
                             if (objInvIssueDetail != null)
                             {
                                 objInvIssueDetail.InvIssueId = iss.Id;
                                 objInvIssueDetail.SlsProductId = item.SlsProductId;
                                 objInvIssueDetail.RequiredQuantity = item.RequiredQuantity;
                                 objInvIssueDetail.IssuedQuantity = item.IssuedQuantity;
                                 objInvIssueDetail.SlsUnitId = item.SlsUnitId;
                                 _IssueDetailService.Update(objInvIssueDetail);
                             }
                             else
                             {
                                 objInvIssueDetail = new InvIssueDetail();
                                 objInvIssueDetail.InvIssueId = iss.Id;
                                 objInvIssueDetail.SlsProductId = item.SlsProductId;
                                 objInvIssueDetail.RequiredQuantity = item.RequiredQuantity;
                                 objInvIssueDetail.IssuedQuantity = item.IssuedQuantity;
                                 objInvIssueDetail.SlsUnitId = item.SlsUnitId;
                                 _IssueDetailService.Save(objInvIssueDetail);
                             }

                         }





                     }
                     
                 }


                 objOperation = _IssueService.Commit();


             }

             return Json(objOperation, JsonRequestBehavior.DenyGet);
         }

        public ActionResult Delete(int Id)
        {
            Operation objOperation = new Operation { Success = false };
            if (Id != 0)
            {
                InvIssue obj = _IssueService.GetById(Id);
                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _IssueService.Delete(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        public ActionResult DeleteDetail(int Id)
        {
            Operation objOperation = new Operation { Success = false };
            if (Id != 0)
            {
                InvIssueDetail obj = _IssueDetailService.GetById(Id);
                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _IssueDetailService.Delete(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }  















    }
}
