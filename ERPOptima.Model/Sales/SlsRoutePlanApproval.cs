using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Sales
{
    public partial class SlsRoutePlanApproval
    {
        public int Id { get; set; }
        public int SlsRoutePlanId { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public int Action { get; set; }
        public string Comment { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual SlsRoutePlan SlsRoutePlan { get; set; }
    }
}
