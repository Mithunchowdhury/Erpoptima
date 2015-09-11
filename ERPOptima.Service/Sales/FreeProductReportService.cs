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
    public interface IFreeProductReportService
    {
        DataTable GetFreeProductsReport(int companyId, int productId);
    }

    public class FreeProductReportService : IFreeProductReportService
    {
        private IFreeProductRepository _freeProductRepository;
        private IUnitOfWork _unitOfWork;


        public FreeProductReportService(IFreeProductRepository freeProductRepository, IUnitOfWork unitOfWork)
        {
            this._freeProductRepository = freeProductRepository;
            this._unitOfWork = unitOfWork;
        }


        public DataTable GetFreeProductsReport(int companyId, int productId)
        {
            DataTable dt = new DataTable();


            SqlParameter[] paramsToStore = new SqlParameter[2];
            paramsToStore[0] = new SqlParameter("@SecCompanyId", companyId);
            paramsToStore[1] = new SqlParameter("@ProductId", productId);


            try
            {
                dt = _freeProductRepository.GetFromStoredProcedure(SPList.Report.RptGetAllFreeProducts, paramsToStore);
            }
            catch (Exception)
            {
            }

            return dt;
        }

        
    }
}
