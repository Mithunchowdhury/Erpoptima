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
    #region Interface
    public interface IHrmDesignationService
    {
        IList<HrmDesignation> GetParentWithChild(int companyId);
        Operation Save(HrmDesignation HrmDesignation);
        Operation Delete(HrmDesignation objHrmDesignation);
        HrmDesignation GetById(long Id);
        Operation Update(HrmDesignation objHrmDesignation);
        DataTable GetTransactionalHeadByProjectId(int projectId, int companyId, int financialYearId);
        IEnumerable<HrmDesignation> GetAll();
    }
    #endregion


    #region Member
    public class HrmDesignationService : IHrmDesignationService
    {
        private IHrmDesignationRepository _HrmDesignationRepository;
        private IUnitOfWork _UnitOfWork;

        public HrmDesignationService(IHrmDesignationRepository hrmDesignationRepository, IUnitOfWork unitOfWork)
        {
            this._HrmDesignationRepository = hrmDesignationRepository;
            this._UnitOfWork = unitOfWork;
        }

        public IList<HrmDesignation> GetParentWithChild(int companyId)
        {
            return _HrmDesignationRepository.GetParentWithChild(companyId);
        }

        public HrmDesignation GetById(long Id)
        {
            HrmDesignation objHrmDesignation = _HrmDesignationRepository.GetById(Id);
            return objHrmDesignation;
        }

        public Operation Update(HrmDesignation objHrmDesignation)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objHrmDesignation.Id };
            _HrmDesignationRepository.Update(objHrmDesignation);

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

        public Operation Delete(HrmDesignation objHrmDesignation)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objHrmDesignation.Id };
            _HrmDesignationRepository.Delete(objHrmDesignation);

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

        public Operation Save(HrmDesignation objHrmDesignation)
        {
            Operation objOperation = new Operation { Success = true };

            long Id = _HrmDesignationRepository.AddEntity(objHrmDesignation);
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

        public DataTable GetTransactionalHeadByProjectId(int projectId, int companyId, int financialYearId)
        {
            DataTable dt = new DataTable();

            SqlParameter[] paramsToStore = new SqlParameter[3];

            paramsToStore[0] = new SqlParameter("@companyId", companyId);
            paramsToStore[1] = new SqlParameter("@projectID", projectId);
            paramsToStore[2] = new SqlParameter("@YearID", financialYearId);

            dt = _HrmDesignationRepository.GetFromStoredProcedure(SPList.Common.GetAnFTransactinalHeadForMappingByProjectId, paramsToStore);

            return dt;
        }

        public IEnumerable<HrmDesignation> GetAll()
        {
            return _HrmDesignationRepository.GetAll();
        }

    }

    #endregion
}
