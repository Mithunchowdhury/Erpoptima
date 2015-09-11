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
    public interface IProductPriceReportService
    {

        DataTable GetPriceListReport(int companyId, int ProductId);

    }
    public class ProductPriceReportService : IProductPriceReportService
    {
        private IProductPriceRepository _ProductPriceRepository;
        private IUnitOfWork _UnitOfWork;
        public ProductPriceReportService(IProductPriceRepository productPriceRepository, IUnitOfWork unitOfWork)
        {
            this._ProductPriceRepository = productPriceRepository;
            this._UnitOfWork = unitOfWork;

        }



        public DataTable GetPriceListReport(int companyId, int ProductId=0)
        {
            DataTable dt = new DataTable();

            SqlParameter[] paramsToStore = new SqlParameter[2];
            paramsToStore[0] = new SqlParameter("@SecCompanyId", companyId);
            paramsToStore[1] = new SqlParameter("@ProductId", ProductId);

            try
            {
                dt = _ProductPriceRepository.GetFromStoredProcedure(SPList.Report.RptSlsProductPrice, paramsToStore);
            }
            catch (Exception)
            {
            }

            return dt;
        }
    }
}
