using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERPOptima.Data.Accounts.Repository
{
    #region Interface

    public interface IAnFVoucherDetailsRepository : IRepository<AnFVoucherDetail>
    {
        void AddEntity(AnFVoucherDetail det);

        long GetLastId();

        List<AnFVoucherDetail> GetManyByVoucherId(long p);

        void DeleteByVoucherId(long p);
    }

    #endregion Interface

    public class AnFVoucherDetailsRepository : BaseRepository<AnFVoucherDetail>, IAnFVoucherDetailsRepository
    {
        public AnFVoucherDetailsRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public void AddEntity(AnFVoucherDetail det)
        {
            base.Add(det);
        }

        public void DeleteByVoucherId(long p)
        {
            List<AnFVoucherDetail> list = GetManyByVoucherId(p);
            if (list!=null && list.Count > 0)
            {
                foreach (AnFVoucherDetail det in list)
                {
                    base.Delete(det);
                }
            }
        }

        public long GetLastId()
        {
            long Id = 1;
            AnFVoucherDetail last = DataContext.AnFVoucherDetails.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;
            }
            return Id;
        }

        public List<AnFVoucherDetail> GetManyByVoucherId(long p)
        {
            try
            {
                List<AnFVoucherDetail> list = DataContext.AnFVoucherDetails.Where(voucher => voucher.AnFVoucherId == p).ToList();

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}