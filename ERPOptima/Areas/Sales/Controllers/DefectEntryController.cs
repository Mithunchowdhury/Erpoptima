using ERPOptima.Data.Common.Repository;
using ERPOptima.Data.Hrm.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Sales;
using ERPOptima.Model.Security;
using ERPOptima.Service.Hrm;
using ERPOptima.Service.Sales;
using ERPOptima.Service.Security;
using ERPOptima.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Optima.Areas.Sales.Controllers
{
    public class DefectEntryController : Controller
    {
        private IDefectEntryService _IDefectEntryService;
        private IDefectDetailEntryService _IDefectDetailService;
        private ISecCompanyService _SecCompanyService;
        private IOfficeService _officeService;
        private IHrmEmployeeService _hrmEmployeeService;

        int userId = 0;
        int companyId = 0;
        public DefectEntryController()
        {
               var dbfactory = new DatabaseFactory();
               _IDefectEntryService = new DefectEntryService(new DefectEntryRepository(dbfactory), new UnitOfWork(dbfactory));
            _IDefectDetailService = new DefectDetailEntryService(new DefectDetailEntryRepository(dbfactory), new UnitOfWork(dbfactory));
            _SecCompanyService = new SecCompanyService(new SecCompanyRepository(dbfactory), new UnitOfWork(dbfactory));
            _officeService = new OfficeService(new OfficeRepository(dbfactory), new UnitOfWork(dbfactory));
            _hrmEmployeeService = new HrmEmployeeService(new HrmEmployeeRepository(dbfactory), new UnitOfWork(dbfactory));
        }
        //
        // GET: /Sales/DefectEntry/
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetAutoNumber(int companyId, int employeeId)
        {
            SecCompany objCmnCompany = _SecCompanyService.GetById(companyId);
            SlsOffice office = _officeService.GetById((int)_hrmEmployeeService.GetById(employeeId).SlsOfficeId);
            var autoNumber = _IDefectEntryService.GetLastCode(companyId, objCmnCompany.Prefix, office.Code);
            return Json(new { Refno = autoNumber }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Save(SlsDefect slsDefect)
        {
            int companyId = Convert.ToInt32(Session["companyId"]);
            int userId = Convert.ToInt32(Session["userId"]);

            Operation objOperation = new Operation { Success = false };
            if (ModelState.IsValid && slsDefect != null)
            {
                if (slsDefect.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        slsDefect.SecCompanyId = companyId;
                        slsDefect.CreatedBy = userId;
                        slsDefect.CreatedDate = DateTime.Now;
                        //invDamage.InvDamageDetails = null;
                        objOperation = _IDefectEntryService.Save(slsDefect);
                    }
                }
                else
                {
                    
                }
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

    }
}
