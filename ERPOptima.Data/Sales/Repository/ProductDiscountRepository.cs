using ERPOptima.Data.Infrastructure;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales.Repository
{
    #region interface
    public interface IProductDiscountRepository : IRepository<SlsProductDiscount>
    {        
        int GetLastId();

        DataTable GetProductDiscount(int categoryId, int regionId, int companyId);


    }

    #endregion
    public class ProductDiscountRepository : BaseRepository<SlsProductDiscount>, IProductDiscountRepository
    {
        public ProductDiscountRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
            {

            }



        public int GetLastId()
        {

            int Id = 1;
            SlsProductDiscount last = DataContext.SlsProductDiscounts.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }         
            return Id;

        }//end of GetLastId


        public DataTable GetProductDiscount(int categoryId, int regionId, int companyId)
        {
            DataTable dt = null;
            try
            {
                dt = new DataTable();

                SqlParameter[] paramsToStore = new SqlParameter[3];

                paramsToStore[0] = new SqlParameter("@CategoryId", categoryId);
                paramsToStore[1] = new SqlParameter("@SlsRegionId", regionId);
                paramsToStore[2] = new SqlParameter("@SecCompanyId", companyId);

                dt = GetFromStoredProcedure(SPList.ProductDiscount.GetSlsProductDiscountBySlsRegionIdNCategoryId, paramsToStore);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;

        }





    }
}
