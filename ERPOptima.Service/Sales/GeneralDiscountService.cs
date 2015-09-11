using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Sales;
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
    public interface IGeneralDiscountService
    {
        Operation Save(SlsGeneralDiscount obj);
        Operation Update(SlsGeneralDiscount obj);
        Operation Delete(SlsGeneralDiscount obj);
        SlsGeneralDiscount GetById(int Id);
        IEnumerable<SlsGeneralDiscountViewModel> GetAll(int regionId=0);
    }
    public class GeneralDiscountService : IGeneralDiscountService
    {
        private IGeneralDiscountRepository _Repository;
        private IUnitOfWork _unitOfWork;


        public GeneralDiscountService(IGeneralDiscountRepository repository, IUnitOfWork unitOfWork)
        {
            this._Repository = repository;
            this._unitOfWork = unitOfWork;
        }
        public IEnumerable<SlsGeneralDiscountViewModel> GetAll(int regionId=0)
        {
            try
            {
                Collection<SlsGeneralDiscountViewModel> list = null;
                SqlParameter[] paramsToStore = new SqlParameter[1];
                paramsToStore[0] = new SqlParameter("@SlsRegionId", regionId);

                DataTable dt = _Repository.GetFromStoredProcedure(SPList.GeneralDiscount.GetSlsGeneralDiscounts, paramsToStore);

                if (dt != null)
                {
                    list = new Collection<SlsGeneralDiscountViewModel>();
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add((SlsGeneralDiscountViewModel)ERPOptima.Lib.Utilities.Helper.FillTo(row, typeof(SlsGeneralDiscountViewModel)));
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public Operation Save(SlsGeneralDiscount obj)
       {
           Operation objOperation = new Operation { Success = true };

           int Id = _Repository.AddEntity(obj);
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
        public Operation Update(SlsGeneralDiscount obj)
        {
            Operation objOperation = new Operation { Success = true, OperationId = obj.Id };
            _Repository.Update(obj);

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
        public Operation Delete(SlsGeneralDiscount obj)
        {
            Operation objOperation = new Operation { Success = true, OperationId = obj.Id };
            _Repository.Delete(obj);

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
        public SlsGeneralDiscount GetById(int Id)
        {
            SlsGeneralDiscount obj = _Repository.GetById(Id);
            return obj;
        }
    }
}
