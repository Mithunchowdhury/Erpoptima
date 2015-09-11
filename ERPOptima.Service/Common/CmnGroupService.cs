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
using ERPOptima.Model.Security;

namespace ERPOptima.Service.Common
{

    public interface ISecGroupService
    {
        IList<SecGroup> GetSecGroups();
        Operation SaveSecGroup(SecGroup objSecGroup);
        Operation DeleteSecGroup(SecGroup objSecGroup);
        SecGroup GetById(int Id);
        Operation UpdateSecGroup(SecGroup objSecGroup);   
    }
    public class SecGroupService : ISecGroupService
    {
        private ISecGroupRepository _SecGroupRepository;
        private IUnitOfWork _UnitOfWork;
        public SecGroupService(ISecGroupRepository SecGroupRepository, IUnitOfWork unitOfWork)
        {
            this._SecGroupRepository = SecGroupRepository;
            this._UnitOfWork = unitOfWork;
        }

        public IList<SecGroup> GetSecGroups()
        {
            return _SecGroupRepository.GetSecGroups();
        }

        public SecGroup GetById(int Id)
        {
            SecGroup objSecGroup = _SecGroupRepository.GetById(Id);
            return objSecGroup;
        }
        public Operation UpdateSecGroup(SecGroup objSecGroup)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objSecGroup.Id };
            _SecGroupRepository.Update(objSecGroup);

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
        public Operation DeleteSecGroup(SecGroup objSecGroup)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objSecGroup.Id };
            _SecGroupRepository.Delete(objSecGroup);

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

        public Operation SaveSecGroup(SecGroup objSecGroup)
        {
            Operation objOperation = new Operation { Success = true };

            long Id = _SecGroupRepository.AddEntity(objSecGroup);
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
    }
}

