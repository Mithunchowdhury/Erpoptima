using ERPOptima.Data.Common.Repository;
using ERPOptima.Data.Hrm.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Inventory.Repository;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib;
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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using Newtonsoft;
using Newtonsoft.Json;
namespace Optima.Areas.Sales.Controllers
{
    public class SalesController : Controller
    {
        //
        // GET: /Sales/Sales/
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Index()
        {
            return View();
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Order()
        {
            return View();
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult OrderList()
        {
            return View();
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult OrderApproval()
        {
            int? soId = null;
            if (TempData["SalesOrderNumber"] != null)
            {
                soId = (int)TempData["SalesOrderNumber"];
                ViewData["SalesOrderNumber"] = soId;
            }

            return View();
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Delivery()
        {
            return View();
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Return()
        {
            return View();
        }

        private ISalesOrderService<SlsSalesOrderApproval, SlsSalesOrderViewModel> _salesOrderService;
        private ApprovalController<SlsSalesOrderApproval, SlsSalesOrderViewModel> _approvalController;
        private IDeliveryService _deliveryService;
        private ISecCompanyService _SecCompanyService;
        private IHrmEmployeeService _hrmEmployeeService;
        private IOfficeService _officeService;
        private IDistributorService _DistributorService;
        private IDealerService _DealerService;
        private ICorporateClientService _CorporateClientService;
        private IRetailerService _RetailerService;
        private IFreeProductService _FreeProductService;
        private IChartOfProductService _ProductService;
        private IUnitOfMeasurementService _UnitService;
        private ISalesDiscountSettingService _salesDiscountSettingService;
        private IPartyCreditReportService _PartyCreditService;
        private IPromotionalOfferService _promotionalOfferService; 
        public SalesController()
        {
            var dbfactory = new DatabaseFactory();
            var unitOfWork = new UnitOfWork(dbfactory);
            _salesOrderService = new SalesOrderService(new SalesOrderRepository(dbfactory),
                new SalesOrderDetailRepository(dbfactory),
                new SalesOrderApprovalRepository(dbfactory),
                new NotificationRepository(dbfactory),
                new NotificationDetailRepository(dbfactory),
                unitOfWork);
            _approvalController = new ApprovalController<SlsSalesOrderApproval, SlsSalesOrderViewModel>(_salesOrderService);
            _deliveryService = new DeliveryService(new DeliveryRepository(dbfactory), new DeliveryDetailRepository(dbfactory), unitOfWork);
            _SecCompanyService = new SecCompanyService(new SecCompanyRepository(dbfactory), unitOfWork);
            _hrmEmployeeService = new HrmEmployeeService(new HrmEmployeeRepository(dbfactory), unitOfWork);
            _officeService = new OfficeService(new OfficeRepository(dbfactory), unitOfWork);
            _DistributorService = new DistributorService(new DistributorRepository(dbfactory), unitOfWork);
            _DealerService = new DealerService(new DealerRepository(dbfactory), unitOfWork);
            _CorporateClientService = new CorporateClientService(new CorporateClientRepository(dbfactory), unitOfWork);
            _RetailerService = new RetailerService(new RetailerRepository(dbfactory), unitOfWork);
            _FreeProductService = new FreeProductService(new FreeProductRepository(dbfactory), new ChartOfProductRepository(dbfactory),
                new UnitOfMeasurementRepository(dbfactory), unitOfWork);
            _ProductService = new ChartOfProductService(new ChartOfProductRepository(dbfactory), unitOfWork);
            _UnitService = new UnitOfMeasurementService(new UnitOfMeasurementRepository(dbfactory), unitOfWork);
            _salesDiscountSettingService = new SalesDiscountSettingService(new SalesDiscountSettingRepository(dbfactory), unitOfWork);
            _PartyCreditService = new PartyCreditReportService(new InvStoreOpeningRepository(dbfactory), unitOfWork);
            _promotionalOfferService = new PromotionalOfferService(new PromotionalOfferRepository(dbfactory),
                new PromotionalOfferDetailRepository(dbfactory),
                new UnitOfWork(dbfactory));
        }

        #region Sales Order

        [HttpGet]
        public string GetRefNo(int companyId, int employeeId)
        {
            SecCompany objCmnCompany = _SecCompanyService.GetById(companyId);
            SlsOffice office = _officeService.GetById((int)_hrmEmployeeService.GetById(employeeId).SlsOfficeId);
            string refNo = _salesOrderService.GetRefNo(companyId, objCmnCompany.Prefix, office.Code);
            return refNo;

        }
        [HttpGet]
        public ActionResult GetSaleRefNo(int companyId, int employeeId)
        {
            SecCompany objCmnCompany = _SecCompanyService.GetById(companyId);
            SlsOffice office = _officeService.GetById((int)_hrmEmployeeService.GetById(employeeId).SlsOfficeId);
            string refNo = _salesOrderService.GetRefNo(companyId, objCmnCompany.Prefix, office.Code);
            var refernce = new { rf=refNo};
            return Json(refernce, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetBySalesOrderId(int SalesOrderId)
        {
            var list = _salesOrderService.GetBySalesOrderId(SalesOrderId).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowSOProductDetails(int productId, int quantity, int unitId, int partyTypeId, int paryId)
        {
            int companyId = Convert.ToInt32(Session["companyId"]);
            var list = _salesOrderService.ShowSOProductDetails(productId, quantity, unitId, partyTypeId, paryId);

            List<SlsSalesOrderProductViewModel> products = new List<SlsSalesOrderProductViewModel>();
            SlsSalesOrderProductViewModel product = new SlsSalesOrderProductViewModel();

            try
            {
                if (list != null && list.Count() > 0)
                {
                    var item = list.First();

                    product.Id = item.Id;
                    product.SlsSalesOrderId = item.SlsSalesOrderId;
                    product.SlsProductId = item.SlsProductId;
                    product.SlsProductName = item.SlsProductName;
                    product.SalesOrderQuantity = item.SalesOrderQuantity;
                    product.SlsUnitId = item.SlsUnitId;
                    product.SlsUnitName = item.SlsUnitName;
                    product.Rate = item.Rate;
                    product.Price = item.Price;
                    product.Discount = item.Discount;
                    product.Total = item.Total;
                                        
                    products.Add(product);

                    //get free products
                    var freeProducts = _FreeProductService.GetAll(companyId);                   

                    freeProducts = freeProducts.Where(i => i.SlsProductId == item.SlsProductId && i.SlsUnitId == item.SlsUnitId
                        && i.StartDate <= DateTime.Now && DateTime.Now <= i.EndDate).ToList().OrderByDescending(j=>j.StartDate).ToList();
                    //i.SlsProductId = product free with item.SlsProductId product
                    var result = freeProducts.Select(i => new SlsSalesOrderProductViewModel()
                    {
                        Id = 0,
                        SlsSalesOrderId = 0,
                        SlsProductId = i.SlsProductId,
                        SlsProductName = i.SlsProductName,
                        SalesOrderQuantity = (int)((item.SalesOrderQuantity * i.FreeQuantity) / i.MeasurementQuantity),
                        SlsUnitId = i.FreeUnitId,
                        SlsUnitName = i.FreeUnitName,
                        Rate = 0,
                        Price = 0,
                        Discount = 0,
                        Total = 0
                    }).Distinct().ToList();

                    products.AddRange(result);
                }
            }
            catch (Exception ex)
            {

            }
            
            return Json(products, JsonRequestBehavior.AllowGet);
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

                        //1=Regular,2=Corporate,3=Retail
                        if(objT.SalesType == 3)
                        {
                            //For retail sale - date will be entry date
                            objT.PreferredDeliveryDate = DateTime.Now;
                        }

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
     
        [HttpPost]
        public ActionResult SaveSalesOrderFromAndriod(SlsSalesOrderViewModel objT)
        {
           
            var memstream = new MemoryStream();
            Request.InputStream.Position = 0;
            Request.InputStream.CopyTo(memstream);
            memstream.Position = 0;
            using (StreamReader reader = new StreamReader(memstream))
            {
                try
                {
                    var text = reader.ReadToEnd();
                   
                    SlsSalesOrderViewModel record = JsonConvert.DeserializeObject<SlsSalesOrderViewModel>(text);
                    if(record!=null)
                    {
                        objT = record;
                    }
                }
                catch(Exception ex)
                { }
               
            }
            
            

            int userId = Convert.ToInt32(Session["userId"]);
            int companyId = Convert.ToInt32(Session["companyId"]);
            int employeeId = Convert.ToInt32(Session["employeeId"]);

            HrmEmployee employee = _hrmEmployeeService.GetById(employeeId);

            Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid)
            {
                if (objT.Id == 0)
                {

                    //objT.Status = 0;
                    objT.RefNo = this.GetRefNo(companyId, employeeId);
                    objT.SlsOfficeId = employee.SlsOfficeId;
                    objT.HrmEmployeeId = employeeId;
                    objT.SecCompnayId = companyId;
                    objT.CreatedBy = userId;
                    objT.CreatedDate = DateTime.Now.Date;
                    objT.OrderDate = DateTime.Now.Date;

                    //1=Regular,2=Corporate,3=Retail
                    if (objT.SalesType == 3)
                    {
                        //For retail sale - date will be entry date
                        objT.PreferredDeliveryDate = DateTime.Now;
                    }

                    objOperation = _salesOrderService.Save(objT);


                }
             
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var list = _salesOrderService.GetAll();
            
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetAllRegularSales() 
        {
            var list = _salesOrderService.GetAll();

            if (list != null)
            {
                //1=Regular,2=Corporate,3=Retail
                //Load all regular sales order list
                list = list.Where(i => i.SalesType == 1).ToList();
            }
            else
            {
                list = new List<SlsSalesOrderViewModel>();
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult FindAll(DateTime? from = null, DateTime? to = null)
        {
            var list = _salesOrderService.GetAll();

            if (from != null)
                list = list.Where(i => i.OrderDate != null && (from <= i.OrderDate.Value)).ToList();

            if (to != null)
                list = list.Where(i => i.OrderDate != null && (to >= i.OrderDate.Value)).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
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
            
            if(discountSettings != null && discountSettings.Count() > 0)
            {
                var checkBothLimit = discountSettings.Where(i => i.LowerLimit <= total && total <= i.UpperLimit).ToList();                

                if(checkBothLimit != null && checkBothLimit.Count > 0)
                {
                    discount = checkBothLimit.FirstOrDefault().DiscountPercentage;
                }                
                else
                {
                    discount = 0;
                }
            }

            return Json(new { Discount = discount}, JsonRequestBehavior.AllowGet);
        }

        #region SO Approval
        [HttpGet]
        public ActionResult GetAllSOForApproval()
        {
            int userId = Convert.ToInt32(Session["userId"]);
            var list = _approvalController.GetAllForApproval(userId);
            return Json(list, JsonRequestBehavior.AllowGet);
        
        }
        [HttpGet]
        public ActionResult GetNewSOToApproval()
        {
            int userId = Convert.ToInt32(Session["userId"]);
            var list = _approvalController.GetAllForApproval(userId).Where(t=>t.ApprovalStatus==1).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetAllSOForApprovalByDate(DateTime fromDate, DateTime toDate)
        {
            int userId = Convert.ToInt32(Session["userId"]);
            var list = _approvalController.GetAllForApprovalByDate(userId, fromDate, toDate);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetSOApprovalBySOId(int salesOrderId)
        {
            var obj = _approvalController.GetApprovalById(salesOrderId);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ApproveSalesOrder(SlsSalesOrderApproval obj, string newComment)
        {
            //According to DB column description: 1=New,2=Approve,3=Pass,4=Reject
            return UpdateSalesOrderApproval(obj, 2, newComment);
        }
        [HttpPost]
        public ActionResult ApproveSalesOrderFromAnriod(int salesOrderId, string newComment)
        {
            //According to DB column description: 1=New,2=Approve,3=Pass,4=Reject
            int userId = Convert.ToInt32(Session["userId"]);
            Operation objOperation = new Operation { Success = false };
            SlsSalesOrderApproval obj = null;
            obj = _approvalController.GetApprovalById(salesOrderId);
            obj.Action = 2;
            obj.ModifiedBy = userId;
            obj.ModifiedDate = DateTime.Now.Date;
            obj.Comment = newComment;
            objOperation = _approvalController.UpdateApproval(obj, newComment);
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }
        public ActionResult PassSalesOrderFromAnriod(int salesOrderId, string newComment)
        {
            //According to DB column description: 1=New,2=Approve,3=Pass,4=Reject
            int userId = Convert.ToInt32(Session["userId"]);
            Operation objOperation = new Operation { Success = false };
            SlsSalesOrderApproval obj = null;
            obj = _approvalController.GetApprovalById(salesOrderId);
            obj.Action = 3;
            obj.ModifiedBy = userId;
            obj.ModifiedDate = DateTime.Now.Date;
            obj.Comment = newComment;
            objOperation = _approvalController.UpdateApproval(obj, newComment);
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }
        public ActionResult RejectSalesOrderFromAnriod(int salesOrderId, string newComment)
        {
            //According to DB column description: 1=New,2=Approve,3=Pass,4=Reject
            int userId = Convert.ToInt32(Session["userId"]);
            Operation objOperation = new Operation { Success = false };
            SlsSalesOrderApproval obj = null;
            obj = _approvalController.GetApprovalById(salesOrderId);
            obj.Action = 4;
            obj.ModifiedBy = userId;
            obj.ModifiedDate = DateTime.Now.Date;
            obj.Comment = newComment;
            objOperation = _approvalController.UpdateApproval(obj, newComment);
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }
        [HttpPost]
        public ActionResult RejectSalesOrder(SlsSalesOrderApproval obj, string newComment)
        {
            //According to DB column description: 1=New,2=Approve,3=Pass,4=Reject
            return UpdateSalesOrderApproval(obj, 4, newComment);
        }
        [HttpPost]
        public ActionResult PassSalesOrder(SlsSalesOrderApproval obj, string newComment)
        {
            //According to DB column description: 1=New,2=Approve,3=Pass,4=Reject
            return UpdateSalesOrderApproval(obj, 3, newComment);
        }
        public ActionResult UpdateSalesOrderApproval(SlsSalesOrderApproval obj, int action, string newComment)
        {
            int userId = Convert.ToInt32(Session["userId"]);
            Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid)
            {
                if (obj.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        //Sales Order Approval is created internally during sales order creation.
                        objOperation.Success = false;
                    }
                    else { objOperation.OperationId = -1; }

                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        //According to DB column description: 1=New,2=Approve,3=Pass,4=Reject
                        obj.Action = action;
                        obj.ModifiedBy = userId;
                        obj.ModifiedDate = DateTime.Now.Date;
                        objOperation = _approvalController.UpdateApproval(obj, newComment);
                    }
                    else { objOperation.OperationId = -2; }
                }
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        public ActionResult OrderApprovalById(int SalesOrderNumber)
        {
            TempData["SalesOrderNumber"] = SalesOrderNumber;
            return RedirectToAction("OrderApproval");
        }
        #endregion
        #endregion


        #region Sales Delivery

        [HttpGet]
        public ActionResult GetAllSORefNo()
        {
            var list = _salesOrderService.GetAll();

            var refNos = list.Select(i => new { Id = i.Id, RefNo = i.RefNo + " (" + i.PartyName + ")" }).Distinct().ToList();
            return Json(refNos, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region Sales Return

        #endregion
    }

}
