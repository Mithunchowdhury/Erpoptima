using ERPOptima.Data.Common.Repository;
using ERPOptima.Data.Hrm.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales;
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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace Optima.Areas.Sales.Controllers
{
    public class FieldVisitController : Controller
    {

        private IFieldVisitListService _fieldVisitListService;
        private IFieldVisitService _fieldVisitService;
        private ISecCompanyService _SecCompanyService;
        private ISecUserService _SecUserService;
        private IOfficeService _officeService;
        private IHrmEmployeeService _hrmEmployeeService;
        public FieldVisitController()
        {
            var dbfactory = new DatabaseFactory();
            _fieldVisitListService = new FieldVisitListService(new FieldVisitListRepository(dbfactory), new UnitOfWork(dbfactory));
            _fieldVisitService = new FieldVisitService(new FieldVisitRepository(dbfactory), new UnitOfWork(dbfactory));
            _SecCompanyService = new SecCompanyService(new SecCompanyRepository(dbfactory), new UnitOfWork(dbfactory));
            _SecUserService = new SecUserService(new ERPOptima.Data.Security.Repository.SecUserRepository(dbfactory), new UnitOfWork(dbfactory));
            _officeService = new OfficeService(new OfficeRepository(dbfactory), new UnitOfWork(dbfactory));
            _hrmEmployeeService = new HrmEmployeeService(new HrmEmployeeRepository(dbfactory), new UnitOfWork(dbfactory));
        }
        //
        // GET: /Sales/FieldVisit/
        [AuthorizeUser]
        [ResourcePermissionAttribute]

        public ActionResult Index()
        {

            return View();
        }




        [AuthorizeUser]
        [ResourcePermissionAttribute]

        public ActionResult List()
        {
            return View();
        }

        public ActionResult GetFieldVisitList(int employeeId, DateTime? startDate, DateTime? endDate)
        {
            var list = _fieldVisitListService.GetFieldVisitList(employeeId, startDate, endDate).ToList();
            list = list.OrderByDescending(i => i.VisitDate).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult getAutoNumber(int companyId, int employeeId)
        {
            SecCompany objCmnCompany = _SecCompanyService.GetById(companyId);
            SlsOffice office = _officeService.GetById((int)_hrmEmployeeService.GetById(employeeId).SlsOfficeId);           
            var autoNumber = _fieldVisitService.getAutoNumber(objCmnCompany.Prefix, office.Code);
            return Json(new { Refno = autoNumber }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetRefNo()
        {
            var autoNumber = GenerateRefNo();
            return Json(new { Refno = autoNumber }, JsonRequestBehavior.AllowGet);
        }
        public string GenerateRefNo()
        {
            int companyId = 0;
            int employeeId = 0;
            companyId = int.Parse(Session["companyId"].ToString());
            int userId = int.Parse(Session["userId"].ToString());

            SecCompany objCmnCompany = _SecCompanyService.GetById(companyId);
            SecUser usr = _SecUserService.GetById(userId);
            employeeId = (int)usr.HrmEmployeeId.Value;
            SlsOffice office = _officeService.GetById((int)_hrmEmployeeService.GetById(employeeId).SlsOfficeId);
            var autoNumber = _fieldVisitService.getAutoNumber(objCmnCompany.Prefix, office.Code);
            return autoNumber;
        }
        public string GenerateRefNo(int cmpId,int usrId)
        {
            int companyId = 0;
            int employeeId = 0;
            companyId = cmpId;
            int userId = usrId;

            SecCompany objCmnCompany = _SecCompanyService.GetById(companyId);
            SecUser usr = _SecUserService.GetById(userId);
            employeeId = (int)usr.HrmEmployeeId.Value;
            SlsOffice office = _officeService.GetById((int)_hrmEmployeeService.GetById(employeeId).SlsOfficeId);
            var autoNumber = _fieldVisitService.getAutoNumber(objCmnCompany.Prefix, office.Code);
            return autoNumber;
        }
        //[HttpGet]
        //public ActionResult GetAll()
        //{
        //    var list = _fieldVisitListService.GetAll();
        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}
        //public ActionResult GetById(int Id)
        //{
        //    var list = _fieldVisitListService.GetAll().Where(i => i.HrmEmployeeId == Id).ToList();
        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}


        [HttpPost]
        public ActionResult Save(SlsFieldVisit fieldVisit)
        {
            int userId = Convert.ToInt32(Session["userId"]);
            Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid)
            {
                if (fieldVisit.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        fieldVisit.RefNo = this.GenerateRefNo();
                        fieldVisit.CreatedBy = userId;

                        fieldVisit.CreatedDate = DateTime.Now.Date;
                        // fieldVisit.FollowupDate = DateTime.Now.Date;
                        fieldVisit.VisitDate = DateTime.Now.Date;
                        SecUser usr = _SecUserService.GetById(userId);
                        var employeeId = (int)usr.HrmEmployeeId.Value;
                        fieldVisit.HrmEmployeeId = employeeId;
                        objOperation = _fieldVisitService.Save(fieldVisit);
                    }
                    else { objOperation.OperationId = -1; }

                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        fieldVisit.ModifiedBy = userId;
                        fieldVisit.ModifiedDate = DateTime.Now.Date;
                        objOperation = _fieldVisitService.Update(fieldVisit);
                    }
                    else { objOperation.OperationId = -2; }
                }
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult SaveFromAndroid(SlsFieldVisit fieldVisit,int cmpId,int userId)
        {
            //var memstream = new MemoryStream();
            //Request.InputStream.Position = 0;
            //Request.InputStream.CopyTo(memstream);
            //memstream.Position = 0;
            //using (StreamReader reader = new StreamReader(memstream))
            //{
            //    var text = reader.ReadToEnd();
            //    Debug.WriteLine(text);
            //}
            
            Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid)
            {
                if (fieldVisit.Id == 0)
                {

                    fieldVisit.RefNo = this.GenerateRefNo(cmpId,userId);
                    fieldVisit.CreatedBy = userId;
                    fieldVisit.CreatedDate = DateTime.Now.Date;
                    fieldVisit.VisitDate = DateTime.Now.Date;
                    SecUser usr = _SecUserService.GetById(userId);
                    var employeeId = (int)usr.HrmEmployeeId.Value;
                    fieldVisit.HrmEmployeeId = employeeId;
                    objOperation = _fieldVisitService.Save(fieldVisit);

                }
            
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        public ActionResult Delete(int Id)
        {
            Operation objOperation = new Operation { Success = false };

            if (Id != 0)
            {
                SlsFieldVisit obj = _fieldVisitService.GetById(Id);

                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _fieldVisitService.Delete(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }
        
    }
}
