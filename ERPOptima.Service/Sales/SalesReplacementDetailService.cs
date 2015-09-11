using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
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
   public interface ISalesReplacementDetailService
    {
        
        int GetLastId();
        IList<SlsReplacementDetail> GetByDetailId(int detailId);
        SlsReplacementDetail GetById(int Id);

        void Add(SlsReplacementDetail objSlsReplacementDetail);
        Operation Save(SlsReplacementDetail objSlsReplacementDetail);
        Operation Update(SlsReplacementDetail objSlsReplacementDetail);
        Operation Delete(SlsReplacementDetail objSlsReplacementDetail);
        Operation Commit();
        IList<SlsReplacementDetail> GetAll();
    }
   public class SalesReplacementDetailService : ISalesReplacementDetailService
   {

       private SalesReplacementDetailRepository _SalesReplacementDetailRepository;
       private IUnitOfWork _UnitOfWork;
       public SalesReplacementDetailService(SalesReplacementDetailRepository salesReplacementDetailRepository, IUnitOfWork unitOfWork)
       {
           this._SalesReplacementDetailRepository = salesReplacementDetailRepository;
           this._UnitOfWork = unitOfWork;
       }

       public IList<SlsReplacementDetail> GetAll()
       {
           var list = _SalesReplacementDetailRepository.GetAll();
           //foreach(var item in list)
           //{
           //    //new ChartOfProductService(new ChartOfProductRepository(new DatabaseFactory())).GetAll()
           //}
           return list;
       }

       public int GetLastId()
       {
           return _SalesReplacementDetailRepository.GetLastId();
       }



       public IList<SlsReplacementDetail> GetByDetailId(int detailId)
       {
           return _SalesReplacementDetailRepository.GetByDetailId(detailId);

       }

       public SlsReplacementDetail GetById(int Id)
       {
           SlsReplacementDetail objReplacementDetail = _SalesReplacementDetailRepository.GetById(Id);
           return objReplacementDetail;
       }

       public void Add(SlsReplacementDetail objSlsReplacementDetail)
       {
           _SalesReplacementDetailRepository.Add(objSlsReplacementDetail);

       }

       public Operation Update(SlsReplacementDetail objSlsReplacementDetail)
       {
           Operation objOperation = new Operation { Success = true, OperationId = objSlsReplacementDetail.Id };
           _SalesReplacementDetailRepository.Update(objSlsReplacementDetail);

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

       public Operation Delete(SlsReplacementDetail objSlsReplacementDetail)
       {
           throw new NotImplementedException();
       }
       public Operation Save(SlsReplacementDetail objSlsReplacementDetail)
       {
           Operation objOperation = new Operation { Success = true };

           long Id = _SalesReplacementDetailRepository.AddEntity(objSlsReplacementDetail);
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

   public class SlsReplacementDetailMapVMToModel
   {
       public static SlsReplacementDetail MapToSlsReplacementDetail(SlsReplacementDetailViewModel obj)
       {
           SlsReplacementDetail model = new SlsReplacementDetail();

           model.Id = obj.Id;

           model.SlsReplacementId = obj.SlsReplacementId;
           model.SlsProductId = obj.SlsProductId;
           model.Quantity = obj.Quantity;
           model.SlsUnitId = obj.SlsUnitId;

           model.AdjustedAmount = obj.AdjustedAmount;
           model.Reason = obj.Reason;

           return model;
       }


   }
   public class SlsReplacementDetailMapModelToVM
   {
       public static SlsReplacementDetailViewModel MapToSlsReplacementDetail(SlsReplacementDetail obj)
       {
           SlsReplacementDetailViewModel model = new SlsReplacementDetailViewModel();

           model.Id = obj.Id;

           model.SlsReplacementId = obj.SlsReplacementId;
           model.SlsProductId = obj.SlsProductId;
           model.Quantity = obj.Quantity;
           model.SlsUnitId = obj.SlsUnitId;

           model.AdjustedAmount = obj.AdjustedAmount;
           model.Reason = obj.Reason;

           return model;
       }


   }
}
