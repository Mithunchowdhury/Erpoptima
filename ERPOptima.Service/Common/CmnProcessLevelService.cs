using ERPOptima.Data.Common.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Service.Common
{

    #region interface



    public interface ICmnProcessLevelService
    {


        IList<CmnProcessLevel> GetProcessLevel();

        Operation Save(CmnProcessLevel objCmnProcessLevel);
        Operation Update(CmnProcessLevel objCmnProcessLevel);
        Operation Delete(CmnProcessLevel objCmnProcessLevel);
        CmnProcessLevel GetById(int Id);
      

    }


    #endregion
    public class CmnProcessLevelService : ICmnProcessLevelService
    {
        private ICmnProcessLevelRepository _CmnProcessLevelRepository;
        private IUnitOfWork _UnitOfWork;

        public CmnProcessLevelService(ICmnProcessLevelRepository cmnProcessLevelRepository, IUnitOfWork unitOfWork)
        {
            this._CmnProcessLevelRepository = cmnProcessLevelRepository;
            this._UnitOfWork = unitOfWork;
        }

        public IList<CmnProcessLevel> GetProcessLevel()
        {
            return _CmnProcessLevelRepository.GetProcessLevel();
        }

        public CmnProcessLevel GetById(int Id)
        {

            CmnProcessLevel objCmnProcessLevel = _CmnProcessLevelRepository.GetById(Id);
            return objCmnProcessLevel;

        }
        public Operation Save(CmnProcessLevel objCmnProcessLevel)
        {
            Operation objOperation = new Operation { Success = true };

            int Id = _CmnProcessLevelRepository.AddEntity(objCmnProcessLevel);
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

        public Operation Update(CmnProcessLevel objCmnProcessLevel)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objCmnProcessLevel.Id };
            _CmnProcessLevelRepository.Update(objCmnProcessLevel);

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



        public Operation Delete(CmnProcessLevel objCmnProcessLevel)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objCmnProcessLevel.Id };
            _CmnProcessLevelRepository.Delete(objCmnProcessLevel);

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
