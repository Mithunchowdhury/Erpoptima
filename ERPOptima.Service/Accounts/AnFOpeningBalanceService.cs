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



    #region interface

    public interface IAnFOpeningBalanceService
    {

        IList<AnFOpeningBalance> Get();

        IList<AnFOpeningBalance> GetByProjectId(int projectId, int companyId, int financialYearId);

        //DataTable GetOpeningBalanceReportById(int companyId, int? financialYearId);//add by muna

        IList<AnFOpeningBalance> GetByFinancialYearId(int financialYearId,int companyId); // add by muna

        AnFOpeningBalance GetById(long Id);
        void Add(AnFOpeningBalance objAnFOpeningBalance);
       
        void Remove(AnFOpeningBalance objAnFOpeningBalance);
        Operation Commit();

        Operation Delete();

        Operation Save(AnFOpeningBalance objviewModelList);

        Operation Update(AnFOpeningBalance objviewModelList);
    }


    #endregion
    public class AnFOpeningBalanceService : IAnFOpeningBalanceService
    {

        private IAnFOpeningBalanceRepository _OpeningBalanceRepository;
        private IUnitOfWork _UnitOfWork;
        public AnFOpeningBalanceService(IAnFOpeningBalanceRepository openingBalanceRepository, IUnitOfWork unitOfWork)
        {
            this._OpeningBalanceRepository = openingBalanceRepository;
            this._UnitOfWork = unitOfWork;
        }


        public IList<AnFOpeningBalance> Get()
        {
            throw new NotImplementedException();
        }

        public IList<AnFOpeningBalance> GetByProjectId(int projectId, int companyId, int financialYearId)
        {

            #region ByDataTable
            //DataTable dt = new DataTable();

            //SqlParameter[] paramsToStore = new SqlParameter[3];

            //paramsToStore[0] = new SqlParameter("@companyID", companyId);
            //paramsToStore[1] = new SqlParameter("@ProjectID", projectId);
            //paramsToStore[2] = new SqlParameter("@FinancialYearId", financialYearId);

            //dt = _OpeningBalanceRepository.GetFromStoredProcedure(SPList.AnFOpeningBalance.GetAnFOpeningBalancesByFPC,paramsToStore);
            //return dt;

            #endregion

            #region ByLinq

            return _OpeningBalanceRepository.GetByProjectId(projectId, companyId, financialYearId);

            #endregion

        }
        //public DataTable GetOpeningBalanceReportById(int companyId, int? financialYearId) //add by muna
        //{
        //    //return _OpeningBalanceRepository.GetByFinancialYearId(financialYearId, companyId);
        //    return _OpeningBalanceRepository.GetMany(op => op.CmnCompanyId == companyId && op.CmnFinancialYearId == financialYearId);
        //}

        public IList<AnFOpeningBalance> GetByFinancialYearId(int financialYearId, int companyId) //add by muna
        {
            return _OpeningBalanceRepository.GetByFinancialYearId(financialYearId, companyId);
        }

       

        //public IList<AnFOpeningBalance> GetByFinancialYearId(int financialYearId, int companyId)  // Add by Bably
        //{
        //    return _OpeningBalanceRepository.GetManyWithInclude(op => op.CmnFinancialYearId == financialYearId && op.CmnCompanyId == companyId, "AnFChartOfAccount").ToList();
        //}
        public AnFOpeningBalance GetById(long Id)
        {
            return _OpeningBalanceRepository.GetById(Id);
        }
        public void Add(AnFOpeningBalance objAnFOpeningBalance)
        {
            long lastId=_OpeningBalanceRepository.GetLastId();
            objAnFOpeningBalance.Id = lastId;

            _OpeningBalanceRepository.Add(objAnFOpeningBalance);
        }

        public void Remove(AnFOpeningBalance objAnFOpeningBalance)
        {
            _OpeningBalanceRepository.Delete(objAnFOpeningBalance);
        }


        public Operation Delete(AnFOpeningBalance objAnFOpeningBalance)
        {
            throw new NotImplementedException();
        }


        public Operation Commit()
        {
            Operation objOperation = new Operation { Success = true };

            try
            {
                _UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                objOperation = new Operation { Success = false };
                
            }
            return objOperation;
        }


        public Operation Delete()
        {
            throw new NotImplementedException();
        }

        public Operation Save(AnFOpeningBalance objviewModelList)
        {
            Operation objOperation = new Operation { Success = true, Message = "Saved successfully." };

            long Id = _OpeningBalanceRepository.AddEntity(objviewModelList);
            objOperation.OperationId = Id;

            try
            {
                _UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                objOperation.Success = false;
                objOperation.Message = "Save not successful.";
            }
            return objOperation;
        }

        public Operation Update(AnFOpeningBalance objviewModelList)
        {
            Operation operation = new Operation { Success = true };
            _OpeningBalanceRepository.Update(objviewModelList);
            try
            {
                _UnitOfWork.Commit();
            }
            catch (Exception)
            {

                operation.Success = false;
            }

            return operation;
        }
    }
}
