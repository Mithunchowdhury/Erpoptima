using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales.Repository
{
    public interface IGeneralDiscountRepository : IRepository<SlsGeneralDiscount>
    {
        int AddEntity(SlsGeneralDiscount obj);
    }
    public class GeneralDiscountRepository : BaseRepository<SlsGeneralDiscount>, IGeneralDiscountRepository
    {
        public GeneralDiscountRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
       public int AddEntity(SlsGeneralDiscount obj)
       {
           int Id = 1;
           SlsGeneralDiscount last = DataContext.SlsGeneralDiscounts.OrderByDescending(x => x.Id).FirstOrDefault();

           if (last != null)
           {
               Id = last.Id + 1;

           }
           obj.Id = Id;
           base.Add(obj);
           return Id;

       }
    }
    public partial class SlsGeneralDiscountViewModel
    {
        public int Id { get; set; }
        public int SlsRegionId { get; set; }
        public decimal Discount { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }




        public string SlsRegionName { get; set; }
    }
}
