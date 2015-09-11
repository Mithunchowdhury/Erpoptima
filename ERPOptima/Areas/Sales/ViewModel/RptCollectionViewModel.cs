using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Optima.Areas.Sales.ViewModel
{
    public class RptCollectionViewModel
    {
        
        
        public string PartyName { get; set; }
        public string TrType { get; set; }
        public decimal CollectedAmount { get; set; }        
        public string CollectedBy { get; set; }
        public DateTime CollectedDate { get; set; }
    }
}