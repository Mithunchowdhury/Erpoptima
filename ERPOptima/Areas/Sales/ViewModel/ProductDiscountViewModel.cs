using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Optima.Areas.Sales.ViewModel
{
    public class ProductDiscountViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SlsRegionId { get; set; }
        public int SlsProuctId { get; set; }
        public decimal Discount { get; set; }
        public System.DateTime Date { get; set; }
        public string Remarks { get; set; }
        public bool Status { get; set; }

    }
}