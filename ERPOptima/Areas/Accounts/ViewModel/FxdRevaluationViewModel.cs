using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Optima.Areas.Accounts.ViewModel
{
    public class FxdRevaluationViewModel
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
        public string AssetName { get; set; }
    }
}