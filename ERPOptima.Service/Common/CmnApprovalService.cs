using ERPOptima.Data.Common.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Lib.Model;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace ERPOptima.Service.Common
{
    public interface ICmnApprovalService
    {
        int GetNoOfUnposted(string sqlText);

        void AutoGenerate(int _companyId, int p, int _userid, long _refId);

        long GetLastId();

        int CheckCount(int _companyId, int p, long processLevelId, long _refId);

        CmnApproval GetApproval(Int64 refId, int processId, int levelId);

        Operation UpdatewithComment(CmnApproval apr);
    }

    public class CmnApprovalService : ICmnApprovalService
    {
        private ICmnApprovalRepository _CmnApprovalRepository;
        private ICmnProcessLevelRepository _CmnProcessLevelRpository;
        private IUnitOfWork _UnitOfWork;

        public CmnApprovalService(ICmnApprovalRepository cmnApprovalRepository, ICmnProcessLevelRepository repo, IUnitOfWork unitOfWork)
        {
            this._CmnApprovalRepository = cmnApprovalRepository;
            this._CmnProcessLevelRpository = repo;
            this._UnitOfWork = unitOfWork;
        }

        public CmnApprovalService(ICmnApprovalRepository cmnApprovalRepository, IUnitOfWork unitOfWork)
        {
            this._CmnApprovalRepository = cmnApprovalRepository;
            this._UnitOfWork = unitOfWork;
        }

        public int GetNoOfUnposted(string sqlText)
        {
            DataTable dt = _CmnApprovalRepository.GetFromStoredProcedure(sqlText, null, false);
            return Convert.ToInt32(dt.Rows[0][0]);
        }

        public long GetLastId()
        {
            return _CmnApprovalRepository.GetLastId();
        }

        public void AutoGenerate(int _companyId, int p, int _userid, long _refId)
        {
            try
            {
                Collection<CmnProcessLevel> records = null;

                try
                {
                    CmnProcessLevel processlevel = null;

                    SqlParameter[] paramsToStore = new SqlParameter[2];

                    paramsToStore[0] = new SqlParameter("@cmnCompanyId", _companyId);
                    paramsToStore[1] = new SqlParameter("@approvalProcessId", p);


                    DataTable dt = _CmnProcessLevelRpository.GetFromStoredProcedure(SPList.CmnProcessLevel.GetCmnProcessLevelsByApprovalProcessId, paramsToStore);
                    if (dt != null)
                    {
                        records = new Collection<CmnProcessLevel>();
                        foreach (DataRow row in dt.Rows)
                        {
                            processlevel = new CmnProcessLevel();
                            records.Add((CmnProcessLevel)Helper.FillTo(row, typeof(CmnProcessLevel)));
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                long _Id = _CmnApprovalRepository.GetLastId();

                foreach (CmnProcessLevel record in records)
                {
                    try
                    {
                        int count = 0;

                      //  count = CheckCount(_companyId, (int)p, _Id, (int)_refId);
                        count = CheckCount(_companyId, p, record.Id, _refId);
                        
                        if (count == 0)
                        {
                            CmnApproval ap = new CmnApproval();
                            ap.Id = _Id;
                            ap.CmnApprovalProcessId = (int)p;
                            ap.RefId = _refId;
                            ap.CmnCompanyId = _companyId;
                            ap.DoneBy = null;
                            ap.DoneDateTime = null;
                            ap.CmnProcessLevelId = (int)record.Id;

                            ap.Value = false;
                            _Id++;
                            long id = _CmnApprovalRepository.AddEntity(ap);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int CheckCount(int _companyId, int p, long processLevelId, long _refId)
        {
            return _CmnApprovalRepository.GetCountByParameters(_companyId, p, processLevelId, _refId);
        }

        public CmnApproval GetApproval(Int64 refId, int processId, int levelId)
        {
            try
            {
                CmnApproval record = null;

                DataTable dt = _CmnApprovalRepository.GetApproval(refId, processId, levelId);
                if (dt != null && dt.Rows.Count > 0)
                {
                    record = new CmnApproval();
                    record = (CmnApproval)Helper.FillTo(dt.Rows[0], typeof(CmnApproval));
                }
                return record;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public Operation UpdatewithComment(CmnApproval approval)
        {
            Operation objOperation = new Operation { Success = true, OperationId = approval.Id };
            _CmnApprovalRepository.Update(approval);

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

   
    }

    //New Interface - Used in ERPOptima MEP Sales
    public interface IApprovalService<TObj, TObjVM>
    {
        IEnumerable<TObjVM> GetAllForApproval(int userId);
        TObj GetApprovalById(int salesOrderId);
        Operation UpdateApproval(TObj obj, string newComment);
        IEnumerable<TObjVM> GetAllForApprovalByDate(DateTime fromDate, DateTime toDate, int userId);
    }
}