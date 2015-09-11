using ERPOptima.Data.Common.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Security.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Security;
using ERPOptima.Service.Security;
using ERPOptima.Web.Authorization;
using ERPOptima.Web.Filters;
using Optima.Areas.Common.ViewModels;
using Optima.Areas.Security.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Common;
using ERPOptima.Web.Filters;

namespace Optima.Areas.Security.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /Security/User/
        private ISecCompanyService _company;
        private ISecUserService _su;

        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Index()
        {
            return View();
        }
        public UserController()
        {
            //Session["ModuleId"] = 1;
            var dbfactory = new DatabaseFactory();
            _su = new SecUserService(new SecUserRepository(dbfactory), new UnitOfWork(dbfactory));
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public int GetUserByLoginId(LoginIdAndPassword login)
        {
            int ret = 0;
            
            SecUser user = _su.GetUserByLogin(login.LoginName);
           
            if (user != null)
            {
                if (user.Status == true)
                {
                    if (user.Password.ToUpper() == login.Password.ToUpper())
                    {
                        CustomPrincipal.Login(user.LoginName);
                        ret = 1;
                        Session["userId"] = user.Id;
                        Session["userName"] = user.LoginName;
                        //Session["userLevel"] = user.Level;
                        Session["employeeId"] = user.HrmEmployeeId;
                        Session["roleId"] = user.SecRoleId;
                        
                    }
                }
            }          
            return ret;
            
        }

        public ActionResult CHeckSession()
        {
            Operation ret = new Operation();
            ret.Success = true;
            ret.Message = "Authenticated user ";
            ret.OperationId = 1;
            if (Session["userId"] == null)
            {
                ret.Success = false;
                ret.Message = "Unauthenticated user ";
                ret.OperationId = -1;
            }
            return Json(ret, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetAllCompanies()
        {
            try
            {
                DatabaseFactory _databaseFactory = new DatabaseFactory();
                _company = new SecCompanyService(new SecCompanyRepository(_databaseFactory), new UnitOfWork(_databaseFactory));               
                var list = _company.GetAll().Select(t => new { Id = t.Id, Name = t.Name }).ToList();               
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public ActionResult GetCompaniesByUserId(int userId)
        {
            //ISecUserService _su;
            ISecCompanyUserService _cu;
            var dbfactory = new DatabaseFactory();
            SecUser user = _su.GetById(userId);
            
            _su = new SecUserService(new SecUserRepository(dbfactory), new UnitOfWork(dbfactory));
            _cu = new SecCompanyUserService(new SecCompanyUserRepository(dbfactory), new UnitOfWork(dbfactory));
            DataTable dt = _cu.GetCompanyUsers(userId);
            var list = dt.DataTableToList<CompanyUserViewModel>().Where(t => t.Status == true).Select(t => new { Id = t.Id, Name = t.Name }).ToList();
            if(user.HrmEmployeeId!=null)
            {
                ERPOptima.Model.HRM.HrmEmployee emp = new ERPOptima.Model.HRM.HrmEmployee();
                ERPOptima.Service.Hrm.HrmEmployeeService es = new ERPOptima.Service.Hrm.HrmEmployeeService(new ERPOptima.Data.Hrm.Repository.HrmEmployeeRepository(dbfactory), new UnitOfWork(dbfactory));
                emp = es.GetById(user.HrmEmployeeId.Value);
                if(emp!=null)
                {
                    ISecCompanyService _company;
                    _company = new SecCompanyService(new SecCompanyRepository(dbfactory), new UnitOfWork(dbfactory));
                    var defaultCompany = _company.GetById(emp.SecCompanyId.Value);
                    if (defaultCompany != null)
                    {
                        var obj = new { Id = defaultCompany.Id, Name = defaultCompany.Name };
                        if (!list.Contains(obj))
                        {
                            list.Insert(0, obj);
                        }
                    }
                }
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Permission(int companyId, int roleId, int moduleId)
        {
            var list = new { OK = "OK" };
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetCompanies()
        {

            DatabaseFactory _databaseFactory = new DatabaseFactory();
            _company = new SecCompanyService(new SecCompanyRepository(_databaseFactory), new UnitOfWork(_databaseFactory));
            if (Session["userId"] != null)
            {
                int userId = Convert.ToInt32(Session["userId"]);

                ISecCompanyUserService _compUserService = new SecCompanyUserService(new SecCompanyUserRepository(_databaseFactory), new UnitOfWork(_databaseFactory));
                var compUserList = _compUserService.GetAll();

                if (compUserList != null && compUserList.Count() > 0)
                {
                    compUserList = compUserList.Where(i => i.SecUserId == userId).ToList();
                    if (compUserList != null && compUserList.Count() > 0)
                    {
                        var companyIds = compUserList.Select(i => i.SecCompanyId).ToList();

                        var permittedCompanyList = _company.GetAll();
                        IList<SecCompany> result = permittedCompanyList.Where(b => companyIds.Contains(b.Id)).ToList();

                        List<CompanyIdAndName> list = GetCompanyList(result);
                        return Json(list, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        //default company
                        IList<SecCompany> defaultCompanyList = new List<SecCompany>();
                        var defaultCompany = _company.GetById(1);
                        defaultCompanyList.Add(defaultCompany);
                        List<CompanyIdAndName> list = GetCompanyList(defaultCompanyList);
                        return Json(list, JsonRequestBehavior.AllowGet);
                    }
                }

                //var a = _company.GetModuleIdAndName();
                //List<CompanyIdAndName> list = new List<CompanyIdAndName>();
                //foreach (var item in a)
                //{
                //    CompanyIdAndName company = new CompanyIdAndName { Id = item.Key, Name = item.Value };
                //    list.Add(company);
                //}
                //return Json(list, JsonRequestBehavior.AllowGet);

                else
                {
                    //default company
                    IList<SecCompany> defaultCompanyList = new List<SecCompany>();
                    var defaultCompany = _company.GetById(1);
                    defaultCompanyList.Add(defaultCompany);
                    List<CompanyIdAndName> list = GetCompanyList(defaultCompanyList);
                    return Json(list, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                Operation ret = new Operation();
                ret.Success = false;
                ret.Message = "Unauthenticated user ";
                ret.OperationId = -1;
                return Json(ret, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public bool SetCompanyId(CompanyId c)
        {
            if (c.Id != 0)
            {
                Session["companyId"] = c.Id;
            }
            else
            {
                return false;
            }
            return true;
        }
        public List<CompanyIdAndName> GetCompanyList(IList<SecCompany> result)
        {
            List<CompanyIdAndName> list = new List<CompanyIdAndName>();
            foreach (var item in result)
            {
                CompanyIdAndName company = new CompanyIdAndName { Id = item.Id, Name = item.Name };
                list.Add(company);
            }
            return list;
        }
        #region User

        [HttpGet]
        public ActionResult GetUsers()
        {
            var list = _su.GetSecUsers().Select(su => new SecUser { Id=su.Id,LoginName=su.LoginName,Status=su.Status,
                HrmEmployeeId=su.HrmEmployeeId,SecRoleId=su.SecRoleId, CreatedBy=su.CreatedBy,CreatedDate=su.CreatedDate}).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }      
       
        [HttpPost]
        public ActionResult SaveUser(SecUser secUser)
        {
            int userId = Convert.ToInt32(Session["userId"]);
            Operation objOperation = new Operation { Success = false };
            if (ModelState.IsValid)
            {
                if (secUser.Id == 0)
                {
                    secUser.CreatedBy = userId;
                    secUser.CreatedDate = DateTime.Now.Date;
                    objOperation = _su.SaveSecUser(secUser);
                }
                else
                {
                    secUser.ModifiedBy = userId;
                    secUser.ModifiedDate = DateTime.Now.Date;
                    objOperation = _su.UpdateSecUser(secUser);
                }
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult DeleteSecUser(int Id)
        {
            Operation objOperation = new Operation { Success = false };
            if (Id != 0)
            {
                SecUser obj = _su.GetById(Id);
                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _su.DeleteSecUser(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        public ActionResult GetSecUsersForCombo()
        {
            var list = _su.GetSecUsers().Select(su => new
            {
                Id=su.Id,
                Name=su.LoginName

            }).ToList();
            return Json(list,JsonRequestBehavior.AllowGet);
        
        }
        public ActionResult GetSecUsersByCompanyId()
        {
            int companyId = Convert.ToInt32(Session["companyId"]);
            var list = _su.GetSecUsers().Select(su => new
            {
                Id = su.Id,
                Name = su.LoginName

            }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);

        }
        public ActionResult PasswordChange()
        {
            return View();
        }
        /// <summary>
        /// get user object
        /// </summary>
        /// <returns></returns>
        public ActionResult GetUserById()
        {
            int userId = Convert.ToInt32(Session["UserId"]);

            var list = _su.GetSecUsers().Where(x => x.Id == userId).Select(su => new
            {
                Id = su.Id,
                LoginName = su.LoginName,
                Password = su.Password,
                Status = su.Status,
                SecRoleId = su.SecRoleId,
                CreatedBy = su.CreatedBy,
                CreatedDate = su.CreatedDate
            }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);

        }
        public ActionResult LogIn(LoginIdAndPassword login)
        {
            int ret = 0;

            SecUser user = _su.GetUserByLogin(login.LoginName);
            Operation operation = new Operation { Success = false };
            if (user != null)
            {
                if (user.Status == true)
                {
                    if (user.Password.ToUpper() == login.Password.ToUpper())
                    {
                        CustomPrincipal.Login(user.LoginName);
                        ret = 1;
                        Session["userId"] = user.Id;
                        Session["userName"] = user.LoginName;
                        //Session["userLevel"] = user.Level;
                        Session["employeeId"] = user.HrmEmployeeId;
                        Session["roleId"] = user.SecRoleId;
                        var list = _su.GetSecUsers().Where(x => x.Id == user.Id).Select(su => new
                        {
                            Success = true,
                            Id = su.Id,
                            LoginName = su.LoginName,
                            SecRoleId = su.SecRoleId,
                            EmployeeId = user.HrmEmployeeId

                        }).ToList();
                        return Json(list[0], JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var obj = new { Success = false, Id = 0, LoginName = "", SecRoleId = 0, EmployeeId = 0 };

                        return Json(obj, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    var obj = new { Success = false, Id = 0, LoginName = "", SecRoleId = 0, EmployeeId = 0 };

                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var obj = new { Success = false, Id = 0, LoginName = "", SecRoleId = 0, EmployeeId = 0 };

                return Json(obj, JsonRequestBehavior.AllowGet);
            }

        }
    
        Tuple<String, int> t = new Tuple<String, int>("Hellow", 2);
        
        #endregion
    }
}
