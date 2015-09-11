using ERPOptima.Data.Hrm.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Lib.Model;
using ERPOptima.Model.HRM;
using ERPOptima.Service.Hrm;
using ERPOptima.Web.Filters;
using Optima.Areas.Hrm.ViewModel;
using Optima.Areas.Security.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Optima.Areas.Hrm.Controllers
{
    public class DepartmentController : Controller
    {
        //
        // GET: /Hrm/Department/
        private IHrmDepartmentService _departmentService;

        public DepartmentController()
        {
            var dbfactory = new DatabaseFactory();
            _departmentService = new HrmDepartmentService(new HrmDepartmentRepository(dbfactory), new UnitOfWork(dbfactory));
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]

        public ActionResult Index()
        {
            return View();
        }
        #region Department

        [HttpGet]
        public ActionResult GetAll()
        {
            Collection<HrmDepartmentViewModel> departments = null;
            DataTable dt = _departmentService.GetAll();
            if (dt != null)
            {
                departments = new Collection<HrmDepartmentViewModel>();
                foreach (DataRow row in dt.Rows)
                {
                    departments.Add((HrmDepartmentViewModel)ERPOptima.Lib.Utilities.Helper.FillTo(row, typeof(HrmDepartmentViewModel)));
                }
            }
            return Json(departments, JsonRequestBehavior.AllowGet);
        }

       

        [HttpPost]
        public ActionResult Save(HrmDepartment hrmDepartment)
        {
            int userId = Convert.ToInt32(Session["userId"]);
            Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid)
            {
                if (hrmDepartment.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        hrmDepartment.CreatedBy = userId;
                        hrmDepartment.CreatedDate = DateTime.Now.Date;
                        objOperation = _departmentService.Save(hrmDepartment);
                    }
                    else { objOperation.OperationId = -1; }

                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        hrmDepartment.ModifiedBy = userId;
                        hrmDepartment.ModifiedDate = DateTime.Now.Date;
                        objOperation = _departmentService.Update(hrmDepartment);
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
                HrmDepartment obj = _departmentService.GetById(Id);

                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _departmentService.Delete(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        #endregion
    }
}
