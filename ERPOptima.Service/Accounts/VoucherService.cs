using ERPOptima.Data.Accounts.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Lib.Model;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Accounts;
using ERPOptima.Service.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Service.Accounts
{
    public interface IVoucherService
    {
        Operation Save(AnFVoucher obj);
        Operation Update(AnFVoucher obj);
        Operation Delete(AnFVoucher obj);
        Operation SaveDetail(AnFVoucherDetail obj);
        int GetNextDetailId();
        Operation UpdateDetail(AnFVoucherDetail obj);
        Operation DeleteDetail(AnFVoucherDetail obj);
        IEnumerable<AnFVoucher> GetAll();
        AnFVoucher GetById(int Id);
        string GenerateVoucherNo(int ParamCmnCompanyId, int ParamType, string ParamTypeCode, int ParamSlsOfficeId, int ParamCmnFinancialYearId, DateTime ParamDate);

    }
    public class VoucherService : IVoucherService
    {
        private IVoucherRepository _voucherRepository;
        private IVoucherDetailRepository _voucherDetailRepository;
        private IUnitOfWork _unitOfWork;
        public VoucherService(IVoucherRepository voucherRepository, IVoucherDetailRepository voucherDetailRepository, IUnitOfWork unitOfWork)
        {
            _voucherRepository = voucherRepository;
            _voucherDetailRepository = voucherDetailRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<AnFVoucher> GetAll()
        {
            return _voucherRepository.GetAll();
        }
        public AnFVoucher GetById(int Id)
        {
            return _voucherRepository.GetById(Id);
        }
        public Operation Save(AnFVoucher obj)
        {
            Operation objOperation = new Operation { Success = false };

            using (var dbContextTransaction = _voucherRepository.BeginTransaction())
            {
                objOperation = SaveVoucher(obj, objOperation, dbContextTransaction);
            }
            return objOperation;


        }
        public Operation Update(AnFVoucher obj)
        {
            Operation objOperation = new Operation { Success = true, OperationId = obj.Id };
            _voucherRepository.Update(obj);

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
        public Operation Delete(AnFVoucher obj)
        {
            Operation objOperation = new Operation { Success = true, OperationId = obj.Id };
            _voucherRepository.Delete(obj);

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

        public Operation SaveVoucher(AnFVoucher obj, Operation objOperation, System.Data.Entity.DbContextTransaction dbContextTransaction)
        {
            try
            {
                bool update = false;
                long Id = 0;
                if (obj.Id <= 0)
                {
                    Id = _voucherRepository.AddEntity(obj);
                }
                else
                {
                    Id = obj.Id;
                    update = true;                    
                }

                if (Id > 0)
                {
                    objOperation.OperationId = Id;

                    var existingDetailList = _voucherDetailRepository.GetAll();
                    if (existingDetailList != null && existingDetailList.Count() > 0)
                    {
                        existingDetailList = existingDetailList.Where(i => i.AnFVoucherId == Id).ToList();
                    }

                    IList<AnFVoucherDetail> newdetailList = new List<AnFVoucherDetail>();
                    //obj.AnFVoucherDetails.First().Id = 1561;
                    //Delete detail
                    foreach (AnFVoucherDetail delete in existingDetailList)
                    {
                        AnFVoucherDetail exists = null;
                        try
                        {
                            exists = obj.AnFVoucherDetails.Where(i => i.Id == delete.Id).FirstOrDefault();
                        }
                        catch(Exception ex)
                        {

                        }
                        if(exists == null)
                        {
                            _voucherDetailRepository.Delete(delete);
                        }
                    }

                    //add/update detail
                    foreach (AnFVoucherDetail detail in obj.AnFVoucherDetails)
                    {
                        detail.AnFVoucherId = int.Parse(objOperation.OperationId.ToString());
                        if (detail.Id <= 0)
                        {
                            //detail.SubVoucherNumber = "MLP-JV-001-2/06-14-2";
                            newdetailList.Add(detail);
                        }
                        else
                        {
                            //detail.SubVoucherNumber = "MLP-JV-001-1/06-14-2";
                            _voucherDetailRepository.Modified(detail);
                        }
                    }
                    //Add offer detail list - new offer details
                    if (newdetailList != null && newdetailList.Count > 0)
                    {
                        _voucherDetailRepository.AddEntityList(newdetailList);
                    }

                    if(update)
                    {
                        _voucherRepository.Modified(obj);                        
                    }
                    _voucherRepository.SaveChanges();


                    _voucherDetailRepository.SaveChanges();
                    try
                    {
                        _voucherRepository.Commit(dbContextTransaction);
                        objOperation.Success = true;
                    }
                    catch (Exception ex)
                    {
                        objOperation.Message = ex.Message;
                        throw ex;
                    }
                }
                else
                {
                    objOperation.Success = false;
                }
            }
            catch (Exception ex)
            {
                objOperation.Success = false;
                _voucherRepository.Rollback(dbContextTransaction);
            }
            return objOperation;
        }
        public Operation SaveDetail(AnFVoucherDetail obj)
        {
            Operation objOperation = new Operation { Success = true };

            long Id = _voucherDetailRepository.AddEntity(obj);
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
        public int GetNextDetailId()
        {
            return _voucherDetailRepository.GetNextDetailId();
        }
        public Operation UpdateDetail(AnFVoucherDetail obj)
        {
            Operation objOperation = new Operation { Success = true, OperationId = obj.Id };
            _voucherDetailRepository.Update(obj);

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
        public Operation DeleteDetail(AnFVoucherDetail obj)
        {
            Operation objOperation = new Operation { Success = true, OperationId = obj.Id };
            _voucherDetailRepository.Delete(obj);

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

        public string GenerateVoucherNo(int ParamCmnCompanyId, int ParamType, string ParamTypeCode, int ParamSlsOfficeId, int ParamCmnFinancialYearId, DateTime ParamDate)
        {
            DataTable dt = null;

            SqlParameter[] paramsToStore = new SqlParameter[6];

            paramsToStore[0] = new SqlParameter("@ParamCmnCompanyId", ParamCmnCompanyId);
            paramsToStore[1] = new SqlParameter("@ParamType", ParamType);
            paramsToStore[2] = new SqlParameter("@ParamTypeCode", ParamTypeCode);
            paramsToStore[3] = new SqlParameter("@ParamSlsOfficeId", ParamSlsOfficeId);
            paramsToStore[4] = new SqlParameter("@ParamCmnFinancialYearId", ParamCmnFinancialYearId);
            paramsToStore[5] = new SqlParameter("@ParamDate", ParamDate);

            try
            {
                dt = _voucherRepository.GetFromStoredProcedure(SPList.AnFVoucher.GetAnFVoucherNo, paramsToStore);

                if (dt.Rows.Count > 0)
                    return dt.Rows[0]["VoucherNo"] != null ? dt.Rows[0]["VoucherNo"].ToString() : "";
                else
                    return string.Empty;
            }
            catch (Exception)
            {
                throw new ArgumentNullException();
            }
        }

    }
}
