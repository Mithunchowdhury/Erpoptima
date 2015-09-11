using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Optima.Areas.Sales.ViewModel
{
    public class CollectionTargetViewModel
    {
        //
        // GET: /Sales/CollectionTargetViewModel/
        public int Id { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string Name { get; set; }
        public int HrmEmployeeId { get; set; }
        public decimal TargetCollectionAmount { get; set; }
        public string Remarks { get; set; }
        public int SecCompanyId { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}