using ERPOptima.Model.Common;
using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;
namespace ERPOptima.Model.Accounts
{
    public class AnFAdjustment
    {
        public int Id { get; set; }
        public int AnFAdvanceId { get; set; }
        public decimal AdjustedAmount { get; set; }
        public System.DateTime Date { get; set; }
        public int CmnFinancialYearId { get; set; }
        public Nullable<int> SecCompanyId { get; set; }
        public Nullable<int> AnFVoucherId { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual AnFAdvance AnFAdvance { get; set; }
        public virtual AnFVoucher AnFVoucher { get; set; }
        public virtual CmnFinancialYear CmnFinancialYear { get; set; }
        public virtual SecCompany SecCompany { get; set; }
    }
}
