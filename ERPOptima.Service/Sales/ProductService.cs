using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Service.Sales
{

    public interface IProductReportService
    {
     
       
        DataTable GetProductListReport(int companyId);
           

    }

    public class ProductReportService : IProductReportService
    {
        private IProductRepository _productRepository;
        private IUnitOfWork _unitOfWork;


        public ProductReportService(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            this._productRepository = productRepository;
            this._unitOfWork = unitOfWork;
        }


        public DataTable GetProductListReport(int companyId)
        {
            DataTable dt = new DataTable();


            SqlParameter[] paramsToStore = new SqlParameter[1];
            paramsToStore[0] = new SqlParameter("@SecCompanyId", companyId);


            try
            {
                dt = _productRepository.GetFromStoredProcedure(SPList.Report.GetAllProductList, paramsToStore);
            }
            catch (Exception)
            {
            }

            return dt;
        }
        
       

    }
}
