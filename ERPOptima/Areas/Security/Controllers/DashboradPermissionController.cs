using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Security.Repository;
using ERPOptima.Model.Security;
using ERPOptima.Service.Security;
using Optima.Areas.Security.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPOptima.Lib.Utilities;
using ERPOptima.Lib.Model;
using ERPOptima.Web.Filters;


namespace Optima.Areas.Security.Controllers
{
    public class DashboardPermissionController : Controller
    {
        //
        // GET: /Security/DashboardPermission/
        private ISecDashboardPermissionService _dashboardPermission;

        //[AuthorizeUser]
        //[ResourcePermissionAttribute]
        public ActionResult Index()
        {
            return View();
        }
        public DashboardPermissionController()
        {
            var datafactory = new DatabaseFactory();
            _dashboardPermission = new SecDashboardPermissionService(new SecDashboardPermissionRepository(datafactory), new UnitOfWork(datafactory));

        }
        [HttpGet]
        public ActionResult GetDashboardPermissionByRoleId(int roleId, int moduleId)
        {
            var dt = _dashboardPermission.GetDashboardPermissionByRoleId(roleId, moduleId);
            List<SecDashboardPermissionViewModel> list = dt.DataTableToList<SecDashboardPermissionViewModel>();
            return Json(list, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult GetPermittedDashBoard(int companyId, int roleId, int moduleId)
        {
            var list = _dashboardPermission.GetPermittedDashBoard(companyId, roleId, moduleId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Permission(int companyId, int roleId, int moduleId)
        {
            var list = _dashboardPermission.GetPermittedDashBoard(companyId, roleId, moduleId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Save(List<SecDashboardPermission> secDashboardPermissionsList, int roleId, int moduleId)
        {
            int userId = Convert.ToInt32(Session["userId"]);
            Operation objOperation = new Operation { Success = false };
            if (ModelState.IsValid)
            {
                int del= _dashboardPermission.Delete(roleId, moduleId);
               
                objOperation = _dashboardPermission.Save(secDashboardPermissionsList,userId);
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }
    }
}
