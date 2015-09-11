using ERPOptima.Model.HRM;
using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Sales
{
    public partial class SlsSalesTarget
    {
        public SlsSalesTarget()
        {
            this.SlsSalesTargetDetails = new List<SlsSalesTargetDetail>();
        }

        public int Id { get; set; }
        public string RefNo { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int HrmEmployeeId { get; set; }
        public Nullable<decimal> TargetSalesAmount { get; set; }
        public string Remarks { get; set; }
        public int SecCompanyId { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual HrmEmployee HrmEmployee { get; set; }
        public virtual SecCompany SecCompany { get; set; }
        public virtual SecUser SecUser { get; set; }
        public virtual SecUser SecUser1 { get; set; }
        public virtual ICollection<SlsSalesTargetDetail> SlsSalesTargetDetails { get; set; }
    }

    






}
