using System;
using System.Collections.Generic;

namespace Optima.Areas.Accounts.ViewModel
{
    public class PaymentVoucherAccountNameAndCode
    {
        public long Id { get; set; }

        public string Particular { get; set; }

        public long code { get; set; }
    }

    public class CostCenterIdAndName
    {
        public long Id { get; set; }

        public string Name { get; set; }
    }

    public class VoucherDateAndType
    {
        public int Type { get; set; }

        public string Prefix { get; set; }

        public DateTime Date { get; set; }

        public int CmnBusinessId { get; set; }
    }

    public class VoucherDetailsViewModel
    {
        public long VoucherId { get; set; }

        public long AnFChartOfAccountId { get; set; }

        public long AcCode { get; set; }

        public int AnFCostCenterId { get; set; }

        public int CmnProjectId { get; set; }

        public string ShortNaration { get; set; }

        public string SubVoucherNumber { get; set; }

        public int VoucherSerial { get; set; }

        public int Debit { get; set; }

        public int Credit { get; set; }
    }

    public class VoucherViewModel
    {
        public int Id { get; set; }
        public DateTime Date
        { get; set; }

        public int Type { get; set; }

        public string VoucherNumber { get; set; }

        public string Naration { get; set; }

        public decimal TotalAmount { get; set; }

        public int CmnFinancialYearId { get; set; }
        public int CmnBusinessId { get; set; }

        public int CmnCompanyId { get; set; }

        private List<VoucherDetailsViewModel> vouchrerDetails = null;

        public List<VoucherDetailsViewModel> VouchrerDetails
        {
            get
            {
                if (vouchrerDetails == null)
                {
                    vouchrerDetails = new List<VoucherDetailsViewModel>();
                }
                return vouchrerDetails;
            }
            set
            {
                vouchrerDetails = value;
            }
        }
    }

    public class VoucherSearchResultViewModel
    {
        public long id { get; set; }

        public string VoucherNumber { get; set; }

        public DateTime Date { get; set; }

        public string DateString { get; set; }

        public double TotalAmount { get; set; }

        public string Naration { get; set; }

        public bool IsCancel { get; set; }

        public int CmnBusinessId { get; set; }


    }

    public class VoucherIdViewModel
    {
        public long id { get; set; }
    }

    public class VoucherSearchResultById
    {
        public string AnFChartOfAcountName { get; set; }

        public string AcCode { get; set; }

        public int  AnFChartOfAccountId{ get; set; }
        public int AnFCostCenterId { get; set; }
        public int CmnProjectId { get; set; }
        public string AnFCostCenterName { get; set; }

        public string CmnProjectName { get; set; }

        public string ShortNaration { get; set; }

        public string SubVoucherNumber { get; set; }

        public long VoucherSerial { get; set; }

        public decimal Debit { get; set; }

        public decimal Credit { get; set; }
    }
}