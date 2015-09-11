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
  public  interface ISalesDiscountSettingService
    {
      IEnumerable<SlsDiscountSetting> GetAll();

      SlsDiscountSetting GetById(int Id);

      Operation Save(SlsDiscountSetting obj);

      Operation Delete(SlsDiscountSetting obj);

      Operation Update(SlsDiscountSetting obj);
    }
  public class SalesDiscountSettingService : ISalesDiscountSettingService
  {
      private ISalesDiscountSettingRepository _salesDiscountSettingRepository;
      private IUnitOfWork _unitOfWork;
  
      private UnitOfWork unit;


      public SalesDiscountSettingService(ISalesDiscountSettingRepository salesDiscountSettingRepository, IUnitOfWork unitOfWork)
      {
          this._salesDiscountSettingRepository = salesDiscountSettingRepository;
          this._unitOfWork = unitOfWork;
      }

     

      public IEnumerable<SlsDiscountSetting> GetAll()
      {
          try
          {
              return _salesDiscountSettingRepository.GetAll();
          }
          catch (Exception ex)
          {
              return null;
          }
      }
      public SlsDiscountSetting GetById(int Id)
      {
          SlsDiscountSetting obj = _salesDiscountSettingRepository.GetById(Id);
          return obj;
      }
      public Operation Update(SlsDiscountSetting obj)
      {
          Operation objOperation = new Operation { Success = true, OperationId = obj.Id };
          _salesDiscountSettingRepository.Update(obj);

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

      public Operation Delete(SlsDiscountSetting obj)
      {
          Operation objOperation = new Operation { Success = true, OperationId = obj.Id };
          _salesDiscountSettingRepository.Delete(obj);

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


      public Operation Save(SlsDiscountSetting obj)
      {
          Operation objOperation = new Operation { Success = true };

          int Id = _salesDiscountSettingRepository.AddEntity(obj);
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
