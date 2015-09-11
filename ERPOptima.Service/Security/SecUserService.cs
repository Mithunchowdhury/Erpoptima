using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Security.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Security;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ERPOptima.Service.Security
{
    public interface ISecUserService
    {
        SecUser GetUserByLogin(string loginName);

        IEnumerable<SecUser> GetSecUsers();
        Operation SaveSecUser(SecUser objSecUser);
        Operation DeleteSecUser(SecUser objSecUser);
        SecUser GetById(int Id);
        Operation UpdateSecUser(SecUser objSecUser);
        DataTable GetUserByLevel(int level,int companyId);
        DataTable GetSecUsersByCompanyId(int companyId);
    }

    public class SecUserService : ISecUserService
    {
        private ISecUserRepository _secUserRepository;
        private IUnitOfWork _UnitOfWork;


        public SecUserService(ISecUserRepository secUserRepository, IUnitOfWork unitOfWork)
        {
            this._secUserRepository = secUserRepository;
            this._UnitOfWork = unitOfWork;
        }

        public DataTable GetSecUsersByCompanyId(int companyId)
        {
            try
            {
                
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@SecCompanyId", companyId);
                DataTable dt = _secUserRepository.GetFromStoredProcedure(SPList.SecUser.GetSecUsersByCompanyId, parameters);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public SecUser GetUserByLogin(string loginName)
        {
            try
            {
                SecUser user = null;
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@LoginName", loginName);
                DataTable dt = _secUserRepository.GetFromStoredProcedure(SPList.SecUser.GetSecUsersByLoginName, parameters);
                user = new SecUser();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Fill(row, user);
                    }
                }
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetUserByLevel(int level,int companyId)
        {
            try
            {               
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@Level", level);
                parameters[1] = new SqlParameter("@CompanyId", companyId);
                DataTable dt = _secUserRepository.GetFromStoredProcedure(SPList.SecUser.GetSecUsersByLevel, parameters);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public IEnumerable<SecUser> GetSecUsers()
        {
            //return _secUserRepository.GetSecUsers();
            return _secUserRepository.GetAll();

        }

        public SecUser GetById(int Id)
        {


            SecUser objSecUser = _secUserRepository.GetById(Id);
            return objSecUser;

        }


        public Operation UpdateSecUser(SecUser objSecUser)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objSecUser.Id };
            _secUserRepository.Update(objSecUser);

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

        public Operation DeleteSecUser(SecUser objSecUser)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objSecUser.Id };
            _secUserRepository.Delete(objSecUser);

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

        public Operation SaveSecUser(SecUser objSecUser)
        {
            Operation objOperation = new Operation { Success = true };
            
            long Id = _secUserRepository.AddEntity(objSecUser);
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

        #region GlobalObject Method

        protected void Fill(DataRow dtrRow, SecUser user)
        {
            try
            {
                if (((!object.ReferenceEquals(dtrRow["Id"], DBNull.Value))))
                {
                    user.Id = (Int32)dtrRow["Id"];
                }
                else user.Id = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
            
            try
            {
                if (((!object.ReferenceEquals(dtrRow["HrmEmployeeId"], DBNull.Value))))
                {
                    user.HrmEmployeeId = (int)dtrRow["HrmEmployeeId"];
                }
                else user.HrmEmployeeId = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            try
            {
                if (((!object.ReferenceEquals(dtrRow["LoginName"], DBNull.Value))))
                {
                    user.LoginName = (string)dtrRow["LoginName"];
                }
                else user.LoginName = string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
          

            try
            {
                if (((!object.ReferenceEquals(dtrRow["Password"], DBNull.Value))))
                {
                    user.Password = (string)dtrRow["Password"];
                }
                else user.Password = string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            try
            {
                if (((!object.ReferenceEquals(dtrRow["Status"], DBNull.Value))))
                {
                    user.Status = (bool)dtrRow["Status"];
                }
                else user.Status = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            try
            {
                if (((!object.ReferenceEquals(dtrRow["SecRoleId"], DBNull.Value))))
                {
                    user.SecRoleId = (int)dtrRow["SecRoleId"];
                }
                else user.SecRoleId = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        #endregion GlobalObject Method


    }
}