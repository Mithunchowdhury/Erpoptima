using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Inventory.Repository;
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
    public interface IStockBalanceReportService
    {

        DataTable GetStockBalanceReport(int companyId, int? storeId);


    }
    public class StockBalanceReportService : IStockBalanceReportService
    {
        private IInvStoreOpeningRepository _InvStoreOpeningRepository;
        private IUnitOfWork _UnitOfWork;
        public StockBalanceReportService(IInvStoreOpeningRepository invStoreOpeningRepository, IUnitOfWork unitOfWork)
        {
            this._InvStoreOpeningRepository = invStoreOpeningRepository;
            this._UnitOfWork = unitOfWork;

        }


        public DataTable GetStockBalanceReport(int companyId, int? storeId)
        {
            DataTable dt = new DataTable();

            SqlParameter[] paramsToStore = new SqlParameter[2];
            paramsToStore[0] = new SqlParameter("@SecCompanyId", companyId);
            paramsToStore[1] = new SqlParameter("@InvStoreId", storeId);

            try
            {
                dt = _InvStoreOpeningRepository.GetFromStoredProcedure(SPList.Report.RptStoreStockBalance, paramsToStore);
            }
            catch (Exception)
            {
            }

            return dt;
        }







    }
}
