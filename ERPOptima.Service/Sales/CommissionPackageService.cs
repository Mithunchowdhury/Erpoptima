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
    public interface ICommissionPackageService
    {
        IList<SlsCommissionPackage> GetAll();
        Operation Save(SlsCommissionPackage obj);
        Operation Delete(SlsCommissionPackage obj);
        Operation Update(SlsCommissionPackage obj);
        decimal GetCommissionRate(DateTime From, DateTime To, decimal netsales, int PartyType, int Party);
    }

    public class CommissionPackageService : ICommissionPackageService
    {
        private ICommissionPackageRepository _CommissionPackageRepository;
        private IUnitOfWork _UnitOfWork;

        public CommissionPackageService(ICommissionPackageRepository commissionPackageRepository, IUnitOfWork unitOfWork)
        {
            this._CommissionPackageRepository = commissionPackageRepository;
            this._UnitOfWork = unitOfWork;
        }
        public IList<SlsCommissionPackage> GetAll()
        {
            try
            {
                return _CommissionPackageRepository.GetAll().ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Operation Update(SlsCommissionPackage obj)
        {
            Operation objOperation = new Operation { Success = true, OperationId = obj.Id };            

            try
            {
                _CommissionPackageRepository.Update(obj);
                _UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                objOperation.Success = false;

            }
            return objOperation;
        }

        public Operation Delete(SlsCommissionPackage obj)
        {
            Operation objOperation = new Operation { Success = true, OperationId = obj.Id };           

            try
            {
                SlsCommissionPackage objD = _CommissionPackageRepository.GetById(obj.Id);
                _CommissionPackageRepository.Delete(objD);
                _UnitOfWork.Commit();
            }
            catch (Exception ex)
            {

                objOperation.Success = false;
            }
            return objOperation;
        }

        public Operation Save(SlsCommissionPackage obj)
        {
            Operation objOperation = new Operation { Success = true };

            try
            {
                int newId = 1;
                try
                {
                    newId = _CommissionPackageRepository.GetTableContext().Max(i => i.Id) + 1;
                }
                catch(Exception ex)
                {

                }
                obj.Id = newId;
                objOperation.OperationId = newId;
                _CommissionPackageRepository.Add(obj);
                              
                _UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                objOperation.Success = false;
            }
            return objOperation;
        }

        /// <summary>
        /// From,To,netsales for General Rate. 
        /// </summary>
        /// <param name="From">From</param>
        /// <param name="To">To</param>
        /// <param name="netsales">netsales</param>
        /// <param name="PartyType">PartyType</param>
        /// <param name="Party">Party</param>
        /// <returns></returns>
        public decimal GetCommissionRate(DateTime From, DateTime To, decimal netsales, int PartyType, int Party)
        {
            decimal commission = 0;

            if (PartyType == 1)
            {
                //set commission from Distributor's own account
                var distributors = new DistributorRepository(new DatabaseFactory()).GetAll();
                var distributor = distributors.Where(i => i.Id == Party).FirstOrDefault();
                if (distributor != null && distributor.Id > 0 && distributor.RateOfCommission != null)
                {
                    commission = (decimal)distributor.RateOfCommission;
                }
            }
            else if (PartyType == 3)
            {
                //set commission from Dealer's own account
                var dealers = new DealerRepository(new DatabaseFactory()).GetAll();
                var dealer = dealers.Where(i => i.Id == Party).FirstOrDefault();
                if (dealer != null && dealer.Id > 0 && dealer.RateOfCommission != null)
                {
                    commission = (decimal)dealer.RateOfCommission;
                }
            }

            if (commission <= 0)
            {
                //Get recent package settings
                var list = _CommissionPackageRepository.GetAll();
                if (list != null && list.Count() > 0)
                {
                    list = list.Where(i => From.Year >= i.Year && To.Year <= i.Year &&
                        From.Month >= i.Month && To.Month <= i.Month &&
                        netsales >= i.LowerTarget && netsales <= i.UpperTarget).ToList();

                    //22-04-2015: Here if multiple package information found, latest package will be set as commission rate.
                    if(list != null && list.Count() > 0)
                    {
                        var recent = list.OrderByDescending(i => i.Year).OrderByDescending(i => i.Month).ToList().FirstOrDefault();
                        commission = recent.Commission;
                    }                    
                }                
            }
            
            return commission;
        }

    }
}
