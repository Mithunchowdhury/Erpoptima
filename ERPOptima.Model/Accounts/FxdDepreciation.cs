using ERPOptima.Model.Common;
using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Model.Accounts
{
    public partial class FxdDepreciation
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public Nullable<int> FxdAcquisitionId { get; set; }
        public int CmnFinancialYearId { get; set; }
        public int SecCompanyId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal DepreciationRate { get; set; }
        public int DepreciationMethod { get; set; }
        public System.DateTime Date { get; set; }
        public decimal Depreciation { get; set; }
        public string Remarks { get; set; }
        public decimal WrittenDownValue { get; set; }
        public Nullable<int> AnfVoucherId { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual AnFVoucher AnFVoucher { get; set; }
        public virtual CmnFinancialYear CmnFinancialYear { get; set; }
        public virtual FxdAcquisition FxdAcquisition { get; set; }
        public virtual SecCompany SecCompany { get; set; }
    }
}
