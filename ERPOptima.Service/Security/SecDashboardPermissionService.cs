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
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Service.Security
{
    
    public interface ISecDashboardPermissionService
    { 
        Operation Save(List<SecDashboardPermission> SecDashboardPermissionList,int userId);

        DataTable GetDashboardPermissionByRoleId(int roleId, int moduleId);

        IList<PermittedDashboard> GetPermittedDashBoard(int companyId, int roleId, int moduleId);

        int Delete(int roleId, int moduleId);

    }
    public class SecDashboardPermissionService : ISecDashboardPermissionService
    {
        private ISecDashboardPermissionRepository _SecDashboardPermissionRepository;
        private IUnitOfWork _UnitOfWork;


        public SecDashboardPermissionService(ISecDashboardPermissionRepository SecDashboardPermissionRepository, IUnitOfWork unitOfWork)
        {
            this._SecDashboardPermissionRepository = SecDashboardPermissionRepository;
            this._UnitOfWork = unitOfWork;
        }
        public DataTable GetDashboardPermissionByRoleId(int roleId, int moduleId)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@SecRoleId", roleId);
                parameters[1] = new SqlParameter("@SecModuleId", moduleId);
                DataTable dt = _SecDashboardPermissionRepository.GetFromStoredProcedure(SPList.SecDashboardPermission.GetDashboardPermissionByRoleId, parameters);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Delete(int roleId,int moduleId)
        {
            int ret = 0;
            try
            {
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@SecRoleId", roleId);
                parameters[1] = new SqlParameter("@SecModuleId", moduleId);
                ret= _SecDashboardPermissionRepository.ExecuteNonQuery(SPList.SecDashboardPermission.DeleteSecDashboardPermissionByRoleId, parameters);
           
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }


        public Operation Save(List<SecDashboardPermission> SecDashboardPermissionList,int userId)
        {
            Operation objOperation = new Operation { Success = true };
            //this.Delete(SecDashboardPermissionList[0].SecRoleId);
            foreach (SecDashboardPermission obj in SecDashboardPermissionList)
            {
                obj.CreatedBy = userId;
                obj.CreatedDate = DateTime.Now.Date;
                long Id = _SecDashboardPermissionRepository.AddEntity(obj);
                objOperation.OperationId = Id;

                try
                {
                    _UnitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    objOperation.Success = false;
                }
            }
            return objOperation;
        }


        public IList<PermittedDashboard> GetPermittedDashBoard(int companyId, int roleId, int moduleId)
        {

            return _SecDashboardPermissionRepository.GetPermittedDashBoard(companyId,roleId,moduleId);


        }





    }
}
