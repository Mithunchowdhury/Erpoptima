using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Sales
{
    public partial class SlsRouteDetail
    {
        public int Id { get; set; }
        public int SlsRouteId { get; set; }
        public int PartyType { get; set; }
        public Nullable<int> SlsDistributorId { get; set; }
        public Nullable<int> SlsRetailerId { get; set; }
        public Nullable<int> SlsCorporateClientId { get; set; }
        public Nullable<int> SlsDealerId { get; set; }
        public virtual SlsCorporateClient SlsCorporateClient { get; set; }
        public virtual SlsDealer SlsDealer { get; set; }
        public virtual SlsDistributor SlsDistributor { get; set; }
        public virtual SlsRetailer SlsRetailer { get; set; }
        public virtual SlsRoute SlsRoute { get; set; }
    }
}
