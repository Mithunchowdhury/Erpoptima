using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales.Repository
{
    public interface IDeliveryDetailsRepository : IRepository<SlsDeliverDetail>
    {
        IList<SlsDeliverDetail> GetAll();
        int AddEntity(SlsDeliverDetail obj);
        SlsDeliverDetail GetById(int id);

    }
    public class DeliveryDetailsRepository : BaseRepository<SlsDeliverDetail>, IDeliveryDetailsRepository
    {

        public DeliveryDetailsRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
            {

            }

        public IList<SlsDeliverDetail> GetAll()
        {

            return DataContext.SlsDeliverDetails.ToList();
          
        }

        public int AddEntity(SlsDeliverDetail obj)
        {
            int Id = 1;
            SlsDeliverDetail last = null;
            try
            {
                last = DataContext.SlsDeliverDetails.OrderByDescending(x => x.Id).FirstOrDefault();
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

        public SlsDeliverDetail GetById(int id)
        {
            return DataContext.SlsDeliverDetails.Where(x => x.Id == id).FirstOrDefault();
        }

    }
}
