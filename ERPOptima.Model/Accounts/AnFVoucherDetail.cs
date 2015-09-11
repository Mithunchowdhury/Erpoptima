using ERPOptima.Model.Common;
using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Accounts
{
    public partial class AnFVoucherDetail
    {
        public int Id { get; set; }
        public int AnFVoucherId { get; set; }
        public long AnFChartOfAccountId { get; set; }
        public int VoucherSerial { get; set; }
        public string SubVoucherNumber { get; set; }
        public Nullable<decimal> Debit { get; set; }
        public Nullable<decimal> Credit { get; set; }
        public string ShortNarration { get; set; }
        public virtual AnFVoucher AnFVoucher { get; set; }
    }
}
