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
    public interface IPromotionalOfferService
    {
        IEnumerable<SlsPromotionalOfferViewModel> GetAll();
        SlsPromotionalOfferViewModel GetById(int Id);
        Operation Save(SlsPromotionalOfferViewModel obj);
        Operation Delete(SlsPromotionalOfferViewModel obj);
        Operation Update(SlsPromotionalOfferViewModel obj);
        IEnumerable<SlsPromotionalOfferDetailViewModel> GetAllCategoriesForOffer();

    }
    public class PromotionalOfferService : IPromotionalOfferService
    {
        private IPromotionalOfferRepository _promotionalOfferRepository;
        private IPromotionalOfferDetailRepository _promotionalOfferDetailRepository;
        private IUnitOfWork _unitOfWork;


        public PromotionalOfferService(IPromotionalOfferRepository promotionalOfferRepository,
            IPromotionalOfferDetailRepository promotionalOfferDetailRepository,
            IUnitOfWork unitOfWork)
        {
            this._promotionalOfferRepository = promotionalOfferRepository;
            this._promotionalOfferDetailRepository = promotionalOfferDetailRepository;
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<SlsPromotionalOfferViewModel> GetAll()
        {
            return GetAllOffers(0);
        }
        /// <summary>
        /// Both GetAll and GetById will reuse this helper
        /// </summary>
        /// <param name="offerId">offerId=0 GetAll, offerId!=0 GetById</param>
        /// <returns></returns>
        public IEnumerable<SlsPromotionalOfferViewModel> GetAllOffers(int offerId)
        {
            try
            {
                Collection<SlsPromotionalOfferViewModel> list = null;
                SqlParameter[] paramsToStore = new SqlParameter[1];
                paramsToStore[0] = new SqlParameter("@OfferId", offerId);
                DataTable dt = _promotionalOfferRepository.GetFromStoredProcedure(SPList.PromotionalOffer.GetAllSlsPromotionalOffers, paramsToStore);

                if (dt != null)
                {
                    list = new Collection<SlsPromotionalOfferViewModel>();
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add((SlsPromotionalOfferViewModel)ERPOptima.Lib.Utilities.Helper.FillTo(row, typeof(SlsPromotionalOfferViewModel)));
                    }
                }

                //to get categories offered for each offer
                foreach (SlsPromotionalOfferViewModel item in list)
                {
                    //attach detail list to item
                    item.OfferCategories = GetAllOfferDetails(item.Id).ToList();
                }

                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IEnumerable<SlsPromotionalOfferDetailViewModel> GetAllOfferDetails(int offerId)
        {
            try
            {
                Collection<SlsPromotionalOfferDetailViewModel> detaillist = null;
                SqlParameter[] paramsToStoreDetail = new SqlParameter[2];
                paramsToStoreDetail[0] = new SqlParameter("@OfferId", offerId);
                paramsToStoreDetail[1] = new SqlParameter("@Level", SPList.PromotionalOffer.CategoryLevel);
                DataTable detaildt = _promotionalOfferDetailRepository.GetFromStoredProcedure(SPList.PromotionalOffer.GetAllSlsPromotionalOfferDetails, paramsToStoreDetail);

                if (detaildt != null)
                {
                    detaillist = new Collection<SlsPromotionalOfferDetailViewModel>();
                    foreach (DataRow row in detaildt.Rows)
                    {
                        detaillist.Add((SlsPromotionalOfferDetailViewModel)ERPOptima.Lib.Utilities.Helper.FillTo(row, typeof(SlsPromotionalOfferDetailViewModel)));                                        
                    }
                }
                return detaillist;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public SlsPromotionalOfferViewModel GetById(int Id)
        {
            SlsPromotionalOfferViewModel obj = GetAllOffers(Id).First();
            return obj;
        }
        public Operation Update(SlsPromotionalOfferViewModel obj)
        {
            Operation objOperation = new Operation { Success = false };
            using (var dbContextTransaction = _promotionalOfferRepository.BeginTransaction())
            {
                try
                {
                    objOperation = new Operation { Success = true, OperationId = obj.Id };
                    SlsPromotionalOffer offer = SlsPromotionalOfferMapVMToModel.MapToSlsPromotionalOffer(obj);
                    _promotionalOfferRepository.Update(offer);
                    _promotionalOfferRepository.SaveChanges();

                    //add or update categories offered to each offer
                    IList<SlsPromotionalOfferDetail> offerDetailList = new List<SlsPromotionalOfferDetail>();
                    foreach (SlsPromotionalOfferDetailViewModel categoryOffered in obj.OfferCategories)
                    {
                        SlsPromotionalOfferDetail offerDetail = SlsPromotionalOfferMapVMToModel.MapToSlsPromotionalOfferDetail(categoryOffered);
                        if (categoryOffered.IsOffered)
                        {
                            offerDetail.SlsPromotionalOfferId = offer.Id;
                            if (offerDetail.Id <= 0)
                                offerDetailList.Add(offerDetail);
                            //_promotionalOfferDetailRepository.AddEntity(offerDetail);
                            else
                                _promotionalOfferDetailRepository.Update(offerDetail);
                        }
                        else
                        {
                            if (offerDetail.Id > 0)
                            {
                                var detToDel = _promotionalOfferDetailRepository.GetById(offerDetail.Id);
                                _promotionalOfferDetailRepository.Delete(detToDel);
                            }
                        }
                    }
                    //Add offer detail list - new offer details
                    if (offerDetailList != null && offerDetailList.Count > 0)
                    {
                        _promotionalOfferDetailRepository.AddEntityList(offerDetailList);                        
                    }
                    _promotionalOfferDetailRepository.SaveChanges();

                    try
                    {
                        //_unitOfWork.Commit();
                        _promotionalOfferRepository.Commit(dbContextTransaction);
                    }
                    catch (Exception ex)
                    {
                        objOperation.Success = false;
                        throw ex;
                    }
                }
                catch (Exception ex)
                {
                    _promotionalOfferRepository.Rollback(dbContextTransaction);
                }
            }
            return objOperation;
        }

        public Operation Delete(SlsPromotionalOfferViewModel obj)
        {
            Operation objOperation = new Operation { Success = true, OperationId = obj.Id };
            SlsPromotionalOffer offer = SlsPromotionalOfferMapVMToModel.MapToSlsPromotionalOffer(obj);
            _promotionalOfferRepository.Delete(offer);
            
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

        public Operation Save(SlsPromotionalOfferViewModel obj)
        {
            Operation objOperation = new Operation { Success = false };
            using (var dbContextTransaction = _promotionalOfferRepository.BeginTransaction())
            {
                try
                {
                    objOperation = new Operation { Success = true };

                    SlsPromotionalOffer offer = SlsPromotionalOfferMapVMToModel.MapToSlsPromotionalOffer(obj);

                    int Id = _promotionalOfferRepository.AddEntity(offer);
                    _promotionalOfferRepository.SaveChanges();
                    objOperation.OperationId = Id;
                    offer.Id = Id;

                    //add or update categories offered to each offer
                    IList<SlsPromotionalOfferDetail> offerDetailList = new List<SlsPromotionalOfferDetail>();
                    foreach (SlsPromotionalOfferDetailViewModel categoryOffered in obj.OfferCategories)
                    {
                        if (categoryOffered.IsOffered)
                        {
                            SlsPromotionalOfferDetail offerDetail = SlsPromotionalOfferMapVMToModel.MapToSlsPromotionalOfferDetail(categoryOffered);
                            offerDetail.SlsPromotionalOfferId = offer.Id;
                            if (offerDetail.Id <= 0)
                                offerDetailList.Add(offerDetail);
                            //_promotionalOfferDetailRepository.AddEntity(offerDetail);
                            else
                                _promotionalOfferDetailRepository.Update(offerDetail);
                        }
                    }
                    //Add offer detail list - new offer details
                    if (offerDetailList != null && offerDetailList.Count > 0)
                    {
                        _promotionalOfferDetailRepository.AddEntityList(offerDetailList);                        
                    }
                    _promotionalOfferDetailRepository.SaveChanges();

                    try
                    {
                        //_unitOfWork.Commit();
                        _promotionalOfferRepository.Commit(dbContextTransaction);
                    }
                    catch (Exception ex)
                    {
                        objOperation.Success = false;
                        throw ex;
                    }
                }
                catch(Exception ex)
                {
                    _promotionalOfferRepository.Rollback(dbContextTransaction);
                }
            }
            return objOperation;
        }

        //Get all categories to be offered - for creating a new offer
        public IEnumerable<SlsPromotionalOfferDetailViewModel> GetAllCategoriesForOffer()
        {
            try
            {
                return GetAllOfferDetails(0);
            }
            catch(Exception ex)
            {
                return null;
            }
        }

    }

    public class SlsPromotionalOfferMapVMToModel
    {
        public static SlsPromotionalOffer MapToSlsPromotionalOffer(SlsPromotionalOfferViewModel obj)
        {
            SlsPromotionalOffer model = new SlsPromotionalOffer();

            model.Id = obj.Id;
            model.Title = obj.Title;
            model.SlsRegionId = obj.SlsRegionId;
            model.StartDate = obj.StartDate;
            model.EndDate = obj.EndDate;
            model.Remarks = obj.Remarks;
            model.IsValid = obj.IsValid;
            model.CreatedBy = obj.CreatedBy;
            model.CreatedDate = obj.CreatedDate;
            model.ModifiedBy = obj.ModifiedBy;

            model.ModifiedDate = obj.ModifiedDate;

            return model;
        }

        public static SlsPromotionalOfferDetail MapToSlsPromotionalOfferDetail(SlsPromotionalOfferDetailViewModel obj)
        {
            SlsPromotionalOfferDetail model = new SlsPromotionalOfferDetail();

            model.Id = obj.Id;
            model.SlsPromotionalOfferId = obj.SlsPromotionalOfferId;
            model.SlsProuctId = obj.SlsProductId;
            model.Discount = obj.Discount;

            return model;
        }
    }

}
