using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPOptima.Web.Accounts.ViewModel
{
    public class VoucherSearchViewModel
    {

        public int ReportType { get; set; }
        public DateTime? DateFrom { get; set; }

        public DateTime? ToDate { get; set; }

        public int? BusinessId { get; set; }
        public int? ProjectId { get; set; }

        public int? CostcenterId { get; set; }

        public int? VoucherTypeId { get; set; }

        

        
    }

    public class VoucherSearchForGridViewModel
    { 
        public int? VoucherTypeId { get; set; }
    
        public string VoucherNumber { get; set; }
        
        public DateTime? DateFrom { get; set; }

        public DateTime? ToDate { get; set; }

        public long? ProjectId { get; set; }

        public long? CostCenterId { get; set; }


    }
}