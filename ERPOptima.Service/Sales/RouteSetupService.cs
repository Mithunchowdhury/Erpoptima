using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Service.Sales
{
    public interface IRouteSetupService
    {
        IList<SlsRoute> GetAll();
        IList<SlsRouteDetail> GetRouteSetupDetails(int routesetupId);
        SlsRoute GetById(int Id);
        Operation Save(SlsRoute record, IList<SlsRouteDetail> recordDetails);       
        Operation Update(SlsRoute record, IList<SlsRouteDetail> recordDetails);
        IList<SlsRoute> GetAll(int officeId);
    }
    public class RouteSetupService:IRouteSetupService
    {
        private IRouteSetupRepository _repository;
        private IRouteSetupDetailRepository _detailrepository;
        private IUnitOfWork _unitOfWork;


        public RouteSetupService(IRouteSetupRepository repository, IRouteSetupDetailRepository detailrepository, IUnitOfWork unitOfWork)
        {
            this._repository = repository;
            this._detailrepository = detailrepository;
            this._unitOfWork = unitOfWork;
        }

        public SlsRoute GetById(int id)
        {
            return null;
        }
        public IList<SlsRoute> GetAll()
        {
            return _repository.GetAll();              
                
        }
        public IList<SlsRouteDetail> GetRouteSetupDetails(int routesetupId)
        {
            return _detailrepository.GetAll().Where(t => t.SlsRouteId == routesetupId).ToList();
        }

        public IList<SlsRoute> GetAll(int officeId)
        {
            return _repository.GetAll().Where(t => t.SlsOfficeId == officeId).ToList<SlsRoute>();
        }

        public Operation Save(SlsRoute record, IList<SlsRouteDetail> recordDetails)
        {
            Operation objOperation = new Operation { Success = false };
            using (var dbContextTransaction = _repository.BeginTransaction())
            {
                try
                {
                    objOperation = new Operation { Success = true };

                    int Id = _repository.AddEntity(record);
                    _repository.SaveChanges();
                    objOperation.OperationId = Id;
                    record.Id = Id;

                    IList<SlsRouteDetail> newrecordDetails = new List<SlsRouteDetail>();
                    //add or update categories offered to each offer
                    if (recordDetails != null && recordDetails.Count > 0)
                    {
                        foreach (SlsRouteDetail detail in recordDetails)
                        {
                            detail.SlsRouteId = record.Id;
                            if (detail.Id <= 0)
                                newrecordDetails.Add(detail);
                                
                                else
                                _detailrepository.Update(detail);
                            
                        }
                    }
                    //Add offer detail list - new offer details
                    if (newrecordDetails != null && newrecordDetails.Count > 0)
                    {
                        _detailrepository.AddEntityList(newrecordDetails);
                    }
                    _detailrepository.SaveChanges();

                    try
                    {
                        //_unitOfWork.Commit();
                        _repository.Commit(dbContextTransaction);
                    }
                    catch (Exception ex)
                    {
                        objOperation.Success = false;
                        throw ex;
                    }
                }
                catch (Exception ex)
                {
                    _repository.Rollback(dbContextTransaction);
                }
            }
            return objOperation;



        }



        public Operation Update(SlsRoute record, IList<SlsRouteDetail> recordDetails)
        {
            Operation objOperation = new Operation { Success = true, OperationId = record.Id };
            using (var dbContextTransaction = _repository.BeginTransaction())
            {
                try
                {
                    objOperation = new Operation { Success = true };

                    _repository.Update(record);
                    _repository.SaveChanges();

                    IList<SlsRouteDetail> newrecordDetails = new List<SlsRouteDetail>();
                    //add or update categories offered to each offer
                    if (recordDetails != null && recordDetails.Count > 0)
                    {
                        foreach (SlsRouteDetail detail in recordDetails)
                        {
                            detail.SlsRouteId = record.Id;
                            if (detail.Id <= 0)
                                newrecordDetails.Add(detail);

                            else
                                _detailrepository.Update(detail);

                        }
                    }
                    //Add offer detail list - new offer details                    
                    if (newrecordDetails != null && newrecordDetails.Count > 0)
                    {
                        _detailrepository.AddEntityList(newrecordDetails);
                    }
                    IList<SlsRouteDetail> fbrecordDetails = GetRouteSetupDetails(record.Id);
                    IList<int> updatedDetailIds = recordDetails.Where(i => i.Id > 0).Select(i => i.Id).ToList();
                    IList<SlsRouteDetail> removedrecordDetails = fbrecordDetails.Where(i => !updatedDetailIds.Contains(i.Id)).ToList();
                    if (removedrecordDetails != null && removedrecordDetails.Count > 0)
                    {
                        foreach (SlsRouteDetail detail in removedrecordDetails)
                        {
                            _detailrepository.Delete(detail);
                        }
                    }
                    _detailrepository.SaveChanges();

                    try
                    {
                        //_unitOfWork.Commit();
                        _repository.Commit(dbContextTransaction);
                    }
                    catch (Exception ex)
                    {
                        objOperation.Success = false;
                        throw ex;
                    }
                }
                catch (Exception ex)
                {
                    _repository.Rollback(dbContextTransaction);
                }
            }
            return objOperation;
        }

        
    }
  
}
