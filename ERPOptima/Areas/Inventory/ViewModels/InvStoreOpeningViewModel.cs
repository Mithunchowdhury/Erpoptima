using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Optima.Areas.Inventory.ViewModels
{
    public class InvStoreOpeningViewModel
    {

        public int Id { get; set; }
        public int InvStoreId { get; set; }
        public Nullable<int> SlsProductId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<int> SlsUnitId { get; set; }
        public string Unit { get; set; }
        public int CreatedBy { get; set; }        
        public Nullable<int> ModifiedBy { get; set; }






    }
}