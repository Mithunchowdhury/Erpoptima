using ERPOptima.Data.Common.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Inventory.Repository;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Inventory;
using ERPOptima.Model.Sales;
using ERPOptima.Service.Inventory;
using ERPOptima.Service.Sales;
using ERPOptima.Service.Security;
using ERPOptima.Web.Filters;
using Optima.Areas.Sales.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Optima.Areas.Sales.Controllers
{

    public class ProductReceiveController : Controller
    {
        private IProductReceiveService _ProductReceiveService;
        private IProductReceiveDetailService _ProductReceiveDetailService;
        private ISecCompanyService _SecCompanyService;
        private IProductDetailService _ProductDetailService;        
        private ISlsProductReceiveService _SlsProductReceiveService;
        //
        // GET: /Sales/ProductReceive/
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Index()
        {
            return View();
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]

        public ActionResult DistributorProductReceive()
        {
            return View();
        }

        public ProductReceiveController()
        {
            var dbfactory = new DatabaseFactory();
            _ProductReceiveService = new ProductReceiveService(new ProductReceiveRepository(dbfactory), new UnitOfWork(dbfactory));
            _ProductReceiveDetailService = new ProductReceiveDetailService(new ProductReceiveDetailRepository(dbfactory), new UnitOfWork(dbfactory));
            _SecCompanyService = new SecCompanyService(new SecCompanyRepository(dbfactory), new UnitOfWork(dbfactory));
            _ProductDetailService = new ProductDetailService(new IssueDetailRepository(dbfactory), new UnitOfWork(dbfactory));
            _SlsProductReceiveService = new SlsProductReceiveService(new SlsProductReceiveRepository(dbfactory), new SlsProductReceiveDetailRepository(dbfactory),
                new ChartOfProductRepository(dbfactory), new UnitOfMeasurementRepository(dbfactory), new UnitOfWork(dbfactory));

        }
        #region ProductReceive

        [HttpGet]
        public ActionResult GetAllProductsForIssue(int issueId)
        {
           
            Collection<GetProductByIssueViewModel> records = null;

            DataTable dt = _ProductDetailService.GetProductDetailByIssueId(issueId);
            if (dt != null)
            {
                records = new Collection<GetProductByIssueViewModel>();
                foreach (DataRow row in dt.Rows)
                {
                    records.Add((GetProductByIssueViewModel)ERPOptima.Lib.Utilities.Helper.FillTo(row, typeof(GetProductByIssueViewModel)));
                }
            }
            return Json(records, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetByIssue(int issueId)
        {

            InvProductReceive obj = _ProductReceiveService.GetByIssue(issueId);
                      
            return Json(obj, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetAll(int issueId)
        {
            //var list = _ProductReceiveDetailService.GetAll();
            Collection<ProductReceiveDetailViewModel> records = null;

            DataTable dt = _ProductReceiveDetailService.GetAll(issueId);
            if (dt != null)
            {
                records = new Collection<ProductReceiveDetailViewModel>();
                foreach (DataRow row in dt.Rows)
                {
                    records.Add((ProductReceiveDetailViewModel)ERPOptima.Lib.Utilities.Helper.FillTo(row, typeof(ProductReceiveDetailViewModel)));
                }
            }
            return Json(records, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]

        public ActionResult Save(InvProductReceive ProductRecievedobj, List<InvProductReceiveDetail> ProductRecievedDetailList)
        {
            
            int userId = Convert.ToInt32(Session["userId"]);
            int companyId = Convert.ToInt32(Session["companyId"]);
            var dbfactory = new DatabaseFactory();

            IOfficeService offservice = new OfficeService(new OfficeRepository(dbfactory), new UnitOfWork(dbfactory));
            SlsOffice off = offservice.GetUserOffice(userId);

            int officeId = off.Id;
            IStoreService storeservice = new StoreService(new InvStoreRepository(dbfactory), new UnitOfWork(dbfactory));
            InvStore store = storeservice.GetStoresForOffice(officeId);

            int storeId = store.Id;
            Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid && ProductRecievedDetailList != null)
            {
                if (ProductRecievedobj.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        ProductRecievedobj.InvStoreId = storeId;
                        ProductRecievedobj.CreatedBy = userId;
                        ProductRecievedobj.CreatedDate = DateTime.Now;
                        objOperation = _ProductReceiveService.Save(ProductRecievedobj);                                              

                        int ProductReceiveId = Convert.ToInt32(objOperation.OperationId);                      
                                                                 

                        foreach (var item in ProductRecievedDetailList)
                        {

                            InvProductReceiveDetail objInvProductReceiveDetail = _ProductReceiveDetailService.GetById(item.Id);

                            if (objInvProductReceiveDetail != null)
                            {
                                objInvProductReceiveDetail.InvProductReceiveId = ProductRecievedobj.Id;
                                objInvProductReceiveDetail.SlsProductId = item.SlsProductId;
                                objInvProductReceiveDetail.ReceivedQuantity = item.ReceivedQuantity;
                                objInvProductReceiveDetail.IssuedQuantity = item.IssuedQuantity;
                                objInvProductReceiveDetail.SlsUnitId = item.SlsUnitId;
                                objInvProductReceiveDetail.Remarks = item.Remarks;
                                _ProductReceiveDetailService.Update(objInvProductReceiveDetail);
                            }
                            else
                            {
                                objInvProductReceiveDetail = new InvProductReceiveDetail();
                                objInvProductReceiveDetail.InvProductReceiveId = ProductReceiveId;
                                objInvProductReceiveDetail.SlsProductId = item.SlsProductId;
                                objInvProductReceiveDetail.ReceivedQuantity = item.ReceivedQuantity;
                                objInvProductReceiveDetail.IssuedQuantity = item.IssuedQuantity;
                                objInvProductReceiveDetail.SlsUnitId = item.SlsUnitId;
                                objInvProductReceiveDetail.Remarks = item.Remarks;
                                _ProductReceiveDetailService.Save(objInvProductReceiveDetail);

                                //InvStockInOut objStockOut = new InvStockInOut();
                                //objStockOut.TransactionType = 1;
                                //objStockOut.RefId = ProductReceiveId;
                                //objStockOut.Status = 0;
                                //objStockOut.SlsProductId = item.SlsProductId.Value;
                                //objStockOut.Quantity = item.ReceivedQuantity.Value;
                                //objStockOut.SlsUnitId = item.SlsUnitId.Value;


                            }

                        }

                    }

                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        ProductRecievedobj.InvStoreId = storeId;
                        ProductRecievedobj.ModifiedBy = userId;
                        ProductRecievedobj.ModifiedDate = DateTime.Now;
                        objOperation = _ProductReceiveService.Update(ProductRecievedobj);

                        foreach (var item in ProductRecievedDetailList)
                        {
                            InvProductReceiveDetail objInvProductReceiveDetail = _ProductReceiveDetailService.GetById(item.Id);
                            if (objInvProductReceiveDetail != null)
                            {
                                objInvProductReceiveDetail.InvProductReceiveId = ProductRecievedobj.Id;
                                objInvProductReceiveDetail.SlsProductId = item.SlsProductId;
                                objInvProductReceiveDetail.ReceivedQuantity = item.ReceivedQuantity;
                                objInvProductReceiveDetail.IssuedQuantity = item.IssuedQuantity;
                                objInvProductReceiveDetail.SlsUnitId = item.SlsUnitId;
                                objInvProductReceiveDetail.Remarks = item.Remarks;
                                _ProductReceiveDetailService.Update(objInvProductReceiveDetail);
                            }
                            else
                            {
                                objInvProductReceiveDetail = new InvProductReceiveDetail();
                                objInvProductReceiveDetail.InvProductReceiveId = ProductRecievedobj.Id;
                                objInvProductReceiveDetail.SlsProductId = item.SlsProductId;
                                objInvProductReceiveDetail.ReceivedQuantity = item.ReceivedQuantity;
                                objInvProductReceiveDetail.IssuedQuantity = item.IssuedQuantity;
                                objInvProductReceiveDetail.SlsUnitId = item.SlsUnitId;
                                objInvProductReceiveDetail.Remarks = item.Remarks;
                                _ProductReceiveDetailService.Save(objInvProductReceiveDetail);
                            }

                        }

                    }
                }
                objOperation = _ProductReceiveService.Commit();

            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }
        #endregion

        #region Product Receive Distributor
        [HttpGet]
        public ActionResult GetAllProductsForDelivery(int delId)
        {

            Collection<GetProductByDeliveryViewModel> records = null;

            DataTable dt = _ProductDetailService.GetProductDetailBydeliveryId(delId);
            if (dt != null)
            {
                records = new Collection<GetProductByDeliveryViewModel>();
                foreach (DataRow row in dt.Rows)
                {
                    records.Add((GetProductByDeliveryViewModel)ERPOptima.Lib.Utilities.Helper.FillTo(row, typeof(GetProductByDeliveryViewModel)));
                }
            }
            return Json(records, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetByDelivery(int delId)
        {
            SlsProductReceive obj = _SlsProductReceiveService.GetByDelivery(delId);

            return Json(obj, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetAllReceivedByDistributor(int delId)
        {
            Collection<ProductReceiveDistDetailViewModel> records = null;

            DataTable dt = _SlsProductReceiveService.GetAllDetails(delId);
            if (dt != null)
            {
                records = new Collection<ProductReceiveDistDetailViewModel>();
                foreach (DataRow row in dt.Rows)
                {
                    records.Add((ProductReceiveDistDetailViewModel)ERPOptima.Lib.Utilities.Helper.FillTo(row, typeof(ProductReceiveDistDetailViewModel)));
                }
            }
            return Json(records, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]

        public ActionResult SaveReceivedByDistributor(SlsProductReceive ProductRecievedobj, List<SlsProductReceiveDetail> ProductRecievedDetailList)
        {

            int userId = Convert.ToInt32(Session["userId"]);
            int companyId = Convert.ToInt32(Session["companyId"]);                                    
            Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid && ProductRecievedDetailList != null)
            {
                if (ProductRecievedobj.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {   
                        ProductRecievedobj.CreatedBy = userId;
                        ProductRecievedobj.CreatedDate = DateTime.Now;
                        objOperation = _SlsProductReceiveService.Save(ProductRecievedobj);

                        int ProductReceiveId = Convert.ToInt32(objOperation.OperationId);


                        foreach (var item in ProductRecievedDetailList)
                        {

                            SlsProductReceiveDetail objSLsProductReceiveDetail = _SlsProductReceiveService.GetDetailById(item.Id);

                            if (objSLsProductReceiveDetail != null)
                            {
                                objSLsProductReceiveDetail.SlsProductReceiveId = ProductRecievedobj.Id;
                                objSLsProductReceiveDetail.SlsProductId = item.SlsProductId;
                                objSLsProductReceiveDetail.ReceivedQuantity = item.ReceivedQuantity;
                                objSLsProductReceiveDetail.DeliveryQuantity = item.DeliveryQuantity;
                                objSLsProductReceiveDetail.SlsUnitId = item.SlsUnitId;
                                objSLsProductReceiveDetail.Remarks = item.Remarks;
                                _SlsProductReceiveService.UpdateDetail(objSLsProductReceiveDetail);
                            }
                            else
                            {
                                objSLsProductReceiveDetail = new SlsProductReceiveDetail();
                                objSLsProductReceiveDetail.SlsProductReceiveId = ProductReceiveId;
                                objSLsProductReceiveDetail.SlsProductId = item.SlsProductId;
                                objSLsProductReceiveDetail.ReceivedQuantity = item.ReceivedQuantity;
                                objSLsProductReceiveDetail.DeliveryQuantity = item.DeliveryQuantity;
                                objSLsProductReceiveDetail.SlsUnitId = item.SlsUnitId;
                                objSLsProductReceiveDetail.Remarks = item.Remarks;
                                _SlsProductReceiveService.SaveDetail(objSLsProductReceiveDetail);
                            }

                        }

                    }

                }
                else
                {
                    if ((bool)Session["Edit"])
                    {                        
                        ProductRecievedobj.ModifiedBy = userId;
                        ProductRecievedobj.ModifiedDate = DateTime.Now;
                        objOperation = _SlsProductReceiveService.Update(ProductRecievedobj);

                        foreach (var item in ProductRecievedDetailList)
                        {
                            SlsProductReceiveDetail objSlsProductReceiveDetail = _SlsProductReceiveService.GetDetailById(item.Id);
                            if (objSlsProductReceiveDetail != null)
                            {
                                objSlsProductReceiveDetail.SlsProductReceiveId = ProductRecievedobj.Id;
                                objSlsProductReceiveDetail.SlsProductId = item.SlsProductId;
                                objSlsProductReceiveDetail.ReceivedQuantity = item.ReceivedQuantity;
                                objSlsProductReceiveDetail.DeliveryQuantity = item.DeliveryQuantity;
                                objSlsProductReceiveDetail.SlsUnitId = item.SlsUnitId;
                                objSlsProductReceiveDetail.Remarks = item.Remarks;
                                _SlsProductReceiveService.UpdateDetail(objSlsProductReceiveDetail);
                            }
                            else
                            {
                                objSlsProductReceiveDetail = new SlsProductReceiveDetail();
                                objSlsProductReceiveDetail.SlsProductReceiveId = ProductRecievedobj.Id;
                                objSlsProductReceiveDetail.SlsProductId = item.SlsProductId;
                                objSlsProductReceiveDetail.ReceivedQuantity = item.ReceivedQuantity;
                                objSlsProductReceiveDetail.DeliveryQuantity = item.DeliveryQuantity;
                                objSlsProductReceiveDetail.SlsUnitId = item.SlsUnitId;
                                objSlsProductReceiveDetail.Remarks = item.Remarks;
                                _SlsProductReceiveService.SaveDetail(objSlsProductReceiveDetail);
                            }

                        }

                    }
                }
                objOperation = _ProductReceiveService.Commit();

            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }
        #endregion
    }
}