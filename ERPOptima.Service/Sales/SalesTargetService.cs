using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Sales;
using ERPOptima.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Service.Sales
{
    #region interface

    public interface ISalesTargetService
    {

        IList<SlsSalesTarget> GetTargetsByYear(int companyId, int month, int year);
        IList<SlsSalesTarget> GetTargetsByYearNEmployeeId(int companyId, int month, int year, int employeeId);
        string GetRefNo(int companyId, string prefix, string offcode);
        Operation Save(SlsSalesTarget objSlsSalesTarget);
        Operation Delete(SlsSalesTarget objSlsSalesTarget);
        SlsSalesTarget GetById(int Id);
        Operation Update(SlsSalesTarget objSlsSalesTarget);
        Operation Commit();

        IList<TargetList> GetTargetList(int companyId);

    }


    #endregion
    public class SalesTargetService:ISalesTargetService
    {


        private ISalesTargetRepository _SalesTargetRepository;
        private IUnitOfWork _UnitOfWork;

        public SalesTargetService(ISalesTargetRepository salesTargetRepository, IUnitOfWork unitOfWork)
        {
            this._SalesTargetRepository = salesTargetRepository;
            this._UnitOfWork = unitOfWork;
        }


        public IList<SlsSalesTarget> GetTargetsByYear(int companyId, int month, int year)
        {

            return _SalesTargetRepository.GetTargetsByYear(companyId,month,year);


        }

        public IList<SlsSalesTarget> GetTargetsByYearNEmployeeId(int companyId, int month, int year, int employeeId)
        {

            return _SalesTargetRepository.GetTargetsByYearNEmployeeId(companyId,month,year,employeeId);


        }
        public string GetRefNo(int companyId, string prefix, string offcode)
        {
            string refno = prefix + "-" + "SLT" + "-" + offcode + "-" + DateTime.Now.ToString("yy") + "-" + DateTime.Now.ToString("MM") + "/" + _SalesTargetRepository.GetRefNo(companyId).ToString();
            return refno;
        }

        public SlsSalesTarget GetById(int Id)
        {
            SlsSalesTarget objSlsSalesTarget = _SalesTargetRepository.GetById(Id);
            return objSlsSalesTarget;
        }

        public Operation Save(SlsSalesTarget objSlsSalesTarget)
        {
            Operation objOperation = new Operation { Success = true };

            int lastId = _SalesTargetRepository.GetLastId(objSlsSalesTarget);
            objSlsSalesTarget.Id = lastId;
            objOperation.OperationId = lastId;

            _SalesTargetRepository.Add(objSlsSalesTarget);
            return objOperation;
        }
        public Operation Update(SlsSalesTarget objSlsSalesTarget)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objSlsSalesTarget.Id };
            _SalesTargetRepository.Update(objSlsSalesTarget);
            return objOperation;

        }
        
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
        public Operation Delete(SlsSalesTarget objSlsSalesTarget)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objSlsSalesTarget.Id };
            _SalesTargetRepository.Delete(objSlsSalesTarget);

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


        public IList<TargetList> GetTargetList(int companyId)
        {

            return _SalesTargetRepository.GetTargetList(companyId);


        }




    }
}
