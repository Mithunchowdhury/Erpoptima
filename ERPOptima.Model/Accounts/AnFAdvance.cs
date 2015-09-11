using ERPOptima.Model.Common;
using ERPOptima.Model.HRM;
using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Model.Accounts
{
    public class AnFAdvance
    {
        public AnFAdvance()
        {
            this.AnFAdjustments = new List<AnFAdjustment>();
        }

        public int Id { get; set; }
        public string RefNo { get; set; }
        public int CmnFinancialYearId { get; set; }
        public Nullable<int> AnFVoucherId { get; set; }
        public int HrmEmployeeId { get; set; }
        public int SecCompanyId { get; set; }
        public Nullable<int> AnFCostCenterId { get; set; }
        public System.DateTime Date { get; set; }
        public decimal Advance { get; set; }
        public string Purpose { get; set; }
        public Nullable<System.DateTime> ProposedReturnDate { get; set; }
        public int Status { get; set; }
        public string FIleLocation { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual ICollection<AnFAdjustment> AnFAdjustments { get; set; }
        public virtual AnFCostCenter AnFCostCenter { get; set; }
        public virtual AnFVoucher AnFVoucher { get; set; }
        public virtual CmnFinancialYear CmnFinancialYear { get; set; }
        public virtual HrmEmployee HrmEmployee { get; set; }
        public virtual SecCompany SecCompany { get; set; }
    }
}
