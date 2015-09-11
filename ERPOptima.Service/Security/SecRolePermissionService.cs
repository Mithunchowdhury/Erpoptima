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
     public interface ISecRolePermissionService
    {
         Operation SaveSecRolePermission(List<SecRolePermission> secRolePermissionList,int userId,int roleId,int moduleId);

         bool IsPermitted(int roleId, int resourceId);
    }
     public class SecRolePermissionService : ISecRolePermissionService
    {
        private ISecRolePermissionRepository _SecRolePermissionRepository;
        private IUnitOfWork _UnitOfWork;


        public SecRolePermissionService(ISecRolePermissionRepository SecRolePermissionRepository, IUnitOfWork unitOfWork)
        {
            this._SecRolePermissionRepository = SecRolePermissionRepository;
            this._UnitOfWork = unitOfWork;
        }
        public int DeleteSecRolePermission(int? roleId, int moduleId)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@SecRoleId", roleId);
                parameters[1] = new SqlParameter("@SecModuleId", moduleId);
                int ret = _SecRolePermissionRepository.ExecuteNonQuery(SPList.SecRolePermission.DeleteSecRolePermissionByRoleId, parameters);
                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool IsPermitted(int roleId, int resourceId)
        {
            bool ret = false;
            SecRolePermission rp = _SecRolePermissionRepository.GetAll().Where(t => t.SecResourceId== resourceId && t.SecRoleId==roleId).FirstOrDefault();
            if (rp != null)
            {
                if(rp.Add==true || rp.Edit==true || rp.Delete==true || rp.ReadOnly==true|| rp.Print==true)
                {
                    ret = true;
                }
            }
            return ret;
        }

        public Operation SaveSecRolePermission(List<SecRolePermission> secRolePermissionList, int userId, int roleId, int moduleId)
        {
            Operation objOperation = new Operation { Success = true };
            int del= this.DeleteSecRolePermission(roleId, moduleId);
            if (secRolePermissionList != null)
            {
                foreach (SecRolePermission objSecCompanyUser in secRolePermissionList)
                {
                    objSecCompanyUser.CreatedBy = userId;
                    objSecCompanyUser.CreatedDate = DateTime.Now.Date;
                    long Id = _SecRolePermissionRepository.AddEntity(objSecCompanyUser);
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
            }
            return objOperation;
        }
    }
  
   
}
