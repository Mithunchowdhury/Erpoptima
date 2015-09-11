using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Model.Sales;
using ERPOptima.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ERPOptima.Service.Sales
{
    public interface IDeliveryDetailsService
    {
        IList<SlsDeliverDetailViewModel> GetAll(int companyId, int deliveryId);    
        
    }
    public class DeliveryDetailsService : IDeliveryDetailsService
    {
        private IDeliveryDetailsRepository _DeliveryDetailsRepository;
        private IChartOfProductRepository _ChartOfProductRepository;
        private IUnitOfMeasurementRepository _UnitOfMeasurementRepository;
        private IDeliveryRepository _DeliveryRepository;
        private ISalesOrderDetailRepository _salesOrderDetailRepository;
        private IUnitOfWork _UnitOfWork;
        public DeliveryDetailsService(IDeliveryDetailsRepository deliveryDetailsRepository,
            IChartOfProductRepository ChartOfProductRepository, IUnitOfMeasurementRepository UnitOfMeasurementRepository,
            IDeliveryRepository deliveryRepository, ISalesOrderDetailRepository salesOrderDetailRepository,
            IUnitOfWork unitOfWork)
        {
            this._DeliveryDetailsRepository = deliveryDetailsRepository;
            this._ChartOfProductRepository = ChartOfProductRepository;
            this._UnitOfMeasurementRepository = UnitOfMeasurementRepository;
            this._DeliveryRepository = deliveryRepository;
            this._salesOrderDetailRepository = salesOrderDetailRepository;
            this._UnitOfWork = unitOfWork;
        }

        public IList<SlsDeliverDetailViewModel> GetAll(int companyId, int deliveryId)
        {
            try
            {
                var delivery = _DeliveryRepository.GetAll().Where(i => i.Id == deliveryId).ToList().FirstOrDefault();
                var soDetaiList = _salesOrderDetailRepository.GetAll().Where(i => i.SlsSalesOrderId == delivery.SlsSalesOrderId).ToList();
                var deliveryDetailList = _DeliveryDetailsRepository.GetAll().Where(i=>i.SlsDeliveryId == deliveryId).ToList();
                var productList = _ChartOfProductRepository.GetAll(companyId);
                var unitList = _UnitOfMeasurementRepository.GetAll();

                var result = deliveryDetailList.Select(i => new SlsDeliverDetailViewModel()
                {
                    Id = i.Id,
                    SlsDeliveryId = i.SlsDeliveryId,
                    SlsProductId = i.SlsProductId,
                    Quantity = i.Quantity,
                    SlsUnitId = i.SlsUnitId,
                    Rate = i.Rate,
                    Price = i.Price,
                    Discount = i.Discount,
                    Total = i.Total,

                    SalesOrderQuantity = soDetaiList.Where(j => j.SlsProductId == i.SlsProductId && j.SlsUnitId == i.SlsUnitId).FirstOrDefault().SalesOrderQuantity,
                    SlsProductName = productList.Where(j => j.Id == i.SlsProductId).FirstOrDefault().Name,
                    SlsUnitName = unitList.Where(j => j.Id == i.SlsUnitId).FirstOrDefault().Name
                }).ToList();

                return result;
            }
            catch (Exception ex)
            {

            }


            return new List<SlsDeliverDetailViewModel>();
        }

        //public bool IsDeliveryComplete(IList<SlsDeliverDetailViewModel> detailList, int deliveryId)
        //{
        //    try
        //    {
        //        var delivery = _DeliveryRepository.GetAll().Where(i => i.Id == deliveryId).ToList().FirstOrDefault();
        //        var soDetaiList = _salesOrderDetailRepository.GetAll().Where(i => i.SlsSalesOrderId == delivery.SlsSalesOrderId).ToList();
        //        if(detailList != null && detailList.Count > 0)
        //        {
        //            foreach(SlsDeliverDetailViewModel obj in detailList)
        //            {
        //                var soObj = soDetaiList.Where(j => j.SlsProductId == obj.SlsProductId && j.SlsUnitId == obj.SlsUnitId).FirstOrDefault();
        //                if(soObj.SalesOrderQuantity > obj.Quantity)
        //                {
        //                    return false;
        //                }
        //            }
        //            return true;
        //        }
        //    }
        //    catch(Exception ex)
        //    {

        //    }

        //    return false;
        //}

    }
}
