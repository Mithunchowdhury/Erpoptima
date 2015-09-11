using ERPOptima.Model.HRM;
using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Sales
{
    public partial class SlsIncentive
    {
        public int Id { get; set; }
        public int HrmEmployeeId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal Commission { get; set; }
        public decimal AmountPaid { get; set; }
        public System.DateTime PaymentDate { get; set; }
        public string Remarks { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual HrmEmployee HrmEmployee { get; set; }
        public virtual SecUser SecUser { get; set; }
        public virtual SecUser SecUser1 { get; set; }
    }
}
