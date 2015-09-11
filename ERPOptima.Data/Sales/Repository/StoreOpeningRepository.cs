using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales.Repository
{  
    #region Interface

    public interface IStoreOpeningRepository : IRepository<SlsOffice>
    {
        IList<SlsOffice> GetCostCenters(int companyId);
        int AddEntity(SlsOffice objSlsOffice);
        SlsOffice GetCostCenterById(long nullable);
    }

    #endregion

    public class StoreOpeningRepository : BaseRepository<SlsOffice>, IStoreOpeningRepository
    {
        public StoreOpeningRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public IList<SlsOffice> GetCostCenters(int companyId)
        {
            return DataContext.SlsOffices.ToList();
        }
        public int AddEntity(SlsOffice objSlsOffice)
        {
            int Id = 1;
            SlsOffice last = DataContext.SlsOffices.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objSlsOffice.Id = Id;
            base.Add(objSlsOffice);
            return Id;

        }

        public SlsOffice GetCostCenterById(long nullable)
        {
            var a = from v in DataContext.SlsOffices
                    where v.Id == nullable
                    select v;
            return DataContext.SlsOffices.Where(x => x.Id == nullable).FirstOrDefault();
        }

    }
}
