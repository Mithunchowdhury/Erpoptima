using ERPOptima.Model.Common;
using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Accounts
{
    public partial class AnFOpeningBalance
    {
        public long Id { get; set; }
        public int CmnCompanyId { get; set; }
        //public int CmnProjectId { get; set; }
        public long AnFChartOfAccountId { get; set; }
        public Nullable<decimal> Debit { get; set; }
        public Nullable<decimal> Credit { get; set; }
        public Nullable<bool> Status { get; set; }
        public int CmnFinancialYearId { get; set; }
        public Nullable<bool> IsEditable { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual AnFChartOfAccount AnFChartOfAccount { get; set; }
        public virtual CmnFinancialYear CmnFinancialYear { get; set; }
        //public virtual CmnProject CmnProject { get; set; }
    }
}
