using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Optima.Areas.Sales.ViewModel
{
    public class SalesReturnListViewModel
    {
        public System.DateTime Date { get; set; }
        public int Id { get; set; }
        public int PartyType { get; set; }
        public Nullable<int> Party { get; set; }
        public string RefNo { get; set; }
        public string Reason { get; set; }
        public Nullable<decimal> AdjustedAmount { get; set; }
        public int SecCompanyId { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
       
    }
}