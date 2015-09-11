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

    public interface IAnFDeliveryMethodService
    {
        IList<AnFDeliveryMethod> GetAnFDeliveryMethods();
        Operation SaveAnFDeliveryMethod(AnFDeliveryMethod objAnFDeliveryMethod);
        Operation DeleteAnFDeliveryMethod(AnFDeliveryMethod objAnFDeliveryMethod);
        AnFDeliveryMethod GetById(int Id);
        Operation UpdateAnFDeliveryMethod(AnFDeliveryMethod objAnFDeliveryMethod);
    }
    public class AnFDeliveryMethodService : IAnFDeliveryMethodService
    {
        private IAnFDeliveryMethodRepository _AnFDeliveryMethodRepository;
        private IUnitOfWork _UnitOfWork;
        public AnFDeliveryMethodService(IAnFDeliveryMethodRepository AnFDeliveryMethodRepository, IUnitOfWork unitOfWork)
        {
            this._AnFDeliveryMethodRepository = AnFDeliveryMethodRepository;
            this._UnitOfWork = unitOfWork;
        }

        public IList<AnFDeliveryMethod> GetAnFDeliveryMethods()
        {
            return _AnFDeliveryMethodRepository.GetAnFDeliveryMethods();
        }

        public AnFDeliveryMethod GetById(int Id)
        {
            AnFDeliveryMethod objAnFDeliveryMethod = _AnFDeliveryMethodRepository.GetById(Id);
            return objAnFDeliveryMethod;
        }
        public Operation UpdateAnFDeliveryMethod(AnFDeliveryMethod objAnFDeliveryMethod)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objAnFDeliveryMethod.Id };
            _AnFDeliveryMethodRepository.Update(objAnFDeliveryMethod);

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
        public Operation DeleteAnFDeliveryMethod(AnFDeliveryMethod objAnFDeliveryMethod)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objAnFDeliveryMethod.Id };
            _AnFDeliveryMethodRepository.Delete(objAnFDeliveryMethod);

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

        public Operation SaveAnFDeliveryMethod(AnFDeliveryMethod objAnFDeliveryMethod)
        {
            Operation objOperation = new Operation { Success = true };

            long Id = _AnFDeliveryMethodRepository.AddEntity(objAnFDeliveryMethod);
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
