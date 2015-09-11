using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Optima.Areas.Sales.ViewModel
{
    public class ProductReceiveDetailViewModel
    {
        public int Id { get; set; }
        public string PrName { get; set; }
        public string Uname { get; set; }
        public decimal IssuedQuantity { get; set; }
        public decimal ReceivedQuantity { get; set; }
        public string Remarks { get; set; }
    }
    public class ProductReceiveDistDetailViewModel
    {
        public int Id { get; set; }
        public string PrName { get; set; }
        public string Uname { get; set; }
        public decimal DeliveryQuantity { get; set; }
        public decimal ReceivedQuantity { get; set; }
        public string Remarks { get; set; }
    }
}