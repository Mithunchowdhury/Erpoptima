using ERPOptima.Model.Sales;
using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Inventory
{
    public partial class InvStore
    {
        public InvStore()
        {
            this.InvDamages = new List<InvDamage>();
            this.InvIssues = new List<InvIssue>();
            this.InvProductReceives = new List<InvProductReceive>();
            this.InvStoreOpenings = new List<InvStoreOpening>();
            this.SlsTransfers = new List<SlsTransfer>();
            this.SlsTransfers1 = new List<SlsTransfer>();
        }

        public int Id { get; set; }
        public Nullable<int> SlsOfficeId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public Nullable<int> SlsDistributorId { get; set; }
        public string Remarks { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual ICollection<InvDamage> InvDamages { get; set; }
        public virtual ICollection<InvIssue> InvIssues { get; set; }
        public virtual ICollection<InvProductReceive> InvProductReceives { get; set; }
        public virtual ICollection<InvStoreOpening> InvStoreOpenings { get; set; }
        public virtual SlsDistributor SlsDistributor { get; set; }
        public virtual SlsOffice SlsOffice { get; set; }
        public virtual ICollection<SlsTransfer> SlsTransfers { get; set; }
        public virtual ICollection<SlsTransfer> SlsTransfers1 { get; set; }
        public virtual ICollection<InvStockInOut> InvStockInOuts { get; set; }
    }
}
