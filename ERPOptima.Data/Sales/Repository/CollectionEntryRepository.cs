using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales
{
    //class CollectionEntryRepository
    //{
    //}

    public interface ICollectionEntryRepository : IRepository<SlsCollection>
    {
        IList<SlsCollection> GetAll();
        SlsCollection GetById(int id);
        int AddEntity(SlsCollection obj);
        int getAutoNumber(int companyId);

        //t
        int SaveChanges();
        System.Data.Entity.DbContextTransaction BeginTransaction();
        void Rollback(System.Data.Entity.DbContextTransaction contextTxn);
        void Commit(System.Data.Entity.DbContextTransaction contextTxn);

        //t
    }
    public class CollectionEntryRepository : BaseRepository<SlsCollection>, ICollectionEntryRepository
    {

        public CollectionEntryRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public IList<SlsCollection> GetAll()
        {
            return DataContext.SlsCollections.ToList();

        }

        public int AddEntity(SlsCollection objCollectionEntry)
        {
            int Id = 1;
            SlsCollection last = DataContext.SlsCollections.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = last.Id + 1;
            }
            objCollectionEntry.Id = Id;
            base.Add(objCollectionEntry);
            return Id;
        }

        public int getAutoNumber(int companyId)
        {
            int SL = 1;
            SlsCollection last = null;
            try
            {
                last = DataContext.SlsCollections.Where(r => r.SecCompanyId == companyId).OrderByDescending(x => x.Id).FirstOrDefault();
            }
            catch (Exception ex)
            {

            }
            if (last != null)
            {
                SL = int.Parse(last.RefNo.Split('/')[1]) + 1;

            }
            return SL;
           
        }


        public SlsCollection GetById(int id)
        {
            return DataContext.SlsCollections.Where(x => x.Id == id).FirstOrDefault();
        }

        //t
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
        //t
    }

    

}
