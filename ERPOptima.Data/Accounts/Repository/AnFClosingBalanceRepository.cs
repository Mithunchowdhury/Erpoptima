using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Accounts.Repository
{
    public interface IAnFClosingBalanceRepository : IRepository<AnFClosingBalance>
    {
        //Method will added here if needed
        IList<AnFClosingBalance> GetClosingBalanceByFinancialYearId(int financialYearId, int companyId);


        long AddEntity(AnFClosingBalance objviewModelList);
    }
    public class AnFClosingBalanceRepository : BaseRepository<AnFClosingBalance>, IAnFClosingBalanceRepository
    {
        public AnFClosingBalanceRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /////

        public IList<AnFClosingBalance> GetClosingBalanceByFinancialYearId(int financialYearId, int companyId)
        {
            if (financialYearId != 0)
            {
                var query = DataContext.AnFClosingBalances.Include("AnFChartOfAccount").Where(op => op.CmnFinancialYearId == financialYearId && op.CmnCompanyId == companyId);

                List<AnFClosingBalance> list = query.ToList();

                //var headList = DataContext.AnFChartOfAccounts.Where(t => t.IsTransactionalHead == true && t.Status == true).ToList();
                var headList = DataContext.AnFChartOfAccounts.SqlQuery(
                    @"select * from AnFChartOfAccounts 
                               where Code like  (select Code from AnFChartOfAccounts
                                where Id=135)+'%' and IsTransactionalHead=1 "
                    ).ToList();

                //t.Code == "0000%"
                /*
                 var accountBalance = context
    .AccountBalanceByDate
    .Where(a => 
        a.Date == context.AccountBalanceByDate
             .Where(b => b.AccountId == a.AccountId && b.Date < date).Max(b => b.Date));
                 */

                foreach (var head in headList)
                {
                    int exists = list.Where(t => t.AnfChartOfAccountId == head.Id && t.CmnCompanyId == companyId && t.CmnFinancialYearId == financialYearId).Count();
                    if (exists == 0)
                    {
                        AnFClosingBalance notExists = new AnFClosingBalance();
                        notExists.Id = 0;
                        notExists.AnfChartOfAccountId = head.Id;
                        notExists.CmnCompanyId = companyId;
                        notExists.CmnFinancialYearId = financialYearId;
                        notExists.Credit = 0;
                        notExists.Debit = 0;
                        notExists.IsEditable = true;
                        notExists.Status = true;
                        list.Add(notExists);
                    }
                }
               
                return list;
            }
            else
            {
                return null;
            }
        }

        public long AddEntity(AnFClosingBalance objviewModelList)
        {
            long Id = 1;
            AnFClosingBalance last = DataContext.AnFClosingBalances.OrderByDescending(x => x.Id).FirstOrDefault();

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
