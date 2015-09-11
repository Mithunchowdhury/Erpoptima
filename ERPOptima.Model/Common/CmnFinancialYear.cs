using ERPOptima.Model.Accounts;
using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Common
{
    public partial class CmnFinancialYear
    {
        public CmnFinancialYear()
        {
            this.AnFAdjustments = new List<AnFAdjustment>();
            this.AnFAdvances = new List<AnFAdvance>();
            this.AnFClosingBalances = new List<AnFClosingBalance>();
            this.AnFExpenses = new List<AnFExpens>();
            this.AnFMonthLocks = new List<AnFMonthLock>();
            this.AnFOpeningBalances = new List<AnFOpeningBalance>();
            this.AnFVouchers = new List<AnFVoucher>();
            this.FxdAcquisitions = new List<FxdAcquisition>();
            this.FxdDepreciations = new List<FxdDepreciation>();
            this.FxdDisposals = new List<FxdDisposal>();
            this.FxdRevaluations = new List<FxdRevaluation>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public System.DateTime OpeningDate { get; set; }
        public Nullable<System.DateTime> ClosingDate { get; set; }
        public Nullable<bool> Status { get; set; }
        public int CmnCompanyId { get; set; }
        public int SecModuleId { get; set; }
        public bool YearClosingStatus { get; set; }
        public Nullable<int> ClosedBy { get; set; }
        public Nullable<System.DateTime> ClosedDate { get; set; }
        public string ClosingNote { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual ICollection<AnFAdjustment> AnFAdjustments { get; set; }
        public virtual ICollection<AnFAdvance> AnFAdvances { get; set; }
        public virtual ICollection<AnFClosingBalance> AnFClosingBalances { get; set; }
        public virtual ICollection<AnFExpens> AnFExpenses { get; set; }
        public virtual ICollection<AnFMonthLock> AnFMonthLocks { get; set; }
        public virtual ICollection<AnFOpeningBalance> AnFOpeningBalances { get; set; }
        public virtual ICollection<AnFVoucher> AnFVouchers { get; set; }
        public virtual SecModule SecModule { get; set; }
        public virtual ICollection<FxdAcquisition> FxdAcquisitions { get; set; }
        public virtual ICollection<FxdDepreciation> FxdDepreciations { get; set; }
        public virtual ICollection<FxdDisposal> FxdDisposals { get; set; }
        public virtual ICollection<FxdRevaluation> FxdRevaluations { get; set; }
    }
}
