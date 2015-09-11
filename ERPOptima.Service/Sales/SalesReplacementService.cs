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
   public interface ISalesReplacementService
    {
       IList<SlsReplacement> GetAll(int companyId);

       SlsReplacement GetById(int Id);
        int GetLastId();
        string GetLastCode(int companyId, string prefix, string offcode);
        SlsReplacement GetBySales(int salesId);
        Operation Save(SlsReplacementViewModel objSlsReplacement);
        Operation Update(SlsReplacementViewModel objSlsReplacementVM);
        Operation Delete(SlsReplacement objSlsReplacement);
        Operation Commit();
    }
   public class SalesReplacementService : ISalesReplacementService
   {

       private ISalesReplacementRepository _salesReplacementRepository;
       private IUnitOfWork _UnitOfWork;
       public SalesReplacementService(ISalesReplacementRepository salesReplacementRepository,

           IUnitOfWork unitOfWork)
       {
           this._salesReplacementRepository = salesReplacementRepository;

           this._UnitOfWork = unitOfWork;
       }


       public IList<SlsReplacement> GetAll(int companyId)
       {
           return _salesReplacementRepository.GetAll(companyId);
       }

       public SlsReplacement GetById(int Id)
       {
           return _salesReplacementRepository.GetById(Id);
       }

       public int GetLastId()
       {
           return _salesReplacementRepository.GetLastId();
       }

       public string GetLastCode(int companyId, string prefix, string offcode)
       {
           string RefNo = prefix + "-" + "RPL" + "-" + offcode + "-" + DateTime.Now.ToString("yy") + "-" + DateTime.Now.ToString("MM") + "/" + _salesReplacementRepository.GetLastCode(companyId).ToString();
           return RefNo;
       }


       public Operation Update(SlsReplacementViewModel objSlsReplacementVM)
       {
           SlsReplacement objSlsReplacement = SlsReplacementMapVMToModel.MapToSlsReplacement(objSlsReplacementVM);
           Operation objOperation = new Operation { Success = true, OperationId = objSlsReplacement.Id };
           _salesReplacementRepository.Update(objSlsReplacement);

           //Detail section.
           //if (objSlsReplacement.SlsReplacementDetails == null)
           //{
           //    objSlsReplacement.SlsReplacementDetails = new List<SlsReplacementDetail>();
           //}
           //if (objSlsReplacementVM.SlsReplacementDetailVMs != null && objSlsReplacementVM.SlsReplacementDetailVMs.Count() > 0)
           //{
           //    foreach (var detailVM in objSlsReplacementVM.SlsReplacementDetailVMs)
           //    {
           //        //make VMDetail from Detail model
           //        var detail = SlsReplacementDetailMapVMToModel.MapToSlsReplacementDetail(detailVM);
           //        objSlsReplacement.SlsReplacementDetails.Add(detail);
           //    }
           //}

           //if (objSlsReplacement.SlsReplacementDetails != null && objSlsReplacement.SlsReplacementDetails.Count() > 0)
           //{
           //    foreach (var detail in objSlsReplacement.SlsReplacementDetails)
           //    {
           //        new SalesReplacementDetailRepository(new DatabaseFactory()).Update(detail);
           //    }
           //}

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

       public Operation Delete(SlsReplacement objSlsReplacement)
       {
           Operation objOperation = new Operation { Success = true, OperationId = objSlsReplacement.Id };
           _salesReplacementRepository.Delete(objSlsReplacement);

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


       public Operation Save(SlsReplacementViewModel objSlsReplacementVM)
       {
           SlsReplacement objSlsReplacement = SlsReplacementMapVMToModel.MapToSlsReplacement(objSlsReplacementVM);
           Operation objOperation = new Operation { Success = true };

           int Id = _salesReplacementRepository.AddEntity(objSlsReplacement);
           objOperation.OperationId = Id;

           //Detail section.
           if (objSlsReplacement.SlsReplacementDetails == null)
           {
               objSlsReplacement.SlsReplacementDetails = new List<SlsReplacementDetail>();
           }
           if (objSlsReplacementVM.SlsReplacementDetailVMs != null && objSlsReplacementVM.SlsReplacementDetailVMs.Count() > 0)
           {
               foreach (var detailVM in objSlsReplacementVM.SlsReplacementDetailVMs)
               {
                   //make VMDetail from Detail model
                   var detail = SlsReplacementDetailMapVMToModel.MapToSlsReplacementDetail(detailVM);
                   objSlsReplacement.SlsReplacementDetails.Add(detail);
               }
           }

           if (objSlsReplacement.SlsReplacementDetails != null && objSlsReplacement.SlsReplacementDetails.Count() > 0)
           {
               new SalesReplacementDetailRepository(new DatabaseFactory()).AddEntityList(objSlsReplacement.SlsReplacementDetails.ToList());
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
       public SlsReplacement GetBySales(int salesId)
       {
           SlsReplacement objProductReceive = _salesReplacementRepository.GetById(salesId);
           return objProductReceive;
       }
   }



   public class SlsReplacementMapVMToModel
   {
       public static SlsReplacement MapToSlsReplacement(SlsReplacementViewModel obj)
       {
           SlsReplacement model = new SlsReplacement();

           model.Id = obj.Id;

           model.RefNo = obj.RefNo;
           model.Date = obj.Date;
           model.SlsSalesOrderId = obj.SlsSalesOrderId;
           model.SecCompanyId = obj.SecCompanyId;

           model.Remarks = obj.Remarks;

           model.CreatedBy = obj.CreatedBy;
           model.CreatedDate = obj.CreatedDate;
           model.ModifiedBy = obj.ModifiedBy;
           model.ModifiedDate = obj.ModifiedDate;

           return model;
       }
       

   }
   public class SlsReplacementMapModelToVM
   {
       public static SlsReplacementViewModel MapToSlsReplacement(SlsReplacement obj)
       {
           SlsReplacementViewModel model = new SlsReplacementViewModel();

           model.Id = obj.Id;

           model.RefNo = obj.RefNo;
           model.Date = obj.Date;
           model.SlsSalesOrderId = obj.SlsSalesOrderId;
           model.SecCompanyId = obj.SecCompanyId;

           model.Remarks = obj.Remarks;

           model.CreatedBy = obj.CreatedBy;
           model.CreatedDate = obj.CreatedDate;
           model.ModifiedBy = obj.ModifiedBy;
           model.ModifiedDate = obj.ModifiedDate;   

           return model;
       }
       

   }

}
