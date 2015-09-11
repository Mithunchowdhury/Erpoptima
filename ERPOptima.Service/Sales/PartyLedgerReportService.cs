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
    public interface IPartyLedgerReportService
    {

        DataTable GetPartyLedgerReport(DateTime DateFrom, DateTime DateTo, int type, int partyId, int companyId);


    }
    public class PartyLedgerReportService : IPartyLedgerReportService
    {

        private IInvStoreOpeningRepository _InvStoreOpeningRepository;
        private IUnitOfWork _UnitOfWork;

        public PartyLedgerReportService(IInvStoreOpeningRepository invStoreOpeningRepository, IUnitOfWork unitOfWork)
        {
            this._InvStoreOpeningRepository = invStoreOpeningRepository;
            this._UnitOfWork = unitOfWork;

        }

        public DataTable GetPartyLedgerReport(DateTime DateFrom, DateTime DateTo, int type, int partyId, int companyId)
        {
            DataTable dt = new DataTable();

            SqlParameter[] paramsToStore = new SqlParameter[5];
            paramsToStore[0] = new SqlParameter("@DateFrom", DateFrom);
            paramsToStore[1] = new SqlParameter("@DateTo", DateTo);
            paramsToStore[2] = new SqlParameter("@Type", type);
            paramsToStore[3] = new SqlParameter("@PartyId", partyId);
            paramsToStore[4] = new SqlParameter("@SecCompanyId", companyId);

            try
            {
                dt = _InvStoreOpeningRepository.GetFromStoredProcedure(SPList.Report.RptPartyLedger, paramsToStore);
            }
            catch (Exception)
            {
            }

            return dt;
        }





    }
}
