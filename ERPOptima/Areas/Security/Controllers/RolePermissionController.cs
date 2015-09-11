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


namespace Optima.Areas.Security.Controllers
{
    public class RolePermissionController : Controller
    {
        //
        // GET: /Security/RolePermission/
        private ISecResourceService _rs;
        private ISecRolePermissionService _rp;
        public ActionResult Index()
        {
            return View();
        }
        public RolePermissionController()
        {
            var datafactory = new DatabaseFactory();
            _rs = new SecResourceService(new SecResourceRepository(datafactory), new UnitOfWork(datafactory));
            _rp = new SecRolePermissionService(new SecRolePermissionRepository(datafactory), new UnitOfWork(datafactory));
        }
        [HttpGet]
        public ActionResult GetResourcePermissionByUserOrRoleId(int roleId,int userId,int moduleId)
        {
            var dt = _rs.GetResourcePermissionByUserOrRoleId(roleId,userId,moduleId);
            List<SecResourceViewModel> list = dt.DataTableToList<SecResourceViewModel>();
            return Json(list, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetResourcePermissionByRoleId(int roleId, int moduleId)
        {
            var dt = _rs.GetResourcePermissionByRoleId(roleId,  moduleId);
            List<SecResourceViewModel> list = dt.DataTableToList<SecResourceViewModel>();
            return Json(list, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public bool IsPermitted(int roleId, int resourceId)
        {
            bool ret = false;
            ret = _rp.IsPermitted(roleId, resourceId);
            return ret;
        }
        [HttpPost]
        public ActionResult SaveSecRolePermissions(List<SecRolePermission> secRolePermissionsList, int roleId, int moduleId)
        {
            int userId = Convert.ToInt32(Session["userId"]);
            Operation objOperation = new Operation { Success = false };
            if (ModelState.IsValid)
            {

                objOperation = _rp.SaveSecRolePermission(secRolePermissionsList,userId, roleId, moduleId);
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);

        }
    }
}
