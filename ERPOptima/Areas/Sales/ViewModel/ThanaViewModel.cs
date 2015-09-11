using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Optima.Areas.Sales.ViewModel
{
    public class ThanaViewModel
    {
        public int Id { get; set; }
        public int SlsDistrictId { get; set; }
        public Nullable<int> SlsOfficeId { get; set; }
        public Nullable<int> SlsRegionId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Office { get; set; }
        public string Remarks { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}