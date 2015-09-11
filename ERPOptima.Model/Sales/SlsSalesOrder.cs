using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Sales
{
    public partial class SlsSalesOrder
    {
        public SlsSalesOrder()
        {
            this.SlsDeliveries = new List<SlsDelivery>();
            this.SlsSalesOrderApprovals = new List<SlsSalesOrderApproval>();
            this.SlsSalesOrderDetails = new List<SlsSalesOrderDetail>();
        }

        public int Id { get; set; }
        public string RefNo { get; set; }
        public Nullable<int> SlsOfficeId { get; set; }
        public int PartyType { get; set; }
        public Nullable<int> Party { get; set; }
        public System.DateTime PreferredDeliveryDate { get; set; }
        public Nullable<int> SlsCorporateSalesApplicationId { get; set; }
        public int TransactionType { get; set; }
        public string TransactionRefNo { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public Nullable<decimal> Advance { get; set; }
        public string BankName { get; set; }
        public string Remarks { get; set; }
        public int SecCompnayId { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> ApprovalStatus { get; set; }
        public Nullable<int> SalesType { get; set; }
        public string OptionalPartyName { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public Nullable<int> HrmEmployeeId { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual SecUser SecUser { get; set; }
        public virtual SecUser SecUser1 { get; set; }
        public virtual SlsCorporateSalesApplication SlsCorporateSalesApplication { get; set; }
        public virtual ICollection<SlsDelivery> SlsDeliveries { get; set; }
        public virtual SlsOffice SlsOffice { get; set; }
        public virtual ICollection<SlsSalesOrderApproval> SlsSalesOrderApprovals { get; set; }
        public virtual ICollection<SlsSalesOrderDetail> SlsSalesOrderDetails { get; set; }
    }
}
