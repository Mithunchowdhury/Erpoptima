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
    public interface IAnFProductOrServiceTypeService
    {
        IList<AnFProductOrServiceType> GetProductOrServiceTypes();
        Operation SaveAnFProductOrServiceType(AnFProductOrServiceType objAnFProductOrServiceType);
        Operation DeleteAnFProductOrServiceType(AnFProductOrServiceType objAnFProductOrServiceType);
        AnFProductOrServiceType GetById(int Id);
        Operation UpdateAnFProductOrServiceType(AnFProductOrServiceType objAnFProductOrServiceType);
    }
    public class AnFProductOrServiceTypeService : IAnFProductOrServiceTypeService
    {
        private IAnFProductOrServiceTypeRepository _AnFProductOrServiceTypeRepository;
        private IUnitOfWork _UnitOfWork;
        public AnFProductOrServiceTypeService(IAnFProductOrServiceTypeRepository AnFProductOrServiceTypeRepository, IUnitOfWork unitOfWork)
        {
            this._AnFProductOrServiceTypeRepository = AnFProductOrServiceTypeRepository;
            this._UnitOfWork = unitOfWork;
        }

        public IList<AnFProductOrServiceType> GetProductOrServiceTypes()
        {
            return _AnFProductOrServiceTypeRepository.GetProductOrServiceTypes();
        }

        public AnFProductOrServiceType GetById(int Id)
        {
            AnFProductOrServiceType objAnFProductOrServiceType = _AnFProductOrServiceTypeRepository.GetById(Id);
            return objAnFProductOrServiceType;
        }
        public Operation UpdateAnFProductOrServiceType(AnFProductOrServiceType objAnFProductOrServiceType)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objAnFProductOrServiceType.Id };
            _AnFProductOrServiceTypeRepository.Update(objAnFProductOrServiceType);

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
        public Operation DeleteAnFProductOrServiceType(AnFProductOrServiceType objAnFProductOrServiceType)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objAnFProductOrServiceType.Id };
            _AnFProductOrServiceTypeRepository.Delete(objAnFProductOrServiceType);

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

        public Operation SaveAnFProductOrServiceType(AnFProductOrServiceType objAnFProductOrServiceType)
        {
            Operation objOperation = new Operation { Success = true };

            long Id = _AnFProductOrServiceTypeRepository.AddEntity(objAnFProductOrServiceType);
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
