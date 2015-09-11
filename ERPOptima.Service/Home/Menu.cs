using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Security.Repository;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Service.Home
{

    public interface IMenuService
    {
        DataTable GetMenusByUserIdAndModualeId(Int32 userId,Int32 moduleId);
        DataTable GetSecModuleResourcesForAndroidByUserIdAndModuleId(Int32 userId, Int32 moduleId);
        DataTable GetApprovalProcessLevelByUserId(int userId, int approvalProcessId);
        DataTable GetApprovalProcessByModuleId(int moduleId);
        DataTable IsPermitted(int userId, int processId, int levelId);
    }
    public class MenuService : IMenuService
    {
        private IMenuRepository _menuRepository;
        private IUnitOfWork _UnitOfWork;
        public MenuService(IMenuRepository menuRepository, IUnitOfWork unitOfWork)
        {
            this._menuRepository = menuRepository;
            this._UnitOfWork = unitOfWork;
        }




        public DataTable GetMenusByUserIdAndModualeId(int userId, int moduleId)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parameters = new SqlParameter[2];


            parameters[0] = new SqlParameter("@uid",userId);
            parameters[1] = new SqlParameter("@moduleid",moduleId);
            dt = _menuRepository.GetFromStoredProcedure(SPList.SecResource.GetSecModuleResourcesByUserIdAndModuleId, parameters);

            return dt;
        }
        public DataTable GetSecModuleResourcesForAndroidByUserIdAndModuleId(int userId, int moduleId)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parameters = new SqlParameter[2];


            parameters[0] = new SqlParameter("@uid", userId);
            parameters[1] = new SqlParameter("@moduleid", moduleId);
            dt = _menuRepository.GetFromStoredProcedure(SPList.SecResource.GetSecModuleResourcesByUserIdAndModuleId, parameters);

            return dt;
        }


        public DataTable GetApprovalProcessLevelByUserId(int userId, int approvalProcessId)
        {
            DataTable dt = null;
            try
            {
                dt = new DataTable();
                SqlParameter[] paramsToStore = new SqlParameter[2];

                paramsToStore[0] = new SqlParameter("@userId", userId);
                paramsToStore[1] = new SqlParameter("@approvalProcessId", approvalProcessId);
               
                dt = _menuRepository.GetFromStoredProcedure(SPList.SecResource.GetCmnProcessLevelByUserId, paramsToStore);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }


        public  DataTable GetApprovalProcessByModuleId(int moduleId)
        {
            DataTable dt = null;
            try
            {
                dt = new DataTable();
                SqlParameter[] paramsToStore = new SqlParameter[1];
                paramsToStore[0] = new SqlParameter("@moduleId", moduleId);

               // dt = SqlHelper.ExecuteDataset(DBConnection.ConnectionString(), CommandType.StoredProcedure, SPList.CmnApprovalProcess.GetCmnApprovalProcessesByModuleId, paramsToStore).Tables[0];
                dt = _menuRepository.GetFromStoredProcedure(SPList.CmnApprovalProcess.GetCmnApprovalProcessesByModuleId, paramsToStore);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable IsPermitted(int userId, int processId, int levelId)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] paramsToStore = new SqlParameter[3];
                paramsToStore[0] = new SqlParameter("@userId", userId);
                paramsToStore[1] = new SqlParameter("@processId", processId);
                paramsToStore[2] = new SqlParameter("@levelId", levelId);
               
                dt = _menuRepository.GetFromStoredProcedure(SPList.CmnApprovalUserPermissions.GetCmnApprovalUserPermissionCountByUserAndProcessAndLevelId, paramsToStore);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

       
    }
}
