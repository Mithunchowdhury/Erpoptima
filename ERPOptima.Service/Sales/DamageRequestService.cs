using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Inventory;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Service.Sales
{

    public interface IDamageRequestService 
    {
        IEnumerable<InvDamageDetailViewModel> ShowDamageProductDetails(int productId, int quantity, int unitId);
        Operation Save(InvDamageRequestViewModel obj);
        Operation Update(InvDamageRequestViewModel obj);
        IEnumerable<InvDamageRequestViewModel> GetAll();
        InvDamageRequestViewModel GetById(int Id);

        string getAutoNumber();
    }


    public class DamageRequestService : IDamageRequestService 
    {
       
        private IDamageRequestRepository _repository;
        private IUnitOfWork _unitOfWork;

        public DamageRequestService(IDamageRequestRepository repository,  IUnitOfWork unitOfWork)
        {
            this._repository = repository;
            this._unitOfWork = unitOfWork;
         
        }

        public string getAutoNumber()
        {
            return _repository.getAutoNumber();
        }


        public Operation Save(InvDamageRequestViewModel obj)
        {
            Operation objOperation = new Operation { Success = false };
             objOperation = new Operation { Success = true };

                return objOperation;
          
        }

        public Operation Update(InvDamageRequestViewModel obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<InvDamageRequestViewModel> GetAll()
        {
            return GetAllDamage(0);
        }

        public IEnumerable<InvDamageRequestViewModel> GetAllDamage(int damageId)
        {
            try
            {
                Collection<InvDamageRequestViewModel> list = null;
                SqlParameter[] paramsToStore = new SqlParameter[1];
                paramsToStore[0] = new SqlParameter("@DamageId", damageId);       
                DataTable dt = _repository.GetFromStoredProcedure(SPList.DamageProduct.DamageProductDetails, paramsToStore);

                if (dt != null)
                {
                    list = new Collection<InvDamageRequestViewModel>();
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add((InvDamageRequestViewModel)ERPOptima.Lib.Utilities.Helper.FillTo(row, typeof(InvDamageRequestViewModel)));
                    }
                }


                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public InvDamageRequestViewModel GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<InvDamageDetailViewModel> ShowDamageProductDetails(int productId, int quantity, int unitId)
        {
            throw new NotImplementedException();
        }
    }

    public class InvDamageMapVMToModel
    {
        public static InvDamage MapToInvDamage(InvDamageRequestViewModel obj)
        {
            InvDamage model = new InvDamage();

            model.Id = obj.Id;
            model.RefNo = obj.RefNo;
            model.SecCompanyId = obj.SecCompanyId;

            model.CreatedBy = obj.CreatedBy;
            model.CreatedDate = obj.CreatedDate;
            model.ModifiedBy = obj.ModifiedBy;

            model.ModifiedDate = obj.ModifiedDate;
          

            return model;
        }

     
        internal static InvDamageDetail MapToInvDamageDetail(InvDamageDetailViewModel detail)
        {
            InvDamageDetail model = new InvDamageDetail();

            return model;
        }
    }
}
