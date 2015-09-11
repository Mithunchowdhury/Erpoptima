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

    public interface IDistrictRepository : IRepository<SlsDistrict>
    {
        IList<SlsDistrict> GetAll();
        int AddEntity(SlsDistrict obj);
        SlsDistrict GetById(int id);
    }

    #endregion

    public class DistrictRepository : BaseRepository<SlsDistrict>, IDistrictRepository
    {
        public DistrictRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public IList<SlsDistrict> GetAll()
        {
            return DataContext.SlsDistricts.ToList();
        }
        public int AddEntity(SlsDistrict objDistrict)
        {
            int Id = 1;
            SlsDistrict last = DataContext.SlsDistricts.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objDistrict.Id = Id;
            base.Add(objDistrict);
            return Id;

        }

        public SlsDistrict GetById(int id)
        {
            return DataContext.SlsDistricts.Where(x => x.Id == id).FirstOrDefault();
        }

    }
}
