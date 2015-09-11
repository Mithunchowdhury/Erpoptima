using ERPOptima.Model.Common;
using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Model.Accounts
{
    public partial class FxdAcquisition
    {
        public FxdAcquisition()
        {
            this.FxdDepreciations = new List<FxdDepreciation>();
            this.FxdDisposals = new List<FxdDisposal>();
            this.FxdRevaluations = new List<FxdRevaluation>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int FxdAssetId { get; set; }
        public int CmnFinancialYearId { get; set; }
        public System.DateTime Date { get; set; }
        public string Supplier { get; set; }
        public string Manufacturer { get; set; }
        public string ModelOrBatchNo { get; set; }
        public string SerialNo { get; set; }
        public Nullable<System.DateTime> WarrantyExpreDate { get; set; }
        public int Quantity { get; set; }
        public int SlsUnitId { get; set; }
        public decimal DepreciationRate { get; set; }
        public int DepreciationMethod { get; set; }
        public Nullable<System.DateTime> DrepreciationStartDate { get; set; }
        public Nullable<System.DateTime> DepresiationEndDate { get; set; }
        public decimal AcquisitionCost { get; set; }
        public bool Status { get; set; }
        public string Description { get; set; }
        public decimal TotalAcquisitionCost { get; set; }
        public Nullable<int> AnfVoucherId { get; set; }
        public Nullable<int> LifeTime { get; set; }
        public string Location { get; set; }
        public int SecCompanyId { get; set; }
        public string Remarks { get; set; }
        public string FileLocation { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual AnFVoucher AnFVoucher { get; set; }
        public virtual CmnFinancialYear CmnFinancialYear { get; set; }
        public virtual FxdAsset FxdAsset { get; set; }
        public virtual SecCompany SecCompany { get; set; }
        public virtual ICollection<FxdDepreciation> FxdDepreciations { get; set; }
        public virtual ICollection<FxdDisposal> FxdDisposals { get; set; }
        public virtual ICollection<FxdRevaluation> FxdRevaluations { get; set; }
    }
}
