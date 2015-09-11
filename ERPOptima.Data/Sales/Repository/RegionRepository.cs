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

    public interface IRegionRepository : IRepository<SlsRegion>
    {
        IList<SlsRegion> GetAll(int RegionId);
        int AddEntity(SlsRegion objSlsRegion);
        SlsRegion GetById(long nullable);
    }

    #endregion

    public class RegionRepository : BaseRepository<SlsRegion>, IRegionRepository
    {
        public RegionRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public IList<SlsRegion> GetAll(int RegionId)
        {
            return DataContext.SlsRegions.ToList();
        }
        public int AddEntity(SlsRegion objSlsRegion)
        {
            int Id = 1;
            SlsRegion last = DataContext.SlsRegions.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objSlsRegion.Id = Id;
            base.Add(objSlsRegion);
            return Id;

        }

        public SlsRegion GetById(long nullable)
        {
             var a = from v in DataContext.SlsRegions
                     where v.Id == nullable
                   select v;
             return DataContext.SlsRegions.Where(x => x.Id == nullable).FirstOrDefault();
        }

    }

}
