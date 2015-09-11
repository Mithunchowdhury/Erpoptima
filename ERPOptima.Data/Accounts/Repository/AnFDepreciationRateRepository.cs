using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Accounts.Repository
{
    #region Interface

    public interface IAnFDepreciationRateRepository : IRepository<AnFDepreciationRate>
    {
        IList<AnFDepreciationRate> GetDepreciationRates();
        int AddEntity(AnFDepreciationRate objAnFDepreciationRate);
        AnFDepreciationRate GetDepreciationRateById(int nullable);
    }

    #endregion

    public class AnFDepreciationRateRepository : BaseRepository<AnFDepreciationRate>, IAnFDepreciationRateRepository
    {
        public AnFDepreciationRateRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public IList<AnFDepreciationRate> GetDepreciationRates()
        {
            return DataContext.AnFDepreciationRates.ToList();
        }

        public int AddEntity(AnFDepreciationRate objAnFDepreciationRate)
        {
            int Id = 1;
            AnFDepreciationRate last = DataContext.AnFDepreciationRates.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objAnFDepreciationRate.Id = Id;
            base.Add(objAnFDepreciationRate);
            return Id;

        }

        public AnFDepreciationRate GetDepreciationRateById(int nullable)
        {
            return DataContext.AnFDepreciationRates.Where(x => x.Id == nullable).FirstOrDefault();
        }

    }
}
