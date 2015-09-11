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
    public interface IRegionService
    {
        IEnumerable<SlsRegion> GetAll();
        DataTable GetRegionByEmployee(int? employeeId);
        SlsRegion GetById(int Id);
        Operation Save(SlsRegion objRegion);
        Operation Delete(SlsRegion objRegion);
        Operation Update(SlsRegion objRegion);
    }

    public class RegionService : IRegionService
    {
        private IRegionRepository _regionRepository;
        private IUnitOfWork _unitOfWork;


        public RegionService(IRegionRepository regionRepository, IUnitOfWork unitOfWork)
        {
            this._regionRepository = regionRepository;
            this._unitOfWork = unitOfWork;
        }
        public DataTable GetRegionByEmployee(int? employeeId)
        {
            try
            {
                SqlParameter[] paramsToStore = new SqlParameter[1];
                paramsToStore[0] = new SqlParameter("@HrmEmployeeId", employeeId);
                DataTable dt = _regionRepository.GetFromStoredProcedure(SPList.Region.GetRegionByEmployee, paramsToStore);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<SlsRegion> GetAll()
        {
            return _regionRepository.GetAll();
        }
        public SlsRegion GetById(int Id)
        {
            SlsRegion objRegion = _regionRepository.GetById(Id);
            return objRegion;
        }
        public Operation Update(SlsRegion objRegion)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objRegion.Id };
            _regionRepository.Update(objRegion);

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
        
       
        public Operation Delete(SlsRegion objRegion)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objRegion.Id };
            _regionRepository.Delete(objRegion);

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

        public Operation Save(SlsRegion objRegion)
        {
            Operation objOperation = new Operation { Success = true };

            long Id = _regionRepository.AddEntity(objRegion);
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
