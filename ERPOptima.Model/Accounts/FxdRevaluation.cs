using ERPOptima.Model.Common;
using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Model.Accounts
{
    public partial class FxdRevaluation
    {
        public int Id { get; set; }
        public string RefNo { get; set; }
        public System.DateTime Date { get; set; }
        public int FxdAcquisitionId { get; set; }
        public decimal Presentvalue { get; set; }
        public decimal Revalue { get; set; }
        public Nullable<int> AnfVoucherId { get; set; }
        public int CmnFinancialYearId { get; set; }
        public int SecCompanyId { get; set; }
        public string Remarks { get; set; }
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
