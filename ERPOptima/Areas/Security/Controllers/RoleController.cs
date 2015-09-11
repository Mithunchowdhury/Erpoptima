using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Security.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Security;
using ERPOptima.Service.Security;
using ERPOptima.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Optima.Areas.Security.Controllers
{
    public class RoleController : Controller
    {
        //
        // GET: /Security/Role/
        private ISecRoleService _sr;

        [AuthorizeUser]
        [ResourcePermissionAttribute]

        public ActionResult Index()
        {
            return View();
        }
        public RoleController()
        {
            //Session["ModuleId"] = 1;
            var dbfactory = new DatabaseFactory();
            _sr = new SecRoleService(new SecRoleRepository(dbfactory), new UnitOfWork(dbfactory));
        }
        #region User

        [HttpGet]
        public ActionResult GetRoles()
        {
            var list = _sr.GetSecRoles().Select(su => new SecRole
            {
                Id = su.Id,
                Name = su.Name,
                Status = su.Status,
                CreatedBy=su.CreatedBy,
                CreatedDate=su.CreatedDate
            }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetRolesByUserId()
        {
            int userId=Convert.ToInt32(Session["userId"]);
            var list = _sr.GetSecRoles().Where(x => x.Id == userId).Select(su => new SecRole
            {
                Id = su.Id,
                Name = su.Name              
            }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SaveRole(SecRole secRole)
        {
            int userId = Convert.ToInt32(Session["userId"]);

             Operation objOperation = new Operation { Success = false };
            if (ModelState.IsValid)
            {
                if (secRole.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        secRole.CreatedBy = userId;
                        secRole.CreatedDate = DateTime.Now.Date;
                        objOperation = _sr.SaveSecRole(secRole);
                    }
                    else { objOperation.OperationId = -1; }
                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        secRole.ModifiedBy = userId;
                        secRole.ModifiedDate = DateTime.Now.Date;
                        objOperation = _sr.UpdateSecRole(secRole);
                    }
                    else { objOperation.OperationId = -2; }
                }
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult DeleteSecRole(int Id)
        {
                        Operation objOperation = new Operation { Success = false };
            if (Id != 0)
            {
                SecRole obj = _sr.GetById(Id);
                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _sr.DeleteSecRole(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

      

        #endregion

    }
}
