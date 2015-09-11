using ERPOptima.Data.Accounts;
using ERPOptima.Data.Common.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Accounts;
using ERPOptima.Service.Accounts;
using ERPOptima.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPOptima.Lib.Model;
using ERPOptima.Web.Accounts.ViewModel;
using ERPOptima.Model.Common;
using ERPOptima.Web.Filters;

namespace Optima.Areas.Accounts.Controllers
{
    public class ProjectCloseController : Controller
    {
        private ICmnProjectService _CmnProjectService;
        private ICmnBusinessService _CmnBusinessService;

        public ProjectCloseController()
        {
           // Session["ModuleId"] = 2;
            var dbfactory = new DatabaseFactory();
            _CmnProjectService = new CmnProjectService(new CmnProjectRepository(dbfactory), new UnitOfWork(dbfactory));
            //_CmnBusinessService = new CmnBusinessService(new CmnBusinessRepository(dbfactory), new UnitOfWork(dbfactory));
        }
        #region Project Close
        //[HttpGet]
        //public ActionResult GetProjects(int businessId)
        //{
        //    var list = _CmnProjectService.GetByBusinessId(businessId).Select(proj => new
        //    {
        //        Id = proj.Id,
        //        Name = proj.Name,
        //        Code = proj.Code,
        //        StartDate = proj.StartDate,
        //        EndDate = proj.EndDate,
        //        ClosingStatus = proj.ClosingStatus,
        //        ClosingNote = proj.ClosingNote
        //    }).ToList();
        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}
        //public ActionResult GetBusinesses()
        //{
        //    int companyId = Convert.ToInt32(Session["companyId"]);
        //    var list = _CmnBusinessService.GetCmnBusinesses(companyId).Select(bzns => new { Id = bzns.Id, Name = bzns.Name }).ToList();
        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}
       [AuthorizeUser]

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UpdateCmnProject(CmnProject objCmnProject)
        {
            int userId = Convert.ToInt32(Session["userId"]);
            Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid)
            {
                CmnProject objProject = _CmnProjectService.GetById(objCmnProject.Id);
                if (objProject != null)
                {                    
                    objProject.ClosingStatus = objCmnProject.ClosingStatus;
                    objProject.ClosingNote = objCmnProject.ClosingNote;
                    objProject.ClosedBy = userId;
                    objProject.ClosingDate = DateTime.Now;
                    objProject.ModifiedBy = userId;
                    objProject.ModifiedDate = DateTime.Now;
                    objOperation = _CmnProjectService.UpdateCmnProject(objProject);
                }
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

       

        #endregion
    }
}
