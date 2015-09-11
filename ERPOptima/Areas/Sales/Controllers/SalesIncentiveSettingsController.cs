using ERPOptima.Data.Common.Repository;
using ERPOptima.Data.Hrm.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Helper;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Sales;
using ERPOptima.Model.Security;
using ERPOptima.Model.ViewModel;
using ERPOptima.Model.ViewModel.Sales;
using ERPOptima.Service.Hrm;
using ERPOptima.Service.Sales;
using ERPOptima.Service.Security;
using ERPOptima.Web.Filters;
using Optima.Helper;
using Optima.ViewModel.Sales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Optima.Areas.Sales.Controllers
{
    public class SalesIncentiveSettingsController : Controller
    {
        //
        // GET: /Sales/SalesIncentiveSettings/
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Index()
        {
            return View();
        }
        private ISecCompanyService _SecCompanyService;
        private ISalesIncentiveSettingsService _salesIncentiveService;
        private ISalesOrderService<SlsSalesOrderApproval, SlsSalesOrderViewModel> _salesOrderService;
        private IIncentivePaymentService _incentivePaymentService;
        private IHrmDesignationService _hrmDesignationService;
        private IHrmEmployeeService _hrmEmployeeService;
        public SalesIncentiveSettingsController()
        {
            var dbfactory = new DatabaseFactory();
            _salesIncentiveService = new SalesIncentiveSettingsService(new SalesIncentiveSettingsRepository(dbfactory), new UnitOfWork(dbfactory));
            _salesOrderService = new SalesOrderService(new SalesOrderRepository(dbfactory),
                new SalesOrderDetailRepository(dbfactory),
                new SalesOrderApprovalRepository(dbfactory),
                new NotificationRepository(dbfactory),
                new NotificationDetailRepository(dbfactory),
                new UnitOfWork(dbfactory));
            _incentivePaymentService = new IncentivePaymentService(new IncentivePaymentRepository(dbfactory), 
                new SalesIncentiveSettingsRepository(dbfactory) ,new UnitOfWork(dbfactory));
            _hrmDesignationService = new HrmDesignationService(new HrmDesignationRepository(dbfactory), new UnitOfWork(dbfactory));
            _hrmEmployeeService = new HrmEmployeeService(new HrmEmployeeRepository(dbfactory), new UnitOfWork(dbfactory));
            _SecCompanyService = new SecCompanyService(new SecCompanyRepository(dbfactory), new UnitOfWork(dbfactory));
        }

        #region Incentive Settings
        [HttpGet]
        public ActionResult GetAll()
        {
            var list = _salesIncentiveService.GetAll();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Save(SlsIncentiveSetting incentiveSetting)// store to value for save
        {
            int userId = Convert.ToInt32(Session["userId"]);
            Operation objOperation = new Operation {  Success  = false };

            if (ModelState.IsValid)
            {
                if (incentiveSetting.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        incentiveSetting.Id = 0;//new value input
                        incentiveSetting.CreatedBy = userId;
                        incentiveSetting.CreatedDate = DateTime.Now.Date;
                        objOperation = _salesIncentiveService.Save(incentiveSetting);
                    }
                    else { objOperation.OperationId = -1; }

                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        incentiveSetting.ModifiedBy = userId;
                        incentiveSetting.ModifiedDate = DateTime.Now.Date;
                        objOperation = _salesIncentiveService.Update(incentiveSetting);
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
                SlsIncentiveSetting obj = _salesIncentiveService.GetById(Id);

                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _salesIncentiveService.Delete(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }
        #endregion

        #region Incentive Payment
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Payment()
        {
            return View();
        }
                
        [HttpGet]
        public ActionResult GetDueIncentives(int year, int month, int salesPersonId)
        {
            decimal netSales = _salesOrderService.GetNetSales(year, month, salesPersonId);
            decimal rateOfIncentive = _salesIncentiveService.GetIncentiveRate(netSales);
            //As per discussion with TL, for now sales * rate to calculate due.
            decimal totalIncentive = (netSales * rateOfIncentive) / 100;

            return Json(new { result = totalIncentive }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetAllIncentivePayment(int year=0, int month=0, int salesPersonId=0)
        {
            //when user selects all 3 of employee, year and month
            var list = GetAllIncentives(year, month, salesPersonId);
            list = list.OrderByDescending(i => i.PaymentDate).ToList();  //order by payment date
            return Json(list, JsonRequestBehavior.AllowGet);            
        }

        public IList<SlsIncentive> GetAllIncentives(int year=0, int month=0, int salesPersonId=0)
        {
            var list = _incentivePaymentService.GetAll();
            if (salesPersonId > 0)
                list = list.Where(i => i.HrmEmployeeId == salesPersonId).ToList();
            if (year > 0)
                list = list.Where(i => i.Year == year).ToList();
            if (month > 0)
                list = list.Where(i => i.Month == month).ToList();

            return list;
        }

        [HttpGet]
        public ActionResult GetAllSalesReprasentatives()
        {
            int companyId = Convert.ToInt32(Session["companyId"]);
            var designations = _hrmDesignationService.GetAll();
            int srId = designations.Where(i => i.ShortName.ToLower() == "sr").Select(i => i.Id).FirstOrDefault();

            var salesPersons = _hrmEmployeeService.GetAllEmployee(companyId).Where(i=>i.HrmDesignationId == srId).ToList();
            var result = salesPersons.Select(i => new 
            {
                Id = i.Id,
                Name = i.Name
            }).Distinct().ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SavePayment(SlsIncentive obj)
        {
            int userId = Convert.ToInt32(Session["userId"]);
            Operation objOperation = new Operation { Success = false };
            
            if (ModelState.IsValid)
            {
                if (obj.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {

                        obj.CreatedBy = userId;
                        obj.CreatedDate = DateTime.Now.Date;
                        obj.PaymentDate = DateTime.Now.Date;
                        objOperation = _incentivePaymentService.Save(obj);
                    }
                    else { objOperation.OperationId = -1; }

                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        obj.ModifiedBy = userId;
                        obj.ModifiedDate = DateTime.Now.Date;
                        obj.PaymentDate = DateTime.Now.Date;
                        objOperation = _incentivePaymentService.Update(obj);
                    }
                    else { objOperation.OperationId = -2; }
                }
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult DeletePayment(int Id)
        {
            Operation objOperation = new Operation { Success = false };

            if (Id != 0)
            {
                SlsIncentive obj = _incentivePaymentService.GetById(Id);

                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _incentivePaymentService.Delete(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        #region Report
        
        public ActionResult ShowIncentiveReport(int salesPersonId = 0, int year = 0, int month = 0)
        {
            var list = GetAllIncentives(year, month, salesPersonId);
            int companyId = Convert.ToInt32(Session["companyId"]);
            var employeeList = _hrmEmployeeService.GetAllEmployee(companyId);
            var designationList = _hrmDesignationService.GetAll();
            
            var result = list.Select(i => new RptSlsIncentiveViewModel()
            {
                Id = i.Id,
                HrmEmployeeId = i.HrmEmployeeId,
                Year = i.Year,
                Month = i.Month,
                Commission = i.Commission,
                AmountPaid = i.AmountPaid,
                PaymentDate = i.PaymentDate,
                Remarks = i.Remarks,
                CreatedBy = i.CreatedBy,
                CreatedDate = i.CreatedDate,
                ModifiedBy = i.ModifiedBy == null ? 0 : (int)i.ModifiedBy,
                ModifiedDate = i.ModifiedDate == null ? DateTime.MinValue : (DateTime)i.ModifiedDate,
                MonthName = DateUtil.DisplayMonthName(i.Month),
                EmployeeName = employeeList.Where(j=>j.Id == i.HrmEmployeeId).FirstOrDefault().Name                
            }).Distinct().ToList();

            foreach(var item in result)
            {
                int? empId = employeeList.Where(j =>
                    j.Id == item.HrmEmployeeId).FirstOrDefault().HrmDesignationId;
                if(empId != null)
                item.EmployeeDesignation = designationList.Where(d => d.Id == (int)empId).FirstOrDefault().Name;
            }

            string filepath = string.Empty;
            filepath = ReportUtil.GetSalesReportPath("IncentiveReport.rpt");
            
            #region ParameterListPreperation
            List<ReportParameter> paramList = new List<ReportParameter>();
            //int companyId = Convert.ToInt32(Session["companyId"]);
            SecCompany objCmnCompany = _SecCompanyService.GetById(companyId);
            string companyName = objCmnCompany.Name;
            string companyAddress = objCmnCompany.Address;
            string reportName = "Sales Incentive Report";

            ReportParameter objcmpName = new ReportParameter();
            objcmpName.Name = "Company";
            objcmpName.Value = companyName;
            paramList.Add(objcmpName);

            ReportParameter objcmpAddress = new ReportParameter();
            objcmpAddress.Name = "CompanyAddress";
            objcmpAddress.Value = companyAddress;
            paramList.Add(objcmpAddress);

            ReportParameter objReportName = new ReportParameter();
            objReportName.Name = "ReportName";
            objReportName.Value = reportName;
            paramList.Add(objReportName);

            #endregion ParameterListPreperation

            if (result != null && result.Count > 0 && filepath != string.Empty)
            {
                return new CrystalReportResult(filepath, result, paramList);
            }
            else
            {
                return RedirectToAction("SalesIncentive", "Report", new { area = "Sales" });
            }
        }
        #endregion

        #endregion

    }
}
