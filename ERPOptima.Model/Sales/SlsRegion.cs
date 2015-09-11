using ERPOptima.Model.HRM;
using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Sales
{
    public partial class SlsRegion
    {
        public SlsRegion()
        {
            this.SlsGeneralDiscounts = new List<SlsGeneralDiscount>();          
            this.SlsOffices = new List<SlsOffice>();
            this.SlsProductDiscounts = new List<SlsProductDiscount>();
            this.SlsPromotionalOffers = new List<SlsPromotionalOffer>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Head { get; set; }
        public string Remarks { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual HrmEmployee HrmEmployee { get; set; }
        public virtual SecUser SecUser { get; set; }
        public virtual SecUser SecUser1 { get; set; }
        public virtual ICollection<SlsGeneralDiscount> SlsGeneralDiscounts { get; set; }       
        public virtual ICollection<SlsOffice> SlsOffices { get; set; }
        public virtual ICollection<SlsProductDiscount> SlsProductDiscounts { get; set; }
        public virtual ICollection<SlsPromotionalOffer> SlsPromotionalOffers { get; set; }
    }
}
