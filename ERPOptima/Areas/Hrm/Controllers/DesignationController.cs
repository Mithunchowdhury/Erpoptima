using ERPOptima.Data.Hrm.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Lib.Model;
using ERPOptima.Model.HRM;
using ERPOptima.Service.Hrm;
using ERPOptima.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Optima.Areas.Hrm.Controllers
{
    public class DesignationController : Controller
    {
        //
        // GET: /Hrm/Designation/
        private IHrmDesignationService _HrmDesignationService;

        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Index()
        {
            return View();
        }
        public DesignationController()
        {
            var dbfactory = new DatabaseFactory();
            _HrmDesignationService = new HrmDesignationService(new HrmDesignationRepository(dbfactory), new UnitOfWork(dbfactory));
        }
        #region Designation

        [HttpPost]
        public ActionResult Delete(long Id = 0)
        {
            Operation objOperation = null;

            if (Id != 0)
            {
                HrmDesignation obj = _HrmDesignationService.GetById(Id);

                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _HrmDesignationService.Delete(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        [HttpGet]
        public ActionResult GetByCompanyId()
        {
            int companyId = Convert.ToInt32(Session["CompanyId"]);
            var list = _HrmDesignationService.GetParentWithChild(companyId).Select(ac => new
            {
                Id = ac.Id,
                HrmDesignationId = ac.HrmDesignationId,
                Name = ac.Name,
                ShortName = ac.ShortName,
                CreatedBy = ac.CreatedBy,
                CreatedDate = ac.CreatedDate
            }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //[AuthorizeUser(Permission = PermissionEnum.Add, ResourceTag = "ChartOfAccountIndex")]
        public ActionResult Save(HrmDesignation HrmDesignationViewModel)
        {
            Operation objOperation = new Operation();
            int companyId = Convert.ToInt32(Session["companyId"]);
            int userId = Convert.ToInt32(Session["userId"]);
            if (ModelState.IsValid)
            {
                HrmDesignation hrmDesignation = new HrmDesignation
                {
                    Id = HrmDesignationViewModel.Id,
                    Name = HrmDesignationViewModel.Name,
                    ShortName = HrmDesignationViewModel.ShortName,
                    HrmDesignationId = HrmDesignationViewModel.HrmDesignationId,
                    //CmnCompanyId = companyId,
                    //Priority = HrmDesignationViewModel.Priority,
                    //Remarks = HrmDesignationViewModel.Remarks,
                    CreatedBy = userId,
                    CreatedDate = DateTime.Now.Date
                };
                if (hrmDesignation.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        hrmDesignation.CreatedBy = userId;
                        hrmDesignation.CreatedDate = DateTime.Now.Date;
                        objOperation = _HrmDesignationService.Save(hrmDesignation);
                    }
                    else { objOperation.OperationId = -1; }
                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        hrmDesignation.ModifiedBy = userId;
                        hrmDesignation.ModifiedDate = DateTime.Now.Date;
                        objOperation = _HrmDesignationService.Update(hrmDesignation);
                    }
                    else { objOperation.OperationId = -2; }
                }
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        #endregion 
    }
}
