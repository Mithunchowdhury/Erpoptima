using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Sales
{
    public partial class SlsSalesOrderApproval
    {
        public int Id { get; set; }
        public int SlsSalesOrderId { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public int Action { get; set; }
        public string Comment { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual SecUser SecUser { get; set; }
        public virtual SecUser SecUser1 { get; set; }
        public virtual SlsSalesOrder SlsSalesOrder { get; set; }
    }
}
