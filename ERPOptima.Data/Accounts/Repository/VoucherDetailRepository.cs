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

    public interface IVoucherDetailRepository : IRepository<AnFVoucherDetail>
    {
        int AddEntity(AnFVoucherDetail obj);
        int AddEntityList(IList<AnFVoucherDetail> list);
        AnFVoucherDetail GetById(int id);
        int GetTotalDebit(int voucherid);
        int GetTotalCredit(int voucherid);
        int GetNextDetailId();


        AnFVoucherDetail Modified(AnFVoucherDetail obj);
        int SaveChanges();
        System.Data.Entity.DbContextTransaction BeginTransaction();
        void Rollback(System.Data.Entity.DbContextTransaction contextTxn);
        void Commit(System.Data.Entity.DbContextTransaction contextTxn);
    }
    public class VoucherDetailRepository : BaseRepository<AnFVoucherDetail>, IVoucherDetailRepository
    {
        public VoucherDetailRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public int AddEntity(AnFVoucherDetail obj)
        {
            int Id = 1;
            AnFVoucherDetail last = DataContext.AnFVoucherDetails.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;
            }
            obj.Id = Id;
            base.Add(obj);
            return Id;

        }
        public int AddEntityList(IList<AnFVoucherDetail> list)
        {
            int Id = 0;
            AnFVoucherDetail last = DataContext.AnFVoucherDetails.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = last.Id;
            }
            foreach (AnFVoucherDetail obj in list)
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
        public int GetNextDetailId()
        {
            int Id = 1;
            AnFVoucherDetail last = DataContext.AnFVoucherDetails.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;
            }           
            
            return Id;

        }
        public AnFVoucherDetail GetById(int id)
        {
            return DataContext.AnFVoucherDetails.Where(x => x.Id == id).FirstOrDefault();
        }
        public AnFVoucherDetail Modified(AnFVoucherDetail obj)
        {
            var exists = DataContext.AnFVoucherDetails.Where(i => i.Id == obj.Id).FirstOrDefault();
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

        public int GetTotalDebit(int voucherid)
        {
            return 0;
        }

        public int GetTotalCredit(int voucherid)
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
