using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Inventory.Repository
{
    public interface IStockInRepository : IRepository<InvStockInOut>
    {
        IList<InvStockInOut> GetAllStockIns();
        int AddEntity(InvStockInOut objInvStore);
        InvStore GetStoreName(string name);
        //int GetRefNo(int companyId);
        int SaveChanges();
        System.Data.Entity.DbContextTransaction BeginTransaction();
        void Rollback(System.Data.Entity.DbContextTransaction contextTxn);
        void Commit(System.Data.Entity.DbContextTransaction contextTxn);
    }
    public class StockInRepository : BaseRepository<InvStockInOut>, IStockInRepository
    {

        public StockInRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public IList<InvStockInOut> GetAllStockIns()
        {
            //For now - as Stock In
            //0=Out, 1=In
            //1=Receive,2=Issue,3-damage,4-transfer
            var list = DataContext.InvStockInOuts.ToList();
            list = list.Where(i => i.Status == 1 && i.TransactionType == 1).ToList();
            return list;
        }
        public int AddEntity(InvStockInOut objInvStore)
        {
            int Id = 1;
            InvStockInOut last = null;
            try
            {
                last = DataContext.InvStockInOuts.OrderByDescending(x => x.Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                //Possibly can occur when no data exists in table.
            }
            if (last != null)
            {
                Id = last.Id + 1;

            }
            objInvStore.Id = Id;
            base.Add(objInvStore);
            return Id;

        }


        public InvStore GetStoreName(string name)
        {
            return DataContext.InvStores.Where(x => x.Name == name).FirstOrDefault();
        }

        //public int GetRefNo(int companyId)
        //{

        //    int SL = 1;
        //    InvStockInOut last = null;
        //    try
        //    {
        //        last = DataContext.InvStockInOuts.OrderByDescending(x => x.Id).FirstOrDefault();
        //    }
        //    catch (Exception ex)
        //    {
                
               
        //    }

        //    if (last != null)
        //    {
        //        SL = last.RefId;
        //        //SL = int.Parse(last.RefId.ToString('-')[3]) + 1;
        //    }
        //    return SL;

        //}
        //public int GetLastCode(int companyId)
        //{

        //    int SL = 1;
        //    InvIssue last = null;
        //    try
        //    {
        //        last = DataContext.InvIssues.Where(r => r.SecCompanyId == companyId).OrderByDescending(x => x.Id).FirstOrDefault();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    if (last != null)
        //    {
        //        //SL = last.RefId;
        //        SL = int.Parse(last.IssueCode.Split('-')[3]) + 1;

        //    }
        //    return SL;

        //}//e

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
