using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Sales
{
    public partial class SlsCorporateClient
    {
        public SlsCorporateClient()
        {
            this.SlsDefects = new List<SlsDefect>();
            this.SlsRouteDetails = new List<SlsRouteDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int SlsDistrictId { get; set; }
        public int SlsCorporateTypeId { get; set; }
        public string Code { get; set; }
        public string BusinessType { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPersonEmail { get; set; }
        public string ContactPersonPhone { get; set; }
        public Nullable<int> ExecutiveName { get; set; }
        public string BankName { get; set; }
        public string BankAccount { get; set; }
        public Nullable<decimal> DefaultCreditLimit { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual SecUser SecUser { get; set; }
        public virtual SecUser SecUser1 { get; set; }
        public virtual SlsCorporateType SlsCorporateType { get; set; }
        public virtual SlsDistrict SlsDistrict { get; set; }
        public virtual ICollection<SlsDefect> SlsDefects { get; set; }
        public virtual ICollection<SlsRouteDetail> SlsRouteDetails { get; set; }
    }
}
