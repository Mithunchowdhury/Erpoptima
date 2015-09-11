using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales.Repository
{
    public interface IPromotionalOfferRepository : IRepository<SlsPromotionalOffer>
    {
        IList<SlsPromotionalOffer> GetAll();
        int AddEntity(SlsPromotionalOffer obj);
        SlsPromotionalOffer GetById(int id);
        int SaveChanges();
        System.Data.Entity.DbContextTransaction BeginTransaction();
        void Rollback(System.Data.Entity.DbContextTransaction contextTxn);
        void Commit(System.Data.Entity.DbContextTransaction contextTxn);
    }
    public class PromotionalOfferRepository : BaseRepository<SlsPromotionalOffer>, IPromotionalOfferRepository
    {

        public PromotionalOfferRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public IList<SlsPromotionalOffer> GetAll()
        {
            return DataContext.SlsPromotionalOffers.ToList();
        }
        public int AddEntity(SlsPromotionalOffer objDistrict)
        {
            int Id = 1;
            SlsPromotionalOffer last = DataContext.SlsPromotionalOffers.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objDistrict.Id = Id;
            base.Add(objDistrict);
            return Id;

        }

        public SlsPromotionalOffer GetById(int id)
        {
            return DataContext.SlsPromotionalOffers.Where(x => x.Id == id).FirstOrDefault();
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

        public void Commit(System.Data.Entity.DbContextTransaction contextTxn)
        {
            contextTxn.Commit();
        }
    }

    public partial class SlsPromotionalOfferViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int SlsRegionId { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public string Remarks { get; set; }
        public bool IsValid { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }



        public string SlsRegionName { get; set; }

        public IList<SlsPromotionalOfferDetailViewModel> OfferCategories { get; set; }
    }


}
