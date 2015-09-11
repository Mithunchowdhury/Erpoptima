using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Optima.Areas.Sales.ViewModel
{
    public class SlsProductUnitViewModel
    {

        public int Id { get; set; }
        public int SlsProductId { get; set; }
        public int SlsUnitId { get; set; }
        public string Unit { get; set; }
        public string ParentUnit { get; set; }
        public Nullable<int> ParentUnitId { get; set; }
        public Nullable<decimal> ConversionRate { get; set; }
        public string Remarks { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }






    }
}