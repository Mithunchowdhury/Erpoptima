using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Sales;
using ERPOptima.Service.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ERPOptima.Service.Sales
{
    public interface IRoutePlanService<TObj, TObjVM> : IApprovalService<TObj, TObjVM>
        where TObj : SlsRoutePlanApproval
        where TObjVM : SlsRoutePlan    
    {
        IList<SlsRoutePlan> GetAll(int employeeId);
        Operation Save(SlsRoutePlan record);
        Operation Update(SlsRoutePlan record);
        Operation Delete(SlsRoutePlan record);

        SlsRoutePlan GetById(int Id);
        IList<RoutePlanDetail> GetByRoutePlanId(int RoutePlanId);
    }
    public class RoutePlanService : IRoutePlanService<SlsRoutePlanApproval, SlsRoutePlan>
    {
        /* *
        * For Notification Messaging 
        * */
        private string NewRoutePlanNotificationMessage = "New Route Plan for Approval";
        private string RoutePlanNotificationURL = "/Sales/RouteSetup/ApprovalById/?RoutePlanNumber=";


        private IRoutePlanRepository _repository;
        private IRoutePlanDetailRepository _detailrepository;
        private INotificationRepository _notificationRepository;
        private INotificationDetailRepository _notificationDetailRepository;
        private IRoutePlanApprovalRepository _approvalRepository;        
        private IUnitOfWork _unitOfWork;
        public RoutePlanService(IRoutePlanRepository repository,
            IRoutePlanDetailRepository detailrepository,
            IRoutePlanApprovalRepository approvalRepository,
            INotificationRepository notificationRepository,
            INotificationDetailRepository notificationDetailRepository, 
            IUnitOfWork unitOfWork)
        {
            this._repository = repository;
            this._detailrepository = detailrepository;
            this._approvalRepository = approvalRepository;
            this._notificationRepository = notificationRepository;
            this._notificationDetailRepository = notificationDetailRepository;
            this._unitOfWork = unitOfWork;
        }
        public IList<SlsRoutePlan> GetAll(int employeeId)
        {
            return _repository.GetAll(employeeId);
        }

        public  IList<RoutePlanDetail> GetByRoutePlanId(int RoutePlanId)
        {
            return _detailrepository.GetByRoutePlanId(RoutePlanId);


        }
        public Operation Save(SlsRoutePlan record)
        {

            Operation objOperation = new Operation { Success = true };

            //0-New, 1- Inprogress or partial, 2-completed
            record.Status = 0;
            //1=New,2=Approve,3=Pass,4=Reject
            record.ApprovalStatus = 1;
         
            List<SlsRoutePlanDetail> list = record.SlsRoutePlanDetails.Where(t => t.SlsRouteId == null).ToList();
            foreach(SlsRoutePlanDetail itm in list)
            {
                record.SlsRoutePlanDetails.Remove(itm);
            }
            int Id = _repository.AddEntity(record);
            objOperation.OperationId = Id;
            //Detail section.
            if (record.SlsRoutePlanDetails != null && record.SlsRoutePlanDetails.Count() > 0)
            {
                new RoutePlanDetailRepository(new DatabaseFactory()).AddEntities(record.SlsRoutePlanDetails.ToList());
            }

            SaveApproval(record);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                objOperation.Success = false;
            }

            return objOperation;


        }
        public SlsRoutePlan GetById(int Id)
        {
            return _repository.GetById(Id);
        }
        public Operation Update(SlsRoutePlan record)
        {
            Operation objOperation = new Operation { Success = true, OperationId = record.Id };
            _repository.Update(record);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                objOperation.Success = false;
            }
            return objOperation;
        }

        public Operation Delete(SlsRoutePlan record)
        {
            Operation objOperation = new Operation { Success = true, OperationId = record.Id };
            _repository.Delete(record);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {

                objOperation.Success = false;
            }
            return objOperation;
        }

        private void SaveApproval(SlsRoutePlan record)
        {
            //Add a row to Sales Order Approval table for Line Manager of this Employee
            int userID = record.CreatedBy;
            int? employeeID = new ERPOptima.Data.Security.Repository.SecUserRepository(new DatabaseFactory()).GetById(userID).HrmEmployeeId;
            int empID = employeeID == null ? 0 : (int)employeeID;
            int? lineManID = new ERPOptima.Data.Hrm.Repository.HrmEmployeeRepository(new DatabaseFactory()).GetById(empID).LineManager;
            int linMID = lineManID == null ? 0 : (int)lineManID;

            SlsRoutePlanApproval approvalObj = new SlsRoutePlanApproval()
            {
                SlsRoutePlanId = record.Id,
                From = empID,
                To = linMID,
                Action = 1,
                Comment = "",
                CreatedBy = record.CreatedBy,
                CreatedDate = record.CreatedDate
            };
            int soAppId = _approvalRepository.AddEntity(approvalObj);
            _approvalRepository.SaveChanges();

            //Notification
            NotificationService notificationService = new NotificationService(_notificationRepository, _notificationDetailRepository, _unitOfWork);
            Operation notificationOperation = notificationService.AddNotification(NewRoutePlanNotificationMessage, RoutePlanNotificationURL,
                approvalObj.SlsRoutePlanId, approvalObj.To, "Route Plan");
        }

        #region Approval
        public IEnumerable<SlsRoutePlan> GetAllForApproval(int userId)
        {
            IEnumerable<SlsRoutePlan> list = _repository.GetAll();

            IList<SlsRoutePlanApproval> approvalList = _approvalRepository.GetAll();

            IList<int> requireApproval = new List<int>();

            if (approvalList != null && approvalList.Count() > 0)
            {
                int? employeeID = new ERPOptima.Data.Security.Repository.SecUserRepository(new DatabaseFactory()).GetById(userId).HrmEmployeeId;

                //According to DB column description: 1=New,2=Approve,3=Pass,4=Reject
                //2015-05-11: earlier it checked Action=1||3, but after Unit test, and TL decision - it will show all 
                requireApproval = approvalList.Where(i => (i.From == employeeID || i.To == employeeID)).Select(j => j.SlsRoutePlanId).ToList();
            }
            if (list != null && list.Count() > 0)
            {
                list = list.Where(s => requireApproval.Contains(s.Id)).ToList();
            }

            return list;
        }

        public IEnumerable<SlsRoutePlan> GetAllForApprovalByDate(DateTime fromDate, DateTime toDate, int userId)
        {
            IEnumerable<SlsRoutePlan> list = GetAllForApproval(userId);

            if (list != null && list.Count() > 0)
            {
                list = list.Where(s => s.CreatedDate >= fromDate && s.CreatedDate <= toDate).ToList();
            }

            return list;
        }

        public SlsRoutePlanApproval GetApprovalById(int appId)
        {
            SlsRoutePlanApproval obj = null;
            IList<SlsRoutePlanApproval> approvalList = _approvalRepository.GetAll();

            if (approvalList != null && approvalList.Count() > 0)
            {
                obj = approvalList.Where(i => i.SlsRoutePlanId == appId).FirstOrDefault();
            }
            return obj;
        }


        public Operation UpdateApproval(SlsRoutePlanApproval obj, string newComment)
        {
            Operation objOperation = new Operation { Success = false };
            using (var dbContextTransaction = _repository.BeginTransaction())
            {
                try
                {
                    objOperation = new Operation { Success = true, OperationId = obj.Id };
                    SlsRoutePlan objT = _repository.GetById(obj.SlsRoutePlanId);

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
                    //1=New,2=Approve,3=Pass,4=Reject. Sales Order Approval Action = Sales Order ApprovalStatus
                    //objT.ApprovalStatus = obj.Action;

                    _repository.Update(objT);
                    _repository.SaveChanges();

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

                    _approvalRepository.Update(obj);
                    _approvalRepository.SaveChanges();

                    //Notification
                    NotificationService notificationService = new NotificationService(_notificationRepository, _notificationDetailRepository, _unitOfWork);
                    Operation notificationOperation = notificationService.UpdateNotification(newComment, RoutePlanNotificationURL,
                        obj.SlsRoutePlanId, obj.To, "Route Plan");

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
