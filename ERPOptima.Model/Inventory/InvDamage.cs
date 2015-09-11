using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Inventory
{
    public partial class InvDamage
    {
       public InvDamage()
        {
            this.InvDamageApprovalProducts = new List<InvDamageApprovalProduct>();
            this.InvDamageApprovals = new List<InvDamageApproval>();
            this.InvDamageDetails = new List<InvDamageDetail>();
        }

        public int Id { get; set; }
        public string RefNo { get; set; }
        public Nullable<int> SecCompanyId { get; set; }

        public Nullable<int> InvStoreId { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> ApprovalStatus { get; set; }    
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual ICollection<InvDamageApprovalProduct> InvDamageApprovalProducts { get; set; }
        public virtual ICollection<InvDamageApproval> InvDamageApprovals { get; set; }
        public virtual ICollection<InvDamageDetail> InvDamageDetails { get; set; }

        public virtual InvStore InvStore { get; set; }


        public virtual SecCompany SecCompany { get; set; }
        public virtual SecUser SecUser { get; set; }
        public virtual SecUser SecUser1 { get; set; }
    }    
}

