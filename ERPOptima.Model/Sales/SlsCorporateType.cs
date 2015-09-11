using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Sales
{
    public partial class SlsCorporateType
    {
        public SlsCorporateType()
        {
            this.SlsCorporateClients = new List<SlsCorporateClient>();
        }

        public int Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual SecUser SecUser { get; set; }
        public virtual SecUser SecUser1 { get; set; }
        public virtual ICollection<SlsCorporateClient> SlsCorporateClients { get; set; }
    }
}
