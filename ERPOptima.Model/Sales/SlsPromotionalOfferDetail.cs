using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Sales
{
    public partial class SlsPromotionalOfferDetail
    {
        public int Id { get; set; }
        public int SlsPromotionalOfferId { get; set; }
        public int SlsProuctId { get; set; }
        public decimal Discount { get; set; }
        public virtual SlsProduct SlsProduct { get; set; }
        public virtual SlsPromotionalOffer SlsPromotionalOffer { get; set; }
    }
}
