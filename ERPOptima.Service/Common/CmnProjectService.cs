using ERPOptima.Data.Common.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using ERPOptima.Lib.Utilities;

namespace ERPOptima.Service.Common
{


    #region interface

    public interface ICmnProjectService
    {

        //IEnumerable<CmnProject> GetByBusinessId(int businessId);
        CmnProject GetById(int Id);
        Operation SaveCmnProject(CmnProject objCmnProject);
        Operation DeleteCmnProject(CmnProject objCmnProject);
        Operation UpdateCmnProject(CmnProject objCmnProject);
        IList<CmnProject> GetCmnProjects();
       

        IEnumerable<CmnProject> GetByCompanyId(int companyId, int p);
      

        DataTable GetByCompanyIdAndBusinessId(int companyId, int p1, long p2);
    }

    #endregion
    public class CmnProjectService:ICmnProjectService
    {
        private ICmnProjectRepository _CmnProjectRepository;
        private IUnitOfWork _UnitOfWork;

        public CmnProjectService(ICmnProjectRepository anfChartOfAccountRepository, IUnitOfWork unitOfWork)
        {
            this._CmnProjectRepository = anfChartOfAccountRepository;
            this._UnitOfWork = unitOfWork;
        }
        public Operation UpdateCmnProject(CmnProject objCmnProject)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objCmnProject.Id };
            _CmnProjectRepository.Update(objCmnProject);

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

        public Operation DeleteCmnProject(CmnProject objCmnProject)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objCmnProject.Id };
            _CmnProjectRepository.Delete(objCmnProject);

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

        public Operation SaveCmnProject(CmnProject objCmnProject)
        {
            Operation objOperation = new Operation { Success = true };

            long Id = _CmnProjectRepository.AddEntity(objCmnProject);
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
        public IList<CmnProject> GetCmnProjects()
        {
            return _CmnProjectRepository.GetAll().ToList();
        }
        
        //public IEnumerable<CmnProject> GetByBusinessId(int businessId)
        //{
        //    try
        //    {
        //        var projects = _CmnProjectRepository.GetMany(x => x.CmnBusinessesId == businessId).ToList();
        //        return projects;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //}


        public CmnProject GetById(int Id) {

            return _CmnProjectRepository.GetById(Id);
        
        }
        //public Operation UpdateCmnProject(CmnProject objCmnProject)
        //{
        //    Operation objOperation = new Operation { Success = true, OperationId = objCmnProject.Id };
        //    _CmnProjectRepository.Update(objCmnProject);

        //    try
        //    {
        //        _UnitOfWork.Commit();
        //    }
        //    catch (Exception ex)
        //    {

        //        objOperation.Success = false;

        //    }
        //    return objOperation;
        //}




        public IEnumerable<CmnProject> GetByCompanyId(int businessId, int p)
        {
            try
            {
                var projects = _CmnProjectRepository.GetMany(x => x.CmnBusinessesId == businessId).ToList();
                return projects;
            }
            catch (Exception)
            {

                throw;
            }
        }
       

        DataTable ICmnProjectService.GetByCompanyIdAndBusinessId(int companyId, int p1, long p2)
        {

            DataTable dt = new DataTable();
            SqlParameter[] paramsToStore = new SqlParameter[3];

            paramsToStore[0] = new SqlParameter("@companyId", companyId);
            paramsToStore[1] = new SqlParameter("@businessId", p1);
            paramsToStore[2] = new SqlParameter("@anfChartOfAccountId", p2);
            try
            {
                dt = _CmnProjectRepository.GetFromStoredProcedure(SPList.CmnProjects.GetCmnProjectsByCompanyAndBusinessIdAndChartOfAccountId, paramsToStore);

            }
            catch (Exception ex)
            {
            }
            return dt;
        }
    }
}
