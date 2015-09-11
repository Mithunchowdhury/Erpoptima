using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Inventory.Repository;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Inventory;
using ERPOptima.Service.Common;
using ERPOptima.Service.Sales;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Service.Inventory
{
    #region interface
     public interface IRequisitionService<TObj, TObjVM> : IApprovalService<TObj, TObjVM>
        where TObj : InvRequisitionApproval
        where TObjVM : InvRequisitionViewModel    
    {

        IList<InvRequisition> GetAll(int companyId);
        IEnumerable<InvRequisition> GetAll();
        InvRequisition GetById(int Id);
        int GetLastId();
        string GetLastCode(int companyId, string prefix,string offcode);

        Operation Save(InvRequisition objInvRequisition);
        Operation Update(InvRequisition objInvRequisition);
        Operation Delete(InvRequisition objInvRequisition);
     
                

    }


    #endregion
    public class RequisitionService : IRequisitionService<InvRequisitionApproval, InvRequisitionViewModel>
    {
        /* *
         * For Notification Messaging 
         * */
        private string NewRequisitionNotificationMessage = "New Requisition for Approval";
        private string RequisitionNotificationURL = "/Inventory/Requisition/ApprovalById/?RequisitionNumber=";


        private IRequisitionApprovalRepository _RequisitionApprovalRepository;
        private IRequisitionRepository _RequisitionRepository;
        private INotificationRepository _notificationRepository;
        private INotificationDetailRepository _notificationDetailRepository;
        private IUnitOfWork _UnitOfWork;        
        public RequisitionService(IRequisitionRepository requisitionRepository,
            IRequisitionApprovalRepository requisitionApprovalRepository,
            INotificationRepository notificationRepository,
            INotificationDetailRepository notificationDetailRepository,
            IUnitOfWork unitOfWork)
        {
            this._RequisitionRepository = requisitionRepository;
            this._RequisitionApprovalRepository = requisitionApprovalRepository;
            this._notificationRepository = notificationRepository;
            this._notificationDetailRepository = notificationDetailRepository;
            this._UnitOfWork = unitOfWork;
        }

        public IEnumerable<InvRequisition> GetAll()
        {
            return _RequisitionRepository.GetAll();
        }


        public int GetLastId()
        {
            return _RequisitionRepository.GetLastId();
        }


        public string GetLastCode(int companyId, string prefix, string offcode)
        {

            string code = prefix + "-" + "REQ" + "-" + offcode + "-" + DateTime.Now.ToString("yy") + "-" + DateTime.Now.ToString("MM") + "/" + _RequisitionRepository.GetLastCode(companyId).ToString();
            return code;
        }
        public IList<InvRequisition> GetAll(int companyId)
        {

            return _RequisitionRepository.GetAll(companyId);
        }

        public InvRequisition GetById(int Id)
        {
            return _RequisitionRepository.GetById(Id);
        }

        public Operation Save(InvRequisition objInvRequisition)
        {
            Operation objOperation = new Operation { Success = true };

            //0-New, 1- Inprogress or partial, 2-completed
            objInvRequisition.Status = 0;
            //1=New,2=Approve,3=Pass,4=Reject
            objInvRequisition.ApprovalStatus = 1;

            int Id = _RequisitionRepository.AddEntity(objInvRequisition);
            objOperation.OperationId = Id;

            SaveApproval(objInvRequisition);

            try
            {
                _UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                objOperation.Success = false;
            }

            return objOperation;
        }

        public Operation Update(InvRequisition objInvRequisition)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objInvRequisition.Id };
            _RequisitionRepository.Update(objInvRequisition);

            try
            {
                _UnitOfWork.Commit();
            }
            catch (Exception)
            {
                objOperation.Success = false;
            }
            return objOperation;
        }

        public Operation Delete(InvRequisition objInvRequisition)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objInvRequisition.Id };
            _RequisitionRepository.Delete(objInvRequisition);

            try
            {
                _UnitOfWork.Commit();
            }
            catch (Exception)
            {
                objOperation.Success = false;
            }
            return objOperation;
        }


        #region Approval

        public Operation SaveApproval(InvRequisition objInvRequisition)
        {
            Operation objOperation = new Operation { Success = true };

            int userID = objInvRequisition.CreatedBy;
            int? employeeID = new ERPOptima.Data.Security.Repository.SecUserRepository(new DatabaseFactory()).GetById(userID).HrmEmployeeId;
            int empID = employeeID == null ? 0 : (int)employeeID;
            int? lineManID = new ERPOptima.Data.Hrm.Repository.HrmEmployeeRepository(new DatabaseFactory()).GetById(empID).LineManager;
            int linMID = lineManID == null ? 0 : (int)lineManID;

            InvRequisitionApproval approvalObj = new InvRequisitionApproval()
            {
                InvRequisitionsId = objInvRequisition.Id,
                From = empID,
                To = linMID,
                Action = 1,
                Comment = "",
                CreatedBy = objInvRequisition.CreatedBy,
                CreatedDate = objInvRequisition.CreatedDate
            };
            int soAppId = _RequisitionApprovalRepository.AddEntity(approvalObj);
            _RequisitionApprovalRepository.SaveChanges();

            //Notification
            if (approvalObj.InvRequisitionsId != null && approvalObj.To != null)
            {
                NotificationService notificationService = new NotificationService(_notificationRepository, _notificationDetailRepository, _UnitOfWork);
                Operation notificationOperation = notificationService.AddNotification(NewRequisitionNotificationMessage, RequisitionNotificationURL,
                    (int)approvalObj.InvRequisitionsId, (int)approvalObj.To, "Requisition");
            }
            
            objOperation.OperationId = soAppId;

            try
            {
                _UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                objOperation.Success = false;
            }

            return objOperation;
        }
        public IEnumerable<InvRequisitionViewModel> GetAllRequisitions(int reqId)
        {
            try
            {
                Collection<InvRequisitionViewModel> list = null;
                SqlParameter[] paramsToStore = new SqlParameter[1];
                paramsToStore[0] = new SqlParameter("@RequisitionId", reqId);
                DataTable dt = _RequisitionRepository.GetFromStoredProcedure(SPList.InvRequisition.GetAllInvRequisitions, paramsToStore);

                if (dt != null)
                {
                    list = new Collection<InvRequisitionViewModel>();
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add((InvRequisitionViewModel)ERPOptima.Lib.Utilities.Helper.FillTo(row, typeof(InvRequisitionViewModel)));
                    }
                }

                //to get categories offered for each offer
                foreach (InvRequisitionViewModel item in list)
                {
                    //attach detail list to item
                    //TODO: as in Approval don't need this now.
                }

                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public IEnumerable<InvRequisitionViewModel> GetAllForApproval(int userId)
        {
            IEnumerable<InvRequisitionViewModel> list = GetAllRequisitions(0);

            IList<InvRequisitionApproval> approvalList = _RequisitionApprovalRepository.GetAll();

            IList<int?> requireApproval = new List<int?>();

            if (approvalList != null && approvalList.Count() > 0)
            {
                int? employeeID = new ERPOptima.Data.Security.Repository.SecUserRepository(new DatabaseFactory()).GetById(userId).HrmEmployeeId;

                //According to DB column description: 1=New,2=Approve,3=Pass,4=Reject
                //2015-05-11: earlier it checked Action=1||3, but after Unit test, and TL decision - it will show all 
                requireApproval = approvalList.Where(i => (i.From == employeeID || i.To == employeeID)).Select(j => j.InvRequisitionsId).ToList();
            }
            if (list != null && list.Count() > 0)
            {
                list = list.Where(s => requireApproval.Contains(s.Id)).ToList();
            }

            return list;
        }

        public IEnumerable<InvRequisitionViewModel> GetAllForApprovalByDate(DateTime fromDate, DateTime toDate, int userId)
        {
            IEnumerable<InvRequisitionViewModel> list = GetAllForApproval(userId);

            if (list != null && list.Count() > 0)
            {
                list = list.Where(s => s.PreferredDeliveryDate >= fromDate && s.PreferredDeliveryDate <= toDate).ToList();
            }

            return list;
        }

        public InvRequisitionApproval GetApprovalById(int reqId)
        {
            InvRequisitionApproval obj = null;
            IList<InvRequisitionApproval> approvalList = _RequisitionApprovalRepository.GetAll();

            if (approvalList != null && approvalList.Count() > 0)
            {
                obj = approvalList.Where(i => i.InvRequisitionsId == reqId).FirstOrDefault();
            }
            return obj;
        }


        public Operation UpdateApproval(InvRequisitionApproval obj, string newComment)
        {
            //////////////////
            Operation objOperation = new Operation { Success = false };
            using (var dbContextTransaction = _RequisitionRepository.BeginTransaction())
            {
                try
                {
                    objOperation = new Operation { Success = true, OperationId = obj.Id };
                    InvRequisition objT = _RequisitionRepository.GetById((int)obj.InvRequisitionsId);

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

                    _RequisitionRepository.Update(objT);
                    _RequisitionRepository.SaveChanges();

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

                    _RequisitionApprovalRepository.Update(obj);
                    _RequisitionApprovalRepository.SaveChanges();

                    //Notification
                    if (obj.InvRequisitionsId != null && obj.To != null)
                    {
                        NotificationService notificationService = new NotificationService(_notificationRepository, _notificationDetailRepository, _UnitOfWork);
                        Operation notificationOperation = notificationService.UpdateNotification(newComment, RequisitionNotificationURL,
                            (int)obj.InvRequisitionsId, (int)obj.To, "Requisition");
                    }

                    try
                    {
                        //_unitOfWork.Commit();
                        _RequisitionRepository.Commit(dbContextTransaction);
                    }
                    catch (Exception ex)
                    {
                        objOperation.Success = false;
                        throw ex;
                    }
                }
                catch (Exception ex)
                {
                    _RequisitionRepository.Rollback(dbContextTransaction);
                }
            }
            return objOperation;
        }

        #endregion

    }
}
