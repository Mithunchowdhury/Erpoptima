using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Sales
{
    public partial class SlsSalesReturn
    {
        public SlsSalesReturn()
        {
            this.SlsSalesReturnDetails = new List<SlsSalesReturnDetail>();
        }

        public int Id { get; set; }
        public int PartyType { get; set; }
        public Nullable<int> Party { get; set; }
        public string RefNo { get; set; }
        public string Reason { get; set; }
        public Nullable<decimal> AdjustedAmount { get; set; }
        public int SecCompanyId { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual SecCompany SecCompany { get; set; }
        public virtual SecUser SecUser { get; set; }
        public virtual SecUser SecUser1 { get; set; }
        public virtual ICollection<SlsSalesReturnDetail> SlsSalesReturnDetails { get; set; }
    }
}
