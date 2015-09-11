using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Optima.Areas.Sales.ViewModel
{
    public class RptMoneyReceiptViewModel
    {
        public int Id { get; set; }
        public string RefNo { get; set; }
        public int PaymentMode { get; set; }
        public int PartyType { get; set; }
        public int CollectedFrom { get; set; }
        public decimal Amount { get; set; }
        public int TransactionType { get; set; }
        public string TransactionRefNo { get; set; }
        public string BankName { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }


        public string PaymentModeName { get; set; }
        public string PartyName { get; set; }
     
    }
}