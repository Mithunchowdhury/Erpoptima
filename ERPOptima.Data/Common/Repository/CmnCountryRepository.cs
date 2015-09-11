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

    public interface ICmnCountryRepository : IRepository<CmnCountry>
    {
        IList<CmnCountry> GetCmnCountries();        
        int AddEntity(CmnCountry objCmnCountry);
        CmnCountry GetCmnCountryById(int id);
    }
    #endregion
    public class CmnCountryRepository : BaseRepository<CmnCountry>, ICmnCountryRepository
    {
        public CmnCountryRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public IList<CmnCountry> GetCmnCountries()
        {
            return DataContext.CmnCountries.ToList();
        }

        public int AddEntity(CmnCountry objCmnCountry)
        {
            int Id = 1;
            CmnCountry last = DataContext.CmnCountries.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = last.Id + 1;
            }
            objCmnCountry.Id = Id;
            base.Add(objCmnCountry);
            return Id;
        }

        public CmnCountry GetCmnCountryById(int id)
        {
            //var a = from v in DataContext.AnFCostCenters
            //        where v.Id == id
            //        select v;
            return DataContext.CmnCountries.Where(x => x.Id == id).FirstOrDefault();
        }

    }
}

