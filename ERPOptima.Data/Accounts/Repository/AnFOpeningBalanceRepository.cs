using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Accounts.Repository
{




    #region interface
    public interface IAnFOpeningBalanceRepository : IRepository<AnFOpeningBalance>
    {
        long GetLastId();
        List<AnFOpeningBalance> GetByProjectId(int projectId, int companyId, int financialYearId);
        List<AnFOpeningBalance> GetByFinancialYearId(int financialYearId,int companyId); // add by muna



        long AddEntity(AnFOpeningBalance objviewModelList);
    }


    #endregion
    public class AnFOpeningBalanceRepository:BaseRepository<AnFOpeningBalance>,IAnFOpeningBalanceRepository
    {

        public AnFOpeningBalanceRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
            {

            }

        public long GetLastId() {

            long Id = 1;
            AnFOpeningBalance last = DataContext.AnFOpeningBalances.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            return Id;
        
        }//end of GetLastId


        public List<AnFOpeningBalance> GetByProjectId(int projectId, int companyId, int financialYearId)
        {

            var query = DataContext.AnFOpeningBalances.Include("AnFChartOfAccount").Where(op => op.CmnCompanyId == companyId && op.CmnFinancialYearId == financialYearId); //&& op.CmnProjectId == projectId);
            List<AnFOpeningBalance> list = query.ToList();
            return list;


        }
        public List<AnFOpeningBalance> GetByFinancialYearId(int financialYearId,int companyId)//add by muna
        {
            if (financialYearId != 0)
            {
                var query = DataContext.AnFOpeningBalances.Include("AnFChartOfAccount").Where(op => op.CmnFinancialYearId == financialYearId && op.CmnCompanyId == companyId);
                
                //var MergedAnFOpeningBalance = DataContext.AnFOpeningBalances.Union<AnFChartOfAccount>.Where(op => op.CmnFinancialYearId == financialYearId && op.CmnCompanyId == companyId); 

                List<AnFOpeningBalance> list = query.ToList();
                var headList = DataContext.AnFChartOfAccounts.Where(t => t.IsTransactionalHead == true && t.Status == true).ToList();
                foreach (var head in headList)
                {
                    int exists = list.Where(t => t.AnFChartOfAccountId == head.Id && t.CmnCompanyId == companyId && t.CmnFinancialYearId == financialYearId).Count();
                    if (exists == 0)
                    {
                        AnFOpeningBalance notExists = new AnFOpeningBalance();
                        notExists.Id = 0;
                        notExists.AnFChartOfAccountId = head.Id;
                        notExists.CmnCompanyId = companyId;
                        notExists.CmnFinancialYearId = financialYearId;
                        notExists.Credit = 0;
                        notExists.Debit = 0;
                        notExists.IsEditable = true;
                        notExists.Status = true;
                        list.Add(notExists);
                    }
                }
                //List<AnFChartOfAccount> List1 = MergedAnFOpeningBalance.ToList();
                return list;
            }
            else
            {
                return null;
            }
        }



        public long AddEntity(AnFOpeningBalance objviewModelList)
        {
            long Id = 1;
            AnFOpeningBalance last = DataContext.AnFOpeningBalances.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objviewModelList.Id = Id;
            base.Add(objviewModelList);
            return Id;
        }
    }
}
