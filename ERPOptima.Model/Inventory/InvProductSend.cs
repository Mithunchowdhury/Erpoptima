using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Inventory
{
    public partial class InvProductSend
    {
        public InvProductSend()
        {
            this.InvProductSendDetails = new List<InvProductSendDetail>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string GatePassNo { get; set; }
        public string VehicleNo { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual ICollection<InvProductSendDetail> InvProductSendDetails { get; set; }
        public virtual SecUser SecUser { get; set; }
        public virtual SecUser SecUser1 { get; set; }
    }
}
