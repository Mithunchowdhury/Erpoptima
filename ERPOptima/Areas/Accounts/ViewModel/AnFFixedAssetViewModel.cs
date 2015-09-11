using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Optima.Areas.Accounts.ViewModel
{
    public class AnFFixedAssetViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Nullable<int> AnFChartOfAccountId { get; set; }
        public int CmnFinancialYearId { get; set; }
        public string Supplier { get; set; }
        public string Manufacturer { get; set; }
        public string ModelOrBatchNo { get; set; }
        public string SerialNo { get; set; }
        public System.DateTime WarrantyExpreDate { get; set; }
        public int Quantity { get; set; }
        public int SlsUnitId { get; set; }
        public decimal DepreciationRate { get; set; }
        public int DepreciationMethod { get; set; }
        public System.DateTime DrepreciationStartDate { get; set; }
        public System.DateTime DepresiationEndDate { get; set; }
        public decimal AcquisitionCost { get; set; }
        public bool Status { get; set; }
        public string Description { get; set; }
        public decimal TotalAcquisitionCost { get; set; }
        public int LifeTime { get; set; }
        public string fxdName { get; set; }       
        public int SecCompanyId { get; set; }
        public System.DateTime Date { get; set; }
        public string Remarks { get; set; }
       
    }
}