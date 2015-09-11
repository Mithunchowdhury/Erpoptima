using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Optima.Areas.Accounts.ViewModel
{
    public class ReportAnFCostOfGoodSoldSearchVIewModel
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int SecCompanyId { get; set; }
    }

    public class ReportAnFCostOfGoodSoldResultVIewModel
    {
        public string particulars { get; set; }
        public int ScheduleNo { get; set; }
        public decimal CurMonth { get; set; }
        public decimal CurYear { get; set; }
        public decimal PrevYear { get; set; }
        public string PLHead { get; set; }
        public string PLHeadShortName { get; set; }
        public int SLNo { get; set; }
        public string Company { get; set; }
        public string Project { get; set; }

        public DateTime preDate { get; set; }
    }
}