using ERPOptima.Model.HRM;
using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Sales
{
    public partial class SlsAreaConfiguration
    {
        public int Id { get; set; }
        public int HrmEmployeeId { get; set; }
        public Nullable<bool> IsRegionBased { get; set; }
        public Nullable<bool> IsOfficeBased { get; set; }
        public Nullable<bool> IsDistrictBased { get; set; }
        public Nullable<bool> IsThanaBased { get; set; }
        public Nullable<bool> IsAreaBased { get; set; }
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
