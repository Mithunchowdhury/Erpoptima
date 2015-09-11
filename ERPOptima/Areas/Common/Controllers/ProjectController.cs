using ERPOptima.Data.Common.Repository;
using ERPOptima.Lib.Utilities;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Service.Common;
using Optima.Areas.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using ERPOptima.Model.Common;
using ERPOptima.Lib.Model;
using ERPOptima.Web.Filters;

namespace Optima.Areas.Common.Controllers
{
    public class ProjectController : Controller
    {
        //
        // GET: /Common/Project/

        private ICmnProjectService _CmnProjectService;

        public ProjectController()
        {
            var dbfactory = new DatabaseFactory();
            _CmnProjectService = new CmnProjectService(new CmnProjectRepository(dbfactory), new UnitOfWork(dbfactory));
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SaveProject(CmnProject cmnProject)
        {
            Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid)
            {

                if (cmnProject.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        objOperation = _CmnProjectService.SaveCmnProject(cmnProject);
                    }
                    else { objOperation.OperationId = -1; }
                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        objOperation = _CmnProjectService.UpdateCmnProject(cmnProject);
                    }
                    else { objOperation.OperationId = -2; }
                }
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult DeleteCmnProject(int Id)
        {
            Operation objOperation = new Operation { Success = false };

            if (Id != 0)
            {
                CmnProject obj = _CmnProjectService.GetById(Id);

                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _CmnProjectService.DeleteCmnProject(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        //public ActionResult GetProjectsByCompanyId(int businessId)
        //{
        //    try
        //    {
        //        //int companyId = Convert.ToInt32(Session["companyId"].ToString());
        //        var query = _CmnProjectService.GetByBusinessId(businessId);

        //        var projects = query.Select(p => new
        //        {
        //            p.Id,
        //            p.Name,
        //            p.ClosingStatus
        //        });

        //        return Json(projects, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (System.Exception)
        //    {

        //        throw;
        //    }
        //}
        [HttpPost]
        public ActionResult GetProjectsByCompanyIdAndBusinessIdAndAnfChartOfAccountId(CmnBusinessesIdViewModel obj)
        {
            try
            {
                int companyId = Convert.ToInt32(Session["companyId"].ToString());
                DataTable dt = _CmnProjectService.GetByCompanyIdAndBusinessId(companyId, obj.CmnBusinessId, obj.AnFChartOfAccountId);

                List<CmnProjectComboViewModel> projects = dt.DataTableToList<CmnProjectComboViewModel>();

                return Json(projects, JsonRequestBehavior.AllowGet);
            }
            catch (System.Exception)
            {

                throw;
            }
        }        
       

        public ActionResult GetProjects()
        {

            var list = _CmnProjectService.GetCmnProjects();
          
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult GetYears()
        //{

        //    var list = _CmnProjectService.GetCmnProjects();

        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}
    }
}