using ERPOptima.Model.Common;
using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Accounts
{
    public partial class AnFVoucher
    {
        public AnFVoucher()
        {
            this.AnFAdjustments = new List<AnFAdjustment>();
            this.AnFAdvances = new List<AnFAdvance>();
            this.AnFExpenses = new List<AnFExpens>();
            this.AnFVoucherDetails = new List<AnFVoucherDetail>();
            this.FxdAcquisitions = new List<FxdAcquisition>();
            this.FxdDepreciations = new List<FxdDepreciation>();
            this.FxdDisposals = new List<FxdDisposal>();
            this.FxdRevaluations = new List<FxdRevaluation>();
        }

        public int Id { get; set; }
        public int CmnCompanyId { get; set; }
        public int Type { get; set; }
        public string VoucherNumber { get; set; }
        public System.DateTime Date { get; set; }
        public Nullable<int> SlsOfficeId { get; set; }
        public Nullable<int> AnFCostCenterId { get; set; }
        public int CmnFinancialYearId { get; set; }
        public decimal TotalAmount { get; set; }
        public string Naration { get; set; }
        public Nullable<bool> Status { get; set; }
        public bool IsPosted { get; set; }
        public Nullable<int> PostedBy { get; set; }
        public Nullable<System.DateTime> PostedDate { get; set; }
        public Nullable<bool> IsCancel { get; set; }
        public Nullable<int> CancelledBy { get; set; }
        public Nullable<System.DateTime> CancelledDate { get; set; }
        public string CancelReason { get; set; }
        public Nullable<int> Purpose { get; set; }
        public string FileLocation { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual ICollection<AnFAdjustment> AnFAdjustments { get; set; }
        public virtual ICollection<AnFAdvance> AnFAdvances { get; set; }
        public virtual ICollection<AnFExpens> AnFExpenses { get; set; }
        public virtual ICollection<AnFVoucherDetail> AnFVoucherDetails { get; set; }
        public virtual CmnFinancialYear CmnFinancialYear { get; set; }
        public virtual ICollection<FxdAcquisition> FxdAcquisitions { get; set; }
        public virtual ICollection<FxdDepreciation> FxdDepreciations { get; set; }
        public virtual ICollection<FxdDisposal> FxdDisposals { get; set; }
        public virtual ICollection<FxdRevaluation> FxdRevaluations { get; set; }
    }
}
