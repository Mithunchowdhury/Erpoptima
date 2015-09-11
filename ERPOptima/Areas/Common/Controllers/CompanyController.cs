using ERPOptima.Data.Common.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Security.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Common;
using ERPOptima.Model.Security;
using ERPOptima.Service.Common;
using ERPOptima.Service.Security;
using ERPOptima.Web.Filters;
using Optima.Areas.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Optima.Areas.Common.Controllers
{
    [Authorize]
    public class CompanyController : Controller
    {
        //
        // GET: /Common/Company/

        private ISecCompanyService _company;
        private ICmnFinancialYearService _financialYear;
        private ISecGroupService _groupService;
        private DatabaseFactory _databaseFactory= new DatabaseFactory();
        public CompanyController()
        {
            _company = new SecCompanyService(new SecCompanyRepository(_databaseFactory), new UnitOfWork(_databaseFactory));
            _financialYear = new CmnFinancialYearService(new CmnFinancialYearRepository(_databaseFactory), new UnitOfWork(_databaseFactory));
            _groupService = new SecGroupService(new SecGroupRepository(_databaseFactory), new UnitOfWork(_databaseFactory));

        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetCopmanyIdAndName()
        {
            var a = _company.GetModuleIdAndName();
            List<CompanyIdAndName> list = new List<CompanyIdAndName>();
            foreach (var item in a)
            {
                CompanyIdAndName company = new CompanyIdAndName { Id = item.Key, Name = item.Value };
                list.Add(company);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public bool SetCompanyId(CompanyId c)
        {
            if (c.Id != 0)
            {
                Session["companyId"] = c.Id;
            }
            return true;
        }
        [HttpPost]
        public ContentResult Upload(HttpPostedFileBase file)
        {
            var filename = Path.GetFileName(file.FileName);
            var path = Path.Combine(Server.MapPath("~/uploads"), filename);
            file.SaveAs(path);

            return new ContentResult
            {
                ContentType = "text/plain",
                Content = filename,
                ContentEncoding = Encoding.UTF8
            };
        }
        [HttpPost]
        public ActionResult SaveCmnCompany(SecCompany secCompany)
        {
            Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid)
            {
                secCompany.SecGroupId = 1;
                if (secCompany.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        secCompany.CreatedBy = Convert.ToInt32(Session["userId"]);
                        secCompany.CreatedDate = DateTime.Now.Date;
                        objOperation = _company.SaveCmnCompany(secCompany);
                    }
                    else { objOperation.OperationId = -1; }
                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        secCompany.ModifiedBy = Convert.ToInt32(Session["userId"]);
                        secCompany.ModifiedDate = DateTime.Now.Date;
                        objOperation = _company.UpdateCmnCompany(secCompany);
                    }
                    else { objOperation.OperationId = -2; }
                }
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult DeleteCmnCompany(int Id)
        {
            Operation objOperation = new Operation { Success = false };

            if (Id != 0)
            {
                SecCompany obj = _company.GetById(Id);

                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _company.DeleteCmnCompany(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }


        public ActionResult GetCmnCompanies()
        {

            var list = _company.GetCmnCompanies();

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CompaniesForUser()
        {
            ISecUserService _su;
            _su = new SecUserService(new SecUserRepository(_databaseFactory), new UnitOfWork(_databaseFactory));
            if (Session["userId"] != null)
            {
                int userId = Convert.ToInt32(Session["userId"]);
                SecUser user = _su.GetById(userId);
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
                        //Default compnay
                        if (user.HrmEmployeeId != null)
                        {
                            ERPOptima.Model.HRM.HrmEmployee emp = new ERPOptima.Model.HRM.HrmEmployee();
                            ERPOptima.Service.Hrm.HrmEmployeeService es = new ERPOptima.Service.Hrm.HrmEmployeeService(new ERPOptima.Data.Hrm.Repository.HrmEmployeeRepository(_databaseFactory), new UnitOfWork(_databaseFactory));
                            emp = es.GetById(user.HrmEmployeeId.Value);
                            if (emp != null)
                            {

                                _company = new SecCompanyService(new SecCompanyRepository(_databaseFactory), new UnitOfWork(_databaseFactory));
                                var defaultCompany = _company.GetById(emp.SecCompanyId.Value);
                                if (defaultCompany != null)
                                {
                                    CompanyIdAndName obj = new CompanyIdAndName();
                                    obj.Id = defaultCompany.Id;
                                    obj.Name = defaultCompany.Name;
                                    if (!list.Contains(obj))
                                    {
                                        list.Insert(0, obj);
                                    }
                                }
                            }
                        }
                        return Json(list, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        List<CompanyIdAndName> list = new List<CompanyIdAndName>();
                        //default company
                        if (user.HrmEmployeeId != null)
                        {
                            ERPOptima.Model.HRM.HrmEmployee emp = new ERPOptima.Model.HRM.HrmEmployee();
                            ERPOptima.Service.Hrm.HrmEmployeeService es = new ERPOptima.Service.Hrm.HrmEmployeeService(new ERPOptima.Data.Hrm.Repository.HrmEmployeeRepository(_databaseFactory), new UnitOfWork(_databaseFactory));
                            emp = es.GetById(user.HrmEmployeeId.Value);
                            if (emp != null)
                            {

                                _company = new SecCompanyService(new SecCompanyRepository(_databaseFactory), new UnitOfWork(_databaseFactory));
                                var defaultCompany = _company.GetById(emp.SecCompanyId.Value);
                                if (defaultCompany != null)
                                {
                                    CompanyIdAndName obj = new CompanyIdAndName();
                                    obj.Id = defaultCompany.Id;
                                    obj.Name = defaultCompany.Name;
                                    if (!list.Contains(obj))
                                    {
                                        list.Insert(0, obj);
                                    }
                                }
                            }
                        }
                     
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
                    //IList<SecCompany> defaultCompanyList = new List<SecCompany>();
                    //var defaultCompany = _company.GetById(1);
                    //defaultCompanyList.Add(defaultCompany);
                    //List<CompanyIdAndName> list = GetCompanyList(defaultCompanyList);
                    List<CompanyIdAndName> list = new List<CompanyIdAndName>();
                    //default company
                    if (user.HrmEmployeeId != null)
                    {
                        ERPOptima.Model.HRM.HrmEmployee emp = new ERPOptima.Model.HRM.HrmEmployee();
                        ERPOptima.Service.Hrm.HrmEmployeeService es = new ERPOptima.Service.Hrm.HrmEmployeeService(new ERPOptima.Data.Hrm.Repository.HrmEmployeeRepository(_databaseFactory), new UnitOfWork(_databaseFactory));
                        emp = es.GetById(user.HrmEmployeeId.Value);
                        if (emp != null)
                        {

                            _company = new SecCompanyService(new SecCompanyRepository(_databaseFactory), new UnitOfWork(_databaseFactory));
                            var defaultCompany = _company.GetById(emp.SecCompanyId.Value);
                            if (defaultCompany != null)
                            {
                                CompanyIdAndName obj = new CompanyIdAndName();
                                obj.Id = defaultCompany.Id;
                                obj.Name = defaultCompany.Name;
                                if (!list.Contains(obj))
                                {
                                    list.Insert(0, obj);
                                }
                            }
                        }
                    }
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
        [HttpGet]
        public string GetCompanies()
        {
            var str = "";
            if (Session["userId"] != null)
            {
                str = "{\"companies\": [{\"Id\":1,\"Name\":\"MEP Energy Saving lamps industries Limited\"},{\"Id\":2,\"Name\":\"MEP Limited\"}] }";
                //return Json("Companies:" + list.ToList(), JsonRequestBehavior.AllowGet);
               
            }
            return str;
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
       
    }
}