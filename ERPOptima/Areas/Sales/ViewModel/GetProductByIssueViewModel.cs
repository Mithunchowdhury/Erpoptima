using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Optima.Areas.Sales.ViewModel
{
    public class GetProductByIssueViewModel
    {
        public int Id { get; set; }
        public int InvIssueId { get; set; }
        public int SlsProductId { get; set; }
        public int SlsUnitId { get; set; }
        public decimal ReceivedQuantity { get; set; }
        public decimal IssuedQuantity { get; set; }
        public string PrName { get; set; }
        public string UName { get; set; }
        public string Remarks { get; set; }
        public string Challan { get; set; }
        public string RecRemarks { get; set; }
    }

    public class GetProductByDeliveryViewModel
    {
        public int Id { get; set; }
        public int SlsDeliveryId { get; set; }
        public int SlsProductId { get; set; }
        public int SlsUnitId { get; set; }
        public decimal ReceivedQuantity { get; set; }
        public decimal DeliveryQuantity { get; set; }
        public string PrName { get; set; }
        public string UName { get; set; }
        public string Remarks { get; set; }
        public string Challan { get; set; }
        public string RecRemarks { get; set; }
    }
}