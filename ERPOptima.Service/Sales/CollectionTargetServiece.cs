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
    public interface ICollectionTargetService
    {
        IList<SlsCollectionTarget> GetAll(int companyId);
        DataTable GetAll(int companyId, int monthId, int yearId, int employeeId);
        //SlsCollectionTargets GetById(int Id);
        Operation Save(SlsCollectionTarget objSlsCollectionTarget);
        // Operation Delete(SlsProductPrice objSlsProductPrice);
        Operation Update(SlsCollectionTarget objSlsCollectionTarget);
    }
    public class CollectionTargetService : ICollectionTargetService
    {
        private ICollectionTargetRepository _collectionTargetRepository;
        private IUnitOfWork _unitOfWork;

        public CollectionTargetService(ICollectionTargetRepository collectionTargetRepository, IUnitOfWork unitOfWork)
        {
            this._collectionTargetRepository = collectionTargetRepository;
            this._unitOfWork = unitOfWork;
        }
        public IList<SlsCollectionTarget> GetAll(int companyId)
        {
            IList<SlsCollectionTarget> list = new List<SlsCollectionTarget>();
            try
            {
                list = _collectionTargetRepository.GetAll().ToList(); ;
                list = list.Where(i => i.SecCompanyId == companyId).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }
        public DataTable GetAll(int companyId, int monthId, int yearId, int employeeId)
        {
            try
            {
                SqlParameter[] paramsToStore = new SqlParameter[4];
                paramsToStore[0] = new SqlParameter("@CompanyId", companyId);
                paramsToStore[1] = new SqlParameter("@MonthId", monthId);
                paramsToStore[2] = new SqlParameter("@YearId", yearId);
                paramsToStore[3] = new SqlParameter("@EmployeeId", employeeId);
                DataTable dt = _collectionTargetRepository.GetFromStoredProcedure(SPList.CollectionTarget.GetCollectionTarget, paramsToStore);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Operation Save(SlsCollectionTarget objSlsCollectionTarget)
        {
            Operation objOperation = new Operation { Success = true };

            long Id = _collectionTargetRepository.AddEntity(objSlsCollectionTarget);
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
        public Operation Update(SlsCollectionTarget objSlsCollectionTarget)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objSlsCollectionTarget.Id };
            _collectionTargetRepository.Update(objSlsCollectionTarget);

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
    }
}
