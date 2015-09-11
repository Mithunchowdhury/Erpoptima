using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Sales
{
    public partial class SlsThana
    {
        public SlsThana()
        {
            this.SlsAreas = new List<SlsArea>();
            this.SlsDealers = new List<SlsDealer>();
            this.SlsDealers1 = new List<SlsDealer>();
            this.SlsDistributors = new List<SlsDistributor>();
        }

        public int Id { get; set; }
        public int SlsDistrictId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Remarks { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual SecUser SecUser { get; set; }
        public virtual SecUser SecUser1 { get; set; }
        public virtual ICollection<SlsArea> SlsAreas { get; set; }
        public virtual ICollection<SlsDealer> SlsDealers { get; set; }
        public virtual ICollection<SlsDealer> SlsDealers1 { get; set; }
        public virtual ICollection<SlsDistributor> SlsDistributors { get; set; }
        public virtual SlsDistrict SlsDistrict { get; set; }
    }
}