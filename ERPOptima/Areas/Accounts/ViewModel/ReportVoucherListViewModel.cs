using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ERPOptima.Web.Accounts.ViewModel
{
    public class ReportVoucherListViewModel
    {
        public long Id { get; set; }
        public string VoucherNumber { get; set; }

        public DateTime Date { get; set; }

        public long  TotalAmount { get; set; }

        public string Naration { get; set; }


        public string DateString
        {
            get {

                return Date.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            
            }

        }

    }
}