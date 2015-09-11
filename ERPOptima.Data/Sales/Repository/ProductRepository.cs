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

    public interface IProductRepository : IRepository<SlsProduct>
    {
        IList<SlsProduct> GetAll(int companyId);
        int AddEntity(SlsProduct obj);
        SlsProduct GetById(int id);
    }

    #endregion

    public class ProductRepository : BaseRepository<SlsProduct>, IProductRepository
    {
        public ProductRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public IList<SlsProduct> GetAll(int companyId)
        {
            return DataContext.SlsProducts.Where(t=>t.SecCompanyId==companyId && t.IsProduct==true).ToList();
        }
        public int AddEntity(SlsProduct objProduct)
        {
            int Id = 1;
            SlsProduct last = DataContext.SlsProducts.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objProduct.Id = Id;
            base.Add(objProduct);
            return Id;

        }

        public SlsProduct GetById(int id)
        {
            return DataContext.SlsProducts.Where(x => x.Id == id).FirstOrDefault();
        }

    }
}
