using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Service.Sales
{
    public interface ITargetNAchievementReportService
    {

        DataTable GetTargetNAchievementReport(int CompanyId, int MonthId, int YearId, int EmployeeId = 0);

    }
    public class TargetNAchievementReportService : ITargetNAchievementReportService
    {
        private ISalesOrderRepository _SalesOrderRepository;
        private IUnitOfWork _UnitOfWork;

        public TargetNAchievementReportService(ISalesOrderRepository salesOrderRepository, IUnitOfWork unitOfWork)
        {
            this._SalesOrderRepository = salesOrderRepository;
            this._UnitOfWork = unitOfWork;

        }
        public DataTable GetTargetNAchievementReport(int CompanyId, int MonthId, int YearId, int EmployeeId)
        {
            DataTable dt = new DataTable();

            SqlParameter[] paramsToStore = new SqlParameter[4];
            paramsToStore[0] = new SqlParameter("@SecCompanyId", CompanyId);
            paramsToStore[1] = new SqlParameter("@Month", MonthId);
            paramsToStore[2] = new SqlParameter("@Year", YearId);
            paramsToStore[3] = new SqlParameter("@EmployeeId", EmployeeId);            
            try
            {
                dt = _SalesOrderRepository.GetFromStoredProcedure(SPList.Report.RptSlsTargetNAchievement, paramsToStore);
            }
            catch (Exception)
            {
            }

            return dt;
        }
    }
}
