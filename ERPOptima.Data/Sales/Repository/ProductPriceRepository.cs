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

    public interface IProductPriceRepository : IRepository<SlsProductPrice>
    {
        IList<SlsProductPrice> GetAll();
        int AddEntity(SlsProductPrice obj);
        SlsProductPrice GetById(int id);
    }

    #endregion

    public class ProductPriceRepository : BaseRepository<SlsProductPrice>, IProductPriceRepository
    {
        public ProductPriceRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public IList<SlsProductPrice> GetAll()
        {
            return DataContext.SlsProductPrices.ToList();
        }
        public int AddEntity(SlsProductPrice objProductPrice)
        {
            int Id = 1;
            SlsProductPrice last = DataContext.SlsProductPrices.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objProductPrice.Id = Id;
            base.Add(objProductPrice);
            return Id;

        }

        public SlsProductPrice GetById(int id)
        {
            return DataContext.SlsProductPrices.Where(x => x.Id == id).FirstOrDefault();
        }
      
    }

    /* public class ProductPriceViewModel
       {
           //
           // GET: /Sales/ProductPriceViewModel/

           public int Id { get; set; }
           public string Product { get; set; }
           public Nullable<int> SlsProductId { get; set; }
           public string Unit { get; set; }
           public Nullable<int> SlsUnitId { get; set; }
           public string Code { get; set; }
           public decimal FactoryCost { get; set; }
           public decimal MRP { get; set; }
           public decimal DistributorCommission { get; set; }
           public decimal RetailCommission { get; set; }
           public System.DateTime DeclarationDate { get; set; }
           public int CreatedBy { get; set; }
           public System.DateTime CreatedDate { get; set; }
           public Nullable<int> ModifiedBy { get; set; }
           public Nullable<System.DateTime> ModifiedDate { get; set; }

       }*/
}
