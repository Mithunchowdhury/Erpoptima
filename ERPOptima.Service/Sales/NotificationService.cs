using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Sales;
using ERPOptima.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Service.Sales
{
    public interface INotificationService
    {
        IList<SlsNotificationViewModel> GetAll(int employeeId);
        IList<SlsNotificationViewModel> GetNewNotifications(int employeeId);
        SlsNotification GetExistingNotification(int employeeId, string url);
        SlsNotificationDetail GetExistingNotificationDetail(int employeeId, int notificationId);
        Operation IgnoreNotification(int nId);
        Operation AddNotification(string message, string url, int id, int EmployeeId, string ntype);
        Operation UpdateNotification(string message, string url, int id, int EmployeeId, string ntype);
    }
    public class NotificationService : INotificationService
    {
        private INotificationRepository _NotificationRepository;
        private INotificationDetailRepository _NotificationDetailRepository;
        private IUnitOfWork _UnitOfWork;

        public NotificationService(INotificationRepository notificationRepository,
            INotificationDetailRepository notificationDetailRepository,
            IUnitOfWork unitOfWork)
        {
            this._NotificationRepository = notificationRepository;
            this._NotificationDetailRepository = notificationDetailRepository;
            this._UnitOfWork = unitOfWork;
        }

        public IList<SlsNotificationViewModel> GetAll(int employeeId)
        {
            try
            {
                var list = _NotificationRepository.GetAll().ToList();
                var detailList = _NotificationDetailRepository.GetAll().Select(i => i.SlsNotificationId).ToList();
                list = list.Where(i => detailList.Contains(i.Id)).ToList();

                var detailNewList = _NotificationDetailRepository.GetAll().Where(i => !i.IsRead).Select(i => i.SlsNotificationId).ToList();
                var listNew = list.Where(i => detailNewList.Contains(i.Id)).ToList();
                var listRead = list.Where(i => !detailNewList.Contains(i.Id)).ToList();

                var resultNew = listNew.Select(i => new SlsNotificationViewModel()
                {
                    Id = i.Id,
                    Message = i.Message,
                    URL = i.URL,
                    Date = i.Date,
                    IsRead = false,
                    NotificationType = i.Type
                }).Distinct().ToList();
                var resultRead = listRead.Select(i => new SlsNotificationViewModel()
                {
                    Id = i.Id,
                    Message = i.Message,
                    URL = i.URL,
                    Date = i.Date,
                    IsRead = true,
                    NotificationType = i.Type
                }).Distinct().ToList();

                var result = new List<SlsNotificationViewModel>();
                result.AddRange(resultNew);
                result.AddRange(resultRead);

                return result;
            }
            catch (Exception ex)
            {
                return new List<SlsNotificationViewModel>();
            }
        }

        public IList<SlsNotificationViewModel> GetNewNotifications(int employeeId)
        {
            try
            {
                var list = _NotificationRepository.GetAll().ToList();

                var detailList = _NotificationDetailRepository.GetAll().Where(i => !i.IsRead).Select(i => i.SlsNotificationId).ToList();

                list = list.Where(i => detailList.Contains(i.Id)).ToList();

                var result = list.Select(i => new SlsNotificationViewModel() { Id = i.Id, Message = i.Message, URL = i.URL, Date = i.Date, IsRead = false, NotificationType = i.Type }).Distinct().ToList();

                return result;
            }
            catch (Exception ex)
            {
                return new List<SlsNotificationViewModel>();
            }
        }

        public SlsNotification GetExistingNotification(int employeeId, string url)
        {
            try
            {
                var list = _NotificationRepository.GetAll().ToList();

                var detailList = _NotificationDetailRepository.GetAll().Where(i => i.HrmEmployeeId == employeeId).Select(i => i.SlsNotificationId).ToList();

                list = list.Where(i => detailList.Contains(i.Id) && i.URL == url).ToList();

                var item = list.FirstOrDefault();

                return item;
            }
            catch (Exception ex)
            {
                return new SlsNotification();
            }
        }

        public SlsNotificationDetail GetExistingNotificationDetail(int employeeId, int notificationId)
        {
            try
            {
                var detailList = _NotificationDetailRepository.GetAll().Where(i => i.HrmEmployeeId == employeeId && i.SlsNotificationId == notificationId).ToList();

                return detailList.FirstOrDefault();
            }
            catch (Exception ex)
            {
                return new SlsNotificationDetail();
            }
        }

        public SlsNotification GetById(int Id)
        {
            SlsNotification obj = _NotificationRepository.GetById(Id);
            return obj;
        }
        public Operation Update(SlsNotification obj)
        {
            Operation objOperation = new Operation { Success = true, OperationId = obj.Id };
            _NotificationRepository.Update(obj);

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

        public Operation Delete(SlsNotification obj)
        {
            Operation objOperation = new Operation { Success = true, OperationId = obj.Id };
            _NotificationRepository.Delete(obj);

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

        public Operation Save(SlsNotification obj)
        {
            Operation objOperation = new Operation { Success = true };

            long Id = _NotificationRepository.AddEntity(obj);
            objOperation.OperationId = Id;

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

        public Operation IgnoreNotification(int nId)
        {
            Operation objOperation = new Operation { Success = true, OperationId = nId };

            SlsNotificationDetail detail = _NotificationDetailRepository.GetAll().Where(i => i.SlsNotificationId == nId).FirstOrDefault();

            detail.IsRead = true;

            _NotificationDetailRepository.Update(detail);

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


        public Operation AddNotification(string message, string url, int id, int EmployeeId, string ntype)
        {
            string typeOfNotification = ntype;
            Operation objOperation = new Operation { Success = true };
            try
            {
                //START Notification Save
                SlsNotification notification = new SlsNotification()
                {
                    Message = message,
                    URL = url + id,
                    Type = typeOfNotification,
                    Date = DateTime.Now
                };

                int notifId = _NotificationRepository.AddEntity(notification);
                _NotificationRepository.SaveChanges();

                SlsNotificationDetail notificationDetail = new SlsNotificationDetail()
                {
                    SlsNotificationId = notifId,
                    HrmEmployeeId = EmployeeId,
                    IsRead = false,
                    Date = DateTime.Now
                };
                int notifDetailId = _NotificationDetailRepository.AddEntity(notificationDetail);
                _NotificationDetailRepository.SaveChanges();
                //END of Notification Save
            }
            catch (Exception ex)
            {
                objOperation.Success = false;
            }
            return objOperation;
        }

        public Operation UpdateNotification(string message, string url, int id, int EmployeeId, string ntype)
        {
            string typeOfNotification = ntype;
            Operation objOperation = new Operation { Success = true };
            try
            {
                //START Notification Save
                SlsNotification notification = new SlsNotification()
                {
                    Message = message,
                    URL = url + id,
                    Type = typeOfNotification,
                    Date = DateTime.Now
                };

                SlsNotification notificationDB = GetExistingNotification(EmployeeId, notification.URL);

                notificationDB.Message = notification.Message;
                notificationDB.URL = notification.URL;
                notificationDB.Type = notification.Type;
                notificationDB.Date = notification.Date;

                _NotificationRepository.Update(notificationDB);
                _NotificationRepository.SaveChanges();

                SlsNotificationDetail notificationDetail = new SlsNotificationDetail()
                {
                    SlsNotificationId = notificationDB.Id,
                    HrmEmployeeId = EmployeeId,
                    IsRead = false,
                    Date = DateTime.Now
                };

                SlsNotificationDetail notificationDetailDB = GetExistingNotificationDetail(notificationDetail.HrmEmployeeId,
                    notificationDetail.SlsNotificationId);

                notificationDetailDB.SlsNotificationId = notificationDetail.SlsNotificationId;
                notificationDetailDB.HrmEmployeeId = notificationDetail.HrmEmployeeId;
                notificationDetailDB.IsRead = notificationDetail.IsRead;
                notificationDetailDB.Date = notificationDetail.Date;

                _NotificationDetailRepository.Update(notificationDetailDB);
                _NotificationDetailRepository.SaveChanges();
                //END of Notification Save
            }
            catch (Exception ex)
            {
                objOperation.Success = false;
            }
            return objOperation;
        }


    }
}
