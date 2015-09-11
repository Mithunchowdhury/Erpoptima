using ERPOptima.Data.Accounts;
using ERPOptima.Data.Accounts.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Lib.Model;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Accounts;
using ERPOptima.Model.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace ERPOptima.Service.Accounts
{

    #region Interface
    public interface IVoucherApprovalService
    {
        //Operation SaveApprovalComment(CmnApprovalComment obj);
        //Collection<CmnApprovalComment> GetApprovalComments(int processId, Int64 refId);
        //CmnApproval GetApproval(Int64 refId, int processId, int levelId);

    }
    #endregion


    #region Member
    public class VoucherApprovalService : IVoucherApprovalService
    {
        private IAnFVoucherApprovalRepository _IAnFVoucherApprovalRepository;
        private IUnitOfWork _UnitOfWork;

        public VoucherApprovalService(IAnFVoucherApprovalRepository approvalRepository, IUnitOfWork unitOfWork)
        {
            this._IAnFVoucherApprovalRepository = approvalRepository;
            this._UnitOfWork = unitOfWork;
        }

        //public Operation SaveApprovalComment(CmnApprovalComment obj)
        //{
        //    Operation objOperation = new Operation { Success = true };

        //    long Id = _IAnFVoucherApprovalRepository.AddEntity(obj);
        //    objOperation.OperationId = Id;

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

        //public Collection<CmnApprovalComment> GetApprovalComments(int processId, Int64 refId)
        //{
        //    try
        //    {
        //        Collection<CmnApprovalComment> records = null;

        //        DataTable dt = _IAnFVoucherApprovalRepository.GetApprovalComments(processId, refId);
        //        if (dt != null)
        //        {
        //            records = new Collection<CmnApprovalComment>();
        //            foreach (DataRow row in dt.Rows)
        //            {
        //                records.Add((CmnApprovalComment)Helper.FillTo(row, typeof(CmnApprovalComment)));
        //            }
        //        }
        //        return records;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
                
        //    }
        //}

        

        //public CmnApproval GetApproval(Int64 refId, int processId, int levelId)
        //{
        //    try
        //    {
        //        CmnApproval record = null;

        //        DataTable dt = _IAnFVoucherApprovalRepository.GetApproval(refId, processId, levelId);
        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            record = new CmnApproval();
        //            record = (CmnApproval)Helper.FillTo(dt.Rows[0], typeof(CmnApproval));
        //        }
        //        return record;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public Int64 UpdatewithComment(CmnApproval approval)
        //{
        //    Int64 ret = 0;
        //    try
        //    {
        //        ret = CmnApprovalDLL.UpdateWithComment(approval);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return ret;
        //}

        //public Operation UpdatewithComment(CmnApproval approval)
        //{
        //    Operation objOperation = new Operation { Success = true, OperationId = approval.Id };
        //    _IAnFVoucherApprovalRepository.Update(approval);

        //    try
        //    {
        //        _UnitOfWork.Commit();
        //    }
        //    catch (Exception)
        //    {
        //        objOperation.Success = false;
        //    }
        //    return objOperation;
        //}


    }

    #endregion
}
