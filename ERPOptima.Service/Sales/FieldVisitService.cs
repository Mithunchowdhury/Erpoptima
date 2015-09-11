using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Service.Sales
{
    

    public interface IFieldVisitService
    {
        IList<SlsFieldVisit> GetAll();
        SlsFieldVisit GetById(int Id);
        Operation Save(SlsFieldVisit record);
        Operation Delete(SlsFieldVisit record);
        Operation Update(SlsFieldVisit record);
        string getAutoNumber(string prefix, string offcode);
    }

    public class FieldVisitService : IFieldVisitService
    {
        private IFieldVisitRepository _repository;
        private IUnitOfWork _unitOfWork;


        public FieldVisitService(IFieldVisitRepository repository, IUnitOfWork unitOfWork)
        {
            this._repository = repository;
            this._unitOfWork = unitOfWork;
        }

      
        IList<SlsFieldVisit> IFieldVisitService.GetAll()
        {
            throw new NotImplementedException();
        }

        SlsFieldVisit IFieldVisitService.GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public Operation Save(SlsFieldVisit record)
        {
           
            Operation objOperation = new Operation { Success = true };

            long Id = _repository.AddEntity(record);
            objOperation.OperationId = Id;
            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                objOperation.Success = false;
            }
            return objOperation;
        }

        public Operation Delete(SlsFieldVisit record)
        {

            Operation objOperation = new Operation { Success = true, OperationId = record.Id };
            _repository.Delete(record);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {

                objOperation.Success = false;
            }
            return objOperation;
        
        }

        public Operation Update(SlsFieldVisit record)
        {
            throw new NotImplementedException();
        }

        public string getAutoNumber(string prefix, string offcode)
        {
            string refNo = prefix + "-" + "FVT" + "-" + offcode + "-" + DateTime.Now.ToString("yy") + "-" + DateTime.Now.ToString("MM") + "/" + _repository.getAutoNumber().ToString();
            return refNo;
        }
    }
}
