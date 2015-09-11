using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Service.Sales
{
    public interface IDefectDetailEntryService
    {
        int GetLastId();
        IList<SlsDefectDetail> GetByDefectId(int defectId);
        SlsDefectDetail GetById(int Id);
        Operation Save(SlsDefectDetail objSlsDefectDetail);
        Operation Update(SlsDefectDetail objSlsDefectDetail);        
        Operation Commit();
    }
    public class DefectDetailEntryService : IDefectDetailEntryService
    {

        private IDefectDetailEntryRepository _DefectDetailEntryRepository;
        private IUnitOfWork _UnitOfWork;
        public DefectDetailEntryService(DefectDetailEntryRepository defectDetailEntryRepository, IUnitOfWork unitOfWork)
        {
            this._DefectDetailEntryRepository = defectDetailEntryRepository;
            this._UnitOfWork = unitOfWork;
        }


        public int GetLastId()
        {
            return _DefectDetailEntryRepository.GetLastId();
        }



        public IList<SlsDefectDetail> GetByDefectId(int defectId)
        {
            return _DefectDetailEntryRepository.GetByDefectId(defectId);

        }

        public SlsDefectDetail GetById(int Id)
        {
            SlsDefectDetail objDefectDetail = _DefectDetailEntryRepository.GetById(Id);
            return objDefectDetail;
        }


        public Operation Update(SlsDefectDetail objSlsDefectDetail)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objSlsDefectDetail.Id };
            _DefectDetailEntryRepository.Update(objSlsDefectDetail);

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
                
        public Operation Save(SlsDefectDetail objSlsDefectDetail)
        {
            Operation objOperation = new Operation { Success = true };

            long Id = _DefectDetailEntryRepository.AddEntity(objSlsDefectDetail);
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
    }
}
