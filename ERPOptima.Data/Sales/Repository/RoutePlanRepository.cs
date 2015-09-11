using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales.Repository
{
    public interface IRoutePlanRepository : IRepository<SlsRoutePlan>
    {
        IList<SlsRoutePlan> GetAll(int employeeId);
        int AddEntity(SlsRoutePlan record);

        int SaveChanges();
        System.Data.Entity.DbContextTransaction BeginTransaction();
        void Rollback(System.Data.Entity.DbContextTransaction contextTxn);
        void Commit(System.Data.Entity.DbContextTransaction contextTxn);
    }
    public class RoutePlanRepository : BaseRepository<SlsRoutePlan>, IRoutePlanRepository
    {
        public RoutePlanRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public IList<SlsRoutePlan> GetAll(int employeeId)
        {
            return DataContext.SlsRoutePlans.Where(t=>t.HrmEmployeeId==employeeId).ToList();
        }
        public int AddEntity(SlsRoutePlan record)
        {
            int Id = 1;
            SlsRoutePlan last = DataContext.SlsRoutePlans.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;
            }
            record.Id = Id;
            base.Add(record);
            return Id;
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
