using ERPOptima.Data.Common.Repository;
using ERPOptima.Data.Hrm.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Helper;
using ERPOptima.Model.Sales;
using ERPOptima.Model.Security;
using ERPOptima.Model.ViewModel;
using ERPOptima.Service.Hrm;
using ERPOptima.Service.Sales;
using ERPOptima.Service.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Optima.Areas.Sales.Controllers
{
    public class DashboardController : Controller
    {
        string[] styles = new string[10];
        Random randomGen = new Random();
        private ISalesTargetService _SalesTargetService;
        private ICollectionTargetService _collectionTargetService;
        private ISalesOrderService<SlsSalesOrderApproval, SlsSalesOrderViewModel> _SalesOrderService;
        private ICollectionEntryService _collectionEntryService;
        private IHrmEmployeeService _hrmEmployeeService;
        private ISecCompanyService _companyService;
        public DashboardController()
        {
            InitStyle();
            randomGen = new Random();

            var dbfactory = new DatabaseFactory();
            var newUoW = new UnitOfWork(dbfactory);

            _SalesTargetService = new SalesTargetService(new SalesTargetRepository(dbfactory), newUoW);
            _SalesOrderService = new SalesOrderService(new SalesOrderRepository(dbfactory), new SalesOrderDetailRepository(dbfactory),
                new SalesOrderApprovalRepository(dbfactory), new NotificationRepository(dbfactory),
                new NotificationDetailRepository(dbfactory), newUoW);
            _collectionTargetService = new CollectionTargetService(new CollectionTargetRepository(dbfactory), newUoW);
            _collectionEntryService = new CollectionEntryService(new CollectionEntryRepository(dbfactory), newUoW);
            _hrmEmployeeService = new HrmEmployeeService(new HrmEmployeeRepository(dbfactory), newUoW);
            _companyService = new SecCompanyService(new SecCompanyRepository(dbfactory), newUoW);
        }

        /// <summary>
        /// Daily Target & Sales of Company
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetDataDTSalesCompany()
        {
            decimal dailyTarget = 0;
            decimal dailySales = 0;
            string todaysDate = "";
            int companyId = Convert.ToInt32(Session["companyId"]);
            DateTime today = DateTime.Now;
            todaysDate = today.ToString("dd/MM/yyyy");

            int days = today.Day;
            int noOfFridays = 0;
            DateTime firstDay = new DateTime(today.Year, today.Month, 1);
            noOfFridays = DateUtility.CountWeekEnds(firstDay, today, DayOfWeek.Friday);

            int activeDays = days - noOfFridays;

            IList<TargetList> targetList = _SalesTargetService.GetTargetList(companyId);
            IList<SlsSalesOrderViewModel> orderList = _SalesOrderService.GetAll().ToList();

            if (targetList != null && targetList.Count() > 0)
            {
                TargetList target = targetList.Where(i => i.SecCompanyId == companyId && i.Year == today.Year &&
                    i.Month == today.Month).FirstOrDefault();
                if (target != null && target.TargetSalesAmount != null)
                {
                    dailyTarget = (decimal)target.TargetSalesAmount / activeDays;
                }
            }
            
            if (orderList != null && orderList.Count() > 0)
            {
                 IList<SlsSalesOrderViewModel> orders = orderList.Where(i => i.SecCompnayId == companyId && i.OrderDate != null &&
                   ((DateTime)i.OrderDate).Year == today.Year &&
                    ((DateTime)i.OrderDate).Month == today.Month && ((DateTime)i.OrderDate).Day == today.Day).ToList();
                                 
                if (orders != null && orders.Count() > 0)
                {
                    decimal totalOrder = orders.Sum(i=>i.Total);
                    dailySales = totalOrder;// / activeDays;
                }
            }

            ChartViewModel target1 = new ChartViewModel() { label = todaysDate, y = decimal.Round(dailyTarget, 2), legendText="", per=0 };
            //ChartViewModel target2 = new ChartViewModel() { label = "28/04/2015", y = 3000, legendText = "", per = 0 };

            ChartViewModel sales1 = new ChartViewModel() { label = todaysDate, y = decimal.Round(dailySales, 2), legendText = "", per = 0 };
            //ChartViewModel sales2 = new ChartViewModel() { label = "28/04/2015", y = 2000, legendText = "", per = 0 };

            ChartTargetSales chartObj = new ChartTargetSales();
            chartObj.Targets = new List<ChartViewModel>();
            chartObj.Sales = new List<ChartViewModel>();
            chartObj.Targets.Add(target1);
            //chartObj.Targets.Add(target2);

            chartObj.Sales.Add(sales1);
            //chartObj.Sales.Add(sales2);

            return Json(chartObj, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetDataDTSalesCompanyFromAndroid(int cmpId)
        {
            decimal dailyTarget = 0;
            decimal dailySales = 0;
            string todaysDate = "";
            int companyId = cmpId;
            DateTime today = DateTime.Now;
            todaysDate = today.ToString("dd/MM/yyyy");

            int days = today.Day;
            int noOfFridays = 0;
            DateTime firstDay = new DateTime(today.Year, today.Month, 1);
            noOfFridays = DateUtility.CountWeekEnds(firstDay, today, DayOfWeek.Friday);

            int activeDays = days - noOfFridays;

            IList<TargetList> targetList = _SalesTargetService.GetTargetList(companyId);
            IList<SlsSalesOrderViewModel> orderList = _SalesOrderService.GetAll().ToList();

            if (targetList != null && targetList.Count() > 0)
            {
                TargetList target = targetList.Where(i => i.SecCompanyId == companyId && i.Year == today.Year &&
                    i.Month == today.Month).FirstOrDefault();
                if (target != null && target.TargetSalesAmount != null)
                {
                    dailyTarget = (decimal)target.TargetSalesAmount / activeDays;
                }
            }

            if (orderList != null && orderList.Count() > 0)
            {
                IList<SlsSalesOrderViewModel> orders = orderList.Where(i => i.SecCompnayId == companyId && i.OrderDate != null &&
                  ((DateTime)i.OrderDate).Year == today.Year &&
                   ((DateTime)i.OrderDate).Month == today.Month && ((DateTime)i.OrderDate).Day == today.Day).ToList();

                if (orders != null && orders.Count() > 0)
                {
                    decimal totalOrder = orders.Sum(i => i.Total);
                    dailySales = totalOrder;// / activeDays;
                }
            }

            ChartViewModel target1 = new ChartViewModel() { label = todaysDate, y = decimal.Round(dailyTarget, 2), legendText = "", per = 0 };
            //ChartViewModel target2 = new ChartViewModel() { label = "28/04/2015", y = 3000, legendText = "", per = 0 };

            ChartViewModel sales1 = new ChartViewModel() { label = todaysDate, y = decimal.Round(dailySales, 2), legendText = "", per = 0 };
            //ChartViewModel sales2 = new ChartViewModel() { label = "28/04/2015", y = 2000, legendText = "", per = 0 };

            ChartTargetSales chartObj = new ChartTargetSales();
            chartObj.Targets = new List<ChartViewModel>();
            chartObj.Sales = new List<ChartViewModel>();
            chartObj.Targets.Add(target1);
            //chartObj.Targets.Add(target2);

            chartObj.Sales.Add(sales1);
            //chartObj.Sales.Add(sales2);

            return Json(chartObj, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Daily Target & Collection of Company
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetDataDTCollectionCompany()
        {
            decimal dailyTarget = 0;
            decimal dailyCollection = 0;
            string todaysDate = "";
            int companyId = Convert.ToInt32(Session["companyId"]);
            DateTime today = DateTime.Now;
            todaysDate = today.ToString("dd/MM/yyyy");

            int days = today.Day;
            int noOfFridays = 0;
            DateTime firstDay = new DateTime(today.Year, today.Month, 1);
            noOfFridays = DateUtility.CountWeekEnds(firstDay, today, DayOfWeek.Friday);

            int activeDays = days - noOfFridays;

            IList<SlsCollectionTarget> targetList = _collectionTargetService.GetAll(companyId);
            IList<SlsCollectionViewModel> collectionList = _collectionEntryService.GetAll().ToList();

            if (targetList != null && targetList.Count() > 0)
            {
                SlsCollectionTarget target = targetList.Where(i => i.SecCompanyId == companyId && i.Year == today.Year &&
                    i.Month == today.Month).FirstOrDefault();
                if (target != null && target.TargetCollectionAmount != null)
                {
                    dailyTarget = (decimal)target.TargetCollectionAmount / activeDays;
                }
            }

            if (collectionList != null && collectionList.Count() > 0)
            {
                IList<SlsCollectionViewModel> collections = collectionList.Where(i => i.SecCompanyId == companyId && i.CollectionDate != null &&
                  ((DateTime)i.CollectionDate).Year == today.Year &&
                   ((DateTime)i.CollectionDate).Month == today.Month && ((DateTime)i.CollectionDate).Day == today.Day).ToList();

                if (collections != null && collections.Count() > 0)
                {
                    decimal totalCollection = collections.Sum(i => i.Amount);
                    dailyCollection = totalCollection;// / activeDays;
                }
            }

            ChartViewModel target1 = new ChartViewModel() { label = todaysDate, y = decimal.Round(dailyTarget, 2), legendText = "", per = 0 };            

            ChartViewModel collection1 = new ChartViewModel() { label = todaysDate, y = decimal.Round(dailyCollection, 2), legendText = "", per = 0 };            

            ChartTargetCollections chartObj = new ChartTargetCollections();
            chartObj.Targets = new List<ChartViewModel>();
            chartObj.Collections = new List<ChartViewModel>();
            chartObj.Targets.Add(target1);            

            chartObj.Collections.Add(collection1);           

            return Json(chartObj, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Current Target & Sales of Employee
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetDataCTSalesEmployee()
        {
            int companyId = Convert.ToInt32(Session["companyId"]);
            int employeeId = Convert.ToInt32(Session["employeeId"]);

            decimal currentTarget = 0;
            decimal currentSales = 0;
            string todaysDate = "";
            
            DateTime today = DateTime.Now;
            todaysDate = today.ToString("dd/MM/yyyy");

            int days = today.Day;
            int noOfFridays = 0;
            DateTime firstDay = new DateTime(today.Year, today.Month, 1);
            noOfFridays = DateUtility.CountWeekEnds(firstDay, today, DayOfWeek.Friday);

            int activeDays = days - noOfFridays;

            IList<TargetList> targetList = _SalesTargetService.GetTargetList(companyId);
            IList<SlsSalesOrderViewModel> orderList = _SalesOrderService.GetAll().ToList();

            if (targetList != null && targetList.Count() > 0)
            {
                TargetList target = targetList.Where(i => i.SecCompanyId == companyId && i.Year == today.Year &&
                    i.Month == today.Month && i.HrmEmployeeId == employeeId).FirstOrDefault();
                if (target != null && target.TargetSalesAmount != null)
                {
                    currentTarget = (decimal)target.TargetSalesAmount;
                        //(decimal)target.TargetSalesAmount / activeDays;
                }
            }

            if (orderList != null && orderList.Count() > 0)
            {
                IList<SlsSalesOrderViewModel> orders = orderList.Where(i => i.SecCompnayId == companyId && i.OrderDate != null &&
                  ((DateTime)i.OrderDate).Year == today.Year &&
                   ((DateTime)i.OrderDate).Month == today.Month && 
                   i.HrmEmployeeId == employeeId).ToList();

                if (orders != null && orders.Count() > 0)
                {
                    decimal totalSales = orders.Sum(i => i.Total);
                    currentSales += totalSales;
                        //totalSales / activeDays;
                }
            }

            ChartViewModel target1 = new ChartViewModel() { label = todaysDate, y = decimal.Round(currentTarget, 2), legendText = "", per = 0 };
            //ChartViewModel target2 = new ChartViewModel() { label = "28/04/2015", y = 3000, legendText = "", per = 0 };

            ChartViewModel sales1 = new ChartViewModel() { label = todaysDate, y = decimal.Round(currentSales, 2), legendText = "", per = 0 };
            //ChartViewModel sales2 = new ChartViewModel() { label = "28/04/2015", y = 2000, legendText = "", per = 0 };

            ChartTargetSales chartObj = new ChartTargetSales();
            chartObj.Targets = new List<ChartViewModel>();
            chartObj.Sales = new List<ChartViewModel>();
            chartObj.Targets.Add(target1);
            //chartObj.Targets.Add(target2);

            chartObj.Sales.Add(sales1);
            //chartObj.Sales.Add(sales2);

            return Json(chartObj, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetDataCTSalesEmployeeFromAndroid(int cmpId,int empId)
        {
            int companyId = cmpId;
            int employeeId = empId;

            decimal currentTarget = 0;
            decimal currentSales = 0;
            string todaysDate = "";

            DateTime today = DateTime.Now;
            todaysDate = today.ToString("dd/MM/yyyy");

            int days = today.Day;
            int noOfFridays = 0;
            DateTime firstDay = new DateTime(today.Year, today.Month, 1);
            noOfFridays = DateUtility.CountWeekEnds(firstDay, today, DayOfWeek.Friday);

            int activeDays = days - noOfFridays;

            IList<TargetList> targetList = _SalesTargetService.GetTargetList(companyId);
            IList<SlsSalesOrderViewModel> orderList = _SalesOrderService.GetAll().ToList();

            if (targetList != null && targetList.Count() > 0)
            {
                TargetList target = targetList.Where(i => i.SecCompanyId == companyId && i.Year == today.Year &&
                    i.Month == today.Month && i.HrmEmployeeId == employeeId).FirstOrDefault();
                if (target != null && target.TargetSalesAmount != null)
                {
                    currentTarget = (decimal)target.TargetSalesAmount;
                    //(decimal)target.TargetSalesAmount / activeDays;
                }
            }

            if (orderList != null && orderList.Count() > 0)
            {
                IList<SlsSalesOrderViewModel> orders = orderList.Where(i => i.SecCompnayId == companyId && i.OrderDate != null &&
                  ((DateTime)i.OrderDate).Year == today.Year &&
                   ((DateTime)i.OrderDate).Month == today.Month &&
                   i.HrmEmployeeId == employeeId).ToList();

                if (orders != null && orders.Count() > 0)
                {
                    decimal totalSales = orders.Sum(i => i.Total);
                    currentSales += totalSales;
                    //totalSales / activeDays;
                }
            }

            ChartViewModel target1 = new ChartViewModel() { label = todaysDate, y = decimal.Round(currentTarget, 2), legendText = "", per = 0 };
            //ChartViewModel target2 = new ChartViewModel() { label = "28/04/2015", y = 3000, legendText = "", per = 0 };

            ChartViewModel sales1 = new ChartViewModel() { label = todaysDate, y = decimal.Round(currentSales, 2), legendText = "", per = 0 };
            //ChartViewModel sales2 = new ChartViewModel() { label = "28/04/2015", y = 2000, legendText = "", per = 0 };

            ChartTargetSales chartObj = new ChartTargetSales();
            chartObj.Targets = new List<ChartViewModel>();
            chartObj.Sales = new List<ChartViewModel>();
            chartObj.Targets.Add(target1);
            //chartObj.Targets.Add(target2);

            chartObj.Sales.Add(sales1);
            //chartObj.Sales.Add(sales2);

            return Json(chartObj, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Current Target & Collection of Employee
        /// </summary>
        /// <returns></returns>
        
        [HttpGet]
        public ActionResult GetDataCTCollectionEmployee()
        {
            int companyId = Convert.ToInt32(Session["companyId"]);
            int employeeId = Convert.ToInt32(Session["employeeId"]);

            decimal currentTarget = 0;
            decimal currentCollection = 0;
            string todaysDate = "";            
            DateTime today = DateTime.Now;
            todaysDate = today.ToString("dd/MM/yyyy");

            int days = today.Day;
            int noOfFridays = 0;
            DateTime firstDay = new DateTime(today.Year, today.Month, 1);
            noOfFridays = DateUtility.CountWeekEnds(firstDay, today, DayOfWeek.Friday);

            int activeDays = days - noOfFridays;

            IList<SlsCollectionTarget> targetList = _collectionTargetService.GetAll(companyId);
            IList<SlsCollectionViewModel> collectionList = _collectionEntryService.GetAll().ToList();

            if (targetList != null && targetList.Count() > 0)
            {
                SlsCollectionTarget target = targetList.Where(i => i.SecCompanyId == companyId && i.Year == today.Year &&
                    i.Month == today.Month && i.HrmEmployeeId == employeeId).FirstOrDefault();
                if (target != null && target.TargetCollectionAmount != null)
                {
                    currentTarget = (decimal)target.TargetCollectionAmount;
                        //(decimal)target.TargetCollectionAmount / activeDays;
                }
            }

            if (collectionList != null && collectionList.Count() > 0)
            {
                IList<SlsCollectionViewModel> collections = collectionList.Where(i => i.SecCompanyId == companyId && i.CollectionDate != null &&
                  ((DateTime)i.CollectionDate).Year == today.Year &&
                   ((DateTime)i.CollectionDate).Month == today.Month &&
                   i.HrmEmployeeId == employeeId).ToList();

                if (collections != null && collections.Count() > 0)
                {
                    decimal totalCollection = collections.Sum(i => i.Amount);
                    currentCollection += totalCollection;
                    //totalCollection / activeDays;
                }
            }

            ChartViewModel target1 = new ChartViewModel() { label = todaysDate, y = decimal.Round(currentTarget, 2), legendText = "", per = 0 };

            ChartViewModel collection1 = new ChartViewModel() { label = todaysDate, y = decimal.Round(currentCollection, 2), legendText = "", per = 0 };

            ChartTargetCollections chartObj = new ChartTargetCollections();
            chartObj.Targets = new List<ChartViewModel>();
            chartObj.Collections = new List<ChartViewModel>();
            chartObj.Targets.Add(target1);

            chartObj.Collections.Add(collection1);

            return Json(chartObj, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetDataCTCollectionEmployeeFromAndroid(int cmpId,int empId)
        {
            int companyId = cmpId;
            int employeeId = empId;

            decimal currentTarget = 0;
            decimal currentCollection = 0;
            string todaysDate = "";
            DateTime today = DateTime.Now;
            todaysDate = today.ToString("dd/MM/yyyy");

            int days = today.Day;
            int noOfFridays = 0;
            DateTime firstDay = new DateTime(today.Year, today.Month, 1);
            noOfFridays = DateUtility.CountWeekEnds(firstDay, today, DayOfWeek.Friday);

            int activeDays = days - noOfFridays;

            IList<SlsCollectionTarget> targetList = _collectionTargetService.GetAll(companyId);
            IList<SlsCollectionViewModel> collectionList = _collectionEntryService.GetAll().ToList();

            if (targetList != null && targetList.Count() > 0)
            {
                SlsCollectionTarget target = targetList.Where(i => i.SecCompanyId == companyId && i.Year == today.Year &&
                    i.Month == today.Month && i.HrmEmployeeId == employeeId).FirstOrDefault();
                if (target != null && target.TargetCollectionAmount != null)
                {
                    currentTarget = (decimal)target.TargetCollectionAmount;
                    //(decimal)target.TargetCollectionAmount / activeDays;
                }
            }

            if (collectionList != null && collectionList.Count() > 0)
            {
                IList<SlsCollectionViewModel> collections = collectionList.Where(i => i.SecCompanyId == companyId && i.CollectionDate != null &&
                  ((DateTime)i.CollectionDate).Year == today.Year &&
                   ((DateTime)i.CollectionDate).Month == today.Month &&
                   i.HrmEmployeeId == employeeId).ToList();

                if (collections != null && collections.Count() > 0)
                {
                    decimal totalCollection = collections.Sum(i => i.Amount);
                    currentCollection += totalCollection;
                    //totalCollection / activeDays;
                }
            }

            ChartViewModel target1 = new ChartViewModel() { label = todaysDate, y = decimal.Round(currentTarget, 2), legendText = "", per = 0 };

            ChartViewModel collection1 = new ChartViewModel() { label = todaysDate, y = decimal.Round(currentCollection, 2), legendText = "", per = 0 };

            ChartTargetCollections chartObj = new ChartTargetCollections();
            chartObj.Targets = new List<ChartViewModel>();
            chartObj.Collections = new List<ChartViewModel>();
            chartObj.Targets.Add(target1);

            chartObj.Collections.Add(collection1);

            return Json(chartObj, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Monthly Sales & Collection of Company
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetDataMonthlySCCompany()
        {            
            decimal monthlySales = 0;
            decimal monthlyCollection = 0;
            string todaysMonth = "";
            int companyId = Convert.ToInt32(Session["companyId"]);
            DateTime today = DateTime.Now;
            todaysMonth = today.ToString("MM/yyyy");

            
            IList<SlsSalesOrderViewModel> orderList = _SalesOrderService.GetAll().ToList();
            IList<SlsCollectionViewModel> collectionList = _collectionEntryService.GetAll().ToList();
            
            if (orderList != null && orderList.Count() > 0)
            {
                IList<SlsSalesOrderViewModel> orders = orderList.Where(i => i.SecCompnayId == companyId && i.OrderDate != null &&
                  ((DateTime)i.OrderDate).Year == today.Year &&
                   ((DateTime)i.OrderDate).Month == today.Month).ToList();

                if (orders != null && orders.Count() > 0)
                {
                    decimal totalOrder = orders.Sum(i => i.Total);
                    monthlySales += totalOrder;
                }
            }
            
            if (collectionList != null && collectionList.Count() > 0)
            {
                IList<SlsCollectionViewModel> collections = collectionList.Where(i => i.SecCompanyId == companyId && i.CollectionDate != null &&
                  ((DateTime)i.CollectionDate).Year == today.Year &&
                   ((DateTime)i.CollectionDate).Month == today.Month).ToList();

                if (collections != null && collections.Count() > 0)
                {
                    decimal totalCollection = collections.Sum(i => i.Amount);
                    monthlyCollection += totalCollection;
                }
            }

            ChartViewModel sales1 = new ChartViewModel() { label = todaysMonth, y = decimal.Round(monthlySales, 2), legendText = "", per = 0 };
            //ChartViewModel target2 = new ChartViewModel() { label = "28/04/2015", y = 3000, legendText = "", per = 0 };

            ChartViewModel collections1 = new ChartViewModel() { label = todaysMonth, y = decimal.Round(monthlyCollection, 2), legendText = "", per = 0 };
            //ChartViewModel sales2 = new ChartViewModel() { label = "28/04/2015", y = 2000, legendText = "", per = 0 };

            ChartSalesCollections chartObj = new ChartSalesCollections();
            chartObj.Collections = new List<ChartViewModel>();
            chartObj.Sales = new List<ChartViewModel>();
            chartObj.Collections.Add(collections1);
            //chartObj.Targets.Add(target2);

            chartObj.Sales.Add(sales1);
            //chartObj.Sales.Add(sales2);

            return Json(chartObj, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetDataMonthlySCCompanyFromAndroid(int cmpId)
        {
            decimal monthlySales = 0;
            decimal monthlyCollection = 0;
            string todaysMonth = "";
            int companyId = cmpId;
            DateTime today = DateTime.Now;
            todaysMonth = today.ToString("MM/yyyy");


            IList<SlsSalesOrderViewModel> orderList = _SalesOrderService.GetAll().ToList();
            IList<SlsCollectionViewModel> collectionList = _collectionEntryService.GetAll().ToList();

            if (orderList != null && orderList.Count() > 0)
            {
                IList<SlsSalesOrderViewModel> orders = orderList.Where(i => i.SecCompnayId == companyId && i.OrderDate != null &&
                  ((DateTime)i.OrderDate).Year == today.Year &&
                   ((DateTime)i.OrderDate).Month == today.Month).ToList();

                if (orders != null && orders.Count() > 0)
                {
                    decimal totalOrder = orders.Sum(i => i.Total);
                    monthlySales += totalOrder;
                }
            }

            if (collectionList != null && collectionList.Count() > 0)
            {
                IList<SlsCollectionViewModel> collections = collectionList.Where(i => i.SecCompanyId == companyId && i.CollectionDate != null &&
                  ((DateTime)i.CollectionDate).Year == today.Year &&
                   ((DateTime)i.CollectionDate).Month == today.Month).ToList();

                if (collections != null && collections.Count() > 0)
                {
                    decimal totalCollection = collections.Sum(i => i.Amount);
                    monthlyCollection += totalCollection;
                }
            }

            ChartViewModel sales1 = new ChartViewModel() { label = todaysMonth, y = decimal.Round(monthlySales, 2), legendText = "", per = 0 };
            //ChartViewModel target2 = new ChartViewModel() { label = "28/04/2015", y = 3000, legendText = "", per = 0 };

            ChartViewModel collections1 = new ChartViewModel() { label = todaysMonth, y = decimal.Round(monthlyCollection, 2), legendText = "", per = 0 };
            //ChartViewModel sales2 = new ChartViewModel() { label = "28/04/2015", y = 2000, legendText = "", per = 0 };

            ChartSalesCollections chartObj = new ChartSalesCollections();
            chartObj.Collections = new List<ChartViewModel>();
            chartObj.Sales = new List<ChartViewModel>();
            chartObj.Collections.Add(collections1);
            //chartObj.Targets.Add(target2);

            chartObj.Sales.Add(sales1);
            //chartObj.Sales.Add(sales2);

            return Json(chartObj, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Monthly Sales & Collection of Branch
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetDataMonthlySCBranch()
        {
            decimal monthlySales = 0;
            decimal monthlyCollection = 0;
            string todaysMonth = "";
            int officeId = 0;
            int companyId = Convert.ToInt32(Session["companyId"]);
            int employeeId = Convert.ToInt32(Session["employeeId"]);
            DateTime today = DateTime.Now;
            todaysMonth = today.ToString("MM/yyyy");

            //Get OfficeId
            var employee = _hrmEmployeeService.GetById(employeeId);
            if (employee != null && employee.SlsOfficeId != null)
            {
                officeId = (int)employee.SlsOfficeId;
            }

            var employees = _hrmEmployeeService.GetAllEmployeeIds(companyId, officeId);
            
            if (officeId > 0)
            {
                IList<SlsSalesOrderViewModel> orderList = _SalesOrderService.GetAll().ToList();
                IList<SlsCollectionViewModel> collectionList = _collectionEntryService.GetAll().ToList();

                if (orderList != null && orderList.Count() > 0)
                {
                    IList<SlsSalesOrderViewModel> orders = orderList.Where(i => i.SecCompnayId == companyId && i.OrderDate != null &&
                      ((DateTime)i.OrderDate).Year == today.Year &&
                       ((DateTime)i.OrderDate).Month == today.Month && 
                       i.SlsOfficeId == officeId).ToList();

                    if (orders != null && orders.Count() > 0)
                    {
                        decimal totalOrder = orders.Sum(i => i.Total);
                        monthlySales += totalOrder;
                    }
                }

                if (collectionList != null && collectionList.Count() > 0)
                {
                    IList<SlsCollectionViewModel> collections = collectionList.Where(i => i.SecCompanyId == companyId && i.CollectionDate != null &&
                      ((DateTime)i.CollectionDate).Year == today.Year &&
                       ((DateTime)i.CollectionDate).Month == today.Month && 
                       i.HrmEmployeeId != null && employees.Contains((int)i.HrmEmployeeId)).ToList();

                    if (collections != null && collections.Count() > 0)
                    {
                        decimal totalCollection = collections.Sum(i => i.Amount);
                        monthlyCollection += totalCollection;
                    }
                }
            }

            ChartViewModel sales1 = new ChartViewModel() { label = todaysMonth, y = decimal.Round(monthlySales, 2), legendText = "", per = 0 };
            //ChartViewModel target2 = new ChartViewModel() { label = "28/04/2015", y = 3000, legendText = "", per = 0 };

            ChartViewModel collections1 = new ChartViewModel() { label = todaysMonth, y = decimal.Round(monthlyCollection, 2), legendText = "", per = 0 };
            //ChartViewModel sales2 = new ChartViewModel() { label = "28/04/2015", y = 2000, legendText = "", per = 0 };

            ChartSalesCollections chartObj = new ChartSalesCollections();
            chartObj.Collections = new List<ChartViewModel>();
            chartObj.Sales = new List<ChartViewModel>();
            chartObj.Collections.Add(collections1);
            //chartObj.Targets.Add(target2);

            chartObj.Sales.Add(sales1);
            //chartObj.Sales.Add(sales2);

            return Json(chartObj, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Monthly Sales & Collection of Branch
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetDataMonthlySCBranchFromAndroid(int cmpId,int empId)
        {
            decimal monthlySales = 0;
            decimal monthlyCollection = 0;
            string todaysMonth = "";
            int officeId = 0;
            int companyId = cmpId;
            int employeeId = empId;

            DateTime today = DateTime.Now;
            todaysMonth = today.ToString("MM/yyyy");

            //Get OfficeId
            var employee = _hrmEmployeeService.GetById(employeeId);
            if (employee != null && employee.SlsOfficeId != null)
            {
                officeId = (int)employee.SlsOfficeId;
            }

            var employees = _hrmEmployeeService.GetAllEmployeeIds(companyId, officeId);

            if (officeId > 0)
            {
                IList<SlsSalesOrderViewModel> orderList = _SalesOrderService.GetAll().ToList();
                IList<SlsCollectionViewModel> collectionList = _collectionEntryService.GetAll().ToList();

                if (orderList != null && orderList.Count() > 0)
                {
                    IList<SlsSalesOrderViewModel> orders = orderList.Where(i => i.SecCompnayId == companyId && i.OrderDate != null &&
                      ((DateTime)i.OrderDate).Year == today.Year &&
                       ((DateTime)i.OrderDate).Month == today.Month &&
                       i.SlsOfficeId == officeId).ToList();

                    if (orders != null && orders.Count() > 0)
                    {
                        decimal totalOrder = orders.Sum(i => i.Total);
                        monthlySales += totalOrder;
                    }
                }

                if (collectionList != null && collectionList.Count() > 0)
                {
                    IList<SlsCollectionViewModel> collections = collectionList.Where(i => i.SecCompanyId == companyId && i.CollectionDate != null &&
                      ((DateTime)i.CollectionDate).Year == today.Year &&
                       ((DateTime)i.CollectionDate).Month == today.Month &&
                       i.HrmEmployeeId != null && employees.Contains((int)i.HrmEmployeeId)).ToList();

                    if (collections != null && collections.Count() > 0)
                    {
                        decimal totalCollection = collections.Sum(i => i.Amount);
                        monthlyCollection += totalCollection;
                    }
                }
            }

            ChartViewModel sales1 = new ChartViewModel() { label = todaysMonth, y = decimal.Round(monthlySales, 2), legendText = "", per = 0 };
            //ChartViewModel target2 = new ChartViewModel() { label = "28/04/2015", y = 3000, legendText = "", per = 0 };

            ChartViewModel collections1 = new ChartViewModel() { label = todaysMonth, y = decimal.Round(monthlyCollection, 2), legendText = "", per = 0 };
            //ChartViewModel sales2 = new ChartViewModel() { label = "28/04/2015", y = 2000, legendText = "", per = 0 };

            ChartSalesCollections chartObj = new ChartSalesCollections();
            chartObj.Collections = new List<ChartViewModel>();
            chartObj.Sales = new List<ChartViewModel>();
            chartObj.Collections.Add(collections1);
            //chartObj.Targets.Add(target2);

            chartObj.Sales.Add(sales1);
            //chartObj.Sales.Add(sales2);

            return Json(chartObj, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetRegionWiseSales()
        {
            IList<RegionWiseSales> list = new List<RegionWiseSales>();

            IDashboardService _service = new DashboardService(new DashboardRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));

            list = _service.GetRegionWiseSales().ToList();

            ChartRegionWiseSales chartObj = new ChartRegionWiseSales();
            chartObj.Sales = new List<ChartViewModel>();
            
            foreach(RegionWiseSales item in list)
            {
                var obj = new ChartViewModel() { label = item.Name, y = decimal.Round(item.Sales, 2), legendText = item.Name, per = decimal.Round(item.SalesPer, 2), };
                chartObj.Sales.Add(obj);
            }

            return Json(chartObj, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetRegionWiseSalesFromAndroid()
        {
            IList<RegionWiseSales> list = new List<RegionWiseSales>();

            IDashboardService _service = new DashboardService(new DashboardRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));

            list = _service.GetRegionWiseSales().ToList();

            ChartRegionWiseSales chartObj = new ChartRegionWiseSales();
            chartObj.Sales = new List<ChartViewModel>();

            foreach (RegionWiseSales item in list)
            {
                if (item.SalesPer > 0)
                {
                    var obj = new ChartViewModel() { label = item.Name, y = decimal.Round(item.Sales, 2), legendText = item.Name, per = decimal.Round(item.SalesPer, 2), };
                    chartObj.Sales.Add(obj);
                }
            }

            return Json(chartObj, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetCompanyStatus()
        {
            //IList<RegionWiseSales> list = new List<RegionWiseSales>();

            //IDashboardService _service = new DashboardService(new DashboardRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));

            //list = _service.GetRegionWiseSales().ToList();

            IList<MngtViewModel> chartList = new List<MngtViewModel>();

            //foreach (RegionWiseSales item in list)
            //{
            //    var obj = new ChartViewModel() { label = item.Name, y = decimal.Round(item.Sales, 2), legendText = item.Name, per = decimal.Round(item.SalesPer, 2), };
            //    chartObj.Sales.Add(obj);
            //}

            var companies = _companyService.GetAll().ToList();

            foreach(var item in companies)
            {
                MngtViewModel obj = new MngtViewModel();

                obj.ClassWithBgColor = styles[randomGen.Next(0, styles.Length)];
                obj.CompanyName = item.Name;
                obj.TodaysDate = DateTime.Now.ToString("dd/MM/yyyy");
                obj.Sales = decimal.Round(DailySalesOfCompany(item.Id), 2);
                obj.Collections = decimal.Round(DailyCollectionOfCompany(item.Id), 2);
                obj.Credit = decimal.Round((obj.Sales - obj.Collections), 2);
                obj.CreditTillDate = decimal.Round(TotalCreditOfCompany(item.Id), 2);

                chartList.Add(obj);
            }

            //dummy
            //chartList.Add(new MngtViewModel()
            //{
            //    ClassWithBgColor = "small-box bg-green",
            //    CompanyName = "MEP ESL",
            //    TodaysDate = DateTime.Now.ToString("dd/MM/yyyy"),
            //    Sales = decimal.Round(500000, 2),
            //    Collections = decimal.Round(450000, 2),
            //    Credit = decimal.Round(50000, 2),
            //    CreditTillDate = decimal.Round(5500000, 2)
            //});
            //chartList.Add(new MngtViewModel()
            //{
            //    ClassWithBgColor = "small-box bg-aqua",
            //    CompanyName = "MEP LTD",
            //    TodaysDate = DateTime.Now.ToString("dd/MM/yyyy"),
            //    Sales = decimal.Round(300000, 2),
            //    Collections = decimal.Round(200000, 2),
            //    Credit = decimal.Round(10000, 2),
            //    CreditTillDate = decimal.Round(5000000, 2)
            //});
            //chartList.Add(new MngtViewModel()
            //{
            //    ClassWithBgColor = "small-box bg-olive",
            //    CompanyName = "MEP Cables",
            //    TodaysDate = DateTime.Now.ToString("dd/MM/yyyy"),
            //    Sales = decimal.Round(650000, 2),
            //    Collections = decimal.Round(550000, 2),
            //    Credit = decimal.Round(20000, 2),
            //    CreditTillDate = decimal.Round(7500000, 2)
            //});
            
            return Json(chartList, JsonRequestBehavior.AllowGet);
        }


        private void InitStyle()
        {
            styles[0] = "small-box bg-op1";
            styles[1] = "small-box bg-op6";
            styles[2] = "small-box bg-op2";
            styles[3] = "small-box bg-op7";
            styles[4] = "small-box bg-op3";
            styles[5] = "small-box bg-op8";
            styles[6] = "small-box bg-op4";
            styles[7] = "small-box bg-op9";
            styles[8] = "small-box bg-op5";
            styles[9] = "small-box bg-op10";
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>        
        public decimal DailySalesOfCompany(int companyId)
        {            
            decimal dailySales = 0;            
            DateTime today = DateTime.Now;           

            IList<SlsSalesOrderViewModel> orderList = _SalesOrderService.GetAll().ToList();            

            if (orderList != null && orderList.Count() > 0)
            {
                IList<SlsSalesOrderViewModel> orders = orderList.Where(i => i.SecCompnayId == companyId && i.OrderDate != null &&
                  ((DateTime)i.OrderDate).Year == today.Year &&
                   ((DateTime)i.OrderDate).Month == today.Month && ((DateTime)i.OrderDate).Day == today.Day).ToList();

                if (orders != null && orders.Count() > 0)
                {
                    decimal totalOrder = orders.Sum(i => i.Total);
                    decimal totalAdvance = (from s in orders
                                                select s.Advance ?? 0).Sum();
                    dailySales = totalOrder - totalAdvance;
                }
            }

            return dailySales;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        
        public decimal DailyCollectionOfCompany(int companyId)
        {            
            decimal dailyCollection = 0;            
            DateTime today = DateTime.Now;            

            IList<SlsCollectionViewModel> collectionList = _collectionEntryService.GetAll().ToList();

            if (collectionList != null && collectionList.Count() > 0)
            {
                IList<SlsCollectionViewModel> collections = collectionList.Where(i => i.SecCompanyId == companyId && i.CollectionDate != null &&
                  ((DateTime)i.CollectionDate).Year == today.Year &&
                   ((DateTime)i.CollectionDate).Month == today.Month && ((DateTime)i.CollectionDate).Day == today.Day).ToList();

                if (collections != null && collections.Count() > 0)
                {
                    decimal totalCollection = collections.Sum(i => i.Amount);
                    dailyCollection = totalCollection;
                }
            }

            return dailyCollection;
        }

        /// <summary>
        /// For this year
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public decimal TotalCreditOfCompany(int companyId)
        {
            decimal totalCredit = 0;

            decimal totalSales = TotalSalesOfCompany(companyId);
            decimal totalCollections = TotalCollectionOfCompany(companyId);

            totalCredit = totalSales - totalCollections;

            return totalCredit;
        }

        /// <summary>
        /// For this year
        /// </summary>
        /// <returns></returns>        
        public decimal TotalSalesOfCompany(int companyId)
        {
            decimal dailySales = 0;
            DateTime today = DateTime.Now;

            IList<SlsSalesOrderViewModel> orderList = _SalesOrderService.GetAll().ToList();

            if (orderList != null && orderList.Count() > 0)
            {
                IList<SlsSalesOrderViewModel> orders = orderList.Where(i => i.SecCompnayId == companyId && i.OrderDate != null &&
                  ((DateTime)i.OrderDate).Year == today.Year).ToList();

                if (orders != null && orders.Count() > 0)
                {
                    decimal totalOrder = orders.Sum(i => i.Total);
                    decimal totalAdvance = (from s in orders
                                            select s.Advance ?? 0).Sum();
                    dailySales = totalOrder - totalAdvance;
                }
            }

            return dailySales;
        }

        /// <summary>
        /// For this year
        /// </summary>
        /// <returns></returns>
        public decimal TotalCollectionOfCompany(int companyId)
        {
            decimal dailyCollection = 0;
            DateTime today = DateTime.Now;

            IList<SlsCollectionViewModel> collectionList = _collectionEntryService.GetAll().ToList();

            if (collectionList != null && collectionList.Count() > 0)
            {
                IList<SlsCollectionViewModel> collections = collectionList.Where(i => i.SecCompanyId == companyId && i.CollectionDate != null &&
                  ((DateTime)i.CollectionDate).Year == today.Year).ToList();

                if (collections != null && collections.Count() > 0)
                {
                    decimal totalCollection = collections.Sum(i => i.Amount);
                    dailyCollection = totalCollection;
                }
            }

            return dailyCollection;
        }



    }
}
