using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPOptima.Model.Accounts;
using ERPOptima.Data.Infrastructure;
using System.Data;

namespace ERPOptima.Data.Accounts
{
    

    #region Interface

    public interface IChartOfAccountRepository : IRepository<AnFChartOfAccount>
    {

        IList<AnFChartOfAccount> GetParentWithChild(int companyId);
        long AddEntity(AnFChartOfAccount objAnFChartOfAccount);






        List<AnFChartOfAccount> GetByCompanyId(int CompanyId);
    }



    #endregion
    public class AnFChartOfAccountRepository : BaseRepository<AnFChartOfAccount>, IChartOfAccountRepository
    {
        public AnFChartOfAccountRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
            {

            }


        public IList<AnFChartOfAccount> GetParentWithChild(int companyId)
        {
            return DataContext.AnFChartOfAccounts.Where(ac => ac.CmnCompanyId == companyId).ToList();
        }

        public  long AddEntity(AnFChartOfAccount objAnFChartOfAccount)
        {
            long Id = 1;
            AnFChartOfAccount last = DataContext.AnFChartOfAccounts.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last !=null)
            {
                Id = last.Id + 1;
                
            }
            objAnFChartOfAccount.Id = Id;
            base.Add(objAnFChartOfAccount);
            return Id;
            
        }

        public List<AnFChartOfAccount> GetByCompanyId(int CompanyId)
        {
            return DataContext.AnFChartOfAccounts.Where(ac => ac.CmnCompanyId == CompanyId).ToList();
        }

    }
}
