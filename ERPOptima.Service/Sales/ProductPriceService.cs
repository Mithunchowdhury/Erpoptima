using ERPOptima.Data.Hrm.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.HRM;
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
    public interface IProductPriceService
    {
        DataTable GetAll(int productId = 0, int unitId = 0);
        IList<SlsProductPrice> GetAllProductPrices();
        SlsProductPrice GetById(int Id);
        Operation Save(SlsProductPrice objSlsProductPrice);
        Operation Delete(SlsProductPrice objSlsProductPrice);
        Operation Update(SlsProductPrice objSlsProductPrice);
    }
    public class ProductPriceService : IProductPriceService
    {
        private IProductPriceRepository _productPriceRepository;
        private IUnitOfWork _unitOfWork;


        public ProductPriceService(IProductPriceRepository productPriceRepository, IUnitOfWork unitOfWork)
        {
            this._productPriceRepository = productPriceRepository;
            this._unitOfWork = unitOfWork;
        }

        public DataTable GetAll(int productId = 0, int unitId = 0)
        {
            try
            {
                SqlParameter[] paramsToStore = new SqlParameter[2];
                paramsToStore[0] = new SqlParameter("@SlsProductId", productId);
                paramsToStore[1] = new SqlParameter("@SlsUnitId", unitId);
                DataTable dt = _productPriceRepository.GetFromStoredProcedure(SPList.ProductPrice.GetProductPriceByProductId, paramsToStore);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<SlsProductPrice> GetAllProductPrices()
        {
            return _productPriceRepository.GetAll();
        }

        //public IEnumerable<SlsProductPrice> GetAll()
        //{
        //    return _productPriceRepository.GetAll();
        //}
        public SlsProductPrice GetById(int Id)
        {
            SlsProductPrice objSlsProductPrice = _productPriceRepository.GetById(Id);
            return objSlsProductPrice;
        }
        public Operation Update(SlsProductPrice objSlsProductPrice)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objSlsProductPrice.Id };
            _productPriceRepository.Update(objSlsProductPrice);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                objOperation.Success = false;

            }
            return objOperation;
        }

        public Operation Delete(SlsProductPrice objSlsProductPrice)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objSlsProductPrice.Id };
            _productPriceRepository.Delete(objSlsProductPrice);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {

                objOperation.Success = false;
            }
            return objOperation;
        }

        public Operation Save(SlsProductPrice objSlsProductPrice)
        {
            Operation objOperation = new Operation { Success = true };

            long Id = _productPriceRepository.AddEntity(objSlsProductPrice);
            objOperation.OperationId = Id;

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                objOperation.Success = false;
            }
            return objOperation;
        }
    }
}
