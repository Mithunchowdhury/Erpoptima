using ERPOptima.Data.Accounts.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Accounts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ERPOptima.Service.Accounts
{
    #region interface

    public interface IAnFVoucherDetailsService
    {

        void Commit();
        void InsertVoucherDetails(AnFVoucherDetail det);
        long GetLastId();

        DataTable GetVoucherDetailsbyVoucherId(long id);


        List<AnFVoucherDetail> GetVoucherDetailsByVId(long p);

        void DeleteDetailsByVoucherId(long voucherId);
    }

    #endregion interface

    public class AnFVoucherDetailsService : IAnFVoucherDetailsService
    {
        private IAnFVoucherDetailsRepository _AnFVoucherDetailsRepository;
        private IUnitOfWork _UnitOfWork;

        public AnFVoucherDetailsService(IAnFVoucherDetailsRepository anFVoucherDetailsRepository, IUnitOfWork unitOfWork)
        {
            this._AnFVoucherDetailsRepository = anFVoucherDetailsRepository;
            this._UnitOfWork = unitOfWork;
        }

  
        public void InsertVoucherDetails(AnFVoucherDetail det)
        {
          
            _AnFVoucherDetailsRepository.AddEntity(det);
            
        }



        public long GetLastId()
        {
           return _AnFVoucherDetailsRepository.GetLastId();
        }

        public void Commit()
        {
            try
            {
                 _UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetVoucherDetailsbyVoucherId(long id) {

            SqlParameter[] paramsToStore = new SqlParameter[1];
            paramsToStore[0] = new SqlParameter("@voucherId", id);

            DataTable dt = _AnFVoucherDetailsRepository.GetFromStoredProcedure(SPList.AnFVoucherDetail.GetAnFVoucherDetailsByVoucherId,paramsToStore);

            return dt;
        }

        public List<AnFVoucherDetail> GetVoucherDetailsByVId(long p)
        {
            return _AnFVoucherDetailsRepository.GetManyByVoucherId(p);
        }



        public void DeleteDetailsByVoucherId(long voucherId)
        {
            _AnFVoucherDetailsRepository.DeleteByVoucherId(voucherId);
        }
    }
}