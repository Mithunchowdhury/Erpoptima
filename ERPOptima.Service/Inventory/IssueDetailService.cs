using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Inventory.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Inventory;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Service.Inventory
{
    #region interface

    public interface IIssueDetailService
    {

        IList<InvIssueDetails> GetIssueDetailByIssueId(int requisitionId,int issueId);

        Operation Save(InvIssueDetail objInvIssueDetail);
        Operation Delete(InvIssueDetail objInvIssueDetail);
        InvIssueDetail GetById(int Id);
        Operation Update(InvIssueDetail objInvIssueDetail);


    }


    #endregion
    public class IssueDetailService : IIssueDetailService
    {

        private IIssueDetailRepository _IssueDetailRepository;
        private IUnitOfWork _UnitOfWork;

        public IssueDetailService(IIssueDetailRepository isueDetailRepository, IUnitOfWork unitOfWork)
        {
            this._IssueDetailRepository = isueDetailRepository;
            this._UnitOfWork = unitOfWork;
        }

        public IList<InvIssueDetails> GetIssueDetailByIssueId(int requisitionId,int issueId)
        {
            
            //return _IssueDetailRepository.GetIssueDetailByIssueId(issueId);


            IList<InvIssueDetails> issueDets = null;           

            SqlParameter[] paramsToStore = new SqlParameter[2];

            paramsToStore[0] = new SqlParameter("@RequisitionId", requisitionId);
            paramsToStore[1] = new SqlParameter("@IssueId", issueId);

            DataTable dt = _IssueDetailRepository.GetFromStoredProcedure(SPList.InvIssueDetail.GetIssueDetailByIssueId, paramsToStore);
            
            if (dt != null)
            {
                issueDets = new Collection<InvIssueDetails>();
                foreach (DataRow row in dt.Rows)
                {
                    issueDets.Add((InvIssueDetails)ERPOptima.Lib.Utilities.Helper.FillTo(row, typeof(InvIssueDetails)));
                }
            }
            return issueDets;


        }
        public InvIssueDetail GetById(int Id)
        {
            InvIssueDetail objInvIssueDetail = _IssueDetailRepository.GetById(Id);
            return objInvIssueDetail;
        }
        public Operation Save(InvIssueDetail objInvIssueDetail)
        {
            Operation objOperation = new Operation { Success = true };

            long Id = _IssueDetailRepository.AddEntity(objInvIssueDetail);
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
        public Operation Update(InvIssueDetail objInvIssueDetail)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objInvIssueDetail.Id };
            _IssueDetailRepository.Update(objInvIssueDetail);

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
        public Operation Delete(InvIssueDetail objInvIssueDetail)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objInvIssueDetail.Id };
            _IssueDetailRepository.Delete(objInvIssueDetail);

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
}
