using ERPOptima.Data.Hrm.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Security.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.HRM;
using ERPOptima.Model.Security;
using ERPOptima.Model.ViewModel;
using ERPOptima.Service.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Service.Hrm
{    
    public interface IHrmEmployeeService
    {
        IEnumerable<HrmEmployee> GetAllEmployee(int companyId);
        DataTable GetAll(int companyId);
        IList<HrmEmployeeViewModel> GetAllEmployees(int companyId);
        IList<int> GetAllEmployeeIds(int companyId, int officeId);
        HrmEmployee GetById(int Id);
        Operation Save(HrmEmployee objHrmEmployee);
        Operation Delete(HrmEmployee objHrmEmployee);
        Operation Update(HrmEmployee objHrmEmployee);
        int GetEmployeeOfficeId(int UserId);
    }

    public class HrmEmployeeService : IHrmEmployeeService
    {
        private IHrmEmployeeRepository _HrmEmployeeRepository;
        private IUnitOfWork _unitOfWork;


        public HrmEmployeeService(IHrmEmployeeRepository hrmEmployeeRepository, IUnitOfWork unitOfWork)
        {
            this._HrmEmployeeRepository = hrmEmployeeRepository;
            this._unitOfWork = unitOfWork;
        }
        public IEnumerable<HrmEmployee> GetAllEmployee(int companyId)
        {
            return _HrmEmployeeRepository.GetAll();
        }
       
        public DataTable GetAll(int companyId)
        {
            try
            {
                SqlParameter[] paramsToStore = new SqlParameter[1];
                paramsToStore[0] = new SqlParameter("@SecCompanyId", companyId);
                DataTable dt = _HrmEmployeeRepository.GetFromStoredProcedure(SPList.HrmEmployee.GetHrmEmployees, paramsToStore);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IList<HrmEmployeeViewModel> GetAllEmployees(int companyId)
        {
            try
            {
                SqlParameter[] paramsToStore = new SqlParameter[1];
                paramsToStore[0] = new SqlParameter("@SecCompanyId", companyId);
                DataTable dt = _HrmEmployeeRepository.GetFromStoredProcedure(SPList.HrmEmployee.GetHrmEmployees, paramsToStore);
                return dt.DataTableToList<HrmEmployeeViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IList<int> GetAllEmployeeIds(int companyId, int officeId)
        {
            try
            {
                IList<int> Ids = new List<int>();
                var list = _HrmEmployeeRepository.GetAll();
                if(list != null && list.Count() > 0)
                {
                    list = list.Where(i => i.SecCompanyId == companyId && i.SlsOfficeId == officeId).ToList();
                    Ids = list.Select(i => i.Id).ToList();
                }
                return Ids;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public HrmEmployee GetById(int Id)
        {
            HrmEmployee objHrmEmployee = _HrmEmployeeRepository.GetById(Id);
            return objHrmEmployee;
        }
        public Operation Update(HrmEmployee objHrmEmployee)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objHrmEmployee.Id };
            _HrmEmployeeRepository.Update(objHrmEmployee);

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

        public Operation Delete(HrmEmployee objHrmEmployee)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objHrmEmployee.Id };
            _HrmEmployeeRepository.Delete(objHrmEmployee);

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

        public Operation Save(HrmEmployee objHrmEmployee)
        {
            Operation objOperation = new Operation { Success = true };

            long Id = _HrmEmployeeRepository.AddEntity(objHrmEmployee);
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

        public int GetEmployeeOfficeId(int UserId)
        {
            try
            {
                SecUserService _SecUserService = new SecUserService(new SecUserRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));
                SecUser user = _SecUserService.GetById(UserId);

                int? officeId = _HrmEmployeeRepository.GetAll().Where(i=>i.Id == user.HrmEmployeeId).First().SlsOfficeId;
                if (officeId != null) 
                    return (int)officeId;
            }
            catch(Exception ex)
            {

            }
            return 0;
        }
    }
}
