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

    public interface IAnFProductOrServiceTaxService
    {
        IList<AnFProductOrServiceTax> GetProductOrServiceTaxes();
        Operation SaveAnFProductOrServiceTax(AnFProductOrServiceTax objAnFProductOrServiceTax);
        Operation DeleteAnFProductOrServiceTax(AnFProductOrServiceTax objAnFProductOrServiceTax);
        AnFProductOrServiceTax GetById(int Id);
        Operation UpdateAnFProductOrServiceTax(AnFProductOrServiceTax objAnFProductOrServiceTax);
    }
    public class AnFProductOrServiceTaxService : IAnFProductOrServiceTaxService
    {
        private IAnFProductOrServiceTaxRepository _AnFProductOrServiceTaxRepository;
        private IUnitOfWork _UnitOfWork;
        public AnFProductOrServiceTaxService(IAnFProductOrServiceTaxRepository AnFProductOrServiceTaxRepository, IUnitOfWork unitOfWork)
        {
            this._AnFProductOrServiceTaxRepository = AnFProductOrServiceTaxRepository;
            this._UnitOfWork = unitOfWork;
        }

        public IList<AnFProductOrServiceTax> GetProductOrServiceTaxes()
        {
            return _AnFProductOrServiceTaxRepository.GetProductOrServiceTaxes();
        }

        public AnFProductOrServiceTax GetById(int Id)
        {
            AnFProductOrServiceTax objAnFProductOrServiceTax = _AnFProductOrServiceTaxRepository.GetById(Id);
            return objAnFProductOrServiceTax;
        }
        public Operation UpdateAnFProductOrServiceTax(AnFProductOrServiceTax objAnFProductOrServiceTax)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objAnFProductOrServiceTax.Id };
            _AnFProductOrServiceTaxRepository.Update(objAnFProductOrServiceTax);

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
        public Operation DeleteAnFProductOrServiceTax(AnFProductOrServiceTax objAnFProductOrServiceTax)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objAnFProductOrServiceTax.Id };
            _AnFProductOrServiceTaxRepository.Delete(objAnFProductOrServiceTax);

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

        public Operation SaveAnFProductOrServiceTax(AnFProductOrServiceTax objAnFProductOrServiceTax)
        {
            Operation objOperation = new Operation { Success = true };

            long Id = _AnFProductOrServiceTaxRepository.AddEntity(objAnFProductOrServiceTax);
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
