using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales.Repository
{


    #region interface
    public interface IProductUnitRepository : IRepository<SlsProductUnit>
    {

        List<SlsProductUnit> GetSlsProductUnitsBySlsProductId(int productId);
        int AddEntity(SlsProductUnit objSlsProductUnit);


    }


    #endregion
    public class ProductUnitRepository : BaseRepository<SlsProductUnit>, IProductUnitRepository
    {

        public ProductUnitRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
            {

            }

        public int AddEntity(SlsProductUnit objSlsProductUnit)
        {
            int Id = 1;
            SlsProductUnit last = DataContext.SlsProductUnits.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objSlsProductUnit.Id = Id;
            base.Add(objSlsProductUnit);
            return Id;

        }

        public List<SlsProductUnit> GetSlsProductUnitsBySlsProductId(int productId)
        {

            var query = DataContext.SlsProductUnits.Include("SlsUnit").Where(op => op.SlsProductId == productId);
            List<SlsProductUnit> list = query.ToList();
            return list;
        
        
        }






    }
}
