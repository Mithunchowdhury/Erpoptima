using ERPOptima.Data.Common.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Security.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Security;
using ERPOptima.Service.Common;
using ERPOptima.Service.Security;
using Optima.Areas.Security.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Common;
using ERPOptima.Web.Filters;

namespace Optima.Areas.Security.Controllers
{
    public class CompanyUserController : Controller
    {
        //
        // GET: /Security/CompanyUser/

         private ISecUserService _su;
         private ISecCompanyUserService _cu;

         [AuthorizeUser]
         public ActionResult Index()
        {
            return View();
        }
        public CompanyUserController()
        {
            //Session["ModuleId"] = 1;
            var dbfactory = new DatabaseFactory();
            _su = new SecUserService(new SecUserRepository(dbfactory), new UnitOfWork(dbfactory));
            _cu = new SecCompanyUserService(new SecCompanyUserRepository(dbfactory), new UnitOfWork(dbfactory));
        }
        
        
        #region Company User       

        //[HttpGet]
        //public ActionResult GetUserByLevel()
        //{
        //    int level = Convert.ToInt32(Session["userLevel"]);
        //    int companyId = Convert.ToInt32(Session["companyId"]);
        //    DataTable dt = _su.GetUserByLevel(level, companyId);
        //    var list = dt.DataTableToList<SecUser>();           
        //    //var list=_su.GetSecUsers();
        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult GetCompanyUsers(int userId)
        {                     
            DataTable dt = _cu.GetCompanyUsers(userId);
            var list = dt.DataTableToList<CompanyUserViewModel>();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetCompaniesByUserId(int userId)
        {
            DataTable dt = _cu.GetCompanyUsers(userId);
            var list = dt.DataTableToList<CompanyUserViewModel>().Where(t=>t.Status=true).Select(t => new { Id = t.Id, Name = t.Name }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SaveSecCompanyUser(List<SecCompanyUser> companyUserList,int userId)
        {
                        Operation objOperation = new Operation { Success = false };
            if (ModelState.IsValid)
            {

                objOperation = _cu.SaveSecCompanyUser(companyUserList,userId);
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

      
       
        
        #endregion
    }
}
