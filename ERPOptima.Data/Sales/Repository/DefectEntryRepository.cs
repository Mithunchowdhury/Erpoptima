using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales.Repository
{
    public interface IDefectEntryRepository : IRepository<SlsDefect>
    {
        int GetLastId();

        int GetLastCode(int companyId);
        int AddEntity(SlsDefect objSlsDefect);
        IList<SlsDefect> GetAll(int companyId);
        SlsDefect GetById(int Id);

        int SaveChanges();
        System.Data.Entity.DbContextTransaction BeginTransaction();
        void Rollback(System.Data.Entity.DbContextTransaction contextTxn);
        void Commit(System.Data.Entity.DbContextTransaction contextTxn);

    }
    public class DefectEntryRepository : BaseRepository<SlsDefect>, IDefectEntryRepository
    {

        public DefectEntryRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {


        }
        public int GetLastId()
        {
            int Id = 1;
            SlsDefect last = DataContext.SlsDefects.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            return Id;
        }

        public int GetLastCode(int companyId)
        {
            int SL = 1;
            SlsDefect last = DataContext.SlsDefects.Where(r => r.SecCompanyId == companyId).OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                SL = int.Parse(last.RefNo.Split('/')[1]) + 1;

            }
            return SL;
        }

        public int AddEntity(SlsDefect objSlsDefect)
        {
            int Id = 1;
            SlsDefect last = DataContext.SlsDefects.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objSlsDefect.Id = Id;
            base.Add(objSlsDefect);
            return Id;
        }

        public IList<SlsDefect> GetAll(int companyId)
        {

            return DataContext.SlsDefects.Where(p => p.SecCompanyId == companyId).ToList();
        }

        public SlsDefect GetById(int Id)
        {
            return DataContext.SlsDefects.Where(p => p.Id == Id).FirstOrDefault();
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
