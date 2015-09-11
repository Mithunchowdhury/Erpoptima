using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Sales
{
    public partial class SlsRetailer
    {
        public SlsRetailer()
        {
            this.SlsDefects = new List<SlsDefect>();
            this.SlsRouteDetails = new List<SlsRouteDetail>();
        }

        public int Id { get; set; }
        public Nullable<int> SlsDistributorId { get; set; }
        public Nullable<int> SlsOfficeId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string ResponsiblePerson { get; set; }
        public string BankName { get; set; }
        public string BankAccount { get; set; }
        public Nullable<decimal> CreditLimit { get; set; }
        public int SecCompanyId { get; set; }
        public bool IsAllCompany { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual SecCompany SecCompany { get; set; }
        public virtual SecUser SecUser { get; set; }
        public virtual SecUser SecUser1 { get; set; }
        public virtual ICollection<SlsDefect> SlsDefects { get; set; }
        public virtual SlsDistributor SlsDistributor { get; set; }
        public virtual SlsOffice SlsOffice { get; set; }
        public virtual ICollection<SlsRouteDetail> SlsRouteDetails { get; set; }
    }
}
