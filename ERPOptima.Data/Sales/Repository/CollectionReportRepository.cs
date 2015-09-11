using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales.Repository
{
    public interface ICollectionReportRepository : IRepository<SlsCollection>
    {
        IList<SlsCollection> GetAll();
        int AddEntity(SlsCollection obj);
        SlsCollection GetById(int id);
        IList<SlsCollection> Get(int companyId, int partyType, int Party, int Office, DateTime StartDate, DateTime EndDate);
        int SaveChanges();
        System.Data.Entity.DbContextTransaction BeginTransaction();
        void Rollback(System.Data.Entity.DbContextTransaction contextTxn);
        void Commit(System.Data.Entity.DbContextTransaction contextTxn);
        int GetLastId(int companyId);
    }
    public class CollectionReportRepository : BaseRepository<SlsCollection>, ICollectionReportRepository
    {
        public CollectionReportRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public int GetLastId(int companyId)
        {

            int SL = 1;
            SlsCollection last = DataContext.SlsCollections.Where(r => r.SecCompanyId == companyId).OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                SL = int.Parse(last.RefNo.Split('-')[3]) + 1;

            }
            return SL;
        }
        public IList<SlsCollection> GetAll()
        {
            return DataContext.SlsCollections.ToList();
        }
        public int AddEntity(SlsCollection obj)
        {
            int Id = 1;
            SlsCollection last = DataContext.SlsCollections.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;
            }
            obj.Id = Id;
            base.Add(obj);
            return Id;

        }
        public SlsCollection GetById(int id)
        {
            return DataContext.SlsCollections.Where(x => x.Id == id).FirstOrDefault();
        }
        public IList<SlsCollection> Get(int companyId, int partyType, int Party, int Office, DateTime StartDate, DateTime EndDate)
        {
            return DataContext.SlsCollections.Where(t => t.SecCompanyId == 1 && StartDate <= t.CreatedDate && EndDate >= t.CreatedDate).ToList();
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
}
