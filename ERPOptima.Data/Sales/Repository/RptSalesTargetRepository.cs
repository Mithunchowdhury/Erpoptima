using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales.Repository
{
    public interface IRptSalesTargetRepository : IRepository<SlsSalesTarget>
    {
        IList<SlsSalesTarget> GetAll();
        int AddEntity(SlsSalesTarget obj);
        SlsSalesTarget GetById(int id);
        IList<SlsSalesTarget> Get(int companyId, int partyType, int Party, int Office, DateTime StartDate, DateTime EndDate);
        int SaveChanges();
        System.Data.Entity.DbContextTransaction BeginTransaction();
        void Rollback(System.Data.Entity.DbContextTransaction contextTxn);
        void Commit(System.Data.Entity.DbContextTransaction contextTxn);
        int GetLastId(int companyId);
    }
    public class RptSalesTargetRepository : BaseRepository<SlsSalesTarget>, IRptSalesTargetRepository
    {
        public RptSalesTargetRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public int GetLastId(int companyId)
        {

            int SL = 1;
            SlsSalesTarget last = DataContext.SlsSalesTargets.Where(r => r.SecCompanyId == companyId).OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                SL = int.Parse(last.RefNo.Split('-')[3]) + 1;

            }
            return SL;
        }
        public IList<SlsSalesTarget> GetAll()
        {
            return DataContext.SlsSalesTargets.ToList();
        }
        public int AddEntity(SlsSalesTarget obj)
        {
            int Id = 1;
            SlsSalesTarget last = DataContext.SlsSalesTargets.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;
            }
            obj.Id = Id;
            base.Add(obj);
            return Id;

        }
        public SlsSalesTarget GetById(int id)
        {
            return DataContext.SlsSalesTargets.Where(x => x.Id == id).FirstOrDefault();
        }
        public IList<SlsSalesTarget> Get(int companyId, int partyType, int Party, int Office, DateTime StartDate, DateTime EndDate)
        {
            return DataContext.SlsSalesTargets.Where(t => t.SecCompanyId == 1 && StartDate <= t.CreatedDate && EndDate >= t.CreatedDate).ToList();
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
