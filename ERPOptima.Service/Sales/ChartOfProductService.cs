using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Service.Sales
{

    #region Interface

    public interface IChartOfProductService
    {
        IList<SlsProduct> GetAll(int companyId);
        SlsProduct GetById(int Id);       
        IList<SlsProduct> GetProducts(int CompanyId);
        IList<SlsProducts> GetProductsByReqId(int requisitionId, int CompanyId);
        IList<SlsProduct> GetCategories(int CompanyId);
        IList<SlsProduct> GetSubCategories(int categoryId, int CompanyId);
        IList<SlsProduct> GetBySlsProductId(int SlsProductId);
        Operation Save(SlsProduct objSlsProduct);
        Operation Update(SlsProduct objSlsProduct);
        Operation Delete(SlsProduct objSlsProduct);
        


    }



    #endregion
    public class ChartOfProductService:IChartOfProductService
    {

        private IChartOfProductRepository _IChartOfProductRepository;
        private IUnitOfWork _UnitOfWork;
       
        public ChartOfProductService(IChartOfProductRepository chartOfProductRepository, IUnitOfWork unitOfWork)
        {
            this._IChartOfProductRepository = chartOfProductRepository;
            this._UnitOfWork = unitOfWork;
        }
                
        public IList<SlsProduct> GetAll(int companyId)
        {
           
            return _IChartOfProductRepository.GetAll(companyId);
        }

        public SlsProduct GetById(int Id)
        {
            return _IChartOfProductRepository.GetById(Id);
        }

        public IList<SlsProduct> GetProducts(int companyId)
        {
            return _IChartOfProductRepository.GetProducts(companyId);
        }
        public IList<SlsProducts> GetProductsByReqId(int requisitionId, int companyId)
        {
            return _IChartOfProductRepository.GetProductsByReqId(requisitionId,companyId);
        }

        public IList<SlsProduct> GetCategories(int companyId)
        {
            return _IChartOfProductRepository.GetCategories(companyId);
        }

        public IList<SlsProduct> GetSubCategories(int categoryId, int companyId)
        {
            return _IChartOfProductRepository.GetSubCategories(categoryId,companyId);
        }

        public IList<SlsProduct> GetBySlsProductId(int SlsProductId)
        {
            return _IChartOfProductRepository.GetBySlsProductId(SlsProductId);
        }

        public Operation Save(SlsProduct objSlsProduct)
        {
            Operation objOperation = new Operation { Success = true };

            int Id = _IChartOfProductRepository.AddEntity(objSlsProduct);
            objOperation.OperationId = Id;

            try
            {
                _UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                objOperation.Success = false;
            }

            return objOperation;
        }

        public Operation Update(SlsProduct objSlsProduct)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objSlsProduct.Id };
            _IChartOfProductRepository.Update(objSlsProduct);

            try
            {
                _UnitOfWork.Commit();
            }
            catch (Exception)
            {
                objOperation.Success = false;
            }
            return objOperation;
        }

        public Operation Delete(SlsProduct objSlsProduct)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objSlsProduct.Id };
            _IChartOfProductRepository.Delete(objSlsProduct);

            try
            {
                _UnitOfWork.Commit();
            }
            catch (Exception)
            {
                objOperation.Success = false;
            }
            return objOperation;
        }









    }





}
