using ERPOptima.Data.Hrm.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Lib.Model;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.HRM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Service.Hrm
{   
    public interface IHrmDepartmentService
    {
        DataTable GetAll();
        HrmDepartment GetById(int Id);
        Operation Save(HrmDepartment objHrmDepartment);
        Operation Delete(HrmDepartment objHrmDepartment);
        Operation Update(HrmDepartment objHrmDepartment); 
    }

    public class HrmDepartmentService : IHrmDepartmentService
    {
        private IHrmDepartmentRepository _hrmDepartmentRepository;
        private IUnitOfWork _unitOfWork;


        public HrmDepartmentService(IHrmDepartmentRepository hrmDepartmentRepository, IUnitOfWork unitOfWork)
        {
            this._hrmDepartmentRepository = hrmDepartmentRepository;
            this._unitOfWork = unitOfWork;
        }
       
        public DataTable GetAll()
        {
            try
            {
                DataTable dt = _hrmDepartmentRepository.GetFromStoredProcedure(SPList.HrmDepartment.GetHrmDepartments, null);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public HrmDepartment GetById(int Id)
        {
            HrmDepartment objHrmDepartment = _hrmDepartmentRepository.GetById(Id);
            return objHrmDepartment;
        }
        public Operation Update(HrmDepartment objHrmDepartment)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objHrmDepartment.Id };
            _hrmDepartmentRepository.Update(objHrmDepartment);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                objOperation.Success = false;

            }
            return objOperation;
        }

        public Operation Delete(HrmDepartment objHrmDepartment)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objHrmDepartment.Id };
            _hrmDepartmentRepository.Delete(objHrmDepartment);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {

                objOperation.Success = false;
            }
            return objOperation;
        }

        public Operation Save(HrmDepartment objHrmDepartment)
        {
            Operation objOperation = new Operation { Success = true };

            long Id = _hrmDepartmentRepository.AddEntity(objHrmDepartment);
            objOperation.OperationId = Id;

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                objOperation.Success = false;
            }
            return objOperation;
        }
    }
}
