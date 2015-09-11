using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales.Repository
{
    public interface IDeliveryDetailRepository : IRepository<SlsDeliverDetail>
    {
        int AddEntity(SlsDeliverDetail obj);
        int AddEntityList(IList<SlsDeliverDetail> list);       
        int SaveChanges();
    }
    public class DeliveryDetailRepository : BaseRepository<SlsDeliverDetail>, IDeliveryDetailRepository
    {

        public DeliveryDetailRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public int AddEntity(SlsDeliverDetail obj)
        {
            int Id = 1;
            SlsDeliverDetail last = DataContext.SlsDeliverDetails.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;
            }
            obj.Id = Id;
            base.Add(obj);
            return Id;

        }

        public int AddEntityList(IList<SlsDeliverDetail> list)
        {
            int Id = 0;
            SlsDeliverDetail last = DataContext.SlsDeliverDetails.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = last.Id;
            }
            foreach (SlsDeliverDetail obj in list)
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
