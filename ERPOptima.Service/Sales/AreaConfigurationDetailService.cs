using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Service.Sales
{

    public interface IAreaConfigurationDetailService
    {
        IEnumerable<SlsAreaConfigurationDetail> GetAll();
        Operation Save(Collection<SlsAreaConfigurationDetail> obj,int configId);
    }

    public class AreaConfigurationDetailService : IAreaConfigurationDetailService
    {
        private IAreaConfigurationDetailRepository _areaRepository;
        private IUnitOfWork _unitOfWork;


        public AreaConfigurationDetailService(IAreaConfigurationDetailRepository areaRepository, IUnitOfWork unitOfWork)
        {
            this._areaRepository = areaRepository;
            this._unitOfWork = unitOfWork;
        }
        public IEnumerable<SlsAreaConfigurationDetail> GetAll()
        {
            return _areaRepository.GetAll();
        }

        public Operation Save(Collection<SlsAreaConfigurationDetail> objDetails, int configId)
        {
            Operation objOperation = new Operation { Success = true };
            foreach (SlsAreaConfigurationDetail obj in objDetails)
            {
                obj.SlsAreaConfigurationId = configId;
                int Id = _areaRepository.AddEntity(obj);
                objOperation.OperationId = Id;

                try
                {
                    _unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    objOperation.Success = false;
                }
            }
            return objOperation;
        }
    }
}
