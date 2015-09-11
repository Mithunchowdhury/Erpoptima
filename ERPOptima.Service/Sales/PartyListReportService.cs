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

    public interface IPartyListReportService
    {

        DataTable GetPartyListReport(int companyId, int? type);


    }
    public class PartyListReportService : IPartyListReportService
    {
        private IDistributorRepository _DistributorRepository;
        private IUnitOfWork _UnitOfWork;
        public PartyListReportService(IDistributorRepository distributorRepository, IUnitOfWork unitOfWork)
        {
            this._DistributorRepository = distributorRepository;
            this._UnitOfWork = unitOfWork;

        }



        public DataTable GetPartyListReport(int companyId, int? type)
        {
            DataTable dt = new DataTable();

            SqlParameter[] paramsToStore = new SqlParameter[2];
            paramsToStore[0] = new SqlParameter("@SecCompanyId", companyId);
            paramsToStore[1] = new SqlParameter("@Type", type);

            try
            {
                dt = _DistributorRepository.GetFromStoredProcedure(SPList.Report.RptSlsPartyList, paramsToStore);
            }
            catch (Exception)
            {
            }

            return dt;
        }



    }
}
