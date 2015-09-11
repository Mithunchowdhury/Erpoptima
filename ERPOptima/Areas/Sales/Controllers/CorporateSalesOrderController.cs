using ERPOptima.Data.Common.Repository;
using ERPOptima.Data.Hrm.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Inventory.Repository;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.HRM;
using ERPOptima.Model.Sales;
using ERPOptima.Model.Security;
using ERPOptima.Model.ViewModel;
using ERPOptima.Model.ViewModel.Sales;
using ERPOptima.Service.Hrm;
using ERPOptima.Service.Sales;
using ERPOptima.Service.Security;
using ERPOptima.Web.Filters;
using Optima.Areas.Common.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Optima.Areas.Sales.Controllers
{
    public class CorporateSalesOrderController : Controller
    {

        private ISalesOrderService<SlsSalesOrderApproval, SlsSalesOrderViewModel> _salesOrderService;
        private IOfficeService _officeService;
        private ISecCompanyService _SecCompanyService;
        private IFreeProductService _FreeProductService;
        private IHrmEmployeeService _hrmEmployeeService;
        private ISalesDiscountSettingService _salesDiscountSettingService;
        private IPartyCreditReportService _PartyCreditService;
        public CorporateSalesOrderController()
        {
            var dbfactory = new DatabaseFactory();
            var unitOfWork = new UnitOfWork(dbfactory);
            _salesOrderService = new SalesOrderService(new SalesOrderRepository(dbfactory),
                new SalesOrderDetailRepository(dbfactory),
                new SalesOrderApprovalRepository(dbfactory),
                new NotificationRepository(dbfactory),
                new NotificationDetailRepository(dbfactory),
                unitOfWork);
            _officeService = new OfficeService(new OfficeRepository(dbfactory), unitOfWork);
            _SecCompanyService = new SecCompanyService(new SecCompanyRepository(dbfactory), unitOfWork);
            _FreeProductService = new FreeProductService(new FreeProductRepository(dbfactory), new ChartOfProductRepository(dbfactory),
                new UnitOfMeasurementRepository(dbfactory), unitOfWork);
            _hrmEmployeeService = new HrmEmployeeService(new HrmEmployeeRepository(dbfactory), unitOfWork);
            _salesDiscountSettingService = new SalesDiscountSettingService(new SalesDiscountSettingRepository(dbfactory), unitOfWork);
            _PartyCreditService = new PartyCreditReportService(new InvStoreOpeningRepository(dbfactory), unitOfWork);
         }

        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                var list = _salesOrderService.GetAll().OrderByDescending(i => i.Id).ToList();

                //1=Regular,2=Corporate,3=Retail
                //Load all regular sales order list
                list = list.Where(i => i.SalesType == 2).ToList();

                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

            }

            return Json(new List<SlsSalesOrderViewModel>(), JsonRequestBehavior.AllowGet);            
            
        }

        [HttpGet]
        public ActionResult GetRefNo(int companyId, int employeeId)
        {
            SecCompany objCmnCompany = _SecCompanyService.GetById(companyId);
            SlsOffice office = _officeService.GetById((int)_hrmEmployeeService.GetById(employeeId).SlsOfficeId);
            string refNo = _salesOrderService.GetRefNo(companyId, objCmnCompany.Prefix, office.Code);
            return Json(new { refNo = refNo }, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult GetProductPrice(int productId, int quantity, int unitId, decimal discount)
        {
            decimal productPrice = 0;

            productPrice = _salesOrderService.CorpSalesProductPrice(productId, quantity, unitId, discount);

            return Json(new { Total = productPrice }, JsonRequestBehavior.AllowGet);
        }
        

        [HttpPost]
        public ActionResult SaveSalesOrder(SlsSalesOrderViewModel objT)
        {
            int userId = Convert.ToInt32(Session["userId"]);
            int companyId = Convert.ToInt32(Session["companyId"]);
            int employeeId = Convert.ToInt32(Session["employeeId"]);

            HrmEmployee employee = _hrmEmployeeService.GetById(employeeId);

            Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid)
            {
                if (objT.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        //objT.Status = 0;
                        objT.SlsOfficeId = employee.SlsOfficeId;
                        objT.HrmEmployeeId = employeeId;
                        objT.SecCompnayId = companyId;
                        objT.CreatedBy = userId;
                        objT.CreatedDate = DateTime.Now.Date;
                        objT.OrderDate = DateTime.Now.Date;
                        objOperation = _salesOrderService.Save(objT);
                    }
                    else { objOperation.OperationId = -1; }

                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        //objT.Status = 0;
                        objT.SlsOfficeId = employee.SlsOfficeId;
                        objT.HrmEmployeeId = employeeId;
                        objT.SecCompnayId = companyId;
                        objT.ModifiedBy = userId;
                        objT.ModifiedDate = DateTime.Now.Date;
                        objOperation = _salesOrderService.Update(objT);
                    }
                    else { objOperation.OperationId = -2; }
                }
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        [HttpGet]
        public ActionResult CheckCreditLimit(int partytype, int partyId, decimal creditLimit, decimal advance)
        {
            bool result = false;
            #region Commented
            //try
            //{
            //    switch (partytype)
            //    {
            //        case (int)Enums.PartyType.Distributor:
            //            var distributor = _DistributorService.GetAll().Where(d => d.Id == partyId).FirstOrDefault();
            //            result = distributor.CreditLimit > creditLimit ? true : false;
            //            break;
            //        case (int)Enums.PartyType.Retailer:
            //            var retailer = _RetailerService.GetAll().Where(d => d.Id == partyId).FirstOrDefault();
            //            result = retailer.CreditLimit > creditLimit ? true : false;
            //            break;
            //        case (int)Enums.PartyType.Dealer:
            //            var dealer = _DealerService.GetAll().Where(d => d.Id == partyId).FirstOrDefault();
            //            result = dealer.CreditLimit > creditLimit ? true : false;
            //            break;
            //        case (int)Enums.PartyType.CorporateClient:
            //            var corporateClient = _CorporateClientService.GetAll().Where(d => d.Id == partyId).FirstOrDefault();
            //            result = corporateClient.DefaultCreditLimit > creditLimit ? true : false;
            //            break;
            //        default:
            //            result = false;
            //            break;
            //    }
            //}
            //catch (Exception ex)
            //{

            //}
            #endregion
            int companyId = Convert.ToInt32(Session["companyId"]);

            try
            {
                var currentCredit = _PartyCreditService.GetPartyCurrentCredit(partytype, partyId, companyId);
                if (currentCredit != null && currentCredit.Count() > 0)
                {
                    result = (currentCredit[0].CurrentCredit + advance) > creditLimit ? true : false;
                }
            }
            catch (Exception ex)
            {

            }

            return Json(new { result = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetDiscount(decimal total)
        {
            decimal discount = 0;
            var discountSettings = _salesDiscountSettingService.GetAll();

            if (discountSettings != null && discountSettings.Count() > 0)
            {
                var checkBothLimit = discountSettings.Where(i => i.LowerLimit <= total && total <= i.UpperLimit).ToList();

                if (checkBothLimit != null && checkBothLimit.Count > 0)
                {
                    discount = checkBothLimit.FirstOrDefault().DiscountPercentage;
                }
                else
                {
                    discount = 0;
                }
            }

            return Json(new { Discount = discount }, JsonRequestBehavior.AllowGet);
        }


    }
}
