using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Optima.Areas.Accounts.ViewModel
{
    public class ReportAnFCostCentreViewModel
    {
        
        public int Id { get; set; }
        public string  Name { get; set; }
        public int CmnCompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string Remarks { get; set; }
        public string ContactNo { get; set; }
        public string ContactPerson { get; set; }
        public bool Status { get; set; }
        public bool IsDefault { get; set; }
        
    }
}