using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Sales
{
    public partial class SlsPromotionalOffer
    {
        public SlsPromotionalOffer()
        {
            this.SlsPromotionalOfferDetails = new List<SlsPromotionalOfferDetail>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int SlsRegionId { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public string Remarks { get; set; }
        public bool IsValid { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual SecUser SecUser { get; set; }
        public virtual SecUser SecUser1 { get; set; }
        public virtual ICollection<SlsPromotionalOfferDetail> SlsPromotionalOfferDetails { get; set; }
        public virtual SlsRegion SlsRegion { get; set; }
    }
}
