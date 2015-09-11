using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPOptima.Data.Accounts.Repository;
using ERPOptima.Data.Common.Repository;
using System.Data;
using System.Data.SqlClient;
using ERPOptima.Model.Accounts;
using ERPOptima.Lib.Model;
using ERPOptima.Lib.Utilities;

namespace ERPOptima.Service.Accounts
{
    public interface IAnFMeasurementUnitService
    {
        IList<AnFMeasurementUnit> GetAnFMeasurementUnits();
        Operation SaveAnFMeasurementUnit(AnFMeasurementUnit objAnFMeasurementUnit);
        Operation DeleteAnFMeasurementUnit(AnFMeasurementUnit objAnFMeasurementUnit);
        AnFMeasurementUnit GetById(int Id);
        Operation UpdateAnFMeasurementUnit(AnFMeasurementUnit objAnFMeasurementUnit);
    }
    public class AnFMeasurementUnitService : IAnFMeasurementUnitService
    {
        private IAnFMeasurementUnitRepository _AnFMeasurementUnitRepository;
        private IUnitOfWork _UnitOfWork;
        public AnFMeasurementUnitService(IAnFMeasurementUnitRepository AnFMeasurementUnitRepository, IUnitOfWork unitOfWork)
        {
            this._AnFMeasurementUnitRepository = AnFMeasurementUnitRepository;
            this._UnitOfWork = unitOfWork;
        }

        public IList<AnFMeasurementUnit> GetAnFMeasurementUnits()
        {
            return _AnFMeasurementUnitRepository.GetAnFMeasurementUnits();
        }

        public AnFMeasurementUnit GetById(int Id)
        {
            AnFMeasurementUnit objAnFMeasurementUnit = _AnFMeasurementUnitRepository.GetById(Id);
            return objAnFMeasurementUnit;
        }
        public Operation UpdateAnFMeasurementUnit(AnFMeasurementUnit objAnFMeasurementUnit)
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
        public Operation DeleteAnFMeasurementUnit(AnFMeasurementUnit objAnFMeasurementUnit)
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

        public Operation SaveAnFMeasurementUnit(AnFMeasurementUnit objAnFMeasurementUnit)
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
    }
}
