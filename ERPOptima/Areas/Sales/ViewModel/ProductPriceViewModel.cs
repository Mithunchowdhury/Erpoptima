using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Optima.Areas.Sales.ViewModel
{
    public class ProductPriceViewModel
    {
        //
        // GET: /Sales/ProductPriceViewModel/

        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int>SlsProductId { get; set; }
        public string Unit { get; set; }
        public Nullable<int> SlsUnitId { get; set; }
        public string Code { get; set; }
        public decimal FactoryCost { get; set; }
        public decimal MRP { get; set; }
        public decimal DistributorCommission { get; set; }
        public decimal RetailCommission { get; set; }
        public System.DateTime DeclarationDate { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

    }
}
