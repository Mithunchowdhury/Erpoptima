using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Service.Sales
{

    public interface IDistrictService
    {
        DataTable GetAll(int? regionId, int? officeId);
        DataTable GetDistrictByEmployee(int? employeeId, int officeId);
        IEnumerable<SlsDistrict> GetAll();
        SlsDistrict GetById(int Id);
        Operation Save(SlsDistrict obj);
        Operation Delete(SlsDistrict obj);
        Operation Update(SlsDistrict obj);
    }

    public class DistrictService : IDistrictService
    {
        private IDistrictRepository _districtRepository;
        private IUnitOfWork _unitOfWork;


        public DistrictService(IDistrictRepository districtRepository, IUnitOfWork unitOfWork)
        {
            this._districtRepository = districtRepository;
            this._unitOfWork = unitOfWork;
        }

        public DataTable GetAll(int? regionId, int? officeId)
        {
            try
            {
                SqlParameter[] paramsToStore = new SqlParameter[2];
                paramsToStore[0] = new SqlParameter("@SlsRegionId", regionId);
                paramsToStore[1] = new SqlParameter("@SlsOfficeId", officeId);
                DataTable dt = _districtRepository.GetFromStoredProcedure(SPList.District.GetSlsDistricts, paramsToStore);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<SlsDistrict> GetAll()
        {
            return _districtRepository.GetAll();
        }

        public DataTable GetDistrictByEmployee(int? employeeId, int officeId)
        {
            try
            {
                SqlParameter[] paramsToStore = new SqlParameter[2];
                paramsToStore[0] = new SqlParameter("@HrmEmployeeId", employeeId);
                paramsToStore[1] = new SqlParameter("@SlsOfficeId", officeId);

                DataTable dt = _districtRepository.GetFromStoredProcedure(SPList.District.GetDistrictByEmployee, paramsToStore);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SlsDistrict GetById(int Id)
        {
            SlsDistrict obj = _districtRepository.GetById(Id);
            return obj;
        }
        public Operation Update(SlsDistrict obj)
        {
            Operation objOperation = new Operation { Success = true, OperationId = obj.Id };
            _districtRepository.Update(obj);

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

        public Operation Delete(SlsDistrict obj)
        {
            Operation objOperation = new Operation { Success = true, OperationId = obj.Id };
            _districtRepository.Delete(obj);

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

        public Operation Save(SlsDistrict obj)
        {
            Operation objOperation = new Operation { Success = true };

            int Id = _districtRepository.AddEntity(obj);
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
