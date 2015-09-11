using ERPOptima.Data.Accounts.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Lib.Model;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Accounts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ERPOptima.Service.Accounts
{
    public interface IAnFChequeBookService
    {
        // string GenerateChartOfAccountChildCode(string parentCode, string childCode, int Level, bool isLastNode);
        Operation SaveAnFChequeBook(AnFChequeBook objAnFChequeBook);

        Operation DeleteAnFChequeBook(AnFChequeBook objAnFChequeBook);

        AnFChequeBook GetById(int Id);

        Operation UpdateAnFChequeBook(AnFChequeBook objAnFChequeBook);

        int GetAnFChequeBooksByTransactionHeadId(int companyId);

        IList<AnFChequeBook> GetChequeBooks();
    }

    public class AnFChequeBookService : IAnFChequeBookService
    {
        private IAnFChequeBookRepository _AnFChequeBookRepository;

        private IUnitOfWork _UnitOfWork;

        public AnFChequeBookService(IAnFChequeBookRepository AnFChequeBookRepository, IUnitOfWork unitOfWork)
        {
            this._AnFChequeBookRepository = AnFChequeBookRepository;

            this._UnitOfWork = unitOfWork;
        }

        public IList<AnFChequeBook> GetChequeBooks()
        {
            return _AnFChequeBookRepository.GetChequeBooks();
        }

        public AnFChequeBook GetById(int Id)
        {
            AnFChequeBook objAnFChequeBook = _AnFChequeBookRepository.GetById(Id);
            return objAnFChequeBook;
        }

        public Operation UpdateAnFChequeBook(AnFChequeBook objAnFChequeBook)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objAnFChequeBook.Id };
            _AnFChequeBookRepository.Update(objAnFChequeBook);

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

        public Operation DeleteAnFChequeBook(AnFChequeBook objAnFChequeBook)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objAnFChequeBook.Id };
            _AnFChequeBookRepository.Delete(objAnFChequeBook);

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

        public Operation SaveAnFChequeBook(AnFChequeBook objAnFChequeBook)
        {
            Operation objOperation = new Operation { Success = true };
            long Id = _AnFChequeBookRepository.GetLastId();
            objAnFChequeBook.Id = Id;
            _AnFChequeBookRepository.Add(objAnFChequeBook);

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

        public int GetAnFChequeBooksByTransactionHeadId(int companyId = 5)
        {
            int transactionId = 0;

            DataTable dt = new DataTable();
            SqlParameter[] paramsToStore = new SqlParameter[1];
            paramsToStore[0] = new SqlParameter("@CmnCompanyId", companyId);

            try
            {
                dt = _AnFChequeBookRepository.GetFromStoredProcedure(SPList.AnFChequeBooks.GetAnFCompanyCashAtBankByCompanyId, paramsToStore);
                transactionId = Convert.ToInt32(dt.Rows[0]["CashAtBankId"]);
            }
            catch (Exception ex)
            {
            }
            return transactionId;
        }
    }
}