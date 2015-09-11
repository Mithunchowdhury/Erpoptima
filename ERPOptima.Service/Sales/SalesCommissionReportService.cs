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
    public interface ISalesCommissionReportService
    {

        DataTable GetSalesCommissionReport(DateTime? DateFrom, DateTime? DateTo, int companyId);


    }
    public class SalesCommissionReportService : ISalesCommissionReportService
    {

        private IInvStoreOpeningRepository _InvStoreOpeningRepository;
        private IUnitOfWork _UnitOfWork;
        public SalesCommissionReportService(IInvStoreOpeningRepository invStoreOpeningRepository, IUnitOfWork unitOfWork)
        {
            this._InvStoreOpeningRepository = invStoreOpeningRepository;
            this._UnitOfWork = unitOfWork;

        }


        public DataTable GetSalesCommissionReport(DateTime? DateFrom, DateTime? DateTo, int companyId)
        {
            DataTable dt = new DataTable();

            SqlParameter[] paramsToStore = new SqlParameter[3];
            paramsToStore[0] = new SqlParameter("@DateFrom", DateFrom);
            paramsToStore[1] = new SqlParameter("@DateTo", DateTo);
            paramsToStore[2] = new SqlParameter("@SecCompanyId", companyId);

            try
            {
                dt = _InvStoreOpeningRepository.GetFromStoredProcedure(SPList.Report.RptSalesCommission, paramsToStore);
            }
            catch (Exception)
            {
            }

            return dt;
        }



    }
}
