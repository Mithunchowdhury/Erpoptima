using ERPOptima.Model.Common;
using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Accounts
{
    public partial class AnFMonthLock
    {
        public int Id { get; set; }
        public int CmnFinancialYearId { get; set; }
        public int Year { get; set; }
        public int MonthNo { get; set; }
        public bool LockingStatus { get; set; }
        public int SecConpanyId { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual CmnFinancialYear CmnFinancialYear { get; set; }
        public virtual SecCompany SecCompany { get; set; }
    }
}
