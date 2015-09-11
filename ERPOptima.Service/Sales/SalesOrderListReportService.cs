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
    
    public interface ISalesOrderListReportService
    {

        DataTable GetSalesOrderReport(int companyId, int Status,DateTime StartDate, DateTime EndDate );

    }
    public class SalesOrderListReportService : ISalesOrderListReportService
    {
        private ISalesOrderRepository _SalesOrderRepository;
        private IUnitOfWork _UnitOfWork;

        public SalesOrderListReportService(ISalesOrderRepository salesOrderRepository, IUnitOfWork unitOfWork)
        {
            this._SalesOrderRepository = salesOrderRepository;
            this._UnitOfWork = unitOfWork;

        }
        public DataTable GetSalesOrderReport(int companyId, int Status,DateTime StartDate, DateTime EndDate )
        {
            DataTable dt = new DataTable();

            SqlParameter[] paramsToStore = new SqlParameter[4];
            paramsToStore[0] = new SqlParameter("@companyid", companyId);
            paramsToStore[1] = new SqlParameter("@Status", Status);
            paramsToStore[2] = new SqlParameter("@StartDate", StartDate);
            paramsToStore[3] = new SqlParameter("@EndDate", EndDate);
           
            
            try
            {
                dt = _SalesOrderRepository.GetFromStoredProcedure(SPList.Report.RptSalesOrderList, paramsToStore);
            }
            catch (Exception)
            {
            }

            return dt;
        }
    }
}
