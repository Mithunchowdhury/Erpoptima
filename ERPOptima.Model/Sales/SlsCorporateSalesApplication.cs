using ERPOptima.Model.HRM;
using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Sales
{
    public partial class SlsCorporateSalesApplication
    {
        public SlsCorporateSalesApplication()
        {
            this.SlsCorporateSalesApplicationDetails = new List<SlsCorporateSalesApplicationDetail>();
            this.SlsCorporateSalesApprovals = new List<SlsCorporateSalesApproval>();
            this.SlsSalesOrders = new List<SlsSalesOrder>();
        }

        public int Id { get; set; }
        public string RefNo { get; set; }
        public int SlsCorporateClientId { get; set; }
        public decimal SalesAmount { get; set; }
        public int PaymentMode { get; set; }
        public int CreditTerms { get; set; }
        public decimal AdvanceAmount { get; set; }
        public string ExtraExpensePerson { get; set; }
        public string Designation { get; set; }
        public string Phone { get; set; }
        public Nullable<decimal> Percentage { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<int> HrmEmployeeId { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> ApprovalStatus { get; set; }
        public Nullable<int> SecCompanyId { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual HrmEmployee HrmEmployee { get; set; }
        public virtual SecUser SecUser { get; set; }
        public virtual SecUser SecUser1 { get; set; }
        public virtual ICollection<SlsCorporateSalesApplicationDetail> SlsCorporateSalesApplicationDetails { get; set; }
        public virtual ICollection<SlsCorporateSalesApproval> SlsCorporateSalesApprovals { get; set; }
        public virtual ICollection<SlsSalesOrder> SlsSalesOrders { get; set; }
        public virtual SecCompany SecCompany { get; set; }
    }
}
