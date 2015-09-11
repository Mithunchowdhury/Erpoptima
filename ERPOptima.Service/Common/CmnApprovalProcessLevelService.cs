using ERPOptima.Data.Common.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Lib.Model;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Service.Common
{
    public interface ICmnApprovalProcessLevelService
    {
        IList<CmnApprovalProcessLevel> GetByAPId(int Id);
        DataTable GetByCompanyModuleApprovalProcessId(int companyId, int approvalProcessId);
        CmnApprovalProcessLevel GetById(int Id);
        int GetLastId();
        void Save(CmnApprovalProcessLevel cmnApprovalProcessLevel);
        void Update(CmnApprovalProcessLevel cmnApprovalProcessLevel);

        void Delete(CmnApprovalProcessLevel cmnApprovalProcessLevel);
        Operation Commit();


    }

    public class CmnApprovalProcessLevelService : ICmnApprovalProcessLevelService
    {


        private ICmnApprovalProcessLevelRepository _CmnApprovalProcessLevelRepository;
        private IUnitOfWork _UnitOfWork;

        public CmnApprovalProcessLevelService(ICmnApprovalProcessLevelRepository cmnApprovalProcessLevelRepository, IUnitOfWork unitOfWork)
        {
            this._CmnApprovalProcessLevelRepository = cmnApprovalProcessLevelRepository;
            this._UnitOfWork = unitOfWork;
        }


        public CmnApprovalProcessLevel GetById(int Id) 
        {
            return _CmnApprovalProcessLevelRepository.GetById(Id);
        }
        //parameter ApprovalProcessId

        public IList<CmnApprovalProcessLevel> GetByAPId(int Id)
        {
            return _CmnApprovalProcessLevelRepository.GetManyWithInclude(apl => apl.CmnApprovalProcessId == Id, "CmnProcessLevel").ToList();
        }


        public int GetLastId()
        {

            return _CmnApprovalProcessLevelRepository.GetLastId();
        
        }

        public void Save(CmnApprovalProcessLevel cmnApprovalProcessLevel)
        {

            _CmnApprovalProcessLevelRepository.Add(cmnApprovalProcessLevel);
        
        }

        public void Update(CmnApprovalProcessLevel cmnApprovalProcessLevel)
        {

            _CmnApprovalProcessLevelRepository.Update(cmnApprovalProcessLevel);
        
        }
        public void Delete(CmnApprovalProcessLevel cmnApprovalProcessLevel)
        {

            _CmnApprovalProcessLevelRepository.Delete(cmnApprovalProcessLevel);
        
        }

        public DataTable GetByCompanyModuleApprovalProcessId(int companyId, int approvalProcessId)
        { 
        
        
            DataTable dt = new DataTable();

            SqlParameter[] paramsToStore = new SqlParameter[2];
            paramsToStore[0] = new SqlParameter("@companyId", companyId);
            paramsToStore[1] = new SqlParameter("@approvalProcessId", approvalProcessId);
            try
            {
                dt = _CmnApprovalProcessLevelRepository.GetFromStoredProcedure(SPList.CmnApprovalProcessLevels.GetCmnApprovalProcessLevelMappingByCompanyIdAndModuleId, paramsToStore);
            }
            catch (Exception ex)
            {
                dt = null;
            }

            return dt;
        
        
        
        }

        public Operation Commit()
        {
            Operation operation = new Operation { Success = true };
            
            try
            {
                _UnitOfWork.Commit();
            }
            catch (Exception ex)
            {

                operation.Success = false;
            }

            return operation;
        }
    }
}
