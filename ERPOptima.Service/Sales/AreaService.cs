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
    public interface IAreaService
    {
        IList<SlsArea> GetAll();
        DataTable GetAll(int? regionId, int? officeId, int? districtId, int? thanaId);
        DataTable GetAreaByEmployee(int? employeeId, int thanaId);
        DataTable GetAreaConfigurationByEmployee(int employeeId);
        SlsArea GetById(int Id);
        Operation Save(SlsArea objSlsArea);
        Operation Delete(SlsArea objSlsArea);
        Operation Update(SlsArea objSlsArea);
    }

    public class AreaService : IAreaService
    {
        private IAreaRepository _areaRepository;
        private IUnitOfWork _unitOfWork;


        public AreaService(IAreaRepository areaRepository, IUnitOfWork unitOfWork)
        {
            this._areaRepository = areaRepository;
            this._unitOfWork = unitOfWork;
        }

        public DataTable GetAll(int? regionId, int? officeId, int? districtId, int? thanaId)
        {
            try
            {
                SqlParameter[] paramsToStore = new SqlParameter[4];
                paramsToStore[0] = new SqlParameter("@SlsRegionId", regionId);
                paramsToStore[1] = new SqlParameter("@SlsOfficeId", officeId);
                paramsToStore[2] = new SqlParameter("@SlsDistrictId", districtId);
                paramsToStore[3] = new SqlParameter("@SlsThanaId", thanaId);
                DataTable dt = _areaRepository.GetFromStoredProcedure(SPList.Area.GetSlsAreas, paramsToStore);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IList<SlsArea> GetAll()
        {
            return _areaRepository.GetAll();
        }
        public DataTable GetAreaByEmployee(int? employeeId, int thanaId)
        {
            try
            {
                SqlParameter[] paramsToStore = new SqlParameter[2];
                paramsToStore[0] = new SqlParameter("@HrmEmployeeId", employeeId);
                paramsToStore[1] = new SqlParameter("@SlsThanaId", thanaId);

                DataTable dt = _areaRepository.GetFromStoredProcedure(SPList.Area.GetAreaByEmployee, paramsToStore);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetAreaConfigurationByEmployee(int employeeId)
        {
            try
            {
                SqlParameter[] paramsToStore = new SqlParameter[1];
                paramsToStore[0] = new SqlParameter("@HrmEmployeeId", employeeId);

                DataTable dt = _areaRepository.GetFromStoredProcedure(SPList.Area.GetAreaConfigurationByEmployee, paramsToStore);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SlsArea GetById(int Id)
        {
            SlsArea objSlsArea = _areaRepository.GetById(Id);
            return objSlsArea;
        }
       
        public Operation Update(SlsArea objSlsArea)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objSlsArea.Id };
            _areaRepository.Update(objSlsArea);

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

        public Operation Delete(SlsArea objSlsArea)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objSlsArea.Id };
            _areaRepository.Delete(objSlsArea);

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

        public Operation Save(SlsArea objSlsArea)
        {
            Operation objOperation = new Operation { Success = true };

            long Id = _areaRepository.AddEntity(objSlsArea);
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
