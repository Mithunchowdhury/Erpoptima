using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Accounts.Repository
{
    public interface IAnfAdvancetListRepository : IRepository<AnFAdvance>
    {
        IList<AnFAdvance> GetAll();
        IList<AnFAdvance> GetAllAdvance(int companyId);
        int GetRefNo(int companyId);  //Add by Bably
        int AddEntity(AnFAdvance objadvanceEntry); //Add By Bably
    }
    public class AnfAdvancetListepository : BaseRepository<AnFAdvance>, IAnfAdvancetListRepository
    {
        public AnfAdvancetListepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        // Add By Bably
        //Auto generated RefNo
        public int GetRefNo(int companyId)
        {

            int AD = 1;
            AnFAdvance last = null;
            try
            {
                last = DataContext.AnFAdvances.Where(r => r.SecCompanyId == companyId).OrderByDescending(x => x.Id).FirstOrDefault();
            }
            catch (Exception ex)
            {

            }
            if (last != null)
            {
                AD = int.Parse(last.RefNo.Split('/')[1]) + 1;

            }
            return AD;

        }//end of GetRefNo 
       
        public IList<AnFAdvance> GetAll()
        {
            return DataContext.AnFAdvances.Include("HrmEmployee").Include("CmnFinancialYear").Include("AnFCostCenter").ToList();
        }

       
        public IList<AnFAdvance> GetAllAdvance(int companyId)
        {
            return DataContext.AnFAdvances.Where(ac => ac.SecCompanyId == companyId).ToList();
        }


        //public AnFAdvance GetById(long nullable)
        //{
        //    var a = from v in DataContext.AnFAdvances
        //            where v.Id == nullable
        //            select v;
        //    return DataContext.AnFAdvances.Where(x => x.Id == nullable).FirstOrDefault();
        //}


        //public int AddEntity(AnFAdvance objAnFAdvance)
        //{
        //    int Id = 1;
        //    AnFAdvance last = DataContext.AnFAdvances.OrderByDescending(x => x.Id).FirstOrDefault();

        //    if (last != null)
        //    {
        //        Id = last.Id + 1;

        //    }
        //    objAnFAdvance.Id = Id;
        //    base.Add(objAnFAdvance);
        //    return Id;

        //}



        //Add by Bably
        public int AddEntity(AnFAdvance objadvanceEntry)  
        {
            int Id = 1;
            AnFAdvance last = DataContext.AnFAdvances.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objadvanceEntry.Id = Id;
            base.Add(objadvanceEntry);
            return Id;
        }  


    }
}
