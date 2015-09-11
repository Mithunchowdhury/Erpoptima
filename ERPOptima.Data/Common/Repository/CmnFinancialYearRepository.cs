using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Common.Repository
{

    #region Interface
    public interface ICmnFinancialYearRepository : IRepository<CmnFinancialYear>
    {
        int GetLastId();
        CmnFinancialYear GetFInancialYearById(int financialYearId);
        int AddEntity(CmnFinancialYear fy);
        void UpdateEntity(CmnFinancialYear fy);
        void DeleteEntity(CmnFinancialYear fy);
        int GetCurrentFinancialYearId(int company, int module);
    }

    #endregion

    public class CmnFinancialYearRepository :BaseRepository<CmnFinancialYear>, ICmnFinancialYearRepository
    {
        public CmnFinancialYearRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
            {

            }
       
        public int AddEntity(CmnFinancialYear fy)
        {
            int Id = 1;
            CmnFinancialYear last = DataContext.CmnFinancialYears.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            fy.Id = Id;
            base.Add(fy);
            return Id;
        }
        public void DeleteEntity(CmnFinancialYear fy)
        {
            CmnFinancialYear cy = DataContext.CmnFinancialYears.Where(x => x.Id == fy.Id).FirstOrDefault();
            base.Delete(cy);
        }
        public void UpdateEntity(CmnFinancialYear fy)
        {
            CmnFinancialYear cy = DataContext.CmnFinancialYears.Where(x => x.Id == fy.Id).FirstOrDefault();
            cy.Id = fy.Id;
            cy.Name = fy.Name;
            cy.OpeningDate = fy.OpeningDate;
            cy.SecModuleId = fy.SecModuleId;
            cy.ClosingDate = fy.ClosingDate;
            cy.CmnCompanyId = fy.CmnCompanyId;
            cy.ModifiedDate = DateTime.Now;
            base.Add(cy);
        }
        public int GetLastId()
        {
            int Id = 1;
            CmnFinancialYear last = DataContext.CmnFinancialYears.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;
            }
            return Id;
        }
        public CmnFinancialYear GetFInancialYearById(int financialYearId)
        {
            return DataContext.CmnFinancialYears.Where(x => x.Id == financialYearId).FirstOrDefault();
        }
        public int GetCurrentFinancialYearId(int company, int module)
        {
            return DataContext.CmnFinancialYears.OrderByDescending(X=>X.OpeningDate).Where(X=>X.CmnCompanyId==company && X.SecModuleId==module).FirstOrDefault().Id;
        }
    }
}
