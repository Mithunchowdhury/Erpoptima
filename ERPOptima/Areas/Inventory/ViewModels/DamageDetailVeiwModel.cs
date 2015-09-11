using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Optima.Areas.Inventory.ViewModels
{
    public class DamageDetailVeiwModel
    {
        public int Id { get; set; }
        public int InvDamageId { get; set; }
        public int SlsProductId { get; set; }
        public decimal Quantity { get; set; }
        public int SlsUnitsId { get; set; }
        public string Reason { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
     
    }
}