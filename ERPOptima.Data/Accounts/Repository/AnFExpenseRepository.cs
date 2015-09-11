using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//nn
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using ERPOptima.Data;
//using ERPOptima.Data.Infrastructure;
//using ERPOptima.Model.Accounts;


namespace ERPOptima.Data.Accounts.Repository
{

    #region Interface



    public interface IAnFExpenseRepository : IRepository<AnFExpens>
    {
        IList<AnFExpens> GetAll(int companyId, int financialYearId);
        IList<AnFExpens> Search(int companyId, int financialYearId, DateTime? dateFrom, DateTime? toDate, bool? status);
        int AddEntity(AnFExpens expense);
        int GetLastCode(int companyId);

     
    }

    #endregion
    
    public class AnFExpenseRepository : BaseRepository<AnFExpens>, IAnFExpenseRepository
    {

        public AnFExpenseRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
            {

            }

        public int GetLastCode(int companyId)
        {

            int SL = 1;
            AnFExpens last = null;
            try
            {
                last = DataContext.AnFExpenses.Where(r => r.SecCompanyId == companyId).OrderByDescending(x => x.Id).FirstOrDefault();
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

        public IList<AnFExpens> GetAll(int companyId, int financialYearId)
        {
            IList<AnFExpens> list = new List<AnFExpens>();
            return list;
            //return DataContext.AnFExpenses.Where(e => e.SecCompanyId == companyId && e.CmnFinancialYearId == financialYearId).ToList();
        }
        public IList<AnFExpens> Search(int companyId, int financialYearId, DateTime? dateFrom, DateTime? toDate, bool? status)
        {
            IList<AnFExpens> list = new List<AnFExpens>();
            return list;
        }

        //IList<AnFExpense> GetAll(int companyId, int financialYearId)
        //{

        //    return DataContext.AnFExpenses.Where(e => e.SecCompanyId == companyId && e.CmnFinancialYearId == financialYearId).ToList();


        //}
        public int AddEntity(AnFExpens expense)
        {
            int Id = 1;
            AnFExpens last = DataContext.AnFExpenses.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            expense.Id = Id;
            base.Add(expense);
            return Id;
        }

       
              


      
       
    }




    
}
