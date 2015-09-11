using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Inventory.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Service.Inventory
{
    #region interface

    public interface IIssueService
    {

        IList<InvIssue> GetIssueByRequisitionId(int requisitionId);
        string GetLastCode(int companyId, string prefix, string offcode);
        Operation Save(InvIssue objInvIssue);
        Operation Delete(InvIssue objInvIssue);
        InvIssue GetById(int Id);
        Operation Update(InvIssue objInvIssue);
        Operation Commit();

    }


    #endregion
    public class IssueService : IIssueService
    {

        private IIssueRepository _IssueRepository;
        private IUnitOfWork _UnitOfWork;

        public IssueService(IIssueRepository issueRepository, IUnitOfWork unitOfWork)
        {
            this._IssueRepository = issueRepository;
            this._UnitOfWork = unitOfWork;
        }


        public IList<InvIssue> GetIssueByRequisitionId(int requisitionId)
        {

            return _IssueRepository.GetIssueByRequisitionId(requisitionId);


        }
        public string GetLastCode(int companyId, string prefix, string offcode)
        {
            string code = prefix + "-" + "ISS" + "-" + offcode + "-" + DateTime.Now.ToString("yy") + "-" + DateTime.Now.ToString("MM") + "/" + _IssueRepository.GetLastCode(companyId).ToString();
            return code;
        }

        public InvIssue GetById(int Id)
        {
            InvIssue objInvIssue = _IssueRepository.GetById(Id);
            return objInvIssue;
        }
        //public Operation Save(InvIssue objInvIssue)
        //{
        //    Operation objOperation = new Operation { Success = true };

        //    long Id = _IssueRepository.AddEntity(objInvIssue);
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
        public Operation Save(InvIssue objInvIssue)
        {
            Operation objOperation = new Operation { Success = true };

            int lastId = _IssueRepository.GetLastId(objInvIssue);
            objInvIssue.Id = lastId;
            objOperation.OperationId = lastId;

            _IssueRepository.Add(objInvIssue);
            return objOperation;
        }
        public Operation Update(InvIssue objInvIssue)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objInvIssue.Id };
            _IssueRepository.Update(objInvIssue);
            return objOperation;

        }
        //public Operation Update(InvIssue objInvIssue)
        //{
        //    Operation objOperation = new Operation { Success = true, OperationId = objInvIssue.Id };
        //    _IssueRepository.Update(objInvIssue);

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
        public Operation Commit()
        {
            Operation objOperation = new Operation { Success = true };

            try
            {
                _UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                objOperation = new Operation { Success = false };

            }
            return objOperation;
        }
        public Operation Delete(InvIssue objInvIssue)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objInvIssue.Id };
            _IssueRepository.Delete(objInvIssue);

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
