using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Optima.Areas.Inventory.ViewModels
{
    public class RequisitionDetailViewModel
    {

        public int Id { get; set; }
        public int InvRequisitionId { get; set; }
        public int SlsProductId { get; set; }
        public decimal RequiredQuantity { get; set; }
        public int SlsUnitId { get; set; }
        public string ProductName { get; set; }
        public string UnitName { get; set; }



    }
}