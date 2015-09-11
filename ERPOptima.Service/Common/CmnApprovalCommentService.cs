﻿using ERPOptima.Data.Accounts;
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
    public interface ICmnApprovalCommentService
    {
        Operation SaveApprovalComment(CmnApprovalComment obj);
        Collection<CmnApprovalComment> GetApprovalComments(int processId, Int64 refId);
       

    }
    #endregion


    #region Member
    public class CmnApprovalCommentService : ICmnApprovalCommentService
    {
        private ICmnApprovalCommentRepository _ICmnApprovalCommentRepository;
        private IUnitOfWork _UnitOfWork;

        public CmnApprovalCommentService(ICmnApprovalCommentRepository approvalRepository, IUnitOfWork unitOfWork)
        {
            this._ICmnApprovalCommentRepository = approvalRepository;
            this._UnitOfWork = unitOfWork;
        }

        public Operation SaveApprovalComment(CmnApprovalComment obj)
        {
            Operation objOperation = new Operation { Success = true };

            long Id = _ICmnApprovalCommentRepository.AddEntity(obj);
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

        public Collection<CmnApprovalComment> GetApprovalComments(int processId, Int64 refId)
        {
            try
            {
                Collection<CmnApprovalComment> records = null;

                DataTable dt = _ICmnApprovalCommentRepository.GetApprovalComments(processId, refId);
                if (dt != null)
                {
                    records = new Collection<CmnApprovalComment>();
                    foreach (DataRow row in dt.Rows)
                    {
                        records.Add((CmnApprovalComment)Helper.FillTo(row, typeof(CmnApprovalComment)));
                    }
                }
                return records;
            }
            catch (Exception ex)
            {
                throw ex;
                
            }
        }



       


    }

    #endregion
}