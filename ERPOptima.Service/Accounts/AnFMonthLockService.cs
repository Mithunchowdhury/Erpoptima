using ERPOptima.Data.Accounts.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Accounts;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ERPOptima.Service.Accounts
{
    #region interface

    public interface IAnFMonthLockService
    {
        IList<AnFMonthLock> GetByfinancialYearId(int financialYearId);
        //IList<AnFMonthLock> GetAll();  // Add by Bably
        AnFMonthLock GetById(int Id);

        Operation Update(AnFMonthLock objAnFMonthLock);
        Operation Save(AnFMonthLock objAnFMonthLock);
        Operation Delete(AnFMonthLock objAnFMonthLock);

     
    }

    #endregion interface

    public class AnFMonthLockService : IAnFMonthLockService
    {
        private IAnFMonthLockRepository anFMonthLockRepository;
        private IUnitOfWork unitOfWork;

        public AnFMonthLockService(IAnFMonthLockRepository anFMonthLockRepository, IUnitOfWork unitOfWork)
        {
            // TODO: Complete member initialization
            this.anFMonthLockRepository = anFMonthLockRepository;
            this.unitOfWork = unitOfWork;
        }

        public IList<AnFMonthLock> GetByfinancialYearId(int financialYearId)
        {
            return anFMonthLockRepository.GetMany(ml => ml.CmnFinancialYearId == financialYearId).ToList();
        }

        public AnFMonthLock GetById(int Id)
        {

            return anFMonthLockRepository.GetById(Id);
        }

        public Operation Update(AnFMonthLock objAnFMonthLock)
        {
            Operation operation = new Operation { Success = true };
            anFMonthLockRepository.Update(objAnFMonthLock);
            try
            {
                unitOfWork.Commit();
            }
            catch (Exception)
            {

                operation.Success = false;
            }

            return operation;
        }

        public Operation Save(AnFMonthLock objAnFMonthLock)
        {
            Operation objOperation = new Operation { Success = true, Message = "Saved successfully." };

            long Id = anFMonthLockRepository.AddEntity(objAnFMonthLock);
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

        public Operation Delete(AnFMonthLock objAnFMonthLock)
        {
            Operation objOperation = new Operation { Success = true, Message = "Deleted successfully." };
            anFMonthLockRepository.Delete(objAnFMonthLock);

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