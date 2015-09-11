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
    public interface ICorporateClientService
    {
        //DataTable GetAll(int? regionId, int? officeId, int? districtId, int? employeeId);
        //DataTable GetCorporateClientById(int? employeeId, int districtId);
        IEnumerable<SlsCorporateClient> GetAll();
        IEnumerable<SlsCorporateClient> GetCorporateName(int companyId);
        Operation Save(SlsCorporateClient obj);
        Operation Delete(SlsCorporateClient obj);
        SlsCorporateClient GetById(int Id);
        Operation Update(SlsCorporateClient obj);
    }
    public class CorporateClientService : ICorporateClientService
    {
        private ICorporateClientRepository _corporateClientRepository;
        private IUnitOfWork _UnitOfWork;
        public CorporateClientService(ICorporateClientRepository corporateClientRepository, IUnitOfWork unitOfWork)
        {
            this._corporateClientRepository = corporateClientRepository;
            this._UnitOfWork = unitOfWork;
        }

        //public DataTable GetAll(int? regionId, int? officeId, int? districtId, int? employeeId)
        //{
        //    try
        //    {
        //        SqlParameter[] paramsToStore = new SqlParameter[4];
        //        paramsToStore[0] = new SqlParameter("@SlsRegionId", regionId);
        //        paramsToStore[1] = new SqlParameter("@SlsOfficeId", officeId);
        //        paramsToStore[2] = new SqlParameter("@SlsDistrictId", districtId);
        //        paramsToStore[3] = new SqlParameter("@HrmEmployeeId", employeeId);

        //        DataTable dt = _corporateClientRepository.GetFromStoredProcedure(SPList.CorporateClient.GetCorporateClientById, paramsToStore);

        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public IEnumerable<SlsCorporateClient> GetAll()
        {
            return _corporateClientRepository.GetAll();
        }
        public IEnumerable<SlsCorporateClient> GetCorporateName(int companyId)
        {
            return _corporateClientRepository.GetCorporateName(companyId);
        }
        //public DataTable GetCorporateClientById(int? employeeId, int districtId)
        //{
        //    try
        //    {
        //        SqlParameter[] paramsToStore = new SqlParameter[2];
        //        paramsToStore[0] = new SqlParameter("@HrmEmployeeId", employeeId);
        //        paramsToStore[1] = new SqlParameter("@SlsDistrictId", districtId);

        //        DataTable dt = _corporateClientRepository.GetFromStoredProcedure(SPList.CorporateClient.GetCorporateClientById, paramsToStore);

        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public SlsCorporateClient GetById(int Id)
        {
            SlsCorporateClient obj = _corporateClientRepository.GetById(Id);
            return obj;
        }
        public Operation Update(SlsCorporateClient obj)
        {
            Operation objOperation = new Operation { Success = true, OperationId = obj.Id };
            _corporateClientRepository.Update(obj);

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
        public Operation Delete(SlsCorporateClient obj)
        {
            Operation objOperation = new Operation { Success = true, OperationId = obj.Id };
            _corporateClientRepository.Delete(obj);

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

        public Operation Save(SlsCorporateClient obj)
        {
            Operation objOperation = new Operation { Success = true };

            long Id = _corporateClientRepository.AddEntity(obj);
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
