using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales.Repository
{
    public interface ISalesReturnRepository : IRepository<SlsSalesReturn>
    {
        IList<SlsSalesReturn> GetAll();
        int NextId();
        int GetLastCode(int companyId);
        int AddEntity(SlsSalesReturn obj);
        SlsSalesReturn GetById(int id);
        IList<SlsSalesReturn> Get(int companyId, DateTime from, DateTime to);
        int SaveChanges();
        System.Data.Entity.DbContextTransaction BeginTransaction();
        void Rollback(System.Data.Entity.DbContextTransaction contextTxn);
        void Commit(System.Data.Entity.DbContextTransaction contextTxn);
        
    }
    public class SalesReturnRepository : BaseRepository<SlsSalesReturn>, ISalesReturnRepository
    {
        public SalesReturnRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public IList<SlsSalesReturn> GetAll()
        {
            return DataContext.SlsSalesReturns.ToList();
        }
        public IList<SlsSalesReturn> Get(int companyId, DateTime StartDate,DateTime EndDate)
        {
            return DataContext.SlsSalesReturns.Where(t => t.SecCompanyId == 1 && StartDate <= t.CreatedDate && EndDate >= t.CreatedDate).ToList();
        }
        public int NextId()
        {
            int Id = 1;
            SlsSalesReturn last = DataContext.SlsSalesReturns.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;
            }
            return Id;
        }
        public int GetLastCode(int companyId)
        {

            int SL = 1;
            SlsSalesReturn last = null;
            try
            {
                last = DataContext.SlsSalesReturns.Where(r => r.SecCompanyId == companyId).OrderByDescending(x => x.Id).FirstOrDefault();
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
        public int AddEntity(SlsSalesReturn obj)
        {
            int Id = NextId();
            obj.Id = Id;
            base.Add(obj);
            return Id;

        }
        public SlsSalesReturn GetById(int id)
        {
            return DataContext.SlsSalesReturns.Where(x => x.Id == id).FirstOrDefault();
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

    public class SlsSalesReturnViewModel
    {
        public int Id { get; set; }
        public int PartyType { get; set; }
        public Nullable<int> Party { get; set; }
        public string RefNo { get; set; }
        public string Reason { get; set; }
        public Nullable<decimal> AdjustedAmount { get; set; }
        public int SecCompanyId { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }


        public IList<SlsSalesReturnDetailViewModel> DetailList { get; set; }
        public bool IsAdjusted { get; set; }
    }

    public class SlsSalesReturnDetailViewModel
    {
        public int Id { get; set; }
        public int SlsReturnId { get; set; }
        public int SlsProductId { get; set; }
        public decimal ReturnedQuantity { get; set; }
        public int SlsUnitId { get; set; }
        public decimal Rate { get; set; }

        public string SlsProductName { get; set; }
        public string SlsUnitName { get; set; }        
        public SlsProduct SlsProduct { get; set; }
        public SlsUnit SlsUnit { get; set; }

    }
}
