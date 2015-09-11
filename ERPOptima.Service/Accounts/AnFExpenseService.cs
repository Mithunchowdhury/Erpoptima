using ERPOptima.Data.Accounts.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Accounts;
using System.Collections.Generic;
using System.Linq;
using System;
//old
//using ERPOptima.Lib.Utilities;
//using ERPOptima.Model.Common;
//using ERPOptima.Service.Common;
//using System.Collections.ObjectModel;
//using System.Data;
//using System.Data.SqlClient;


namespace ERPOptima.Service.Accounts
{
    #region interface

    public interface IAnFExpenseService
    {
        //IList<AnFExpense> Get();
        AnFExpens GetById(int Id);
        string GetLastCode(int companyId, string prefix, string offcode);

        IList<AnFExpens> GetAll(int companyId, int financialYearId);
        IList<AnFExpens> Search(int companyId, int financialYearId, DateTime? dateFrom, DateTime? toDate, bool? status);
        Operation Update(AnFExpens objexpense);
        Operation Save(AnFExpens objexpense);
        //Operation Delete(AnFExpens objexpense);
        IList<AnFExpens> GetByfinancialYearId(int financialYearId);
        Operation Delete(AnFExpens objAnFExpens); 
      
    }

    #endregion interface

    public class AnFExpenseService : IAnFExpenseService
    {
        private IAnFExpenseRepository _AnFExpenseRepository;
        //private ICmnApprovalProcessService _cmnApprovalProcess;

        private IUnitOfWork unitOfWork;

        public AnFExpenseService(IAnFExpenseRepository anFExpenseRepository, IUnitOfWork unitOfWork)
        {
            this._AnFExpenseRepository = anFExpenseRepository;
            this.unitOfWork = unitOfWork;
        }

        //public IList<AnFExpense> IAnFExpenseService.GetAll()
        //{
        //    throw new NotImplementedException();
        //}

        public IList<AnFExpens> GetAll(int companyId, int financialYearId) 
        {
            IList<AnFExpens> list = new List<AnFExpens>();
            return list;
        }
        public AnFExpens GetById(int Id)
        {

            return _AnFExpenseRepository.GetById(Id);
        }
        //get auto generate referance number

        public IList<AnFExpens> GetByfinancialYearId(int financialYearId)
        {
            return _AnFExpenseRepository.GetMany(ml => ml.CmnFinancialYearId == financialYearId).ToList();
        }

        public string GetLastCode(int companyId, string prefix, string offcode)
        {
            //string code = prefix + "-" + "EXP" + "-" + offcode + "-" + DateTime.Now.ToString("yy") + "-" + DateTime.Now.ToString("MM").ToString();
            string code = prefix + "-" + "EXP" + "-" + offcode + "-" + DateTime.Now.ToString("yy") + "-" + DateTime.Now.ToString("MM") + "/" + _AnFExpenseRepository.GetLastCode(companyId).ToString();
            return code;
        }

        /*---------------------For Expense List Search---------------------*/
        public IList<AnFExpens> Search(int companyId, int financialYearId, DateTime? dateFrom, DateTime? toDate, bool? status)
        {
            IList<AnFExpens> list = new List<AnFExpens>();
            //Logic
            if (dateFrom == null && toDate == null)
            {
                if (status != null)
                {
                    var innerlist = _AnFExpenseRepository.GetAll().ToList();
                    list = innerlist.Where(e => e.SecCompanyId == companyId && e.CmnFinancialYearId == financialYearId && e.IsPosted == status).ToList();

                }
                else
                {
                    var innerlist = _AnFExpenseRepository.GetAll().ToList();
                    list = innerlist.Where(e => e.SecCompanyId == companyId && e.CmnFinancialYearId == financialYearId ).ToList();
                }
            }

            else if (dateFrom != null && toDate == null)
            {
                toDate = DateTime.Now;
                if (status != null)
                {
                    var innerlist = _AnFExpenseRepository.GetAll().ToList();
                    list = innerlist.Where(e => e.SecCompanyId == companyId && e.CmnFinancialYearId == financialYearId && e.IsPosted == status && e.Date >= dateFrom && e.Date <= toDate).ToList();
                }
                else
                {
                    var innerlist = _AnFExpenseRepository.GetAll().ToList();
                    list = innerlist.Where(e => e.SecCompanyId == companyId && e.CmnFinancialYearId == financialYearId && e.Date >= dateFrom && e.Date <= toDate).ToList();
                }
                
            }
            else if (dateFrom == null && toDate != null)
            {
                if (status != null)
                {
                    var innerlist = _AnFExpenseRepository.GetAll().ToList();
                    list = innerlist.Where(e => e.SecCompanyId == companyId && e.CmnFinancialYearId == financialYearId && e.IsPosted == status && e.Date <= toDate).ToList();
                }
                else
                {
                    var innerlist = _AnFExpenseRepository.GetAll().ToList();
                    list = innerlist.Where(e => e.SecCompanyId == companyId && e.CmnFinancialYearId == financialYearId && e.Date <= toDate).ToList();
                }
                
                
            }
            else
            {
                if (status != null)
                {
                    var innerlist = _AnFExpenseRepository.GetAll().ToList();
                    list = innerlist.Where(e => e.SecCompanyId == companyId && e.CmnFinancialYearId == financialYearId && e.IsPosted == status && e.Date >= dateFrom && e.Date <= toDate).ToList();
                }
                else
                {
                    var innerlist = _AnFExpenseRepository.GetAll().ToList();
                    list = innerlist.Where(e => e.SecCompanyId == companyId && e.CmnFinancialYearId == financialYearId && e.Date >= dateFrom && e.Date <= toDate).ToList();
                }
            }
            
            return list;
        }
        /*---------------------For Expense List Search end---------------------*/
        public Operation Update(AnFExpens objexpense)
        {
            Operation operation = new Operation { Success = true, Message = "Update successfully." };
            _AnFExpenseRepository.Update(objexpense);
            try
            {
                unitOfWork.Commit();
            }
            catch (Exception)
            {

                operation.Success = false;
                operation.Message = "Update not successful.";
            }

            return operation;
            //ff
            //Operation objOperation = new Operation { Success = true, Message = "Update successfully." };
            //_AnfAdvanceAdjustmentRepository.Update(objadjustment);

            //try
            //{
            //    _UnitOfWork.Commit();
            //}
            //catch (Exception)
            //{
            //    objOperation.Success = false;
            //    objOperation.Message = "Save not successful.";
            //}
            //return objOperation;
            //ff
        }

        public Operation Save(AnFExpens objexpense)
        {
            Operation objOperation = new Operation { Success = true, Message = "Save successfully." };

            int Id = _AnFExpenseRepository.AddEntity(objexpense);
            objOperation.OperationId = Id;

            try
            {
                unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                objOperation.Success = false;
                objOperation.Message = "Save not successful.";
            }
            return objOperation;
        }
        public Operation Delete(AnFExpens objAnFExpens)
        {
            Operation objOperation = new Operation { Success = true, Message = "Deleted successfully." };
            _AnFExpenseRepository.Delete(objAnFExpens);

            try
            {
                unitOfWork.Commit();
            }
            catch (Exception)
            {

                objOperation.Success = false;
                objOperation.Message = "Delete not successful.";
            }
            return objOperation;
        }

    }
}