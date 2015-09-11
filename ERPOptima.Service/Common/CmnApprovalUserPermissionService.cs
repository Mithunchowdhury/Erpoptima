using ERPOptima.Data.Common.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Lib.Model;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Common;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ERPOptima.Service.Common
{
    #region interface

    public interface ICmnApprovalUserPermissionService
    {
        CmnApprovalUserPermission GetById(int Id);
        int GetLastId();

        DataTable GetApprovalPermission(int moduleId, int companyId, int userId);

        void Save(CmnApprovalUserPermission cmnApprovalUserPermission);

        void Update(CmnApprovalUserPermission cmnApprovalUserPermission);

        void Delete(CmnApprovalUserPermission cmnApprovalUserPermission);

        Operation Commit();
    }

    #endregion interface

    public class CmnApprovalUserPermissionService : ICmnApprovalUserPermissionService
    {
        private ICmnApprovalUserPermissionRepository _cmnApprovalUserPermissionRepository;
        private IUnitOfWork _UnitOfWork;

        public CmnApprovalUserPermissionService(ICmnApprovalUserPermissionRepository cmnApprovalUserPermissionRepository, IUnitOfWork unitOfWork)
        {
            this._cmnApprovalUserPermissionRepository = cmnApprovalUserPermissionRepository;
            this._UnitOfWork = unitOfWork;
        }


        public CmnApprovalUserPermission GetById(int Id)
        {
            return _cmnApprovalUserPermissionRepository.GetById(Id);
        
        }
        public int GetLastId()
        {
            return _cmnApprovalUserPermissionRepository.GetLastId();
        }

        public DataTable GetApprovalPermission(int moduleId, int companyId, int userId)
        {
            DataTable dt = new DataTable();

            SqlParameter[] paramsToStore = new SqlParameter[3];
            paramsToStore[0] = new SqlParameter("@companyId", companyId);
            paramsToStore[1] = new SqlParameter("@moduleId", moduleId);
            paramsToStore[2] = new SqlParameter("@userId", userId);
            try
            {
                dt = _cmnApprovalUserPermissionRepository.GetFromStoredProcedure(SPList.CmnApprovalUserPermissions.GetApprovalUserPermission, paramsToStore);
            }
            catch (Exception ex)
            {
                dt = null;
            }

            return dt;
        }

        public void Save(CmnApprovalUserPermission cmnApprovalUserPermission)
        {
            _cmnApprovalUserPermissionRepository.Add(cmnApprovalUserPermission);
        }

        public void Update(CmnApprovalUserPermission cmnApprovalUserPermission)
        {
            _cmnApprovalUserPermissionRepository.Update(cmnApprovalUserPermission);
        }

        public void Delete(CmnApprovalUserPermission cmnApprovalUserPermission)
        {
            _cmnApprovalUserPermissionRepository.Delete(cmnApprovalUserPermission);
        }

        public Operation Commit()
        {
            Operation operation = new Operation { Success = true };

            try
            {
                _UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                operation.Success = false;
            }

            return operation;
        }
    }
}