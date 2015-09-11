using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales.Repository
{

    public interface IDamageDetailRepository : IRepository<InvDamageDetail>
    {
        IList<InvDamageDetail> GetAll();
        int AddEntity(InvDamageDetail obj);
        int AddEntityList(IList<InvDamageDetail> list);
        InvDamageDetail GetById(int id);
        int SaveChanges();
    }

    public class DamageDetailRepository : BaseRepository<InvDamageDetail>, IDamageDetailRepository 
    {
        public DamageDetailRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public new IList<InvDamageDetail> GetAll()
        {
            return DataContext.InvDamageDetails.ToList();
        }

        public int AddEntity(InvDamageDetail obj)
        {
            int Id = 1;
            InvDamageDetail last = DataContext.InvDamageDetails.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            obj.Id = Id;
            base.Add(obj);
            return Id;
        }

        public int AddEntityList(IList<InvDamageDetail> list)
        {
            int Id = 0;
            InvDamageDetail last = DataContext.InvDamageDetails.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = last.Id;
            }
            foreach (InvDamageDetail obj in list)
            {
                if (obj.Id <= 0)
                {
                    Id++;
                    obj.Id = Id;
                    base.Add(obj);
                }
            }
            return Id;
        }

        public InvDamageDetail GetById(int id)
        {
            return DataContext.InvDamageDetails.Where(x => x.Id == id).FirstOrDefault();
        }

        public int SaveChanges()
        {
            return base.DataContext.SaveChanges();
        }
    }

     public partial class InvDamageDetailViewModel
    {
        public int Id { get; set; }
        public int InvDamageId { get; set; }
        public int SlsProductId { get; set; }
        public decimal Quantity { get; set; }
        public int SlsUnitsId { get; set; }
        public string Reason { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
      


        public string SlsProductName { get; set; }
        public string SlsUnitName { get; set; }
    }
}
