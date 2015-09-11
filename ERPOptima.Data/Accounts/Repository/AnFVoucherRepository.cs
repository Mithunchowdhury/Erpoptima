using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPOptima.Data;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Accounts;


namespace ERPOptima.Data.Accounts.Repository
{

    #region Interface



    public interface IAnFVoucherRepository : IRepository<AnFVoucher>
    {

      int GetTotalByCompanyIdAndType(int companyId,int financialYearId,int type);
      int GetTotalCancelByCompanyIdAndType(int companyId, int financialYearId, int type);
      int GetTotalPostedByCompanyIdAndType(int companyId, int financialYearId, int type);
      int GetTotalUnPostedByCompanyIdAndType(int companyId, int financialYearId, int type);



      int AddEntity(AnFVoucher v);


      int UpdateEntity(AnFVoucher v);
    }

    #endregion








    public class AnFVoucherRepository : BaseRepository<AnFVoucher>, IAnFVoucherRepository
    {

        public AnFVoucherRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
            {

            }


        public int GetTotalByCompanyIdAndType(int companyId, int financialYearId, int type)
        {

            return DataContext.AnFVouchers.Where(v => v.CmnCompanyId == companyId && v.CmnFinancialYearId == financialYearId && v.Type == type).Count();
        
        
        }
        public int GetTotalCancelByCompanyIdAndType(int companyId, int financialYearId, int type)
        {

            return DataContext.AnFVouchers.Where(v => v.CmnCompanyId == companyId && v.CmnFinancialYearId == financialYearId && v.Type == type && v.IsCancel==true).Count();


        }

        public int GetTotalPostedByCompanyIdAndType(int companyId, int financialYearId, int type) 
        {

            return DataContext.AnFVouchers.Where(v => v.CmnCompanyId == companyId && v.CmnFinancialYearId == financialYearId && v.Type == type && v.IsPosted == true).Count();
        
        }

        public int GetTotalUnPostedByCompanyIdAndType(int companyId, int financialYearId, int type)
        {

            return DataContext.AnFVouchers.Where(v => v.CmnCompanyId == companyId && v.CmnFinancialYearId == financialYearId && v.Type == type && v.IsPosted == false).Count();
        
        }

        public int AddEntity(AnFVoucher  voucher)
        {
            int Id = 1;
            AnFVoucher last = DataContext.AnFVouchers.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            voucher.Id = Id;
            base.Add(voucher);
            return Id;

        }

        public int UpdateEntity(AnFVoucher voucher)
        {
      
            AnFVoucher obj = DataContext.AnFVouchers.Where(X=>X.Id==voucher.Id).FirstOrDefault();
            obj.CancelledBy = voucher.CancelledBy;
            obj.CancelledDate = voucher.CancelledDate;
            obj.CancelReason = voucher.CancelReason;
            obj.CmnCompanyId = voucher.CmnCompanyId;
            obj.Date = voucher.Date;
            obj.VoucherNumber = voucher.VoucherNumber;
            obj.Naration = voucher.Naration;
            obj.TotalAmount = voucher.TotalAmount;
            obj.ModifiedDate = voucher.ModifiedDate;
            obj.ModifiedBy = voucher.ModifiedBy;


            base.Update(obj);
            return obj.Id;

        }
       
    }




    
}
