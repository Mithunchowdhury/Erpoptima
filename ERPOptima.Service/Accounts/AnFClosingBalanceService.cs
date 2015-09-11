using ERPOptima.Data.Accounts.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Lib.Model;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Accounts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ERPOptima.Service.Accounts
{
    public interface IAnFClosingBalanceService
    {
        //Method will added here if needed

       // DataTable GetClosingBalanceByFinancialYearId(int financialYearId, int companyId);
        IList<AnFClosingBalance> GetClosingBalanceByFinancialYearId(int financialYearId, int companyId);

        AnFClosingBalance GetById(long Id);

        Operation Commit();

        Operation Save(AnFClosingBalance objviewModelList);

        Operation Update(AnFClosingBalance objviewModelList);
    }
    public class AnFClosingBalanceService : IAnFClosingBalanceService
    {
        private IAnFClosingBalanceRepository _anfClosingBalanceRepository;
        private IUnitOfWork _unitOfWork;
        public AnFClosingBalanceService(IAnFClosingBalanceRepository anfClosingBalanceRepository, IUnitOfWork unitOfWork)
        {
            _anfClosingBalanceRepository = anfClosingBalanceRepository;
            _unitOfWork = unitOfWork;
        }

        //---------------Code for using SP--------------------

        //public DataTable GetClosingBalanceByFinancialYearId(int financialYearId, int companyId)
        //{

        //    DataTable dt = new DataTable();
        //    SqlParameter[] paramsToStore = new SqlParameter[2];
        //    paramsToStore[0] = new SqlParameter("@financialyearId", financialYearId);
        //    paramsToStore[1] = new SqlParameter("@companyId", companyId);
        //    try
        //    {
        //        dt = _anfClosingBalanceRepository.GetFromStoredProcedure(SPList.AnFClosingBlance.GetAnfClosingBalanceByFinancialYearId, paramsToStore);
        //        return dt;
        //    }
        //    catch (Exception)
        //    {
        //        throw new ArgumentNullException();
        //    }

        //}

        public IList<AnFClosingBalance> GetClosingBalanceByFinancialYearId(int financialYearId, int companyId)
        {
            return _anfClosingBalanceRepository.GetClosingBalanceByFinancialYearId(financialYearId, companyId);
        }

       
        public AnFClosingBalance GetById(long Id)
        {
            return _anfClosingBalanceRepository.GetById(Id);
        }

        public Operation Commit()
        {
            Operation objOperation = new Operation { Success = true };

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                objOperation = new Operation { Success = false };

            }
            return objOperation;
        }

        public Operation Save(AnFClosingBalance objviewModelList)
        {
            Operation objOperation = new Operation { Success = true, Message = "Saved successfully." };

            long Id = _anfClosingBalanceRepository.AddEntity(objviewModelList);
            objOperation.OperationId = Id;

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                objOperation.Success = false;
                objOperation.Message = "Save not successful.";
            }
            return objOperation;
        }


        public Operation Update(AnFClosingBalance objviewModelList)
        {
            Operation operation = new Operation { Success = true };
            _anfClosingBalanceRepository.Update(objviewModelList);
            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {

                operation.Success = false;
            }

            return operation;
        }
    }
}
