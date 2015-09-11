using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ERPOptima.Service.Sales
{

    #region interface

    public interface IProductUnitService
    {
       
        IList<SlsProductUnit> GetSlsProductUnitsBySlsProductId(int productId);

        Operation Save(SlsProductUnit objSlsProductUnit);
        Operation Delete(SlsProductUnit objSlsProductUnit);
        SlsProductUnit GetById(int Id);
        Operation Update(SlsProductUnit objSlsProductUnit);

      //  SlsProductUnit GetByProductId(int uId);

        IEnumerable<SlsProductUnit> GetAll();       
        
    }


    #endregion
    public class ProductUnitService:IProductUnitService
    {
        private IProductUnitRepository _ProductUnitRepository;
        private IUnitOfWork _UnitOfWork;
        public ProductUnitService(IProductUnitRepository productUnitRepository, IUnitOfWork unitOfWork)
        {
            this._ProductUnitRepository = productUnitRepository;
            this._UnitOfWork = unitOfWork;
        }

        public IList<SlsProductUnit> GetSlsProductUnitsBySlsProductId(int productId)
        {                

            return _ProductUnitRepository.GetSlsProductUnitsBySlsProductId(productId);
          

        }

        public SlsProductUnit GetById(int Id)
        {
            SlsProductUnit objSlsProductUnit = _ProductUnitRepository.GetById(Id);
            return objSlsProductUnit;
        }
        public Operation Save(SlsProductUnit objSlsProductUnit)
        {
            Operation objOperation = new Operation { Success = true };

            long Id = _ProductUnitRepository.AddEntity(objSlsProductUnit);
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
        public Operation Update(SlsProductUnit objSlsProductUnit)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objSlsProductUnit.Id };
            _ProductUnitRepository.Update(objSlsProductUnit);

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
        public Operation Delete(SlsProductUnit objSlsProductUnit)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objSlsProductUnit.Id };
            _ProductUnitRepository.Delete(objSlsProductUnit);

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



        public IEnumerable<SlsProductUnit> GetAll()
        {
            try
            {
                return _ProductUnitRepository.GetAll();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


   
    }
}
