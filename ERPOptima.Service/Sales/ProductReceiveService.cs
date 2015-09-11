using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Service.Sales
{
    public interface IProductReceiveService
    {           
        Operation Save(InvProductReceive objInvProductReceive);
        Operation Commit();
       
        Operation Update(InvProductReceive ProductRecievedobj);
       

        InvProductReceive GetByIssue(int issueId);
       
    }

    public class ProductReceiveService : IProductReceiveService
    {
        private IProductReceiveRepository _productReceiveRepository;
        private IUnitOfWork _unitOfWork;
        public ProductReceiveService(IProductReceiveRepository productReceiveRepository, IUnitOfWork unitOfWork)
        {
            this._productReceiveRepository = productReceiveRepository;
            this._unitOfWork = unitOfWork;
        }
        

        public Operation Save(InvProductReceive objInvProductReceive)
        {
            Operation objOperation = new Operation { Success = true };

            long Id = _productReceiveRepository.AddEntity(objInvProductReceive);
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

        public Operation Update(InvProductReceive ProductRecievedobj)
        {
            Operation objOperation = new Operation { Success = true, OperationId = ProductRecievedobj.Id };
            _productReceiveRepository.Update(ProductRecievedobj);
            return objOperation;

        }
        public Operation Commit()
        {
            Operation objOperation = new Operation { Success = true };

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                objOperation = new Operation { Success = false };

            }
            return objOperation;
        }


        public InvProductReceive GetByIssue(int issueId)
        {
            InvProductReceive objProductReceive = _productReceiveRepository.GetByIssue(issueId);
            return objProductReceive;
        }
    }
}
