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
    public interface IUnitOfMeasurementService
    {
        IEnumerable<SlsUnit> GetAll();
        Operation Save(SlsUnit objAnFMeasurementUnit);
        Operation Delete(SlsUnit objAnFMeasurementUnit);
        SlsUnit GetById(int Id);
        SlsUnit GetByName(string name);
        Operation Update(SlsUnit objAnFMeasurementUnit);
        IList<SlsUnits> GetUnitByProductRequisition(int requisitionId, int productId);

    }
    public class UnitOfMeasurementService : IUnitOfMeasurementService
    {
        private IUnitOfMeasurementRepository _AnFMeasurementUnitRepository;
        private IUnitOfWork _UnitOfWork;
        public UnitOfMeasurementService(IUnitOfMeasurementRepository AnFMeasurementUnitRepository, IUnitOfWork unitOfWork)
        {
            this._AnFMeasurementUnitRepository = AnFMeasurementUnitRepository;
            this._UnitOfWork = unitOfWork;
        }

        public IEnumerable<SlsUnit> GetAll()
        {
            return _AnFMeasurementUnitRepository.GetAll();
        }

        public SlsUnit GetById(int Id)
        {
            SlsUnit objAnFMeasurementUnit = _AnFMeasurementUnitRepository.GetById(Id);
            return objAnFMeasurementUnit;
        }
        public SlsUnit GetByName(string name)
        {
            SlsUnit objAnFMeasurementUnit = _AnFMeasurementUnitRepository.GetByName(name);
            return objAnFMeasurementUnit;
        }
        public Operation Update(SlsUnit objAnFMeasurementUnit)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objAnFMeasurementUnit.Id };
            _AnFMeasurementUnitRepository.Update(objAnFMeasurementUnit);

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
        public Operation Delete(SlsUnit objAnFMeasurementUnit)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objAnFMeasurementUnit.Id };
            _AnFMeasurementUnitRepository.Delete(objAnFMeasurementUnit);

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

        public Operation Save(SlsUnit objAnFMeasurementUnit)
        {
            Operation objOperation = new Operation { Success = true };

            long Id = _AnFMeasurementUnitRepository.AddEntity(objAnFMeasurementUnit);
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

        public IList<SlsUnits> GetUnitByProductRequisition(int requisitionId, int productId)
        {
            return _AnFMeasurementUnitRepository.GetUnitByProductRequisition(requisitionId, productId);
        }



    }
}
