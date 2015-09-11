using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales.Repository
{
    public interface ISalesOrderRepository : IRepository<SlsSalesOrder>
    {
        IList<SlsSalesOrder> GetAll();
        int AddEntity(SlsSalesOrder obj);
        SlsSalesOrder GetById(int id);
        int SaveChanges();
        System.Data.Entity.DbContextTransaction BeginTransaction();
        void Rollback(System.Data.Entity.DbContextTransaction contextTxn);
        void Commit(System.Data.Entity.DbContextTransaction contextTxn);
        int GetLastId(int companyId);
    }
    public class SalesOrderRepository : BaseRepository<SlsSalesOrder>, ISalesOrderRepository
    {
        public SalesOrderRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public int GetLastId(int companyId)
        {           

            int SL = 1;
            SlsSalesOrder last = DataContext.SlsSalesOrders.Where(r => r.SecCompnayId == companyId).OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                SL = int.Parse(last.RefNo.Split('/')[1]) + 1;

            }
            return SL;
        }
        public IList<SlsSalesOrder> GetAll()
        {
            return DataContext.SlsSalesOrders.ToList();
        }
        public int AddEntity(SlsSalesOrder obj)
        {
            int Id = 1;
            SlsSalesOrder last = DataContext.SlsSalesOrders.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;
            }
            obj.Id = Id;
            base.Add(obj);
            return Id;

        }
        public SlsSalesOrder GetById(int id)
        {
            return DataContext.SlsSalesOrders.Where(x => x.Id == id).FirstOrDefault();
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
