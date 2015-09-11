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

    public interface ICorporateSalesRepository : IRepository<SlsCorporateSalesApplication>
    {
        IList<SlsCorporateSalesApplication> GetAll(int companyId);
        int AddEntity(SlsCorporateSalesApplication objSlsArea);
        //SlsCorporateSalesApplication GetById(int id);
        int GetLastCode(int companyId);
        int SaveChanges();
        System.Data.Entity.DbContextTransaction BeginTransaction();
        void Rollback(System.Data.Entity.DbContextTransaction contextTxn);
        void Commit(System.Data.Entity.DbContextTransaction contextTxn);
    }

    #endregion
    public class CorporateSalesRepository : BaseRepository<SlsCorporateSalesApplication>, ICorporateSalesRepository
    {
        public CorporateSalesRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public IList<SlsCorporateSalesApplication> GetAll(int companyId)
        {
            List<SlsCorporateSalesApplication> list= DataContext.SlsCorporateSalesApplications.Where(t=>t.SecCompanyId==companyId).ToList();
            return list;
        }
        public int AddEntity(SlsCorporateSalesApplication obj)
        {
            int Id = 1;
            SlsCorporateSalesApplication last = DataContext.SlsCorporateSalesApplications.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            obj.Id = Id;
            base.Add(obj);
            return Id;
        }
        public int GetLastCode(int companyId)
        {

            int SL = 1;
            SlsCorporateSalesApplication last = null;
            try
            {
                last = DataContext.SlsCorporateSalesApplications.Where(r => r.SecCompanyId == companyId).OrderByDescending(x => x.Id).FirstOrDefault();
            }
            catch (Exception ex)
            {

            }
            if (last != null)
            {
                SL = int.Parse(last.RefNo.Split('/')[1]) + 1;

            }
            return SL;

        }//end of GetLastCode
        //public SlsCorporateSalesApplication GetById(int id)
        //{
        //    return null;
        //}

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
