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
    public interface ICommissionService
    {
        IList<SlsCommission> GetAll();
        Operation Save(SlsCommissionViewModel vmobj);
        Operation Delete(int Id);
        Operation Update(SlsCommissionViewModel vmobj);
        IList<SlsCommissionViewModel> GetAllVM();
    }

    public class CommissionService : ICommissionService
    {
        private ICommissionRepository _CommissionRepository;
        private IUnitOfWork _UnitOfWork;

        public CommissionService(ICommissionRepository commissionRepository, IUnitOfWork unitOfWork)
        {
            this._CommissionRepository = commissionRepository;
            this._UnitOfWork = unitOfWork;
        }
        public IList<SlsCommission> GetAll()
        {
            try
            {
                return _CommissionRepository.GetAll().ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IList<SlsCommissionViewModel> GetAllVM()
        {
            try
            {
                IList<SlsCommissionViewModel> result = new List<SlsCommissionViewModel>();
                var list = _CommissionRepository.GetAll().ToList();
                foreach(var item in list)
                {
                    SlsCommissionViewModel obj = SlsCommissionMapModelToVM.MapToSlsCommission(item);
                    result.Add(obj);
                }
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Operation Update(SlsCommissionViewModel vmobj)
        {
            SlsCommission obj = new SlsCommission();
            obj = SlsCommissionMapVMToModel.MapToSlsCommission(vmobj);
            Operation objOperation = new Operation { Success = true, OperationId = obj.Id };

            try
            {
                _CommissionRepository.Update(obj);
                _UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                objOperation.Success = false;

            }
            return objOperation;
        }

        public Operation Delete(int Id)
        {
            Operation objOperation = new Operation { Success = true, OperationId = Id };

            try
            {
                SlsCommission objD = _CommissionRepository.GetById(Id);
                _CommissionRepository.Delete(objD);
                _UnitOfWork.Commit();
            }
            catch (Exception ex)
            {

                objOperation.Success = false;
            }
            return objOperation;
        }

        public Operation Save(SlsCommissionViewModel vmobj)
        {
            SlsCommission obj = new SlsCommission();
            Operation objOperation = new Operation { Success = true };

            try
            {
                obj = SlsCommissionMapVMToModel.MapToSlsCommission(vmobj);
                int newId = 1;
                try
                {
                    newId = _CommissionRepository.GetTableContext().Max(i => i.Id) + 1;
                }
                catch (Exception ex)
                {

                }
                obj.Id = newId;
                objOperation.OperationId = newId;
                _CommissionRepository.Add(obj);

                _UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                objOperation.Success = false;
            }
            return objOperation;
        }

       

    }



    public class SlsCommissionMapVMToModel
    {
        public static SlsCommission MapToSlsCommission(SlsCommissionViewModel obj)
        {
            SlsCommission model = new SlsCommission();

            model.Id = obj.Id;
                        
            model.NetSaleAmount = obj.NetSaleAmount;
            model.CommissionPercentage = obj.CommissionPercentage;
            model.Commission = obj.Commission;
            model.Date = obj.Date;
            model.ChequeNo = obj.ChequeNo;
            model.Bank = obj.Bank;
            model.Remarks = obj.Remarks;

            model.MonthFrom = obj.From.Month;
            model.MonthTo = obj.To.Month;
            model.YearFrom = obj.From.Year;
            model.YearTo = obj.To.Year;
            //1=Distributor, 3=Dealer - as per DB column description
            if(obj.PartyType == 1)
            {
                model.SlsDitributorId = obj.Party;
                model.SlsDealerId = null;
            }
            else if (obj.PartyType == 3)
            {
                model.SlsDealerId = obj.Party;
                model.SlsDitributorId = null;
            }

            model.CreatedBy = obj.CreatedBy;
            model.CreatedDate = obj.CreatedDate;
            model.ModifiedBy = obj.ModifiedBy;
            model.ModifiedDate = obj.ModifiedDate;

            return model;
        }       

    }
    public class SlsCommissionMapModelToVM
    {
        public static SlsCommissionViewModel MapToSlsCommission(SlsCommission obj)
        {
            SlsCommissionViewModel model = new SlsCommissionViewModel();

            model.Id = obj.Id;

            model.SlsDitributorId = obj.SlsDitributorId;
            model.SlsDealerId = obj.SlsDealerId;

            if(obj.SlsDitributorId != null)
            {
                model.PartyType = 1;
                model.Party = (int)obj.SlsDitributorId;
            }
            else if (obj.SlsDealerId != null)
            {
                model.PartyType = 3;
                model.Party = (int)obj.SlsDealerId;
            }
            
            model.MonthFrom = obj.MonthFrom;
            model.MonthTo = obj.MonthTo;
            model.YearFrom = obj.YearFrom;
            model.YearTo = obj.YearTo;
            model.NetSaleAmount = obj.NetSaleAmount;
            model.CommissionPercentage = obj.CommissionPercentage;
            model.Commission = obj.Commission;
            model.Date = obj.Date;
            model.ChequeNo = obj.ChequeNo;
            model.Bank = obj.Bank;
            model.Remarks = obj.Remarks;
                        
            var lastDayOfMonth = model.To.AddMonths(1).AddDays(-model.To.Day);            
            model.From = new DateTime(model.YearFrom, model.MonthFrom, 1);
            model.To = new DateTime(model.YearTo, model.MonthTo, lastDayOfMonth.Day);

            model.CreatedBy = obj.CreatedBy;
            model.CreatedDate = obj.CreatedDate;
            model.ModifiedBy = obj.ModifiedBy;
            model.ModifiedDate = obj.ModifiedDate;

            return model;
        }       

    }

}
