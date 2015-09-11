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

    public interface IAreaRepository : IRepository<SlsArea>
    {
        IList<SlsArea> GetAll();
        int AddEntity(SlsArea objSlsArea);
        SlsArea GetById(int id);
    }

    #endregion

    public class AreaRepository : BaseRepository<SlsArea>, IAreaRepository
    {
        public AreaRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public IList<SlsArea> GetAll()
        {
            return DataContext.SlsAreas.ToList();
        }
        public int AddEntity(SlsArea objSlsArea)
        {
            int Id = 1;
            SlsArea last = DataContext.SlsAreas.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objSlsArea.Id = Id;
            base.Add(objSlsArea);
            return Id;

        }

        public SlsArea GetById(int id)
        {
            return DataContext.SlsAreas.Where(x => x.Id == id).FirstOrDefault();
        }

    }

}
