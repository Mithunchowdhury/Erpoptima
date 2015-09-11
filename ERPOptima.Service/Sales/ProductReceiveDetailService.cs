using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Inventory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Service.Sales
{
   
    #region interface

    public interface IProductReceiveDetailService
    {
               
        DataTable GetAll(int issueId);
        Operation Save(InvProductReceiveDetail objInvProductReceiveDetail);       
        InvProductReceiveDetail GetById(int Id);
        Operation Update(InvProductReceiveDetail objInvProductReceiveDetail);
        InvProductReceiveDetail GetAllByPrRecId(int receiveId);
        
    }


    #endregion
    public class ProductReceiveDetailService : IProductReceiveDetailService
    {

       private IProductReceiveDetailRepository _ProductReceiveDetailRepository;
       private IUnitOfWork _UnitOfWork;

       public ProductReceiveDetailService(IProductReceiveDetailRepository productReceiveDetailRepository, IUnitOfWork unitOfWork)
        {
            this._ProductReceiveDetailRepository = productReceiveDetailRepository;
            this._UnitOfWork = unitOfWork;
       }

       public DataTable GetAll(int issueId)
       {

           //return _ProductReceiveDetailRepository.GetAll();
           try
           {
               SqlParameter[] paramsToStore = new SqlParameter[1];
               paramsToStore[0] = new SqlParameter("@IssueId", issueId);

               DataTable dt = _ProductReceiveDetailRepository.GetFromStoredProcedure(SPList.GetProductReceiveDetails.GetAllProductReceiveDetails, paramsToStore);

               return dt;
           }
           catch (Exception ex)
           {
               throw ex;
           }


       }
       public InvProductReceiveDetail GetAllByPrRecId(int receiveId)
       {
           InvProductReceiveDetail objProductReceiveDetail = _ProductReceiveDetailRepository.GetAllByPrRecId(receiveId);
           return objProductReceiveDetail;
       }
       public InvProductReceiveDetail GetById(int Id)
       {
           InvProductReceiveDetail objProductReceiveDetail = _ProductReceiveDetailRepository.GetById(Id);
           return objProductReceiveDetail;
       }
       public Operation Save(InvProductReceiveDetail objProductReceiveDetail)
       {
           Operation objOperation = new Operation { Success = true };

           long Id = _ProductReceiveDetailRepository.AddEntity(objProductReceiveDetail);
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
       public Operation Update(InvProductReceiveDetail objProductReceiveDetail)
       {
           Operation objOperation = new Operation { Success = true, OperationId = objProductReceiveDetail.Id };
           _ProductReceiveDetailRepository.Update(objProductReceiveDetail);

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
       


    }

 }

