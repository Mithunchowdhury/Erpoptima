using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales.Repository
{
    public interface INotificationRepository : IRepository<SlsNotification>
    {
        int AddEntity(SlsNotification obj);
        int SaveChanges();
    }
    public class NotificationRepository : BaseRepository<SlsNotification>, INotificationRepository
    {
        public NotificationRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }


        public int AddEntity(SlsNotification obj)
        {
            int Id = 1;
            SlsNotification last = null;
            try
            {
                last = DataContext.SlsNotifications.OrderByDescending(x => x.Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                //Possibly can occur when no data exists in table.
            }
            if (last != null)
            {
                Id = last.Id + 1;

            }
            obj.Id = Id;
            base.Add(obj);
            return Id;

        }
        public int SaveChanges()
        {
            return base.DataContext.SaveChanges();
        }

    }
}
