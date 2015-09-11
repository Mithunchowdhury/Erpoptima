using ERPOptima.Data.Hrm.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.HRM;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Service.Sales
{

    public interface IOfficeTypeService
    {
        IEnumerable<SlsOfficeType> GetAll();
        SlsOfficeType GetById(int Id);
        Operation Save(SlsOfficeType objSlsOfficeType);
        Operation Delete(SlsOfficeType objSlsOfficeType);
        Operation Update(SlsOfficeType objSlsOfficeType);
    }

    public class OfficeTypeService : IOfficeTypeService
    {
        private IOfficeTypeRepository _officeTypeRepository;
        private IUnitOfWork _unitOfWork;


        public OfficeTypeService(IOfficeTypeRepository officeTypeRepository, IUnitOfWork unitOfWork)
        {
            this._officeTypeRepository = officeTypeRepository;
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<SlsOfficeType> GetAll()
        {
            return _officeTypeRepository.GetAll();
        }
        public SlsOfficeType GetById(int Id)
        {
            SlsOfficeType objSlsOfficeType = _officeTypeRepository.GetById(Id);
            return objSlsOfficeType;
        }
        public Operation Update(SlsOfficeType objSlsOfficeType)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objSlsOfficeType.Id };
            _officeTypeRepository.Update(objSlsOfficeType);

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

        public Operation Delete(SlsOfficeType objSlsOfficeType)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objSlsOfficeType.Id };
            _officeTypeRepository.Delete(objSlsOfficeType);

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

        public Operation Save(SlsOfficeType objSlsOfficeType)
        {
            Operation objOperation = new Operation { Success = true };

            long Id = _officeTypeRepository.AddEntity(objSlsOfficeType);
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
    }
}
