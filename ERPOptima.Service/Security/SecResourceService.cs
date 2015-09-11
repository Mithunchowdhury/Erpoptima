using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Security.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ERPOptima.Service.Security
{
    #region Interface

    public interface ISecResourceService
    {
        DataTable GetResourcePermissionByUserId(string resourceName, int userId, int secModuleId);
        DataTable GetResourcePermissionByUserOrRoleId(int roleId, int userId, int secModuleId);
        IEnumerable<SecResource> GetByModuleId(int moduleId);

        SecResource GetById(int Id);

        void Update(SecResource secResource);

        Operation Commit();

        DataTable GetResourcePermissionByRoleId(int roleId, int moduleId);

    }

    #endregion Interface

    public class SecResourceService : ISecResourceService
    {
        private ISecResourceRepository _secResourceRepository;
        private IUnitOfWork _UnitOfWork;

        public SecResourceService(ISecResourceRepository secResourceRepository, IUnitOfWork unitOfWork)
        {
            this._secResourceRepository = secResourceRepository;
            this._UnitOfWork = unitOfWork;
        }

        public DataTable GetResourcePermissionByUserId(string resourceName, int userId, int secModuleId)
        {
            DataTable dt = new DataTable();

            SqlParameter[] paramsToStore = new SqlParameter[3];
            paramsToStore[0] = new SqlParameter("@SecUserId", userId);
            paramsToStore[1] = new SqlParameter("@SecResourceName", resourceName);
            paramsToStore[2] = new SqlParameter("@SecModuleId", secModuleId);
            try
            {
                dt = _secResourceRepository.GetFromStoredProcedure(SPList.Common.GetSecResourceButtonPermission, paramsToStore);
            }
            catch (Exception ex)
            {
                dt = null;
            }

            return dt;
        }
         public DataTable GetResourcePermissionByUserOrRoleId(int roleId, int userId, int secModuleId)
        {
            DataTable dt = new DataTable();

            SqlParameter[] paramsToStore = new SqlParameter[3];
            paramsToStore[1] = new SqlParameter("@SecRoleId", roleId);
            paramsToStore[0] = new SqlParameter("@SecUserId", userId);
            paramsToStore[2] = new SqlParameter("@SecModuleId", secModuleId);
            try
            {
                dt = _secResourceRepository.GetFromStoredProcedure(SPList.SecRolePermission.GetSecRolePermissionsByRoleId, paramsToStore);
            }
            catch (Exception ex)
            {
                dt = null;
            }

            return dt;
        }
         public DataTable GetResourcePermissionByRoleId(int roleId, int secModuleId)
         {
             DataTable dt = new DataTable();

             SqlParameter[] paramsToStore = new SqlParameter[2];
             paramsToStore[0] = new SqlParameter("@SecRoleId", roleId);
             
             paramsToStore[1] = new SqlParameter("@SecModuleId", secModuleId);
             try
             {
                 dt = _secResourceRepository.GetFromStoredProcedure(SPList.SecRolePermission.GetSecRolePermissionsByRoleId, paramsToStore);
             }
             catch (Exception ex)
             {
                 dt = null;
             }

             return dt;
         }
        public IEnumerable<SecResource> GetByModuleId(int moduleId)
        {
            return _secResourceRepository.GetMany(sm => sm.SecModuleId == moduleId).ToList();
        }

        public SecResource GetById(int Id)
        {
            return _secResourceRepository.GetById(Id);
        }

        public void Update(SecResource secResource)
        {

            _secResourceRepository.Update(secResource);
        }
       
        public Operation Commit()
        {
            Operation objOperation = new Operation { Success = true };

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
    }
}