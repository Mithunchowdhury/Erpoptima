using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales.Repository
{
    public interface IIncentivePaymentRepository : IRepository<SlsIncentive>
    {
        
        int AddEntity(SlsIncentive objSlsIncentive);
        SlsIncentive GetById(int id); 
    }
    public class IncentivePaymentRepository : BaseRepository<SlsIncentive>, IIncentivePaymentRepository
    {

        public IncentivePaymentRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        

        public int AddEntity(SlsIncentive obj)
        {
            int Id = 1;
            SlsIncentive last = null;
            try
            {
                last = DataContext.SlsIncentives.OrderByDescending(x => x.Id).FirstOrDefault();
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

        public SlsIncentive GetById(int id)
        {
            return DataContext.SlsIncentives.Where(x => x.Id == id).FirstOrDefault();
        }



    }
}
