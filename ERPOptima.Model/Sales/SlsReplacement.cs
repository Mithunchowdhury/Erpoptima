using ERPOptima.Model.Inventory;
using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Sales
{
    public partial class SlsReplacement
    {
        public SlsReplacement()
        {
            this.SlsReplacementDetails = new List<SlsReplacementDetail>();
        }

        public int Id { get; set; }
        public Nullable<int> SecCompanyId { get; set; }
        public string RefNo { get; set; }
        public Nullable<int> SlsSalesOrderId { get; set; }
        public System.DateTime Date { get; set; }
        public string Remarks { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual ICollection<SlsReplacementDetail> SlsReplacementDetails { get; set; }
    }
}
