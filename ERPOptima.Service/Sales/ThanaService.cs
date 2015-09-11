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
    
    public interface IThanaService
    {
        DataTable GetAll(int? regionId, int? officeId, int? districtId);
        DataTable GetThanaByEmployee(int? employeeId, int districtId);
        IEnumerable<SlsThana> GetAll();
        Operation Save(SlsThana obj);
        Operation Delete(SlsThana obj);
        SlsThana GetById(int Id);
        Operation Update(SlsThana obj);
        IList<SlsThana> GetThanasForRegion(int regionId);
        IList<SlsThana> GetThanasForOffice(int officeId);
        IList<SlsThana> GetThanasForDistrict(int districtId);
    }
    public class ThanaService : IThanaService
    {
        private IThanaRepository _thanaRepository;
        private IUnitOfWork _UnitOfWork;
        public ThanaService(IThanaRepository thanaRepository, IUnitOfWork unitOfWork)
        {
            this._thanaRepository = thanaRepository;
            this._UnitOfWork = unitOfWork;
        }

        public DataTable GetAll(int? regionId, int? officeId, int? districtId)
        {
            try
            {
                SqlParameter[] paramsToStore = new SqlParameter[3];
                paramsToStore[0] = new SqlParameter("@SlsRegionId", regionId);
                paramsToStore[1] = new SqlParameter("@SlsOfficeId", officeId);
                paramsToStore[2] = new SqlParameter("@SlsDistrictId", districtId);
                DataTable dt = _thanaRepository.GetFromStoredProcedure(SPList.Thana.GetSlsThanas, paramsToStore);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<SlsThana> GetAll()
        {
            return _thanaRepository.GetAll();
        }
        public DataTable GetThanaByEmployee(int? employeeId, int districtId)
        {
            try
            {
                SqlParameter[] paramsToStore = new SqlParameter[2];
                paramsToStore[0] = new SqlParameter("@HrmEmployeeId", employeeId);
                paramsToStore[1] = new SqlParameter("@SlsDistrictId", districtId);

                DataTable dt = _thanaRepository.GetFromStoredProcedure(SPList.Thana.GetThanaByEmployee, paramsToStore);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SlsThana GetById(int Id)
        {
            SlsThana obj = _thanaRepository.GetById(Id);
            return obj;
        }
        public Operation Update(SlsThana obj)
        {
            Operation objOperation = new Operation { Success = true, OperationId = obj.Id };
            _thanaRepository.Update(obj);

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
        public Operation Delete(SlsThana obj)
        {
            Operation objOperation = new Operation { Success = true, OperationId = obj.Id };
            _thanaRepository.Delete(obj);

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

        public Operation Save(SlsThana obj)
        {
            Operation objOperation = new Operation { Success = true };

            long Id = _thanaRepository.AddEntity(obj);
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

        public IList<SlsThana> GetThanasForRegion(int regionId)
        {
            IList<SlsThana> list = _thanaRepository.GetThanasForRegion(regionId);
            return list;
        }
        public IList<SlsThana> GetThanasForOffice(int officeId)
        {
            IList<SlsThana> list = _thanaRepository.GetThanasForOffice(officeId);
            return list;
        }
        public IList<SlsThana> GetThanasForDistrict(int districtId)
        {
            IList<SlsThana> list = _thanaRepository.GetThanasForDistrict(districtId);
            return list;
        }

    }
}
