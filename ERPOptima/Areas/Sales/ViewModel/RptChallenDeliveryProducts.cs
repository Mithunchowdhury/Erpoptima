using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Optima.Areas.Sales.ViewModel
{
    public class RptChallenDeliveryProducts
    {
        public string Product { get; set; }
        public string Code { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public string ChallanNo { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int PartyName { get; set; }
        public string Address { get; set; }
        public string TrackNo { get; set; }
        public string PreparedBy { get; set; }
        public string ApprovedBy { get; set; }

    }
}