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
    public interface IDailySalesReportService
    {

        DataTable GetDailySalesReport(int companyId, DateTime StartDate, DateTime EndDate, int OfficeId = 0, int CategoryId = 0, int SubCategoryId=0);

    }
    public class DailySalesReportService : IDailySalesReportService
    {
        private ISalesOrderRepository _SalesOrderRepository;
        private IUnitOfWork _UnitOfWork;

        public DailySalesReportService(ISalesOrderRepository salesOrderRepository, IUnitOfWork unitOfWork)
        {
            this._SalesOrderRepository = salesOrderRepository;
            this._UnitOfWork = unitOfWork;

        }
        public DataTable GetDailySalesReport(int companyId, DateTime StartDate, DateTime EndDate, int OfficeId = 0, int CategoryId = 0, int SubCategoryId=0)
        {
            DataTable dt = new DataTable();

            SqlParameter[] paramsToStore = new SqlParameter[6];
            paramsToStore[0] = new SqlParameter("@SecCompanyId", companyId);
            paramsToStore[1] = new SqlParameter("@StartDate", StartDate);
            paramsToStore[2] = new SqlParameter("@EndDate", EndDate);
            paramsToStore[3] = new SqlParameter("@OfficeId", OfficeId);
            paramsToStore[4] = new SqlParameter("@CategoryId", CategoryId);
            paramsToStore[5] = new SqlParameter("@SubCategoryId", SubCategoryId);

            try
            {
                dt = _SalesOrderRepository.GetFromStoredProcedure(SPList.Report.RptSlsDailySales, paramsToStore);
            }
            catch (Exception)
            {
            }

            return dt;
        }
    }
}
