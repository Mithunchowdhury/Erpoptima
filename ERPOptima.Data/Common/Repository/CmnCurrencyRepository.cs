using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Common.Repository
{

    #region Interface

    public interface ICmnCurrencyRepository : IRepository<CmnCurrency>
    {
        IList<CmnCurrency> GetCmnCurrencies();
        int AddEntity(CmnCurrency objCmnCurrency);
        CmnCurrency GetCmnCurrencyById(int id);
    }
    #endregion
    public class CmnCurrencyRepository : BaseRepository<CmnCurrency>, ICmnCurrencyRepository
    {
        public CmnCurrencyRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public IList<CmnCurrency> GetCmnCurrencies()
        {
            return DataContext.CmnCurrencies.ToList();
        }

        public int AddEntity(CmnCurrency objCmnCurrency)
        {
            int Id = 1;
            CmnCurrency last = DataContext.CmnCurrencies.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = last.Id + 1;
            }
            objCmnCurrency.Id = Id;
            base.Add(objCmnCurrency);
            return Id;
        }

        public CmnCurrency GetCmnCurrencyById(int id)
        {
            return DataContext.CmnCurrencies.Where(x => x.Id == id).FirstOrDefault();
        }

    }
}

