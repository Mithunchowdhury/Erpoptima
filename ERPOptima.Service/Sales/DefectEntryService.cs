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
    public interface IDefectEntryService
    {
        IList<SlsDefect> GetAll(int companyId);

        SlsDefect GetById(int Id);
        int GetLastId();
        string GetLastCode(int companyId, string prefix, string offcode);

        Operation Save(SlsDefect objSlsDefect);
        Operation Update(SlsDefect objSlsDefect);
        Operation Delete(SlsDefect objSlsDefect);
        Operation Commit();

    }
    public class DefectEntryService : IDefectEntryService
    {
        private IDefectEntryRepository _DefectEntryRepository;
        private IUnitOfWork _UnitOfWork;

        public DefectEntryService(IDefectEntryRepository defectEntryRepository, IUnitOfWork unitOfWork)
        {
            this._DefectEntryRepository = defectEntryRepository;
            this._UnitOfWork = unitOfWork;
        }


        public IList<SlsDefect> GetAll(int companyId)
        {
            return _DefectEntryRepository.GetAll(companyId);
        }

        public SlsDefect GetById(int Id)
        {
            return _DefectEntryRepository.GetById(Id);
        }

        public int GetLastId()
        {
            return _DefectEntryRepository.GetLastId();
        }

        public string GetLastCode(int companyId, string prefix, string offcode)
        {
            string RefNo = prefix + "-" + "DFT" + "-" + offcode + "-" + DateTime.Now.ToString("yy") + "-" + DateTime.Now.ToString("MM") + "/" + _DefectEntryRepository.GetLastCode(companyId).ToString();
            return RefNo;
        }


        public Operation Update(SlsDefect objSlsDefect)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objSlsDefect.Id };
            _DefectEntryRepository.Update(objSlsDefect);

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

        public Operation Delete(SlsDefect objSlsDefect)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objSlsDefect.Id };
            _DefectEntryRepository.Delete(objSlsDefect);

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


        public Operation Save(SlsDefect obj)
        {

            //Operation objOperation = new Operation { Success = true };

            //int Id = _DefectEntryRepository.AddEntity(obj);
            //objOperation.OperationId = Id;

            //try
            //{
            //    _UnitOfWork.Commit();
            //}
            //catch (Exception ex)
            //{
            //    objOperation.Success = false;
            //}

            //return objOperation;
            Operation objOperation = new Operation { Success = true };
            obj.Date = DateTime.Now.Date;
            int Id = _DefectEntryRepository.AddEntity(obj);
            objOperation.OperationId = Id;

            //Detail section.
            if (obj.SlsDefectDetails != null && obj.SlsDefectDetails.Count() > 0)
            {
                new DefectDetailEntryRepository(new DatabaseFactory()).AddEntityList(obj.SlsDefectDetails.ToList());
            }



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
