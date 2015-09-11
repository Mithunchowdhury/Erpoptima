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
    public interface IAreaConfigurationService
    {
        IEnumerable<SlsAreaConfiguration> GetAll();
        DataTable DeleteConfiguration(int employeeId);
        Operation Save(SlsAreaConfiguration obj);
        DataTable GetByEmployeeId(int employeeId);
    }

    public class AreaConfigurationService : IAreaConfigurationService
    {
        private IAreaConfigurationRepository _areaRepository;
        private IUnitOfWork _unitOfWork;


        public AreaConfigurationService(IAreaConfigurationRepository areaRepository, IUnitOfWork unitOfWork)
        {
            this._areaRepository = areaRepository;
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<SlsAreaConfiguration> GetAll()
        {
            return _areaRepository.GetAll();
        }
        public DataTable GetByEmployeeId(int employeeId)
        {
            try
            {
                SqlParameter[] paramsToStore = new SqlParameter[1];
                paramsToStore[0] = new SqlParameter("@HrmEmployeeId", employeeId);
                DataTable dt = _areaRepository.GetFromStoredProcedure(SPList.Area.GetAreaConfigurationByEmployeeId, paramsToStore);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DeleteConfiguration(int employeeId)
        {
            try
            {
                SqlParameter[] paramsToStore = new SqlParameter[1];
                paramsToStore[0] = new SqlParameter("@HrmEmployeeId", employeeId);
                DataTable dt = _areaRepository.GetFromStoredProcedure(SPList.Area.DeleteConfiguration, paramsToStore);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }       

        public Operation Save(SlsAreaConfiguration objSlsArea)
        {
            Operation objOperation = new Operation { Success = true };

            int Id = _areaRepository.AddEntity(objSlsArea);
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
