using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Inventory.Repository
{
    //class InvDamageRepository
    //{
    //}


    #region interface
    public interface IInvDamageRepository : IRepository<InvDamage>
    {

        int GetLastId();

        int GetLastCode(int companyId);
        int AddEntity(InvDamage objInvDamage);
        IList<InvDamage> GetAll(int companyId);
        InvDamage GetById(int Id);

        int SaveChanges();
        System.Data.Entity.DbContextTransaction BeginTransaction();
        void Rollback(System.Data.Entity.DbContextTransaction contextTxn);
        void Commit(System.Data.Entity.DbContextTransaction contextTxn);
    }

    #endregion

    public class InvDamageRepository : BaseRepository<InvDamage>, IInvDamageRepository
    {

        public InvDamageRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {


        }
        public int GetLastId()
        {
            int Id = 1;
            InvDamage last = DataContext.InvDamages.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            return Id;
        }

        public int GetLastCode(int companyId)
        {
            int SL = 1;
            InvDamage last = DataContext.InvDamages.Where(r => r.SecCompanyId == companyId).OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                SL = int.Parse(last.RefNo.Split('/')[1]) + 1;

            }
            return SL;
        }

        public int AddEntity(InvDamage objInvDamage)
        {
            int Id = 1;
            InvDamage last = DataContext.InvDamages.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objInvDamage.Id = Id;
            base.Add(objInvDamage);
            return Id;
        }

        public IList<InvDamage> GetAll(int companyId)
        {

            var list = DataContext.InvDamages.Where(p => p.SecCompanyId == companyId).ToList();
           
            return list;
        }

        public InvDamage GetById(int Id)
        {
            return DataContext.InvDamages.Where(p => p.Id == Id).FirstOrDefault();
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
