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
    #region interface

    public interface IProductDetailService
    {

        DataTable GetProductDetailByIssueId(int issueId);

        DataTable GetProductDetailBydeliveryId(int deliveryId);
    }


    #endregion
    public class ProductDetailService : IProductDetailService
    {
        private IIssueDetailRepository _IssueDetailRepository;
        private IUnitOfWork _UnitOfWork;

        public ProductDetailService(IIssueDetailRepository isueDetailRepository, IUnitOfWork unitOfWork)
        {
            this._IssueDetailRepository = isueDetailRepository;
            this._UnitOfWork = unitOfWork;
        }

        public DataTable GetProductDetailByIssueId(int issueId)
         {
             try
             {
                 SqlParameter[] paramsToStore = new SqlParameter[1];
                 paramsToStore[0] = new SqlParameter("@IssueId", issueId);

                 DataTable dt = _IssueDetailRepository.GetFromStoredProcedure(SPList.GetProductDetails.GetProductByIssue, paramsToStore);

                 return dt;
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }

        public DataTable GetProductDetailBydeliveryId(int deliveryId)
        {
            try
            {
                SqlParameter[] paramsToStore = new SqlParameter[1];
                paramsToStore[0] = new SqlParameter("@DeliveryId", deliveryId);

                DataTable dt = _IssueDetailRepository.GetFromStoredProcedure(SPList.GetProductRcvDelivery.GetProductByDelivery, paramsToStore);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
