using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Security.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Service.Security
{
    public interface ISecRoleService
    {
        IEnumerable<SecRole> GetSecRoles();
        Operation SaveSecRole(SecRole objSecRole);
        Operation DeleteSecRole(SecRole objSecRole);
        SecRole GetById(int Id);
        Operation UpdateSecRole(SecRole objSecRole);
    }

    public class SecRoleService : ISecRoleService
    {
        private ISecRoleRepository _SecRoleRepository;
        private IUnitOfWork _UnitOfWork;


        public SecRoleService(ISecRoleRepository SecRoleRepository, IUnitOfWork unitOfWork)
        {
            this._SecRoleRepository = SecRoleRepository;
            this._UnitOfWork = unitOfWork;
        }

        public IEnumerable<SecRole> GetSecRoles()
        {
            return _SecRoleRepository.GetAll();
        }

        public SecRole GetById(int Id)
        {
            SecRole objSecRole = _SecRoleRepository.GetById(Id);
            return objSecRole;
        }
        public Operation UpdateSecRole(SecRole objSecRole)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objSecRole.Id };
            _SecRoleRepository.Update(objSecRole);

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

        public Operation DeleteSecRole(SecRole objSecRole)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objSecRole.Id };
            _SecRoleRepository.Delete(objSecRole);

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

        public Operation SaveSecRole(SecRole objSecRole)
        {
            Operation objOperation = new Operation { Success = true };

            long Id = _SecRoleRepository.AddEntity(objSecRole);
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
