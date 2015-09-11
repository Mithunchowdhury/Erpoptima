using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Accounts;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Accounts.Repository
{
    public interface IVoucherRepository : IRepository<AnFVoucher>
    {
        int AddEntity(AnFVoucher obj);
        AnFVoucher GetById(int id);        
        int GetTotal(int companyId, int financialYearId, int type);
        int GetPostedTotal(int companyId, int financialYearId, int type);
        int GetCanceledTotal(int companyId, int financialYearId, int type);


        AnFVoucher Modified(AnFVoucher obj);
        int SaveChanges();
        System.Data.Entity.DbContextTransaction BeginTransaction();
        void Rollback(System.Data.Entity.DbContextTransaction contextTxn);
        void Commit(System.Data.Entity.DbContextTransaction contextTxn);
    }
    public class VoucherRepository : BaseRepository<AnFVoucher>, IVoucherRepository
    {
        public VoucherRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
                
        public int AddEntity(AnFVoucher obj)
        {
            int Id = 1;
            AnFVoucher last = DataContext.AnFVouchers.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;
            }
            obj.Id = Id;
            base.Add(obj);
            return Id;

        }
        public AnFVoucher GetById(int id)
        {
            return DataContext.AnFVouchers.Where(x => x.Id == id).FirstOrDefault();
        }
        public AnFVoucher Modified(AnFVoucher obj)
        {
            var exists = DataContext.AnFVouchers.Where(i => i.Id == obj.Id).FirstOrDefault();
            DataContext.Entry(exists).State = System.Data.Entity.EntityState.Detached;
            DataContext.Entry(obj).State = System.Data.Entity.EntityState.Modified;
            return obj;
        }
        public int SaveChanges()
        {
            try
            {
                return base.DataContext.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                throw e;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetTotal(int companyId, int financialYearId, int type)
        {
            return 0;
        }

        public int GetPostedTotal(int companyId, int financialYearId, int type)
        {
            return 0;
        }

        public int GetCanceledTotal(int companyId, int financialYearId, int type)
        {
            return 0;
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
