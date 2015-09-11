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
    public interface ISecCompanyUserService
    {
        DataTable GetCompanyUsers(int userId);
        Operation SaveSecCompanyUser(List<SecCompanyUser> companyUserList,int userId);
        DataTable DeleteSecCompanyUser(int userId);
        IEnumerable<SecCompanyUser> GetAll();

    }

    public class SecCompanyUserService : ISecCompanyUserService
    {
        private ISecCompanyUserRepository _SecCompanyUserRepository;
        private IUnitOfWork _UnitOfWork;


        public SecCompanyUserService(ISecCompanyUserRepository SecCompanyUserRepository, IUnitOfWork unitOfWork)
        {
            this._SecCompanyUserRepository = SecCompanyUserRepository;
            this._UnitOfWork = unitOfWork;
        }

        public DataTable GetCompanyUsers(int userId)
        {
            try
            {
               
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@SecUserId", userId);
                DataTable dt = _SecCompanyUserRepository.GetFromStoredProcedure(SPList.SecCompanyUser.GetSecCompanyUserBySecUserId, parameters);
                
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public DataTable DeleteSecCompanyUser(int userId)
        {
            try
            {               
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@SecUserId",userId);
                DataTable dt = _SecCompanyUserRepository.GetFromStoredProcedure(SPList.SecCompanyUser.DeleteSecCompanyUsersBySecUserId, parameters);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Operation SaveSecCompanyUser(List<SecCompanyUser> companyUserList,int userId)
        {
            Operation objOperation = new Operation { Success = true };
            this.DeleteSecCompanyUser(userId);
            if (companyUserList != null)
            {
                foreach (SecCompanyUser objSecCompanyUser in companyUserList)
                {
                    objSecCompanyUser.CreatedBy = userId;
                    objSecCompanyUser.CreatedDate = DateTime.Now.Date;
                    long Id = _SecCompanyUserRepository.AddEntity(objSecCompanyUser);
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

        public IEnumerable<SecCompanyUser> GetAll()
        {
            try
            {
                return _SecCompanyUserRepository.GetAll();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }






}

