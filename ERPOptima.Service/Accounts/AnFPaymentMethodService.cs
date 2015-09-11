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
    public interface IAnFPaymentMethodService
    {
        IList<AnFPaymentMethod> GetAnFPaymentMethods();
        Operation SaveAnFPaymentMethod(AnFPaymentMethod objAnFPaymentMethod);
        Operation DeleteAnFPaymentMethod(AnFPaymentMethod objAnFPaymentMethod);
        AnFPaymentMethod GetById(int Id);
        Operation UpdateAnFPaymentMethod(AnFPaymentMethod objAnFPaymentMethod);
    }
    public class AnFPaymentMethodService : IAnFPaymentMethodService
    {
        private IAnFPaymentMethodRepository _AnFPaymentMethodRepository;
        private IUnitOfWork _UnitOfWork;
        public AnFPaymentMethodService(IAnFPaymentMethodRepository AnFPaymentMethodRepository, IUnitOfWork unitOfWork)
        {
            this._AnFPaymentMethodRepository = AnFPaymentMethodRepository;
            this._UnitOfWork = unitOfWork;
        }

        public IList<AnFPaymentMethod> GetAnFPaymentMethods()
        {
            return _AnFPaymentMethodRepository.GetAnFPaymentMethods();
        }

        public AnFPaymentMethod GetById(int Id)
        {
            AnFPaymentMethod objAnFPaymentMethod = _AnFPaymentMethodRepository.GetById(Id);
            return objAnFPaymentMethod;
        }
        public Operation UpdateAnFPaymentMethod(AnFPaymentMethod objAnFPaymentMethod)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objAnFPaymentMethod.Id };
            _AnFPaymentMethodRepository.Update(objAnFPaymentMethod);

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
        public Operation DeleteAnFPaymentMethod(AnFPaymentMethod objAnFPaymentMethod)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objAnFPaymentMethod.Id };
            _AnFPaymentMethodRepository.Delete(objAnFPaymentMethod);

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

        public Operation SaveAnFPaymentMethod(AnFPaymentMethod objAnFPaymentMethod)
        {
            Operation objOperation = new Operation { Success = true };

            long Id = _AnFPaymentMethodRepository.AddEntity(objAnFPaymentMethod);
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
