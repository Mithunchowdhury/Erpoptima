using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales.Repository
{
    public interface ISlsProductReceiveRepository : IRepository<SlsProductReceive>
    {
        IList<SlsProductReceive> GetAll(int companyId);
        int AddEntity(SlsProductReceive obj);
        SlsProductReceive GetById(int id);
        SlsProductReceive GetByDelivery(int deliveryId);
    }
    public class SlsProductReceiveRepository : BaseRepository<SlsProductReceive>, ISlsProductReceiveRepository
    {
        public SlsProductReceiveRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public IList<SlsProductReceive> GetAll(int companyId)
        {

            return DataContext.SlsProductReceives.ToList();
          
        }

        public int AddEntity(SlsProductReceive obj)
        {
            int Id = 1;
            SlsProductReceive last = null;
            try
            {
                last = DataContext.SlsProductReceives.OrderByDescending(x => x.Id).FirstOrDefault();
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

        public SlsProductReceive GetById(int id)
        {
            return DataContext.SlsProductReceives.Where(x => x.Id == id).FirstOrDefault();
        }

        public SlsProductReceive GetByDelivery(int deliveryId)
        {
            SlsProductReceive PrReceive = new SlsProductReceive();
            PrReceive = DataContext.SlsProductReceives.Where(x => x.SlsDeliveryId == deliveryId).FirstOrDefault();
            return PrReceive;
        }
    }
}
