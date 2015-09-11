using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Service.Sales
{
    #region interface

    public interface IProductDiscountService
    {


        DataTable GetProductDiscount(int categoryId, int regionId, int companyId);

        SlsProductDiscount GetById(long Id);
        int GetLastId();
        void Add(SlsProductDiscount objSlsProductDiscount);
        void Update(SlsProductDiscount objSlsProductDiscount);
       
        Operation Commit();


        void Delete(SlsProductDiscount objSlsProductDiscount);

    }


    #endregion
    public class ProductDiscountService : IProductDiscountService
    {

        private IProductDiscountRepository _ProductDiscountRepository;
        private IUnitOfWork _UnitOfWork;
        public ProductDiscountService(IProductDiscountRepository productDiscountRepository, IUnitOfWork unitOfWork)
        {
            this._ProductDiscountRepository = productDiscountRepository;
            this._UnitOfWork = unitOfWork;
        }


        public DataTable GetProductDiscount(int categoryId, int regionId, int companyId)
        {
            return _ProductDiscountRepository.GetProductDiscount(categoryId, regionId, companyId);
        }
        public SlsProductDiscount GetById(long Id)
        {
            return _ProductDiscountRepository.GetById(Id);
        }

        public int GetLastId()
        {
            return _ProductDiscountRepository.GetLastId();
        }
        public void Add(SlsProductDiscount objSlsProductDiscount)
        {
            _ProductDiscountRepository.Add(objSlsProductDiscount);
        }

        public void Update(SlsProductDiscount objSlsProductDiscount)
        {
            _ProductDiscountRepository.Update(objSlsProductDiscount);
        }

              
        public Operation Commit()
        {
            Operation objOperation = new Operation { Success = true };

            try
            {
                _UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                objOperation = new Operation { Success = false };

            }
            return objOperation;
        }


        public void Delete(SlsProductDiscount objSlsProductDiscount)
        {
            _ProductDiscountRepository.Delete(objSlsProductDiscount);
        }

    }
}
