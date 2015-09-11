using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Sales;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Service.Sales
{
    public interface IDeliveryService
    {
        string GetChallanNo(string prefix, string offcode);
        string GetInvoiceNo(string prefix, string offcode);
        IList<SlsDelivery> GetAll();
        IList<ChallanList> GetChallanList();
        IList<SlsDeliveryViewModel> GetAllVM();
        Operation Save(SlsDeliveryViewModel obj);
        Operation Update(SlsDeliveryViewModel obj);

        SlsDelivery GetAllByOrderID(int orderId);
        DataTable GetDetailsByOrderIDVM(int orderId);


        Operation Update(SlsDelivery objSalesReceive);

        DataTable GetOrderViewModel();
    }
    public class DeliveryService : IDeliveryService
    {
        private IDeliveryRepository _DeliveryRepository;
        private IUnitOfWork _UnitOfWork;
        private IDeliveryDetailRepository _DeliveryDetailRepository;
        private ISalesOrderDetailRepository _salesOrderDetailRepository;

        public DeliveryService(IDeliveryRepository deliverychallenRepository, IDeliveryDetailRepository deliveryDetailRepository,
            IUnitOfWork unitOfWork)
        {
            this._DeliveryRepository = deliverychallenRepository;
            this._DeliveryDetailRepository = deliveryDetailRepository;
            this._UnitOfWork = unitOfWork;
        }
        public DeliveryService(IDeliveryRepository deliverychallenRepository, IDeliveryDetailRepository deliveryDetailRepository,
             ISalesOrderDetailRepository salesOrderDetailRepository, IUnitOfWork unitOfWork)
        {
            this._DeliveryRepository = deliverychallenRepository;
            this._DeliveryDetailRepository = deliveryDetailRepository;
            this._UnitOfWork = unitOfWork;
            this._salesOrderDetailRepository = salesOrderDetailRepository;
        }


        public string GetChallanNo(string prefix, string offcode)
        {
            string challanNo = prefix + "-" + "CLN" + "-" + offcode + "-" + DateTime.Now.ToString("yy") + "-" + DateTime.Now.ToString("MM") + "/" + _DeliveryRepository.GetLastChallanNo().ToString();
            return challanNo;
        }

        public string GetInvoiceNo(string prefix, string offcode)
        {
            string invoiceNo = prefix + "-" + "INV" + "-" + offcode + "-" + DateTime.Now.ToString("yy") + "-" + DateTime.Now.ToString("MM") + "/" + _DeliveryRepository.GetLastInvoiceNo().ToString();
            return invoiceNo;
        }
        public IList<SlsDelivery> GetAll()
        {
            try
            {
                return _DeliveryRepository.GetAll();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public IList<ChallanList> GetChallanList()
        {
            try
            {
                List<ChallanList> list = null;


                DataTable dt = new DataTable();
                dt = _DeliveryRepository.GetFromStoredProcedure(SPList.GetProductRcvDelivery.GetChallanList, null);

                if (dt != null)
                {
                    list = new List<ChallanList>();
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add((ChallanList)ERPOptima.Lib.Utilities.Helper.FillTo(row, typeof(ChallanList)));
                    }
                }

                return list;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable GetDetailsByOrderIDVM(int orderId)
        {
            try
            {
                SqlParameter[] paramsToStore = new SqlParameter[1];
                paramsToStore[0] = new SqlParameter("@SlsSalesOrderId", orderId);
                DataTable dt = _DeliveryRepository.GetFromStoredProcedure(SPList.SalesDelivery.GetDetailsByOrder, paramsToStore);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetOrderViewModel()
        {
            try
            {
                SqlParameter[] paramsToStore = new SqlParameter[0];

                DataTable dt = _DeliveryRepository.GetFromStoredProcedure(SPList.SalesDelivery.GetOrders, paramsToStore);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SlsDelivery GetAllByOrderID(int orderId)
        {
            SlsDelivery objDeliveryDetails = _DeliveryRepository.GetAllByOrderID(orderId);
            return objDeliveryDetails;
        }


        public IList<SlsDeliveryViewModel> GetAllVM()
        {
            try
            {
                IList<SlsDeliveryViewModel> list = new List<SlsDeliveryViewModel>();
                IList<SlsDelivery> listM = new List<SlsDelivery>();
                listM = _DeliveryRepository.GetAll();

                if (listM != null && listM.Count() > 0)
                {
                    list = listM.Select(x => new SlsDeliveryViewModel
                    {
                        Id = x.Id,
                        SlsSalesOrderId = x.SlsSalesOrderId,
                        DeliveryDate = x.DeliveryDate,
                        ChallanNo = x.ChallanNo,
                        InvoiceNo = x.InvoiceNo,
                        VehicleNo = x.VehicleNo,
                        Remarks = x.Remarks,
                        Discount = x.Discount,
                        Total = x.Total,
                        ReceivedStatus = x.ReceivedStatus,
                        ReceivedDate = x.ReceivedDate,
                        ReceivedRemarks = x.ReceivedRemarks,
                        CreatedBy = x.CreatedBy,
                        CreatedDate = x.CreatedDate,
                        ModifiedBy = x.ModifiedBy,
                        ModifiedDate = x.ModifiedDate
                    }).ToList();
                    if (list != null && list.Count() > 0)
                    {
                        IList<SlsSalesOrderDetail> salesOrderDetailList = new SalesOrderDetailRepository(new DatabaseFactory()).GetAll();
                        foreach (SlsDeliveryViewModel item in list)
                        {
                            item.SlsDeliverDetails = GetAllDeliveryDetails(item.Id).ToList();

                            foreach (SlsDeliverDetailViewModel itemDetail in item.SlsDeliverDetails)
                            {
                                //Product Ordered Number - get by sales order id, product id, unit id
                                int? salesOrderId = item.SlsSalesOrderId;
                                int productId = itemDetail.SlsProductId;
                                int unitId = itemDetail.SlsUnitId;
                                if (salesOrderId != null)
                                {
                                    SlsSalesOrderDetail soDetailObj = salesOrderDetailList.Where(i => i.SlsSalesOrderId == salesOrderId &&
                                        i.SlsProductId == productId &&
                                        i.SlsUnitId == unitId).FirstOrDefault();
                                    if (soDetailObj != null)
                                    {
                                        itemDetail.SalesOrderQuantity = soDetailObj.SalesOrderQuantity;
                                    }
                                }
                            }
                        }
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public IEnumerable<SlsDeliverDetailViewModel> GetAllDeliveryDetails(int deliveryId)
        {
            try
            {
                Collection<SlsDeliverDetailViewModel> list = null;
                SqlParameter[] paramsToStore = new SqlParameter[1];
                paramsToStore[0] = new SqlParameter("@SlsDeliveryId", deliveryId);
                DataTable dt = _DeliveryRepository.GetFromStoredProcedure(SPList.SalesDelivery.GetAllSlsDeliveryDetails, paramsToStore);

                if (dt != null)
                {
                    list = new Collection<SlsDeliverDetailViewModel>();
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add((SlsDeliverDetailViewModel)ERPOptima.Lib.Utilities.Helper.FillTo(row, typeof(SlsDeliverDetailViewModel)));
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public Operation Save(SlsDeliveryViewModel objVM)
        {
            Operation objOperation = new Operation { Success = false };

            using (var dbContextTransaction = _DeliveryRepository.BeginTransaction())
            {
                try
                {

                    objOperation = new Operation { Success = true };

                    SlsDelivery objT = SlsSalesDeliveryMapVMToModel.MapToSlsSalesDelivery(objVM, null);

                    //set status 
                    objT.ReceivedStatus = IsDeliveryComplete(objVM.SlsDeliverDetails, objT.Id);

                    int Id = _DeliveryRepository.AddEntity(objT);
                    _DeliveryRepository.SaveChanges();
                    objOperation.OperationId = Id;
                    objT.Id = Id;

                    //add or update categories to each item
                    IList<SlsDeliverDetail> detailList = new List<SlsDeliverDetail>();
                    foreach (SlsDeliverDetailViewModel detail in objVM.SlsDeliverDetails)
                    {
                        SlsDeliverDetail objD = SlsSalesDeliveryMapVMToModel.MapToSlsSalesDeliveryDetail(detail);
                        objD.SlsDeliveryId = objT.Id;

                        //calculations
                        objD.Price = objD.Rate * objD.Quantity;

                        if (objD.Id <= 0)
                            detailList.Add(objD);

                        else
                            _DeliveryDetailRepository.Update(objD);
                    }
                    //Add detail list - new 
                    if (detailList != null && detailList.Count > 0)
                    {
                        _DeliveryDetailRepository.AddEntityList(detailList);
                    }
                    _DeliveryDetailRepository.SaveChanges();


                    try
                    {
                        _DeliveryRepository.Commit(dbContextTransaction);
                    }
                    catch (Exception ex)
                    {
                        objOperation.Success = false;
                        throw ex;
                    }
                }
                catch (Exception ex)
                {
                    _DeliveryRepository.Rollback(dbContextTransaction);
                }
            }
            return objOperation;
        }
        public int IsDeliveryComplete(IList<SlsDeliverDetailViewModel> detailList, int deliveryId)
        {
            try
            {
                if (detailList != null && detailList.Count > 0)
                {
                    var delivery = _DeliveryRepository.GetAll().Where(i => i.Id == deliveryId).ToList().FirstOrDefault();
                    var soDetaiList = _salesOrderDetailRepository.GetAll().Where(i => i.SlsSalesOrderId == delivery.SlsSalesOrderId).ToList();
                    foreach (SlsDeliverDetailViewModel obj in detailList)
                    {
                        var soObj = soDetaiList.Where(j => j.SlsProductId == obj.SlsProductId && j.SlsUnitId == obj.SlsUnitId).FirstOrDefault();
                        if (soObj.SalesOrderQuantity > obj.Quantity)
                        {
                            return 0;
                        }
                    }
                    return 1;
                }
            }
            catch (Exception ex)
            {

            }

            return 0;
        }
        public Operation Update(SlsDeliveryViewModel objVM)
        {
            Operation objOperation = new Operation { Success = false };
            using (var dbContextTransaction = _DeliveryRepository.BeginTransaction())
            {
                try
                {
                    //set status 
                    objVM.ReceivedStatus = IsDeliveryComplete(objVM.SlsDeliverDetails, objVM.Id);

                    SlsDelivery objT = _DeliveryRepository.GetById(objVM.Id);
                    objT = SlsSalesDeliveryMapVMToModel.MapToSlsSalesDelivery(objVM, objT);
                    objOperation = new Operation { Success = true, OperationId = objT.Id };

                    _DeliveryRepository.Update(objT);
                    _DeliveryRepository.SaveChanges();

                    //add or update categories to each item
                    IList<SlsDeliverDetail> detailList = new List<SlsDeliverDetail>();
                    foreach (SlsDeliverDetailViewModel detail in objVM.SlsDeliverDetails)
                    {

                        SlsDeliverDetail objD = SlsSalesDeliveryMapVMToModel.MapToSlsSalesDeliveryDetail(detail);
                        objD.SlsDeliveryId = objT.Id;

                        //calculations
                        objD.Price = objD.Rate * objD.Quantity;


                        if (objD.Id <= 0)
                            detailList.Add(objD);

                        else
                            _DeliveryDetailRepository.Update(objD);

                    }
                    //Add detail list - new
                    if (detailList != null && detailList.Count > 0)
                    {
                        _DeliveryDetailRepository.AddEntityList(detailList);
                    }

                    ////To delete removed items
                    //IList<int> savedItems = _DeliveryDetailRepository.GetAll().Where(j => j.SlsDeliveryId == objT.Id).Select(i => i.Id).ToList();
                    //foreach (int savedId in savedItems)
                    //{
                    //    var addedOrUpdatedObj = detailList.Where(i => i.Id == savedId).FirstOrDefault();
                    //    if (addedOrUpdatedObj == null)
                    //    {
                    //        //this saved id item removed from UI
                    //        var removedObj = _DeliveryDetailRepository.GetById(savedId);
                    //        _DeliveryDetailRepository.Delete(removedObj);
                    //    }
                    //}
                    ////end of deletion action

                    _DeliveryDetailRepository.SaveChanges();

                    try
                    {
                        //_unitOfWork.Commit();
                        _DeliveryRepository.Commit(dbContextTransaction);
                    }
                    catch (Exception ex)
                    {
                        objOperation.Success = false;
                        throw ex;
                    }
                }
                catch (Exception ex)
                {
                    _DeliveryRepository.Rollback(dbContextTransaction);
                }
            }
            return objOperation;
        }



        public Operation Update(SlsDelivery objSalesReceive)
        {
            //var vmObj = SlsSalesDeliveryMapModelToVM.MapToSlsSalesDelivery(objSalesReceive);
            //return Update(vmObj);
            Operation objOperation = new Operation { Success = true, OperationId = objSalesReceive.Id };
            _DeliveryRepository.Update(objSalesReceive);

            try
            {
                _UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                objOperation = new Operation { Success = false };

            }
            return objOperation;
        }



    }

    public class SlsSalesDeliveryMapVMToModel
    {
        public static SlsDelivery MapToSlsSalesDelivery(SlsDeliveryViewModel obj, SlsDelivery model)
        {
            if (model == null)
                model = new SlsDelivery();

            model.Id = obj.Id;
            model.SlsSalesOrderId = obj.SlsSalesOrderId;
            model.DeliveryDate = obj.DeliveryDate;
            model.ChallanNo = obj.ChallanNo;
            model.InvoiceNo = obj.InvoiceNo;
            model.VehicleNo = obj.VehicleNo;
            model.Remarks = obj.Remarks;
            model.Discount = obj.Discount;
            model.Total = obj.Total;
            model.ReceivedStatus = obj.ReceivedStatus;
            model.ReceivedDate = obj.ReceivedDate;
            model.ReceivedRemarks = obj.ReceivedRemarks;
            model.CreatedBy = obj.CreatedBy;
            model.CreatedDate = obj.CreatedDate;
            model.ModifiedBy = obj.ModifiedBy;
            model.ModifiedDate = obj.ModifiedDate;

            return model;
        }
        public static SlsDeliverDetail MapToSlsSalesDeliveryDetail(SlsDeliverDetailViewModel obj)
        {
            SlsDeliverDetail model = new SlsDeliverDetail();

            model.Id = obj.Id;
            model.SlsDeliveryId = obj.SlsDeliveryId;
            model.SlsProductId = obj.SlsProductId;
            model.Quantity = obj.Quantity;
            model.SlsUnitId = obj.SlsUnitId;
            model.Rate = obj.Rate;
            model.Price = obj.Price;
            model.Discount = obj.Discount;
            model.Total = obj.Total;

            return model;
        }


    }

    public class SlsSalesDeliveryMapModelToVM
    {
        public static SlsDeliveryViewModel MapToSlsSalesDelivery(SlsDelivery obj)
        {
            SlsDeliveryViewModel model = new SlsDeliveryViewModel();

            model.Id = obj.Id;
            model.SlsSalesOrderId = obj.SlsSalesOrderId;
            model.DeliveryDate = obj.DeliveryDate;
            model.ChallanNo = obj.ChallanNo;
            model.InvoiceNo = obj.InvoiceNo;
            model.VehicleNo = obj.VehicleNo;
            model.Remarks = obj.Remarks;
            model.Discount = obj.Discount;
            model.Total = obj.Total;
            model.ReceivedStatus = obj.ReceivedStatus;
            model.ReceivedDate = obj.ReceivedDate;
            model.ReceivedRemarks = obj.ReceivedRemarks;
            model.CreatedBy = obj.CreatedBy;
            model.CreatedDate = obj.CreatedDate;
            model.ModifiedBy = obj.ModifiedBy;
            model.ModifiedDate = obj.ModifiedDate;

            return model;
        }
        public static SlsDeliverDetailViewModel MapToSlsSalesDeliveryDetail(SlsDeliverDetail obj)
        {
            SlsDeliverDetailViewModel model = new SlsDeliverDetailViewModel();

            model.Id = obj.Id;
            model.SlsDeliveryId = obj.SlsDeliveryId;
            model.SlsProductId = obj.SlsProductId;
            model.Quantity = obj.Quantity;
            model.SlsUnitId = obj.SlsUnitId;
            model.Rate = obj.Rate;
            model.Price = obj.Price;
            model.Discount = obj.Discount;
            model.Total = obj.Total;

            return model;
        }


    }
}
