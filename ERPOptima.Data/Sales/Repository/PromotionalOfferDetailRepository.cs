using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales.Repository
{
    public interface IPromotionalOfferDetailRepository : IRepository<SlsPromotionalOfferDetail>
    {
        IList<SlsPromotionalOfferDetail> GetAll();
        int AddEntity(SlsPromotionalOfferDetail obj);
        int AddEntityList(IList<SlsPromotionalOfferDetail> list);
        SlsPromotionalOfferDetail GetById(int id);
        int SaveChanges();
        System.Data.Entity.DbContextTransaction BeginTransaction();
        void Rollback(System.Data.Entity.DbContextTransaction contextTxn);
    }
    public class PromotionalOfferDetailRepository : BaseRepository<SlsPromotionalOfferDetail>, IPromotionalOfferDetailRepository
    {

        public PromotionalOfferDetailRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public IList<SlsPromotionalOfferDetail> GetAll()
        {
            return DataContext.SlsPromotionalOfferDetails.ToList();
        }
        public int AddEntity(SlsPromotionalOfferDetail obj)
        {
            int Id = 1;
            SlsPromotionalOfferDetail last = DataContext.SlsPromotionalOfferDetails.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            obj.Id = Id;
            base.Add(obj);
            return Id;

        }

        public int AddEntityList(IList<SlsPromotionalOfferDetail> list)
        {
            int Id = 0;
            SlsPromotionalOfferDetail last = DataContext.SlsPromotionalOfferDetails.OrderByDescending(x => x.Id).FirstOrDefault();
            if(last != null)
            {
                Id = last.Id;
            }
            foreach (SlsPromotionalOfferDetail obj in list)
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

        public SlsPromotionalOfferDetail GetById(int id)
        {
            return DataContext.SlsPromotionalOfferDetails.Where(x => x.Id == id).FirstOrDefault();
        }


        public int SaveChanges()
        {
             return base.DataContext.SaveChanges();
        }

        public System.Data.Entity.DbContextTransaction BeginTransaction()
        {
            return base.DataContext.Database.BeginTransaction();
        } 

        public void Rollback(System.Data.Entity.DbContextTransaction contextTxn)
        {
            contextTxn.Rollback();
        }

    }
      
    public partial class SlsPromotionalOfferDetailViewModel
    {
        public int Id { get; set; }
        public int SlsPromotionalOfferId { get; set; }
        public bool IsOffered { get; set; }
        public int SlsProductId { get; set; }
        public string SlsProductName { get; set; }
        public decimal Discount { get; set; }




    }

}
