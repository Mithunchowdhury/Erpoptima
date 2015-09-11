using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Sales
{
    public partial class SlsDistrict
    {
        public SlsDistrict()
        {
            this.SlsCorporateClients = new List<SlsCorporateClient>();
            this.SlsThanas = new List<SlsThana>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Nullable<int> SlsOfficeId { get; set; }
        public string Remarks { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual SecUser SecUser { get; set; }
        public virtual SecUser SecUser1 { get; set; }
        public virtual ICollection<SlsCorporateClient> SlsCorporateClients { get; set; }
        public virtual SlsOffice SlsOffice { get; set; }
        public virtual ICollection<SlsThana> SlsThanas { get; set; }
    }
}
