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
    public interface IAnFPaymentTermService
    {
        IList<AnFPaymentTerm> GetAnFPaymentTerms();
        Operation SaveAnFPaymentTerm(AnFPaymentTerm objAnFPaymentTerm);
        Operation DeleteAnFPaymentTerm(AnFPaymentTerm objAnFPaymentTerm);
        AnFPaymentTerm GetById(int Id);
        Operation UpdateAnFPaymentTerm(AnFPaymentTerm objAnFPaymentTerm);
    }
    public class AnFPaymentTermService : IAnFPaymentTermService
    {
        private IAnFPaymentTermRepository _AnFPaymentTermRepository;
        private IUnitOfWork _UnitOfWork;
        public AnFPaymentTermService(IAnFPaymentTermRepository AnFPaymentTermRepository, IUnitOfWork unitOfWork)
        {
            this._AnFPaymentTermRepository = AnFPaymentTermRepository;
            this._UnitOfWork = unitOfWork;
        }

        public IList<AnFPaymentTerm> GetAnFPaymentTerms()
        {
            return _AnFPaymentTermRepository.GetAnFPaymentTerms();
        }

        public AnFPaymentTerm GetById(int Id)
        {
            AnFPaymentTerm objAnFPaymentTerm = _AnFPaymentTermRepository.GetById(Id);
            return objAnFPaymentTerm;
        }
        public Operation UpdateAnFPaymentTerm(AnFPaymentTerm objAnFPaymentTerm)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objAnFPaymentTerm.Id };
            _AnFPaymentTermRepository.Update(objAnFPaymentTerm);

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
        public Operation DeleteAnFPaymentTerm(AnFPaymentTerm objAnFPaymentTerm)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objAnFPaymentTerm.Id };
            _AnFPaymentTermRepository.Delete(objAnFPaymentTerm);

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

        public Operation SaveAnFPaymentTerm(AnFPaymentTerm objAnFPaymentTerm)
        {
            Operation objOperation = new Operation { Success = true };

            long Id = _AnFPaymentTermRepository.AddEntity(objAnFPaymentTerm);
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
