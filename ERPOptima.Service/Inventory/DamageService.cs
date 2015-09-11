using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Inventory.Repository;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Inventory;
using ERPOptima.Service.Common;
using ERPOptima.Service.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Service.Inventory
{
    

    #region interface
    public interface IDamageService<TObj, TObjVM> : IApprovalService<TObj, TObjVM>
        where TObj : InvDamageApproval
        where TObjVM : InvDamage
    {

        IEnumerable<InvDamage> GetAll();
        IList<InvDamage> GetAll(int companyId);
        InvDamage GetById(int Id);
        int GetLastId();
        string GetLastCode(int companyId, string companyPrefix, string offcode);

        Operation Save(InvDamage objInvDamage);
        Operation Update(InvDamage objInvDamage);
        Operation Delete(InvDamage objInvDamage);
        Operation Commit();

    }

    #endregion

    public class DamageService : IDamageService<InvDamageApproval, InvDamage>
    {
        /* *
         * For Notification Messaging 
         * */
        private string NewDanageNotificationMessage = "New Damage for Approval";
        private string DamageNotificationURL = "/Inventory/Damage/ApprovalById/?DamageNumber=";

        private IInvDamageRepository _DamageRepository;
        private INotificationRepository _notificationRepository;
        private INotificationDetailRepository _notificationDetailRepository;
        private IInvDamageApprovalRepository _DamageApprovalRepository;
        private IUnitOfWork _UnitOfWork;
        public DamageService(IInvDamageRepository damageRepository,
             INotificationRepository notificationRepository,
            INotificationDetailRepository notificationDetailRepository,
            IInvDamageApprovalRepository damageApprovalRepository,
            IUnitOfWork unitOfWork)
        {
            this._DamageRepository = damageRepository;
            this._notificationRepository = notificationRepository;
            this._notificationDetailRepository = notificationDetailRepository;
            this._DamageApprovalRepository = damageApprovalRepository;
            this._UnitOfWork = unitOfWork;
        }


        public IEnumerable<InvDamage> GetAll()
        {
            return _DamageRepository.GetAll();
        }
        public IList<InvDamage> GetAll(int companyId)
        {
            return _DamageRepository.GetAll(companyId);
        }
        public InvDamage GetById(int Id)
        {
            return _DamageRepository.GetById(Id);
        }

        public int GetLastId()
        {
            return _DamageRepository.GetLastId();
        }

        public string GetLastCode(int companyId, string companyPrefix, string offcode)
        {
            string RefNo = companyPrefix + "-" + "DMG" + "-" + offcode + "-" + DateTime.Now.ToString("yy") + "-" + DateTime.Now.ToString("MM") + "/" + _DamageRepository.GetLastCode(companyId).ToString();
            return RefNo;
        }


        public Operation Update(InvDamage objInvDamage)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objInvDamage.Id };
            _DamageRepository.Update(objInvDamage);

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

        public Operation Delete(InvDamage objInvDamage)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objInvDamage.Id };
            _DamageRepository.Delete(objInvDamage);

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


        public Operation Save(InvDamage objInvDamage)
        {

            Operation objOperation = new Operation { Success = true };

            //0-New, 1- Inprogress or partial, 2-completed
            objInvDamage.Status = 0;
            //1=New,2=Approve,3=Pass,4=Reject
            objInvDamage.ApprovalStatus = 1;

            int Id = _DamageRepository.AddEntity(objInvDamage);
            objOperation.OperationId = Id;

            //Detail section.
            if (objInvDamage.InvDamageDetails != null && objInvDamage.InvDamageDetails.Count() > 0)
            {
                new InvDamageDetailRepository(new DatabaseFactory()).AddEntityList(objInvDamage.InvDamageDetails.ToList());
            }

            SaveApproval(objInvDamage);

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
        public Operation Commit()
        {
            Operation objOperation = new Operation { Success = true };

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

        #region Approval
        public Operation SaveApproval(InvDamage obj)
        {
            Operation objOperation = new Operation { Success = true };

            int userID = obj.CreatedBy;
            int? employeeID = new ERPOptima.Data.Security.Repository.SecUserRepository(new DatabaseFactory()).GetById(userID).HrmEmployeeId;
            int empID = employeeID == null ? 0 : (int)employeeID;
            int? lineManID = new ERPOptima.Data.Hrm.Repository.HrmEmployeeRepository(new DatabaseFactory()).GetById(empID).LineManager;
            int linMID = lineManID == null ? 0 : (int)lineManID;

            InvDamageApproval approvalObj = new InvDamageApproval()
            {
                InvDamageId = obj.Id,
                From = empID,
                To = linMID,
                Action = 1,
                Comment = "",
                CreatedBy = obj.CreatedBy,
                CreatedDate = obj.CreatedDate
            };
            int soAppId = _DamageApprovalRepository.AddEntity(approvalObj);
            _DamageApprovalRepository.SaveChanges();

            //Notification

            NotificationService notificationService = new NotificationService(_notificationRepository, _notificationDetailRepository, _UnitOfWork);
            Operation notificationOperation = notificationService.AddNotification(NewDanageNotificationMessage, DamageNotificationURL,
                (int)approvalObj.InvDamageId, (int)approvalObj.To, "Damage");


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
        public IEnumerable<InvDamage> GetAllForApproval(int userId)
        {
            IEnumerable<InvDamage> list = _DamageRepository.GetAll().ToList();

            IList<InvDamageApproval> approvalList = _DamageApprovalRepository.GetAll();

            IList<int> requireApproval = new List<int>();

            if (approvalList != null && approvalList.Count() > 0)
            {
                int? employeeID = new ERPOptima.Data.Security.Repository.SecUserRepository(new DatabaseFactory()).GetById(userId).HrmEmployeeId;

                //According to DB column description: 1=New,2=Approve,3=Pass,4=Reject
                //2015-05-11: earlier it checked Action=1||3, but after Unit test, and TL decision - it will show all 
                requireApproval = approvalList.Where(i => (i.From == employeeID || i.To == employeeID)).Select(j => j.InvDamageId).ToList();
            }
            if (list != null && list.Count() > 0)
            {
                list = list.Where(s => requireApproval.Contains(s.Id)).ToList();
            }

            return list;
        }

        public IEnumerable<InvDamage> GetAllForApprovalByDate(DateTime fromDate, DateTime toDate, int userId)
        {
            IEnumerable<InvDamage> list = _DamageRepository.GetAll().ToList();

            if (list != null && list.Count() > 0)
            {
                list = list.Where(s => s.CreatedDate >= fromDate && s.CreatedDate <= toDate).ToList();
            }

            return list;
        }

        public InvDamageApproval GetApprovalById(int damageId)
        {
            InvDamageApproval obj = null;
            IList<InvDamageApproval> approvalList = _DamageApprovalRepository.GetAll();

            if (approvalList != null && approvalList.Count() > 0)
            {
                obj = approvalList.Where(i => i.InvDamageId == damageId).FirstOrDefault();
            }
            return obj;
        }


        public Operation UpdateApproval(InvDamageApproval obj, string newComment)
        {
            ////////////////
            Operation objOperation = new Operation { Success = false };
            using (var dbContextTransaction = _DamageRepository.BeginTransaction())
            {
                try
                {
                    objOperation = new Operation { Success = true, OperationId = obj.Id };
                    InvDamage objT = _DamageRepository.GetById(obj.InvDamageId);

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

                    _DamageRepository.Update(objT);
                    _DamageRepository.SaveChanges();

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

                    _DamageApprovalRepository.Update(obj);
                    _DamageApprovalRepository.SaveChanges();

                    //Notification
                    NotificationService notificationService = new NotificationService(_notificationRepository, _notificationDetailRepository, _UnitOfWork);
                    Operation notificationOperation = notificationService.UpdateNotification(newComment, NewDanageNotificationMessage,
                        obj.InvDamageId, obj.To, "Damage");

                    try
                    {
                        //_unitOfWork.Commit();
                        _DamageRepository.Commit(dbContextTransaction);
                    }
                    catch (Exception ex)
                    {
                        objOperation.Success = false;
                        throw ex;
                    }
                }
                catch (Exception ex)
                {
                    _DamageRepository.Rollback(dbContextTransaction);
                }
            }
            return objOperation;
        }
        #endregion


    }


}
