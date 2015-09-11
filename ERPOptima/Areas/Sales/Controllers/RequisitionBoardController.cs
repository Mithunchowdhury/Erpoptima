using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Inventory.Repository;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Model.Inventory;
using ERPOptima.Service.Inventory;
using ERPOptima.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Optima.Areas.Sales.Controllers
{
    public class RequisitionBoardController : Controller
    {
        private IRequisitionService<InvRequisitionApproval, InvRequisitionViewModel> _RequisitionService;
        private IRequisitionDetailService _RequisitionDetailService;
        //
        // GET: /Sales/RequisitionBoard/
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Index()
        {
            return View();
        }

        public RequisitionBoardController()
        {
            var dbfactory = new DatabaseFactory();
            _RequisitionService = new RequisitionService(new RequisitionRepository(dbfactory),
                new RequisitionApprovalRepository(dbfactory),
                new NotificationRepository(dbfactory), new NotificationDetailRepository(dbfactory),
                new UnitOfWork(dbfactory));
        }

        [HttpGet]
        public ActionResult Search(int companyId, int[] statusVal, DateTime? from, DateTime? to)
        {
            var list = _RequisitionService.GetAll(companyId).OrderByDescending(t=>t.CreatedDate).Select(p => new
            {
                Id = p.Id,
                RequisitionCode = p.RequisitionCode,
                PreferredDeliveryDate = p.PreferredDeliveryDate,
                SecCompanyId = p.SecCompanyId,
                Status = p.Status,
                ApprovalStatus = p.ApprovalStatus,
                Remarks = p.Remarks,
                CreatedBy = p.CreatedBy,
                ModifiedBy = p.ModifiedBy
            }).ToList();
            
            //Only Company 
            if (companyId > 0 && statusVal == null && from == null && to == null)
            {
                var result = list.Where(i =>
                (companyId > 0 && i.SecCompanyId == companyId)).ToList();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            //Only Date 
            else if (companyId <= 0 && statusVal == null && from != null && to == null)
            {
                var result = list.Where(i =>                    
                (from != null && from <= i.PreferredDeliveryDate)).ToList();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else if (companyId <= 0 && statusVal == null && from == null && to != null)
            {
                var result = list.Where(i =>
                    (to != null && i.PreferredDeliveryDate <= to)).ToList();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            //Only Status
            else if (companyId <= 0 && statusVal != null && statusVal.Length > 0 && from == null && to == null)
            {
                var result = list.Where(i =>
               (i.Status != null && statusVal.Contains((int)i.Status))).ToList();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            //Company + Date
            else if (companyId > 0 && statusVal == null && from != null && to == null)
            {
                var result = list.Where(i => (companyId > 0 && i.SecCompanyId == companyId) &&
                (from != null && from <= i.PreferredDeliveryDate)).ToList();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else if (companyId > 0 && statusVal == null && from == null && to != null)
            {
                var result = list.Where(i => (companyId > 0 && i.SecCompanyId == companyId) &&
                    (to != null && i.PreferredDeliveryDate <= to)).ToList();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else if (companyId > 0 && statusVal == null && from != null && to != null)
            {
                var result = list.Where(i => (companyId > 0 && i.SecCompanyId == companyId) &&
                    (from != null && from <= i.PreferredDeliveryDate) && (to != null && i.PreferredDeliveryDate <= to)).ToList();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            //Company + Status
            else if (companyId > 0 && statusVal != null && statusVal.Length > 0 && from == null && to == null)
            {
                var result = list.Where(i => (companyId > 0 && i.SecCompanyId == companyId) &&
               (i.Status != null && statusVal.Contains((int)i.Status))).ToList();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            // Date + Status
            else if (companyId <= 0 && statusVal != null && statusVal.Length > 0 && from != null && to == null)
            {
                var result = list.Where(i => (companyId > 0 && i.SecCompanyId == companyId) &&
                (from != null && from <= i.PreferredDeliveryDate) && (i.Status != null && statusVal.Contains((int)i.Status))).ToList();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else if (companyId <= 0 && statusVal != null && statusVal.Length > 0 && from == null && to != null)
            {
                var result = list.Where(i => (companyId > 0 && i.SecCompanyId == companyId) &&
                    (to != null && i.PreferredDeliveryDate <= to) && (i.Status != null && statusVal.Contains((int)i.Status))).ToList();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else if (companyId <= 0 && statusVal != null && statusVal.Length > 0 && from != null && to != null)
            {
                var result = list.Where(i =>                   
                (from != null && from <= i.PreferredDeliveryDate) && (to != null && i.PreferredDeliveryDate <= to) &&
                (i.Status != null && statusVal.Contains((int)i.Status))).ToList();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            //All
            else if (companyId > 0 && statusVal != null && statusVal.Length > 0 && from != null && to == null)
            {
                var result = list.Where(i =>
                    (companyId > 0 && i.SecCompanyId == companyId) &&
                (from != null && from <= i.PreferredDeliveryDate) &&
                (i.Status != null && statusVal.Contains((int)i.Status))).ToList();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else if (companyId > 0 && statusVal != null && statusVal.Length > 0 && from == null && to != null)
            {
                var result = list.Where(i =>
                    (companyId > 0 && i.SecCompanyId == companyId) &&
                (to != null && i.PreferredDeliveryDate <= to) &&
                (i.Status != null && statusVal.Contains((int)i.Status))).ToList();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else if (companyId > 0 && statusVal != null && statusVal.Length > 0 && from != null && to != null)
            {
                var result = list.Where(i =>
                    (companyId > 0 && i.SecCompanyId == companyId) &&
                (from != null && from <= i.PreferredDeliveryDate) && (to != null && i.PreferredDeliveryDate <= to) &&
                (i.Status != null && statusVal.Contains((int)i.Status))).ToList();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var result = list.Where(i =>
                    (companyId > 0 && i.SecCompanyId == companyId) &&
                (from != null && from <= i.PreferredDeliveryDate) && (to != null && i.PreferredDeliveryDate <= to) &&
                (i.Status != null && statusVal.Contains((int)i.Status))).ToList();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }



    }
}
