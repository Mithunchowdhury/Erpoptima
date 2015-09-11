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
    public interface IChallenReportService
    {

        DataTable GetChallenList(int DeliveryId);


    }
    public class ChallenReportService : IChallenReportService
    {
        private IDistributorRepository _DistributorRepository;
        private IUnitOfWork _UnitOfWork;
        public ChallenReportService(IDistributorRepository distributorRepository, IUnitOfWork unitOfWork)
        {
            this._DistributorRepository = distributorRepository;
            this._UnitOfWork = unitOfWork;

        }



        public DataTable GetChallenList(int DeliveryId)
        {
            DataTable dt = new DataTable();

            SqlParameter[] paramsToStore = new SqlParameter[1];
            paramsToStore[0] = new SqlParameter("@SlsDeliveryId", DeliveryId);
            

            try
            {
                dt = _DistributorRepository.GetFromStoredProcedure(SPList.Report.RptGetChallenStatement, paramsToStore);
            }
            catch (Exception)
            {
            }

            return dt;
        }



    }
}
