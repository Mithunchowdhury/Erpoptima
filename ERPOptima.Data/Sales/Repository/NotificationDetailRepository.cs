using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales.Repository
{
    public interface INotificationDetailRepository : IRepository<SlsNotificationDetail>
    {
        int AddEntity(SlsNotificationDetail obj);
        int SaveChanges();
    }
    public class NotificationDetailRepository : BaseRepository<SlsNotificationDetail>, INotificationDetailRepository
    {
        public NotificationDetailRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public int AddEntity(SlsNotificationDetail obj)
        {
            int Id = 1;
            SlsNotificationDetail last = null;
            try
            {
                last = DataContext.SlsNotificationDetails.OrderByDescending(x => x.Id).FirstOrDefault();
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

        public int AddEntityList(IList<SlsNotificationDetail> list)
        {
            int Id = 0;
            SlsNotificationDetail last = DataContext.SlsNotificationDetails.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = last.Id;
            }
            foreach (SlsNotificationDetail obj in list)
            {
                if (obj.Id <= 0)
                {
                    Id++;
                    obj.Id = Id;
                    base.Add(obj);
                }
            }
            return Id;
        }
        public int SaveChanges()
        {
            return base.DataContext.SaveChanges();
        }

    }
    
}
