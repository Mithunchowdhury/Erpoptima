using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Sales;
using ERPOptima.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Service.Sales
{
    public interface IFreeProductService
    {
        IList<SlsFreeProductsViewModel> GetAll(int companyId);
        SlsFreeProduct GetById(int Id);
        Operation Save(SlsFreeProduct obj);
        Operation Delete(SlsFreeProduct obj);
        Operation Update(SlsFreeProduct obj);
    }

    public class FreeProductService : IFreeProductService
    {
        private IFreeProductRepository _FreeProductRepository;
        private IChartOfProductRepository _ChartOfProductRepository;
        private IUnitOfMeasurementRepository _UnitOfMeasurementRepository;
        private IUnitOfWork _UnitOfWork;

        public FreeProductService(IFreeProductRepository FreeProductRepository,
            IChartOfProductRepository ChartOfProductRepository, IUnitOfMeasurementRepository UnitOfMeasurementRepository,
            IUnitOfWork unitOfWork)
        {
            this._FreeProductRepository = FreeProductRepository;
            this._ChartOfProductRepository = ChartOfProductRepository;
            this._UnitOfMeasurementRepository = UnitOfMeasurementRepository;
            this._UnitOfWork = unitOfWork;
        }

        public IList<SlsFreeProductsViewModel> GetAll(int companyId)
        {
            var freeProductList = _FreeProductRepository.GetAll(companyId);
            var productList = _ChartOfProductRepository.GetAll(companyId);
            var unitList = _UnitOfMeasurementRepository.GetAll();

            var result = freeProductList.Select(i => new SlsFreeProductsViewModel() 
            {
                Id = i.Id,
                SlsProductId = i.SlsProductId,
                StartDate = i.StartDate,
                EndDate = i.EndDate,
                MeasurementQuantity = i.MeasurementQuantity,
                SlsUnitId = i.SlsUnitId,
                FreeQuantity = i.FreeQuantity,
                FreeUnitId = i.FreeUnitId,
                Remarks = i.Remarks,
                SecCompnayId = i.SecCompnayId,
                CreatedBy = i.CreatedBy,
                CreatedDate = i.CreatedDate,
                ModifiedBy = i.ModifiedBy,
                ModifiedDate = i.ModifiedDate,

                SlsProductName = productList.Where(j => j.Id == i.SlsProductId).FirstOrDefault().Name,
                SlsUnitName = unitList.Where(j => j.Id == i.SlsUnitId).FirstOrDefault().Name,
                FreeUnitName = unitList.Where(j => j.Id == i.FreeUnitId).FirstOrDefault().Name
            }).ToList();



            return result;
        }

        public SlsFreeProduct GetById(int Id)
        {
            SlsFreeProduct obj = _FreeProductRepository.GetById(Id);
            return obj;
        }
        public Operation Update(SlsFreeProduct obj)
        {
            Operation objOperation = new Operation { Success = true, OperationId = obj.Id };
            _FreeProductRepository.Update(obj);

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

        public Operation Delete(SlsFreeProduct obj)
        {
            Operation objOperation = new Operation { Success = true, OperationId = obj.Id };
            _FreeProductRepository.Delete(obj);

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

        public Operation Save(SlsFreeProduct obj)
        {
            Operation objOperation = new Operation { Success = true };

            long Id = _FreeProductRepository.AddEntity(obj);
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


    }

}
