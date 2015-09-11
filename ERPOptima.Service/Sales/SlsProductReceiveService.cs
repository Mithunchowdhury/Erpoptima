using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Service.Sales
{
    public interface ISlsProductReceiveService
    {
        IList<SlsProductReceive> GetAll(int companyId);
        SlsProductReceive GetById(int Id);
        Operation Save(SlsProductReceive obj);
        Operation Delete(SlsProductReceive obj);
        Operation Update(SlsProductReceive obj); 
        SlsProductReceive GetByDelivery(int deliveryId);
        DataTable GetAllDetails(int deliveryId);

        SlsProductReceiveDetail GetDetailById(int Id);
        Operation UpdateDetail(SlsProductReceiveDetail obj);
        Operation DeleteDetail(SlsProductReceiveDetail obj);
        Operation SaveDetail(SlsProductReceiveDetail obj);

        Operation Commit();

    }
    public class SlsProductReceiveService : ISlsProductReceiveService
    {
        private ISlsProductReceiveRepository _SlsProductReceiveRepository;
        private ISlsProductReceiveDetailRepository _SlsProductReceiveDetailRepository;
        private IChartOfProductRepository _ChartOfProductRepository;
        private IUnitOfMeasurementRepository _UnitOfMeasurementRepository;
        private IUnitOfWork _UnitOfWork;

        public SlsProductReceiveService(ISlsProductReceiveRepository SlsProductReceiveRepository, ISlsProductReceiveDetailRepository SlsProductReceiveDetailRepository,
            IChartOfProductRepository ChartOfProductRepository, IUnitOfMeasurementRepository UnitOfMeasurementRepository,
            IUnitOfWork unitOfWork)
        {
            this._SlsProductReceiveRepository = SlsProductReceiveRepository;
            this._SlsProductReceiveDetailRepository = SlsProductReceiveDetailRepository;
            this._ChartOfProductRepository = ChartOfProductRepository;
            this._UnitOfMeasurementRepository = UnitOfMeasurementRepository;
            this._UnitOfWork = unitOfWork;
        }

        public IList<SlsProductReceive> GetAll(int companyId)
        {
            var freeProductList = _SlsProductReceiveRepository.GetAll(companyId);
            var productList = _ChartOfProductRepository.GetAll(companyId);
            var unitList = _UnitOfMeasurementRepository.GetAll();

            var result = freeProductList.Select(i => new SlsProductReceive() 
            {
                Id = i.Id,
                SlsDeliveryId = i.SlsDeliveryId,
                ChallanNo = i.ChallanNo,
                InvoiceNo = i.InvoiceNo,
                Remarks = i.Remarks,
                VehicleNo = i.VehicleNo,
                CreatedBy = i.CreatedBy,
                CreatedDate = i.CreatedDate,
                ModifiedBy = i.ModifiedBy,
                ModifiedDate = i.ModifiedDate
            }).ToList();



            return result;
        }

        public SlsProductReceive GetById(int Id)
        {
            SlsProductReceive obj = _SlsProductReceiveRepository.GetById(Id);
            return obj;
        }
        public Operation Update(SlsProductReceive obj)
        {
            Operation objOperation = new Operation { Success = true, OperationId = obj.Id };
            _SlsProductReceiveRepository.Update(obj);

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

        public Operation Delete(SlsProductReceive obj)
        {
            Operation objOperation = new Operation { Success = true, OperationId = obj.Id };
            _SlsProductReceiveRepository.Delete(obj);

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

        public Operation Save(SlsProductReceive obj)
        {
            Operation objOperation = new Operation { Success = true };

            long Id = _SlsProductReceiveRepository.AddEntity(obj);
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

        public SlsProductReceive GetByDelivery(int deliveryId)
        {
            SlsProductReceive objProductReceive = _SlsProductReceiveRepository.GetByDelivery(deliveryId);
            return objProductReceive;
        }
        public DataTable GetAllDetails(int deliveryId)
        {
            try
            {
                SqlParameter[] paramsToStore = new SqlParameter[1];
                paramsToStore[0] = new SqlParameter("@DeliveryId", deliveryId);

                DataTable dt = _SlsProductReceiveRepository.GetFromStoredProcedure(SPList.GetProductReceiveDetails.GetAllProductReceiveDistDetails, paramsToStore);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        #region Detail
        public SlsProductReceiveDetail GetDetailById(int Id)
        {
            SlsProductReceiveDetail obj = _SlsProductReceiveDetailRepository.GetById(Id);
            return obj;
        }
        public Operation UpdateDetail(SlsProductReceiveDetail obj)
        {
            Operation objOperation = new Operation { Success = true, OperationId = obj.Id };
            _SlsProductReceiveDetailRepository.Update(obj);

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

        public Operation DeleteDetail(SlsProductReceiveDetail obj)
        {
            Operation objOperation = new Operation { Success = true, OperationId = obj.Id };
            _SlsProductReceiveDetailRepository.Delete(obj);

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

        public Operation SaveDetail(SlsProductReceiveDetail obj)
        {
            Operation objOperation = new Operation { Success = true };

            long Id = _SlsProductReceiveDetailRepository.AddEntity(obj);
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
        #endregion
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
