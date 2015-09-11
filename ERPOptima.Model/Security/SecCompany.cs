using ERPOptima.Model.Accounts;
using ERPOptima.Model.HRM;
using ERPOptima.Model.Inventory;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Model.Security
{
    public partial class SecCompany
    {
        public SecCompany()
        {
            this.AnFAdjustments = new List<AnFAdjustment>();
            this.AnFAdvances = new List<AnFAdvance>();
            this.AnFExpenses = new List<AnFExpens>();
            this.AnFMonthLocks = new List<AnFMonthLock>();
            this.CmnAccountHeadMappers = new List<CmnAccountHeadMapper>();
            this.FxdAcquisitions = new List<FxdAcquisition>();
            this.FxdAssets = new List<FxdAsset>();
            this.FxdDepreciations = new List<FxdDepreciation>();
            this.FxdDisposals = new List<FxdDisposal>();
            this.FxdRevaluations = new List<FxdRevaluation>();
            this.HrmEmployees = new List<HrmEmployee>();
            this.InvDamages = new List<InvDamage>();
            this.InvIssues = new List<InvIssue>();
            this.InvRequisitions = new List<InvRequisition>();
            this.SecCompanyUsers = new List<SecCompanyUser>();
            this.SecDashboards = new List<SecDashboard>();
            this.SlsCollections = new List<SlsCollection>();
            this.SlsCollectionTargets = new List<SlsCollectionTarget>();
            this.SlsDealers = new List<SlsDealer>();
            this.SlsDealers1 = new List<SlsDealer>();
            this.SlsDistributors = new List<SlsDistributor>();
            this.SlsProducts = new List<SlsProduct>();
            this.SlsRetailers = new List<SlsRetailer>();
            this.SlsSalesReturns = new List<SlsSalesReturn>();
            this.SlsSalesTargets = new List<SlsSalesTarget>();
            this.SlsTransfers = new List<SlsTransfer>();
        }

        public int Id { get; set; }
        public int SecGroupId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Web { get; set; }
        public string Remarks { get; set; }
        public string Prefix { get; set; }
        public string ShortName { get; set; }
        public byte[] Logo { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual ICollection<AnFAdjustment> AnFAdjustments { get; set; }
        public virtual ICollection<AnFAdvance> AnFAdvances { get; set; }
        public virtual ICollection<AnFExpens> AnFExpenses { get; set; }
        public virtual ICollection<AnFMonthLock> AnFMonthLocks { get; set; }
        public virtual ICollection<CmnAccountHeadMapper> CmnAccountHeadMappers { get; set; }
        public virtual ICollection<FxdAcquisition> FxdAcquisitions { get; set; }
        public virtual ICollection<FxdAsset> FxdAssets { get; set; }
        public virtual ICollection<FxdDepreciation> FxdDepreciations { get; set; }
        public virtual ICollection<FxdDisposal> FxdDisposals { get; set; }
        public virtual ICollection<FxdRevaluation> FxdRevaluations { get; set; }
        public virtual ICollection<HrmEmployee> HrmEmployees { get; set; }
        public virtual ICollection<InvDamage> InvDamages { get; set; }
        public virtual ICollection<InvIssue> InvIssues { get; set; }
        public virtual ICollection<InvRequisition> InvRequisitions { get; set; }
        public virtual SecGroup SecGroup { get; set; }
        public virtual ICollection<SecCompanyUser> SecCompanyUsers { get; set; }
        public virtual ICollection<SecDashboard> SecDashboards { get; set; }
        public virtual ICollection<SlsCollection> SlsCollections { get; set; }
        public virtual ICollection<SlsCollectionTarget> SlsCollectionTargets { get; set; }
        public virtual ICollection<SlsDealer> SlsDealers { get; set; }
        public virtual ICollection<SlsDealer> SlsDealers1 { get; set; }
        public virtual ICollection<SlsDistributor> SlsDistributors { get; set; }
        public virtual ICollection<SlsProduct> SlsProducts { get; set; }
        public virtual ICollection<SlsRetailer> SlsRetailers { get; set; }
        public virtual ICollection<SlsSalesReturn> SlsSalesReturns { get; set; }
        public virtual ICollection<SlsSalesTarget> SlsSalesTargets { get; set; }
        public virtual ICollection<SlsTransfer> SlsTransfers { get; set; }
    }
}
