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

    public interface ICorporateIndustryTypeService
    {
        IEnumerable<SlsCorporateType> GetAll();
        SlsCorporateType GetById(int Id);
        Operation Save(SlsCorporateType objSlsCorporateType);
        Operation Delete(SlsCorporateType objSlsOfficeType);
        Operation Update(SlsCorporateType objSlsOfficeType);
    }

    public class CorporateIndustryTypeService : ICorporateIndustryTypeService
    {
        private ICorporateIndustryTypeRepository _corporateIndustryTypeRepository;
        private IUnitOfWork _unitOfWork;


        public CorporateIndustryTypeService(ICorporateIndustryTypeRepository corporateIndustryTypeRepository, IUnitOfWork unitOfWork)
        {
            this._corporateIndustryTypeRepository = corporateIndustryTypeRepository;
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<SlsCorporateType> GetAll()
        {
            return _corporateIndustryTypeRepository.GetAll();
        }
        public SlsCorporateType GetById(int Id)
        {
            SlsCorporateType objSlsCorporateType = _corporateIndustryTypeRepository.GetById(Id);
            return objSlsCorporateType;
        }
        public Operation Update(SlsCorporateType objSlsCorporateType)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objSlsCorporateType.Id };
            _corporateIndustryTypeRepository.Update(objSlsCorporateType);

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

        public Operation Delete(SlsCorporateType objSlsCorporateType)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objSlsCorporateType.Id };
            _corporateIndustryTypeRepository.Delete(objSlsCorporateType);

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

        public Operation Save(SlsCorporateType objSlsCorporateType)
        {
            Operation objOperation = new Operation { Success = true };

            long Id = _corporateIndustryTypeRepository.AddEntity(objSlsCorporateType);
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
