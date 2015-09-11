using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Inventory.Repository
{
    #region interface
    public interface IRequisitionRepository : IRepository<InvRequisition>
    {

        int GetLastId();
        int GetLastCode(int companyId);
        int AddEntity(InvRequisition objInvRequisition);
        IList<InvRequisition> GetAll(int companyId);
        InvRequisition GetById(int Id);

        int SaveChanges();
        System.Data.Entity.DbContextTransaction BeginTransaction();
        void Rollback(System.Data.Entity.DbContextTransaction contextTxn);
        void Commit(System.Data.Entity.DbContextTransaction contextTxn);
    }

    #endregion
    public class RequisitionRepository : BaseRepository<InvRequisition>, IRequisitionRepository
    {

        public RequisitionRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
            {

            }


        public int GetLastId()
        {

            int Id = 1;
            InvRequisition last = DataContext.InvRequisitions.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            return Id;

        }//end of GetLastId

        public int GetLastCode(int companyId)
        {

            int SL = 1;
            InvRequisition last = DataContext.InvRequisitions.Where(r => r.SecCompanyId == companyId).OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                //SL = int.Parse(last.RequisitionCode.Split('-')[3]) + 1;
                SL = int.Parse(last.RequisitionCode.Split('/')[1]) + 1;

            }
            return SL;
            
        }//end of GetLastCode
        public int AddEntity(InvRequisition objInvRequisition)
        {
            int Id = 1;
            InvRequisition last = DataContext.InvRequisitions.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objInvRequisition.Id = Id;
            base.Add(objInvRequisition);
            return Id;

        }
        public IList<InvRequisition> GetAll(int companyId)
        {
            return DataContext.InvRequisitions.Where(p => p.SecCompanyId == companyId).ToList();
        }

        public InvRequisition GetById(int Id)
        {
            return DataContext.InvRequisitions.Where(p => p.Id == Id).FirstOrDefault();
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
