using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Inventory.Repository;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.ViewModel.Sales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Service.Sales
{
    public interface IPartyCreditReportService
    {

        DataTable GetPartyCreditReport(int? type, int? partyId, int companyId);

        IList<SpPartyCreditViewModel> GetPartyCurrentCredit(int? type, int? partyId, int companyId);
    }
    public class PartyCreditReportService : IPartyCreditReportService
    {

        private IInvStoreOpeningRepository _InvStoreOpeningRepository;
        private IUnitOfWork _UnitOfWork;
        public PartyCreditReportService(IInvStoreOpeningRepository invStoreOpeningRepository, IUnitOfWork unitOfWork)
        {
            this._InvStoreOpeningRepository = invStoreOpeningRepository;
            this._UnitOfWork = unitOfWork;

        }


        public DataTable GetPartyCreditReport(int? type, int? partyId, int companyId)
        {
            DataTable dt = new DataTable();

            SqlParameter[] paramsToStore = new SqlParameter[3];
            paramsToStore[0] = new SqlParameter("@Type", type);
            paramsToStore[1] = new SqlParameter("@PartyId", partyId);
            paramsToStore[2] = new SqlParameter("@SecCompanyId", companyId);

            try
            {
                dt = _InvStoreOpeningRepository.GetFromStoredProcedure(SPList.Report.RptPartyCredit, paramsToStore);
            }
            catch (Exception)
            {
            }

            return dt;
        }
        //DataTableToList
        public IList<SpPartyCreditViewModel> GetPartyCurrentCredit(int? type, int? partyId, int companyId)
        {
            IList<SpPartyCreditViewModel> list = new List<SpPartyCreditViewModel>();
            DataTable dt = new DataTable();

            SqlParameter[] paramsToStore = new SqlParameter[3];
            paramsToStore[0] = new SqlParameter("@Type", type);
            paramsToStore[1] = new SqlParameter("@PartyId", partyId);
            paramsToStore[2] = new SqlParameter("@SecCompanyId", companyId);

            try
            {
                dt = _InvStoreOpeningRepository.GetFromStoredProcedure(SPList.SalesOrder.GetPartyCredit, paramsToStore);
                if (dt != null && dt.Rows.Count > 0)
                {
                    list = dt.DataTableToList<SpPartyCreditViewModel>();
                }
            }
            catch (Exception)
            {
            }

            return list;
        }


    }
}
