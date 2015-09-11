using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Inventory
{
    public partial class InvProductSendDetail
    {
        public InvProductSendDetail()
        {
            this.InvProductSendDetailItems = new List<InvProductSendDetailItem>();
        }

        public int Id { get; set; }
        public int InvProductSendsId { get; set; }
        public int InvRequisitionsId { get; set; }
        public int InvIssuesId { get; set; }
        public int SlsOfficesId { get; set; }
        public string ChallanNo { get; set; }
        public virtual InvIssue InvIssue { get; set; }
        public virtual ICollection<InvProductSendDetailItem> InvProductSendDetailItems { get; set; }
        public virtual InvProductSend InvProductSend { get; set; }
        public virtual InvRequisition InvRequisition { get; set; }
        public virtual SlsOffice SlsOffice { get; set; }
    }
}
