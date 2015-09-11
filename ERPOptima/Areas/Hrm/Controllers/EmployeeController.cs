using ERPOptima.Data.Hrm.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Lib.Model;
using ERPOptima.Model.HRM;
using ERPOptima.Service.Hrm;
using ERPOptima.Web.Filters;
using Optima.Areas.Hrm.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Optima.Areas.Hrm.Controllers
{
    public class EmployeeController : Controller
    {
        //
        // GET: /Hrm/Employee/
        private IHrmEmployeeService _hrmEmployeeService;
        public EmployeeController()
        {
            var dbfactory = new DatabaseFactory();
            _hrmEmployeeService = new HrmEmployeeService(new HrmEmployeeRepository(dbfactory), new UnitOfWork(dbfactory));
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]

        public ActionResult Index()
        {
            return View();
        }
        #region Employee

        [HttpGet]
        public ActionResult GetAll()
        {
            int companyId = Convert.ToInt32(Session["companyId"]);
            Collection<HrmEmployeeViewModel> employees = null;
            DataTable dt = _hrmEmployeeService.GetAll(companyId);
            if (dt != null)
            {
                employees = new Collection<HrmEmployeeViewModel>();
                foreach (DataRow row in dt.Rows)
                {
                    employees.Add((HrmEmployeeViewModel)ERPOptima.Lib.Utilities.Helper.FillTo(row, typeof(HrmEmployeeViewModel)));
                }
            }

            return Json(employees, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetById(int id)
        {
            var list = _hrmEmployeeService.GetById(id);//.Select(ac => new
            //{
            //    Id = ac.Id,
            //    Name = ac.Name,
            //    Code = ac.Code,
            //    Location = ac.Location
            //});
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Save(HrmEmployeeViewModel hrmEmployeeViewModel)
        {
            Operation objOperation = new Operation { Success = false };
            int companyId = Convert.ToInt32(Session["companyId"]);
            int userId = Convert.ToInt32(Session["userId"]);
            if (ModelState.IsValid)
            {
                HrmEmployee hrmEmployee = new HrmEmployee
                {
                    Id = hrmEmployeeViewModel.Id,
                    Name = hrmEmployeeViewModel.Name,
                    Address = hrmEmployeeViewModel.Address,
                    HrmDesignationId = hrmEmployeeViewModel.HrmDesignationId,
                    Email = hrmEmployeeViewModel.Email,
                    Phone = hrmEmployeeViewModel.Phone,
                    HrmDepartmentId = hrmEmployeeViewModel.HrmDepartmentId,
                    LineManager = hrmEmployeeViewModel.LineManager,
                    SlsOfficeId = hrmEmployeeViewModel.SlsOfficeId,
                    SecCompanyId = companyId,
                    CreatedBy = hrmEmployeeViewModel.CreatedBy,
                    CreatedDate = hrmEmployeeViewModel.CreatedDate
                };
                if (hrmEmployee.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        hrmEmployee.CreatedBy = userId;
                        hrmEmployee.CreatedDate = DateTime.Now.Date;
                        objOperation = _hrmEmployeeService.Save(hrmEmployee);
                    }
                    else { objOperation.OperationId = -1; }
                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        hrmEmployee.ModifiedBy = userId;
                        hrmEmployee.ModifiedDate = DateTime.Now.Date;
                        objOperation = _hrmEmployeeService.Update(hrmEmployee);
                    }
                    else { objOperation.OperationId = -2; }

                }
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            Operation objOperation = new Operation { Success = false };

            if (Id != 0)
            {
                HrmEmployee obj = _hrmEmployeeService.GetById(Id);

                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _hrmEmployeeService.Delete(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        #endregion

    }
}
