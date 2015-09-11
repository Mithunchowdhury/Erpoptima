using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales.Repository
{
    public interface ISalesReplacementRepository : IRepository<SlsReplacement>
    {
        int GetLastId();

        int GetLastCode(int companyId);
        int AddEntity(SlsReplacement objSlsReplacement);
        IList<SlsReplacement> GetAll(int companyId);
        SlsReplacement GetById(int Id);

        int SaveChanges();
        System.Data.Entity.DbContextTransaction BeginTransaction();
        void Rollback(System.Data.Entity.DbContextTransaction contextTxn);
        void Commit(System.Data.Entity.DbContextTransaction contextTxn);
    }
    public class SalesReplacementRepository : BaseRepository<SlsReplacement>, ISalesReplacementRepository
    {

        public SalesReplacementRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {


        }
        public int GetLastId()
        {
            int Id = 1;
            SlsReplacement last = DataContext.SlsReplacements.OrderByDescending(x => x.Id).FirstOrDefault();
            
            if (last != null)
            {
                Id = last.Id + 1;

            }
            return Id;
        }

        public int GetLastCode(int companyId)
        {
            int SL = 1;
            SlsReplacement last = DataContext.SlsReplacements.Where(r => r.SecCompanyId == companyId).OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                SL = int.Parse(last.RefNo.Split('/')[1]) + 1;

            }
            return SL;
        }

        public int AddEntity(SlsReplacement objSlsReplacement)
        {
            int Id = 1;
            SlsReplacement last = DataContext.SlsReplacements.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objSlsReplacement.Id = Id;
            base.Add(objSlsReplacement);
            return Id;
        }

        public IList<SlsReplacement> GetAll(int companyId)
        {

            return DataContext.SlsReplacements.Where(p => p.SecCompanyId == companyId).ToList();
        }

        public SlsReplacement GetById(int Id)
        {
            SlsReplacement PrReplacement = new SlsReplacement();
            PrReplacement = DataContext.SlsReplacements.Where(p => p.Id == Id).FirstOrDefault();
            return PrReplacement;
            //return DataContext.SlsReplacements.Where(p => p.Id == Id).FirstOrDefault();
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

    public partial class SlsReplacementViewModel : SlsReplacement
    {
        public IList<SlsReplacementDetailViewModel> SlsReplacementDetailVMs { get; set; }
    }


}
