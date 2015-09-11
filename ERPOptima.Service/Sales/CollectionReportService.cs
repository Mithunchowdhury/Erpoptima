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
    public interface ICollectionReportService
    {
        DataTable GetCollectionReport(int? partyType, int? Party,int? Office, DateTime StartDate, DateTime EndDate);
    }
    public class CollectionReportService : ICollectionReportService
    {
        private ICollectionReportRepository _collectionReportRepository;
        private IUnitOfWork _UnitOfWork;

        public CollectionReportService(ICollectionReportRepository collectionReportRepository, IUnitOfWork unitOfWork)
        {
            this._collectionReportRepository = collectionReportRepository;
            this._UnitOfWork = unitOfWork;

        }
        public DataTable GetCollectionReport(int? partyType, int? Party, int? Office, DateTime StartDate, DateTime EndDate)
        {
            DataTable dt = new DataTable();

            SqlParameter[] paramsToStore = new SqlParameter[5];
            paramsToStore[0] = new SqlParameter("@partyType", partyType);
            paramsToStore[1] = new SqlParameter("@Party", Party);
            paramsToStore[2] = new SqlParameter("@Office", Office);
            paramsToStore[3] = new SqlParameter("@StartDate", StartDate);
            paramsToStore[4] = new SqlParameter("@EndDate", EndDate);


            try
            {
                dt = _collectionReportRepository.GetFromStoredProcedure(SPList.Report.RptCollectionReport, paramsToStore);
            }
            catch (Exception)
            {
            }

            return dt;
        }
    }
}
