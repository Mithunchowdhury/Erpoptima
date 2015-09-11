using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Sales
{
    public partial class SlsRoute
    {
        public SlsRoute()
        {
            this.SlsRouteDetails = new List<SlsRouteDetail>();
            this.SlsRoutePlanDetails = new List<SlsRoutePlanDetail>();
        }

        public int Id { get; set; }
        public int SlsOfficeId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Remarks { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual SecUser SecUser { get; set; }
        public virtual SecUser SecUser1 { get; set; }
        public virtual SlsOffice SlsOffice { get; set; }
        public virtual ICollection<SlsRouteDetail> SlsRouteDetails { get; set; }
        public virtual ICollection<SlsRoutePlanDetail> SlsRoutePlanDetails { get; set; }
    }
}
