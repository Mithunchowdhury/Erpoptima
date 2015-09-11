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

namespace ERPOptima.Service.Common
{


    #region interface

    public interface ICmnApprovalProcessService
    {
        IList<CmnApprovalProcess> GetCmnApprovalProcesses();
        Operation SaveCmnApprovalProcess(CmnApprovalProcess objCmnApprovalProcess);
        Operation DeleteCmnApprovalProcess(CmnApprovalProcess objCmnApprovalProcess);
        CmnApprovalProcess GetById(int Id);
        Operation UpdateCmnApprovalProcess(CmnApprovalProcess objCmnApprovalProcess);

        CmnApprovalProcess GetByShortName(string shortname,int moduleId);

        IList<CmnApprovalProcess> GetByModuleId(int moduleId);
    }



    #endregion
    public class CmnApprovalProcessService : ICmnApprovalProcessService
    {

        private ICmnApprovalProcessRepository _CmnApprovalProcessRepository;
        private IUnitOfWork _UnitOfWork;

        public CmnApprovalProcessService(ICmnApprovalProcessRepository cmnApprovalProcessRepository, IUnitOfWork unitOfWork)
        {
            this._CmnApprovalProcessRepository = cmnApprovalProcessRepository;
            this._UnitOfWork = unitOfWork;
        }

        public IList<CmnApprovalProcess> GetCmnApprovalProcesses( )
        {
            return _CmnApprovalProcessRepository.GetCmnApprovalProcesses();
        }

        public CmnApprovalProcess GetById(int Id)
        {
            CmnApprovalProcess objCmnApprovalProcess = _CmnApprovalProcessRepository.GetById(Id);
            return objCmnApprovalProcess;
        }
        public Operation UpdateCmnApprovalProcess(CmnApprovalProcess objCmnApprovalProcess)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objCmnApprovalProcess.Id };
            _CmnApprovalProcessRepository.Update(objCmnApprovalProcess);

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
        public Operation DeleteCmnApprovalProcess(CmnApprovalProcess objCmnApprovalProcess)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objCmnApprovalProcess.Id };
            _CmnApprovalProcessRepository.Delete(objCmnApprovalProcess);

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

        public Operation SaveCmnApprovalProcess(CmnApprovalProcess objCmnApprovalProcess)
        {
            Operation objOperation = new Operation { Success = true };

            long Id = _CmnApprovalProcessRepository.AddEntity(objCmnApprovalProcess);
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

        public CmnApprovalProcess GetByShortName(string shortname, int moduleId)
        {
           return _CmnApprovalProcessRepository.Get(cap => cap.ShortName == shortname && cap.SecModuleId==moduleId);
        }

        public IList<CmnApprovalProcess> GetByModuleId(int moduleId)
        {

            return _CmnApprovalProcessRepository.GetMany(cap => cap.SecModuleId == moduleId).ToList();
        
        }
    }
}
