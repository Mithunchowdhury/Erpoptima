using ERPOptima.Model.Common;
using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Accounts
{
    public partial class AnFExpense
    {
        //public AnFExpense()
        //{
        //    this.AnFVoucherDetails = new List<AnFVoucherDetail>();
        //}

        public int Id { get; set; }
        public string RefNo { get; set; }
        public string Particular { get; set; }
        public int AnFChartOfAccountId { get; set; }
        public int AnFCostCenterId { get; set; }
        public System.DateTime Date { get; set; }
        public string BillNo { get; set; }
        public int Mode { get; set; }
        public string Narration { get; set; }
        public string FileLocation { get; set; }
        public int CmnFinancialYearId { get; set; }
        public int SecCompanyId { get; set; }
        public int AnFVoucherId { get; set; }
        public decimal Amount { get; set; }
        //public bool IsPosted { get; set; }
        public int IsPosted { get; set; }
        public Nullable<int> PostedBy { get; set; }
        public Nullable<System.DateTime> PostedDate { get; set; }
        public Nullable<bool> IsCancel { get; set; }
        public Nullable<int> CancelledBy { get; set; }
        public Nullable<System.DateTime> CancelledDate { get; set; }
        public string CancelReason { get; set; }
        public int Purpose { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
