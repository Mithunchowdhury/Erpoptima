using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Sales;
using ERPOptima.Model.ViewModel;
using ERPOptima.Service.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ERPOptima.Service.Sales
{
    public interface ICorporateSalesService<TObj, TObjVM> : IApprovalService<TObj, TObjVM>
        where TObj : SlsCorporateSalesApproval
        where TObjVM : SlsCorporateSalesApplication
    {

        IEnumerable<SlsCorporateSalesApplication> GetAll(int companyId);
        IEnumerable<SlsCorporateSalesApplication> GetAll();
        Operation Save(SlsCorporateSalesApplication obj);
        Operation Delete(SlsCorporateSalesApplication obj);
        SlsCorporateSalesApplication GetById(int Id);
        Operation Update(SlsCorporateSalesApplication obj);

        string GetRef(int companyId, string prefix, string offcode);
        IList<CorporateSalesDetail> GetByCorporateSalesId(int CorporateSalesId);
        IEnumerable<SlsCorporateSalesApplicationDetail> GetDetails(int corporateAppId);
    }
    public class CorporateSalesService : ICorporateSalesService<SlsCorporateSalesApproval, SlsCorporateSalesApplication>
    {
        /* *
        * For Notification Messaging 
        * */
        private string NewCoSalesNotificationMessage = "New Corporate Sales for Approval";
        private string CoSalesNotificationURL = "/Sales/CorporateSales/ApprovalById/?CorporateSalesNumber=";


        private ICorporateSalesRepository _repository;
        private INotificationRepository _notificationRepository;
        private ICorporateSalesApprovalRepository _approvalRepository;
        private INotificationDetailRepository _notificationDetailRepository;
        private ICorporateSalesDetailRepository _detailRepository;
        private IUnitOfWork _unitOfWork;
        public CorporateSalesService(ICorporateSalesRepository repository, ICorporateSalesDetailRepository detailRepository,
            ICorporateSalesApprovalRepository approvalRepository,
            INotificationRepository notificationRepository,
            INotificationDetailRepository notificationDetailRepository,
            IUnitOfWork unitOfWork)
        {
            this._repository = repository;
            this._detailRepository = detailRepository;
            this._approvalRepository = approvalRepository;
            this._notificationRepository = notificationRepository;
            this._notificationDetailRepository = notificationDetailRepository;
            this._unitOfWork = unitOfWork;
        }
        public IEnumerable<SlsCorporateSalesApplication> GetAll(int companyId)
        {

            return _repository.GetAll(companyId);
            //return _corporateClientRepository.GetAll();
            //return null;
        }
        public IEnumerable<SlsCorporateSalesApplication> GetAll()
        {
            return _repository.GetAll();
        }

        public IEnumerable<SlsCorporateSalesApplicationDetail> GetDetails(int corporateAppId)
        {
            var list = _detailRepository.GetAll();

            if (list != null)
            {
                try
                {
                    list = list.Where(i => i.SlsCorporateSalesApplicationId == corporateAppId).ToList();
                }
                catch(Exception ex)
                {

                }
            }

            return list;
        }
        public IList<CorporateSalesDetail> GetByCorporateSalesId(int CorporateSalesId)
        {
            return _detailRepository.GetByCorporateSalesId(CorporateSalesId);


        }
        public SlsCorporateSalesApplication GetById(int Id)
        {
            //SlsCorporateClient obj = _corporateClientRepository.GetById(Id);
            //return obj;
            return null;
        }
        public Operation Update(SlsCorporateSalesApplication obj)
        {
            //Operation objOperation = new Operation { Success = true, OperationId = obj.Id };
            //_corporateClientRepository.Update(obj);

            //try
            //{
            //    _UnitOfWork.Commit();
            //}
            //catch (Exception)
            //{
            //    objOperation.Success = false;

            //}
            //return objOperation;
            return null;
        }
        public Operation Delete(SlsCorporateSalesApplication obj)
        {
            //Operation objOperation = new Operation { Success = true, OperationId = obj.Id };
            //_corporateClientRepository.Delete(obj);

            //try
            //{
            //    _UnitOfWork.Commit();
            //}
            //catch (Exception)
            //{

            //    objOperation.Success = false;
            //}
            //return objOperation;
            return null;
        }
        public string GetRef(int companyId, string prefix, string offcode)
        {
            string refNo = prefix + "-" + "CSD" + "-" + offcode + "-" + DateTime.Now.ToString("yy") + "-" + DateTime.Now.ToString("MM") + "/" + _repository.GetLastCode(companyId).ToString();
            return refNo;
        }
        public Operation Save(SlsCorporateSalesApplication obj)
        {
            Operation objOperation = new Operation { Success = false };
            using (var dbContextTransaction = _repository.BeginTransaction())
            {
                try
                {
                    objOperation = new Operation { Success = true };

                    //0-New, 1- Inprogress or partial, 2-completed
                    obj.Status = 0;
                    //1=New,2=Approve,3=Pass,4=Reject
                    obj.ApprovalStatus = 1;

                    int Id = _repository.AddEntity(obj);

                    //Detail section.
                    if (obj.SlsCorporateSalesApplicationDetails != null && obj.SlsCorporateSalesApplicationDetails.Count() > 0)
                    {
                        _detailRepository.AddEntityList(obj.SlsCorporateSalesApplicationDetails.ToList());
                    }

                    _repository.SaveChanges();
                    objOperation.OperationId = Id;
                    _detailRepository.SaveChanges();

                    //Add a row to Sales Order Approval table for Line Manager of this Employee
                    int userID = obj.CreatedBy;
                    int? employeeID = new ERPOptima.Data.Security.Repository.SecUserRepository(new DatabaseFactory()).GetById(userID).HrmEmployeeId;
                    int empID = employeeID == null ? 0 : (int)employeeID;
                    int? lineManID = new ERPOptima.Data.Hrm.Repository.HrmEmployeeRepository(new DatabaseFactory()).GetById(empID).LineManager;
                    int linMID = lineManID == null ? 0 : (int)lineManID;

                    SlsCorporateSalesApproval approvalObj = new SlsCorporateSalesApproval()
                    {
                        SlsCorporateSalesApplicationId = obj.Id,
                        From = empID,
                        To = linMID,
                        Event = 1,
                        Comment = "",
                        CreatedBy = obj.CreatedBy,
                        CreatedDate = obj.CreatedDate
                    };
                    int soAppId = _approvalRepository.AddEntity(approvalObj);
                    _approvalRepository.SaveChanges();

                    //Notification
                    NotificationService notificationService = new NotificationService(_notificationRepository, _notificationDetailRepository, _unitOfWork);
                    Operation notificationOperation = notificationService.AddNotification(NewCoSalesNotificationMessage, CoSalesNotificationURL,
                        approvalObj.SlsCorporateSalesApplicationId, approvalObj.To, "Corporate Sales");


                    try
                    {
                        _repository.Commit(dbContextTransaction);
                    }
                    catch (Exception ex)
                    {
                        objOperation.Success = false;
                        throw ex;
                    }
                }
                catch (Exception ex)
                {
                    _repository.Rollback(dbContextTransaction);
                    objOperation.Success = false;
                    throw ex;
                }
            }
            return objOperation;
        }

        #region Approval
        public IEnumerable<SlsCorporateSalesApplication> GetAllForApproval(int userId)
        {
            IEnumerable<SlsCorporateSalesApplication> list = _repository.GetAll();

            IList<SlsCorporateSalesApproval> approvalList = _approvalRepository.GetAll();

            IList<int> requireApproval = new List<int>();

            if (approvalList != null && approvalList.Count() > 0)
            {
                int? employeeID = new ERPOptima.Data.Security.Repository.SecUserRepository(new DatabaseFactory()).GetById(userId).HrmEmployeeId;

                //According to DB column description: 1=New,2=Approve,3=Pass,4=Reject
                //2015-05-11: earlier it checked Action=1||3, but after Unit test, and TL decision - it will show all 
                requireApproval = approvalList.Where(i => (i.From == employeeID || i.To == employeeID)).Select(j => j.SlsCorporateSalesApplicationId).ToList();
            }
            if (list != null && list.Count() > 0)
            {
                list = list.Where(s => requireApproval.Contains(s.Id)).ToList();
            }

            return list;
        }

        public IEnumerable<SlsCorporateSalesApplication> GetAllForApprovalByDate(DateTime fromDate, DateTime toDate, int userId)
        {
            IEnumerable<SlsCorporateSalesApplication> list = GetAllForApproval(userId);

            if (list != null && list.Count() > 0)
            {
                list = list.Where(s => s.CreatedDate >= fromDate && s.CreatedDate <= toDate).ToList();
            }

            return list;
        }

        public SlsCorporateSalesApproval GetApprovalById(int appId)
        {
            SlsCorporateSalesApproval obj = null;
            IList<SlsCorporateSalesApproval> approvalList = _approvalRepository.GetAll();

            if (approvalList != null && approvalList.Count() > 0)
            {
                obj = approvalList.Where(i => i.SlsCorporateSalesApplicationId == appId).FirstOrDefault();
            }
            return obj;
        }


        public Operation UpdateApproval(SlsCorporateSalesApproval obj, string newComment)
        {
            Operation objOperation = new Operation { Success = false };
            using (var dbContextTransaction = _repository.BeginTransaction())
            {
                try
                {
                    objOperation = new Operation { Success = true, OperationId = obj.Id };
                    SlsCorporateSalesApplication objT = _repository.GetById(obj.SlsCorporateSalesApplicationId);

                    //Update sales order status and approval status
                    //Sales Order Approval: 1=New,2=Approve,3=Pass,4=Reject
                    switch (obj.Event)
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                            //1=New,2=Approve,3=Pass,4=Reject
                            objT.ApprovalStatus = obj.Event;
                            break;
                        default:
                            objT.ApprovalStatus = 1;
                            break;
                    }
                    ////1=New,2=Approve,3=Pass,4=Reject. Sales Order Approval Action = Sales Order ApprovalStatus
                    //objT.ApprovalStatus = obj.Event;

                    _repository.Update(objT);
                    _repository.SaveChanges();

                    //Sales Order Approval - update
                    //According to DB column description: 1=New,2=Approve,3=Pass,4=Reject
                    //For passed Sales Order - From and To will be reassigned. Else these will hold previous value.
                    if (obj.Event == 3)
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

                    _approvalRepository.Update(obj);
                    _approvalRepository.SaveChanges();

                    //Notification
                    NotificationService notificationService = new NotificationService(_notificationRepository, _notificationDetailRepository, _unitOfWork);
                    Operation notificationOperation = notificationService.UpdateNotification(newComment, CoSalesNotificationURL,
                        obj.SlsCorporateSalesApplicationId, obj.To, "Sales");

                    try
                    {
                        //_unitOfWork.Commit();
                        _repository.Commit(dbContextTransaction);
                    }
                    catch (Exception ex)
                    {
                        objOperation.Success = false;
                        throw ex;
                    }


                    ////2015-06-11: If Corporate sales application approved, make a sales order record.
                    ////1=New,2=Approve,3=Pass,4=Reject. Sales Order Approval Action = Sales Order ApprovalStatus
                    //try
                    //{
                    //    if (objT != null && objT.ApprovalStatus == 2)
                    //    {

                    //        var dbfactory = new DatabaseFactory();
                    //        var unitOfWork = new UnitOfWork(dbfactory);
                    //        var _salesOrderService = new SalesOrderService(new SalesOrderRepository(dbfactory), new SalesOrderDetailRepository(dbfactory),
                    //                                 new SalesOrderApprovalRepository(dbfactory), new NotificationRepository(dbfactory),
                    //                                 new NotificationDetailRepository(dbfactory), unitOfWork);

                    //        SlsSalesOrderViewModel newSalesObj = new SlsSalesOrderViewModel();

                    //        //Binding corporate sales value to sales order record.



                    //        objOperation = _salesOrderService.SaveSalesOrder(newSalesObj, objOperation, dbContextTransaction);
                    //        if(!objOperation.Success)
                    //        {
                    //            throw new Exception("New Sales Order Could not be created.");
                    //        }
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    objOperation.Success = false;
                    //    throw ex;
                    //}
                }
                catch (Exception ex)
                {
                    _repository.Rollback(dbContextTransaction);
                }
            }
            return objOperation;
        }
        #endregion


    }
}
