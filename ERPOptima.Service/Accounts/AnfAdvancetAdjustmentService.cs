using ERPOptima.Data.Accounts.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Lib.Model;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Accounts;
//using Optima.Areas.Accounts.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Service.Accounts
{
    #region interface
    public interface IAnfAdvancetAdjustmentService
    {
        IList<AnFAdjustment> GetAll(int companyId, int financialYearId);
        IList<AnFAdjustment> AdjustmentListSearch(int AnFAdvanceId);
        IList<AnFAdjustment> AdjustmentList();
        Operation Save(AnFAdjustment objadjustment);
        Operation Update(AnFAdjustment objadjustment);
        AnFAdjustment GetById(int Id);
        Operation Delete(AnFAdjustment objadjustment);

        IList<AnFAdvance> GetRefNoByEmpId(int AnFAdvanceId);  //Add by Bably
    }
    #endregion interface
    public class AnfAdvancetAdjustmentService : IAnfAdvancetAdjustmentService
    {
        private IAnfAdvancetListRepository _anfAdvancetListRepository;
        private IAnfAdvanceAdjustmentRepository _AnfAdvanceAdjustmentRepository;
        private IUnitOfWork _UnitOfWork;
        public AnfAdvancetAdjustmentService(IAnfAdvanceAdjustmentRepository anfAnfAdvanceAdjustmentRepository, IUnitOfWork unitOfWork)
        {
            var dbfactory = new DatabaseFactory(); //Add by me

            this._AnfAdvanceAdjustmentRepository = anfAnfAdvanceAdjustmentRepository;
            this._UnitOfWork = unitOfWork;
            _anfAdvancetListRepository = new AnfAdvancetListepository(dbfactory); // add  by me
        }


        public IList<AnFAdjustment> GetAll(int companyId, int financialYearId)
        {
            IList<AnFAdjustment> list = new List<AnFAdjustment>();
            return list;
        }


        //AdvanceAdjustmentReport added by Bably
        public IList<AnFAdvance> GetRefNoByEmpId(int EmplyeeID)
        {
            //IList<AnFAdjustment> list = _AnfAdvanceAdjustmentRepository.GetAll().ToList();
            IList<AnFAdvance> list = new List<AnFAdvance>();
            var innerlist = _anfAdvancetListRepository.GetAll();
            list = innerlist.Where(t => t.HrmEmployeeId == EmplyeeID).ToList();
            return list;
        }

        /*---------------------For  Search---------------------*/
        public IList<AnFAdjustment> AdjustmentListSearch(int AnFAdvanceId)
        {
            IList<AnFAdjustment> list = _AnfAdvanceAdjustmentRepository.GetAllByAdvanceId(AnFAdvanceId).ToList();
            return list;
        }
        /*---------------------For Search end---------------------*/
        public IList<AnFAdjustment> AdjustmentList()
        {
            IList<AnFAdjustment> list = _AnfAdvanceAdjustmentRepository.GetAllWithoutId().ToList();
            return list;
        }
        public Operation Save(AnFAdjustment objadjustment)
        {
            Operation objOperation = new Operation { Success = true, Message = "Saved successfully." };

            int Id = _AnfAdvanceAdjustmentRepository.AddEntity(objadjustment);
            objOperation.OperationId = Id;

            try
            {
                _UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                objOperation.Success = false;
                objOperation.Message = "Save not successful.";
            }
            return objOperation;
        }

        public Operation Update(AnFAdjustment objadjustment)
        {
            Operation objOperation = new Operation { Success = true, Message = "Update successfully." };
            _AnfAdvanceAdjustmentRepository.Update(objadjustment);

            try
            {
                _UnitOfWork.Commit();
            }
            catch (Exception)
            {
                objOperation.Success = false;
                objOperation.Message = "Update not successful.";
            }
            return objOperation;
        }
        public AnFAdjustment GetById(int Id)
        {

            return _AnfAdvanceAdjustmentRepository.GetById(Id);
        }
        public Operation Delete(AnFAdjustment objadjustment)
        {
            Operation objOperation = new Operation { Success = true, Message = "Deleted successfully." };
            _AnfAdvanceAdjustmentRepository.Delete(objadjustment);

            try
            {
                _UnitOfWork.Commit();
            }
            catch (Exception)
            {

                objOperation.Success = false;
                objOperation.Message = "Delete not successful.";
            }
            return objOperation;
        }
    }
}
