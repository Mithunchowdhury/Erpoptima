using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Sales;
using ERPOptima.Model.ViewModel;
using ERPOptima.Service.Common;
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
    public interface ISalesOrderService<TObj, TObjVM> : IApprovalService<TObj, TObjVM>
        where TObj : SlsSalesOrderApproval
        where TObjVM : SlsSalesOrderViewModel
    {
        decimal CorpSalesProductPrice(int productId, int quantity, int unitId, decimal discount);
        IEnumerable<SlsSalesOrderDetailViewModel> ShowSOProductDetails(int productId, int quantity, int unitId, int partyTypeId, int paryId);
        Operation Save(SlsSalesOrderViewModel obj);
        Operation Update(SlsSalesOrderViewModel obj);
        IEnumerable<SlsSalesOrderViewModel> GetAll();
        SlsSalesOrderViewModel GetById(int Id);
        decimal GetNetSales(DateTime From, DateTime To, int PartyType, int Party);
        decimal GetNetSales(int year, int month, int salesPersonId);
        string GetRefNo(int companyId, string prefix, string offcode);

        IList<SalesOrderDetail> GetBySalesOrderId(int SalesOrderId);
        /// <summary>
        /// To call from other services, inside transaction block
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="objOperation"></param>
        /// <param name="dbContextTransaction"></param>
        /// <returns></returns>
        Operation SaveSalesOrder(SlsSalesOrderViewModel obj, Operation objOperation, System.Data.Entity.DbContextTransaction dbContextTransaction);
    }
    public class SalesOrderService : ISalesOrderService<SlsSalesOrderApproval, SlsSalesOrderViewModel>
    {
        /* *
         * For Notification Messaging 
         * */
        private string NewSalesOrderNotificationMessage = "New Sales Order for Approval";
        private string SalesOrderNotificationURL = "/Sales/Sales/OrderApprovalById/?SalesOrderNumber=";

        private ISalesOrderRepository _salesOrderRepository;
        private ISalesOrderDetailRepository _salesOrderDetailRepository;
        private ISalesOrderApprovalRepository _salesOrderApprovalRepository;
        private INotificationRepository _notificationRepository;
        private INotificationDetailRepository _notificationDetailRepository;
        private IUnitOfWork _unitOfWork;


        public SalesOrderService(ISalesOrderRepository salesOrderRepository,
            ISalesOrderDetailRepository salesOrderDetailRepository,
            ISalesOrderApprovalRepository salesOrderApprovalRepository,
            INotificationRepository notificationRepository,
            INotificationDetailRepository notificationDetailRepository,
            IUnitOfWork unitOfWork)
        {
            this._salesOrderRepository = salesOrderRepository;
            this._salesOrderDetailRepository = salesOrderDetailRepository;
            this._salesOrderApprovalRepository = salesOrderApprovalRepository;
            this._notificationRepository = notificationRepository;
            this._notificationDetailRepository = notificationDetailRepository;
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Both GetAll and GetById will reuse this helper
        /// </summary>
        /// <param name="orderId">orderId=0 GetAll, orderId!=0 GetById</param>
        /// <returns></returns>
        public IEnumerable<SlsSalesOrderViewModel> GetAllOrders(int orderId)
        {
            try
            {
                Collection<SlsSalesOrderViewModel> list = null;
                SqlParameter[] paramsToStore = new SqlParameter[1];
                paramsToStore[0] = new SqlParameter("@SalesOrderId", orderId);
                DataTable dt = _salesOrderRepository.GetFromStoredProcedure(SPList.SalesOrder.GetAllSlsSalesOrders, paramsToStore);

                if (dt != null)
                {
                    list = new Collection<SlsSalesOrderViewModel>();
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add((SlsSalesOrderViewModel)ERPOptima.Lib.Utilities.Helper.FillTo(row, typeof(SlsSalesOrderViewModel)));
                    }
                }

                //to get categories offered for each offer
                foreach (SlsSalesOrderViewModel item in list)
                {
                    //attach detail list to item
                    item.SalesOrderDetails = GetAllDetails(item.Id).ToList();
                }

                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IList<SalesOrderDetail> GetBySalesOrderId(int SalesOrderId)
        {
            return _salesOrderDetailRepository.GetBySalesOrderId(SalesOrderId);


        }

        public IEnumerable<SlsSalesOrderViewModel> GetAll()
        {
            return GetAllOrders(0);
        }
        public SlsSalesOrderViewModel GetById(int Id)
        {
            SlsSalesOrderViewModel obj = GetAllOrders(Id).First();
            return obj;
        }
        public IEnumerable<SlsSalesOrderDetailViewModel> GetAllDetails(int orderId)
        {
            try
            {
                Collection<SlsSalesOrderDetailViewModel> detaillist = null;
                SqlParameter[] paramsToStoreDetail = new SqlParameter[1];
                paramsToStoreDetail[0] = new SqlParameter("@SOId", orderId);

                DataTable detaildt = _salesOrderDetailRepository.GetFromStoredProcedure(SPList.SalesOrder.GetSOProductDetails, paramsToStoreDetail);

                if (detaildt != null)
                {
                    detaillist = new Collection<SlsSalesOrderDetailViewModel>();
                    foreach (DataRow row in detaildt.Rows)
                    {
                        detaillist.Add((SlsSalesOrderDetailViewModel)ERPOptima.Lib.Utilities.Helper.FillTo(row, typeof(SlsSalesOrderDetailViewModel)));
                    }
                }
                return detaillist;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IEnumerable<SlsSalesOrderDetailViewModel> ShowSOProductDetails(int productId, int quantity, int unitId, int partyTypeId, int paryId)
        {
            try
            {
                IList<SlsSalesOrderDetailViewModel> list = new List<SlsSalesOrderDetailViewModel>();
                SqlParameter[] paramsToStore = null;
                DataTable dt = null;


                paramsToStore = new SqlParameter[5];
                paramsToStore[0] = new SqlParameter("@ProductId", productId);
                paramsToStore[1] = new SqlParameter("@Quantity", quantity);
                paramsToStore[2] = new SqlParameter("@UnitId", unitId);
                paramsToStore[3] = new SqlParameter("@PartyTypeId", partyTypeId);
                paramsToStore[4] = new SqlParameter("@ParyId", paryId);

                dt = _salesOrderDetailRepository.GetFromStoredProcedure(SPList.SalesOrder.SOProductDetails, paramsToStore);

                //check if promotional offer applied or not.
                if (dt == null || (dt != null && dt.Rows.Count <= 0))
                {
                    paramsToStore = new SqlParameter[5];
                    paramsToStore[0] = new SqlParameter("@ProductId", productId);
                    paramsToStore[1] = new SqlParameter("@Quantity", quantity);
                    paramsToStore[2] = new SqlParameter("@UnitId", unitId);
                    paramsToStore[3] = new SqlParameter("@PartyTypeId", partyTypeId);
                    paramsToStore[4] = new SqlParameter("@ParyId", paryId);

                    dt = _salesOrderDetailRepository.GetFromStoredProcedure(SPList.SalesOrder.SOProductDetailsOffer, paramsToStore);

                    //check again without discount applied
                    if (dt == null || (dt != null && dt.Rows.Count <= 0))
                    {
                        paramsToStore = new SqlParameter[3];
                        paramsToStore[0] = new SqlParameter("@ProductId", productId);
                        paramsToStore[1] = new SqlParameter("@Quantity", quantity);
                        paramsToStore[2] = new SqlParameter("@UnitId", unitId);

                        dt = _salesOrderDetailRepository.GetFromStoredProcedure(SPList.SalesOrder.SOProductDetailsWithoutDiscount, paramsToStore);
                    }
                }               

                if (dt != null)
                {
                    list = new Collection<SlsSalesOrderDetailViewModel>();
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add((SlsSalesOrderDetailViewModel)ERPOptima.Lib.Utilities.Helper.FillTo(row, typeof(SlsSalesOrderDetailViewModel)));
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="quantity">Supplied</param>
        /// <param name="unitId"></param>
        /// <param name="discount">Supplied</param>
        /// <returns></returns>
        public decimal CorpSalesProductPrice(int productId, int quantity, int unitId, decimal discount)
        {
            decimal productPrice = 0;
            try
            {
                IList<SlsSalesOrderDetailViewModel> list = new List<SlsSalesOrderDetailViewModel>();
                SqlParameter[] paramsToStore = null;
                DataTable dt = null;

                paramsToStore = new SqlParameter[3];
                paramsToStore[0] = new SqlParameter("@ProductId", productId);
                paramsToStore[1] = new SqlParameter("@Quantity", quantity);
                paramsToStore[2] = new SqlParameter("@UnitId", unitId);

                dt = _salesOrderDetailRepository.GetFromStoredProcedure(SPList.SalesOrder.SOProductDetailsWithoutDiscount, paramsToStore);


                if (dt != null)
                {
                    list = new Collection<SlsSalesOrderDetailViewModel>();
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add((SlsSalesOrderDetailViewModel)ERPOptima.Lib.Utilities.Helper.FillTo(row, typeof(SlsSalesOrderDetailViewModel)));
                    }

                    //
                    if(list != null && list.Count > 0)
                    {
                        decimal total = list[0].Total;
                        productPrice = total - (total * discount / 100);
                    }
                }
                return productPrice;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Operation Save(SlsSalesOrderViewModel obj)
        {
            Operation objOperation = new Operation { Success = false };

            using (var dbContextTransaction = _salesOrderRepository.BeginTransaction())
            {
                objOperation = SaveSalesOrder(obj, objOperation, dbContextTransaction);
            }
            return objOperation;
        }

        public Operation SaveSalesOrder(SlsSalesOrderViewModel obj, Operation objOperation, System.Data.Entity.DbContextTransaction dbContextTransaction)
        {
            try
            {

                objOperation = new Operation { Success = true };

                SlsSalesOrder objT = SlsSalesOrderMapVMToModel.MapToSlsSalesOrder(obj);

                //0-New, 1- Inprogress or partial, 2-completed
                objT.Status = 0;
                //1=New,2=Approve,3=Pass,4=Reject
                objT.ApprovalStatus = 1;
                if (objT.SalesType == 2)
                {
                    objT.ApprovalStatus = 2;
                }
                else if(objT.SalesType == 3)
                {
                    objT.ApprovalStatus = 2;
                    objT.Status = 2;
                }

                int Id = _salesOrderRepository.AddEntity(objT);
                _salesOrderRepository.SaveChanges();
                objOperation.OperationId = Id;
                objT.Id = Id;

                //add or update categories offered to each offer
                IList<SlsSalesOrderDetail> detailList = new List<SlsSalesOrderDetail>();
                foreach (SlsSalesOrderDetailViewModel detail in obj.SalesOrderDetails)
                {
                    SlsSalesOrderDetail objD = SlsSalesOrderMapVMToModel.MapToSlsSalesOrderDetail(detail);
                    objD.SlsSalesOrderId = objT.Id;
                    if (objD.Id <= 0)
                        detailList.Add(objD);

                    else
                        _salesOrderDetailRepository.Update(objD);
                }
                //Add offer detail list - new offer details
                if (detailList != null && detailList.Count > 0)
                {
                    _salesOrderDetailRepository.AddEntityList(detailList);
                }
                _salesOrderDetailRepository.SaveChanges();

                //ignore the approval process for corporate sales (approved corporate app) and retail saels.
                if (objT.SalesType == 1)
                {
                    //Add a row to Sales Order Approval table for Line Manager of this Employee
                    int userID = objT.CreatedBy;
                    int? employeeID = new ERPOptima.Data.Security.Repository.SecUserRepository(new DatabaseFactory()).GetById(userID).HrmEmployeeId;
                    int empID = employeeID == null ? 0 : (int)employeeID;
                    int? lineManID = new ERPOptima.Data.Hrm.Repository.HrmEmployeeRepository(new DatabaseFactory()).GetById(empID).LineManager;
                    int linMID = lineManID == null ? 0 : (int)lineManID;

                    SlsSalesOrderApproval soApprovalObj = new SlsSalesOrderApproval()
                    {
                        SlsSalesOrderId = objT.Id,
                        From = empID,
                        To = linMID,
                        Action = 1,
                        Comment = "",
                        CreatedBy = objT.CreatedBy,
                        CreatedDate = objT.CreatedDate
                    };
                    int soAppId = _salesOrderApprovalRepository.AddEntity(soApprovalObj);
                    _salesOrderApprovalRepository.SaveChanges();

                    //Notification
                    NotificationService notificationService = new NotificationService(_notificationRepository, _notificationDetailRepository, _unitOfWork);
                    Operation notificationOperation = notificationService.AddNotification(NewSalesOrderNotificationMessage, SalesOrderNotificationURL,
                        soApprovalObj.SlsSalesOrderId, soApprovalObj.To, "Sales");                    
                }
                try
                {
                    _salesOrderRepository.Commit(dbContextTransaction);
                }
                catch (Exception ex)
                {
                    objOperation.Success = false;
                    objOperation.Message = ex.Message;
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                _salesOrderRepository.Rollback(dbContextTransaction);
            }
            return objOperation;
        }

        public Operation Update(SlsSalesOrderViewModel obj)
        {
            Operation objOperation = new Operation { Success = false };
            using (var dbContextTransaction = _salesOrderRepository.BeginTransaction())
            {
                try
                {
                    objOperation = new Operation { Success = true, OperationId = obj.Id };
                    SlsSalesOrder objT = SlsSalesOrderMapVMToModel.MapToSlsSalesOrder(obj);
                    _salesOrderRepository.Update(objT);
                    _salesOrderRepository.SaveChanges();

                    //add or update categories offered to each offer
                    IList<SlsSalesOrderDetail> adddOrUpdateList = new List<SlsSalesOrderDetail>();
                    IList<SlsSalesOrderDetail> detailList = new List<SlsSalesOrderDetail>();
                    foreach (SlsSalesOrderDetailViewModel detail in obj.SalesOrderDetails)
                    {

                        SlsSalesOrderDetail objD = SlsSalesOrderMapVMToModel.MapToSlsSalesOrderDetail(detail);
                        objD.SlsSalesOrderId = objT.Id;
                        if (objD.Id <= 0)
                        {
                            detailList.Add(objD);
                            adddOrUpdateList.Add(objD);
                        }

                        else
                        {
                            _salesOrderDetailRepository.Update(objD);
                            adddOrUpdateList.Add(objD);
                        }

                    }
                    //Add offer detail list - new offer details
                    if (detailList != null && detailList.Count > 0)
                    {
                        _salesOrderDetailRepository.AddEntityList(detailList);
                    }

                    //To delete removed items
                    IList<int> savedItems = _salesOrderDetailRepository.GetAll().Where(j => j.SlsSalesOrderId == objT.Id).Select(i => i.Id).ToList();
                    foreach (int savedId in savedItems)
                    {
                        var addedOrUpdatedObj = adddOrUpdateList.Where(i => i.Id == savedId).FirstOrDefault();
                        if (addedOrUpdatedObj == null)
                        {
                            //this saved id item removed from UI
                            var removedObj = _salesOrderDetailRepository.GetById(savedId);
                            _salesOrderDetailRepository.Delete(removedObj);
                        }
                    }
                    //end of deletion action

                    _salesOrderDetailRepository.SaveChanges();

                    try
                    {
                        //_unitOfWork.Commit();
                        _salesOrderRepository.Commit(dbContextTransaction);
                    }
                    catch (Exception ex)
                    {
                        objOperation.Success = false;
                        throw ex;
                    }
                }
                catch (Exception ex)
                {
                    objOperation.Success = false;
                    _salesOrderRepository.Rollback(dbContextTransaction);
                }
            }
            return objOperation;
        }

        public int? GetPartyOfficeByPartyTypeAndId(int partytype, int partyid)
        {
            int? result = null;
            switch (partytype)
            {
                case 1:
                    {
                        var distributorList = new DistributorRepository(new DatabaseFactory()).GetAll();
                        var party = distributorList.Where(i => i.Id == partyid).FirstOrDefault();
                        if (party != null)
                        {
                            result = party.HeadOfficeId;
                        }
                        break;
                    }
                case 2:
                    {
                        var retailerList = new RetailerRepository(new DatabaseFactory()).GetAll();
                        var party = retailerList.Where(i => i.Id == partyid).FirstOrDefault();
                        if (party != null)
                        {
                            result = party.SlsOfficeId;
                        }
                        break;
                    }
                case 3:
                    {
                        var dealerList = new DealerRepository(new DatabaseFactory()).GetAll();
                        var party = dealerList.Where(i => i.Id == partyid).FirstOrDefault();
                        if (party != null)
                        {
                            result = party.HeadOfficeId;
                        }
                        break;
                    }
                case 4:
                    {
                        var corporateClientList = new CorporateClientRepository(new DatabaseFactory()).GetAll();
                        var party = corporateClientList.Where(i => i.Id == partyid).FirstOrDefault();
                        if (party != null)
                        {
                            int districtId = party.SlsDistrictId;
                            var districtList = new DistrictRepository(new DatabaseFactory()).GetAll();
                            var district = districtList.Where(i => i.Id == districtId).FirstOrDefault();
                            if (district != null)
                            {
                                result = district.SlsOfficeId;
                            }
                        }
                        break;
                    }
            }
            return result;
        }

        public IEnumerable<SlsSalesOrderViewModel> GetAllForApproval(int userId)
        {
            IEnumerable<SlsSalesOrderViewModel> list = GetAllOrders(0);

            IList<SlsSalesOrderApproval> approvalList = _salesOrderApprovalRepository.GetAll();

            IList<int> requireApproval = new List<int>();

            if (approvalList != null && approvalList.Count() > 0)
            {
                int? employeeID = new ERPOptima.Data.Security.Repository.SecUserRepository(new DatabaseFactory()).GetById(userId).HrmEmployeeId;

                //According to DB column description: 1=New,2=Approve,3=Pass,4=Reject
                //2015-05-11: earlier it checked Action=1||3, but after Unit test, and TL decision - it will show all 
                requireApproval = approvalList.Where(i => (i.From == employeeID || i.To == employeeID)).Select(j => j.SlsSalesOrderId).ToList();
            }
            if (list != null && list.Count() > 0)
            {
                list = list.Where(s => requireApproval.Contains(s.Id)).OrderByDescending(d => d.OrderDate).OrderByDescending(i => i.Id).ToList();
            }

            return list;
        }

        public IEnumerable<SlsSalesOrderViewModel> GetAllForApprovalByDate(DateTime fromDate, DateTime toDate, int userId)
        {
            IEnumerable<SlsSalesOrderViewModel> list = GetAllForApproval(userId);

            if (list != null && list.Count() > 0)
            {
                list = list.Where(s => s.PreferredDeliveryDate >= fromDate && s.PreferredDeliveryDate <= toDate).OrderByDescending(d=>d.OrderDate).OrderByDescending(i=>i.Id).ToList();
            }


            return list;
        }

        public SlsSalesOrderApproval GetApprovalById(int salesOrderId)
        {
            SlsSalesOrderApproval obj = null;
            IList<SlsSalesOrderApproval> approvalList = _salesOrderApprovalRepository.GetAll();

            if (approvalList != null && approvalList.Count() > 0)
            {
                obj = approvalList.Where(i => i.SlsSalesOrderId == salesOrderId).FirstOrDefault();
            }
            return obj;
        }


        public Operation UpdateApproval(SlsSalesOrderApproval obj, string newComment)
        {
            //Operation objOperation = new Operation { Success = true, OperationId = obj.Id };
            //try
            //{
            //    //According to DB column description: 1=New,2=Approve,3=Pass,4=Reject
            //    //For passed Sales Order - From and To will be reassigned. Else these will hold previous value.
            //    if (obj.Action == 3)
            //    {
            //        //Add a row to Sales Order Approval table for Line Manager of this Employee
            //        int userID = obj.CreatedBy;
            //        int? employeeID = new ERPOptima.Data.Security.Repository.SecUserRepository(new DatabaseFactory()).GetById(userID).HrmEmployeeId;
            //        int empID = employeeID == null ? 0 : (int)employeeID;
            //        int? lineManID = new ERPOptima.Data.Hrm.Repository.HrmEmployeeRepository(new DatabaseFactory()).GetById(empID).LineManager;
            //        int linMID = lineManID == null ? 0 : (int)lineManID;

            //        obj.From = empID;
            //        obj.To = linMID;
            //    }

            //    _salesOrderApprovalRepository.Update(obj);                

            //    _unitOfWork.Commit();
            //}
            //catch (Exception ex)
            //{
            //    objOperation.Success = false;
            //}
            //return objOperation;


            ////////////////
            Operation objOperation = new Operation { Success = false };
            using (var dbContextTransaction = _salesOrderRepository.BeginTransaction())
            {
                try
                {
                    objOperation = new Operation { Success = true, OperationId = obj.Id };
                    SlsSalesOrder objT = _salesOrderRepository.GetById(obj.SlsSalesOrderId);

                    //Update sales order status and approval status
                    //Sales Order Approval: 1=New,2=Approve,3=Pass,4=Reject
                    switch (obj.Action)
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                            //1=New,2=Approve,3=Pass,4=Reject
                            objT.ApprovalStatus = obj.Action;
                            break;                        
                        default:
                            objT.ApprovalStatus = 1;
                            break;
                    }
                    ////1=New,2=Approve,3=Pass,4=Reject. Sales Order Approval Action = Sales Order ApprovalStatus
                    //objT.ApprovalStatus = obj.Action;

                    _salesOrderRepository.Update(objT);
                    _salesOrderRepository.SaveChanges();

                    //Sales Order Approval - update
                    //According to DB column description: 1=New,2=Approve,3=Pass,4=Reject
                    //For passed Sales Order - From and To will be reassigned. Else these will hold previous value.
                    if (obj.Action == 3)
                    {
                        //Add a row to Sales Order Approval table for Line Manager of this Employee
                        int userID = obj.CreatedBy;
                        int? employeeID = new ERPOptima.Data.Security.Repository.SecUserRepository(new DatabaseFactory()).GetById(userID).HrmEmployeeId;
                        int empID = employeeID == null ? 0 : (int)employeeID;
                        int? lineManID = new ERPOptima.Data.Hrm.Repository.HrmEmployeeRepository(new DatabaseFactory()).GetById(empID).LineManager;
                        int linMID = lineManID == null ? 0 : (int)lineManID;

                        obj.From = empID;
                        obj.To = linMID;
                    }

                    _salesOrderApprovalRepository.Update(obj);
                    _salesOrderApprovalRepository.SaveChanges();

                    //Notification
                    NotificationService notificationService = new NotificationService(_notificationRepository, _notificationDetailRepository, _unitOfWork);
                    Operation notificationOperation = notificationService.UpdateNotification(newComment, SalesOrderNotificationURL,
                        obj.SlsSalesOrderId, obj.To, "Sales");

                    try
                    {
                        //_unitOfWork.Commit();
                        _salesOrderRepository.Commit(dbContextTransaction);
                    }
                    catch (Exception ex)
                    {
                        objOperation.Success = false;
                        throw ex;
                    }
                }
                catch (Exception ex)
                {
                    _salesOrderRepository.Rollback(dbContextTransaction);
                }
            }
            return objOperation;
        }


        public decimal GetNetSales(DateTime From, DateTime To, int PartyType, int Party)
        {
            decimal result = 0;
            decimal? returnTotal = 0;

            //Get sum of all sales orders total by date range and party
            var list = _salesOrderRepository.GetAll();
            if (list != null && list.Count() > 0)
            {
                list = list.Where(i => i.OrderDate >= From && i.OrderDate <= To && i.PartyType == PartyType && i.Party == Party).ToList();
                foreach (var item in list)
                {
                    result += item.Total;
                }
            }

            //Get sum of all sales returns total by date range and party
            //AND then subtract from earlier sum.            
            var list2 = new SalesReturnRepository(new DatabaseFactory()).GetAll();
            if (list2 != null && list2.Count() > 0)
            {
                list2 = list2.Where(i => i.CreatedDate >= From && i.CreatedDate <= To && i.PartyType == PartyType && i.Party == Party).ToList();
                foreach (var item in list2)
                {
                    returnTotal += item.AdjustedAmount;
                }
            }
            if (returnTotal > 0)
                result = result - (decimal)returnTotal;

            return result;
        }

        public decimal GetNetSales(int year, int month, int salesPersonId)
        {
            decimal result = 0;

            var list = _salesOrderRepository.GetAll();
            if (list != null && list.Count() > 0)
            {
                list = list.Where(i => i.OrderDate != null && i.OrderDate.Value.Year == year &&
                    i.OrderDate.Value.Month == month && i.HrmEmployeeId == salesPersonId).ToList();
                foreach (var item in list)
                {
                    result += item.Total;
                }
            }

            ////Get sum of all sales returns total by date range and party
            ////AND then subtract from earlier sum.            
            //var list2 = new SalesReturnRepository(new DatabaseFactory()).GetAll();
            //if (list2 != null && list2.Count() > 0)
            //{
            //    list2 = list2.Where(i => i.CreatedDate >= From && i.CreatedDate <= To && i.HrmEmployeeId == salesPersonId).ToList();
            //    foreach (var item in list2)
            //    {
            //        returnTotal += item.AdjustedAmount;
            //    }
            //}
            //if (returnTotal > 0)
            //    result = result - (decimal)returnTotal;

            return result;
        }

        public string GetRefNo(int companyId, string prefix, string offcode)
        {
            string RefNo = prefix + "-" + "SO" + "-" + offcode + "-" + DateTime.Now.ToString("yy") + "-" + DateTime.Now.ToString("MM") + "/" + _salesOrderRepository.GetLastId(companyId).ToString();
            return RefNo;
        }

    }

    public class SlsSalesOrderMapVMToModel
    {
        public static SlsSalesOrder MapToSlsSalesOrder(SlsSalesOrderViewModel obj)
        {
            SlsSalesOrder model = new SlsSalesOrder();

            model.Id = obj.Id;

            model.RefNo = obj.RefNo;
            model.SlsOfficeId = obj.SlsOfficeId;
            model.PartyType = obj.PartyType;
            model.Party = obj.Party;
            model.PreferredDeliveryDate = obj.PreferredDeliveryDate;
            model.SlsCorporateSalesApplicationId = obj.SlsCorporateSalesApplicationId;
            model.TransactionType = obj.TransactionType;
            model.TransactionRefNo = obj.TransactionRefNo;
            model.Discount = obj.Discount;
            model.Total = obj.Total;
            model.Advance = obj.Advance;
            model.BankName = obj.BankName;
            model.Remarks = obj.Remarks;
            model.SecCompnayId = obj.SecCompnayId;

            model.Status = obj.Status;
            model.ApprovalStatus = obj.ApprovalStatus;
            model.SalesType = obj.SalesType;
            model.OptionalPartyName = obj.OptionalPartyName;
            model.OrderDate = obj.OrderDate;
            model.HrmEmployeeId = obj.HrmEmployeeId;

            model.CreatedBy = obj.CreatedBy;
            model.CreatedDate = obj.CreatedDate;
            model.OrderDate = obj.OrderDate;
            model.ModifiedBy = obj.ModifiedBy;
            model.ModifiedDate = obj.ModifiedDate;

            return model;
        }

        public static SlsSalesOrderDetail MapToSlsSalesOrderDetail(SlsSalesOrderDetailViewModel obj)
        {
            SlsSalesOrderDetail model = new SlsSalesOrderDetail();

            model.Id = obj.Id;
            model.SlsSalesOrderId = obj.SlsSalesOrderId;
            model.SlsProductId = obj.SlsProductId;
            model.SalesOrderQuantity = obj.SalesOrderQuantity;
            model.SlsUnitId = obj.SlsUnitId;
            model.Rate = obj.Rate;
            model.Price = obj.Price;
            model.Discount = obj.Discount;
            model.Total = obj.Total;

            return model;
        }
    }
}
