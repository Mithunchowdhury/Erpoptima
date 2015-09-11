using ERPOptima.Model.Common;
using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Accounts
{
    public partial class AnFChartOfAccount
    {
        public AnFChartOfAccount()
        {
            this.AnFChartOfAccounts1 = new List<AnFChartOfAccount>();
            this.AnFDepreciationRates = new List<AnFDepreciationRate>();
            this.AnFExpenses = new List<AnFExpens>();
            this.AnFOpeningBalances = new List<AnFOpeningBalance>();
            this.AnFClosingBalances = new List<AnFClosingBalance>();  //Add by Bably
            this.CmnAccountHeadMappers = new List<CmnAccountHeadMapper>();
            this.FxdAssets = new List<FxdAsset>();
        }

        public long Id { get; set; }
        public Nullable<long> AnFChartOfAccountId { get; set; }
        public int CmnCompanyId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ScheduleNo { get; set; }
        public bool IsTransactionalHead { get; set; }
        public bool Status { get; set; }
        public Nullable<int> DepthLevel { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual ICollection<AnFChartOfAccount> AnFChartOfAccounts1 { get; set; }
        public virtual AnFChartOfAccount AnFChartOfAccount1 { get; set; }
        public virtual ICollection<AnFDepreciationRate> AnFDepreciationRates { get; set; }
        public virtual ICollection<AnFExpens> AnFExpenses { get; set; }
        public virtual ICollection<AnFOpeningBalance> AnFOpeningBalances { get; set; }
        public virtual ICollection<AnFClosingBalance> AnFClosingBalances { get; set; }  //Add by Bably
        public virtual ICollection<CmnAccountHeadMapper> CmnAccountHeadMappers { get; set; }
        public virtual ICollection<FxdAsset> FxdAssets { get; set; }
    }
}
