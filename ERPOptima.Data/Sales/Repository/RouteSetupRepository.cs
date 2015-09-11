using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales.Repository
{
    #region Interface

    public interface IRouteSetupRepository : IRepository<SlsRoute>
    {
        IList<SlsRoute> GetAll();
        int AddEntity(SlsRoute objSlsArea);
        SlsRoute GetById(int id);

        int SaveChanges();
        System.Data.Entity.DbContextTransaction BeginTransaction();
        void Rollback(System.Data.Entity.DbContextTransaction contextTxn);
        void Commit(System.Data.Entity.DbContextTransaction contextTxn);
    }

    #endregion
    public class RouteSetupRepository : BaseRepository<SlsRoute>, IRouteSetupRepository
    {
        public RouteSetupRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public IList<SlsRoute> GetAll()
        {
            return DataContext.SlsRoutes.ToList();

        }
        //Save
        public int AddEntity(SlsRoute record)
        {            
            int Id = 1;
            SlsRoute last = null;
            try
            {
                last = DataContext.SlsRoutes.OrderByDescending(x => x.Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                //Possibly can occur when no data exists in table.
            }
            if (last != null)
            {
                Id = last.Id + 1;

            }
            record.Id = Id;
            base.Add(record);
            return Id;
        }
        public SlsRoute GetById(int id)
        {
            return DataContext.SlsRoutes.Where(x => x.Id == id).FirstOrDefault();
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
