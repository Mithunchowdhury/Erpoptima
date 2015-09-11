using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Model.ViewModel
{
    public class ChartViewModel
    {
        public decimal y { get; set; }
        public string label { get; set; }
        public string legendText { get; set; }
        public decimal per { get; set; }
    }

    public class ChartTargetSales
    {
        public IList<ChartViewModel> Targets { get; set; }
        public IList<ChartViewModel> Sales { get; set; }
    }

    public class ChartTargetCollections
    {
        public IList<ChartViewModel> Targets { get; set; }
        public IList<ChartViewModel> Collections { get; set; }
    }

    public class ChartSalesCollections
    {
        public IList<ChartViewModel> Collections { get; set; }
        public IList<ChartViewModel> Sales { get; set; }
    }


    public class ChartRegionWiseSales
    {
        public IList<ChartViewModel> Sales { get; set; }
    }
        

    public class MngtViewModel
    {
        public string ClassWithBgColor { get; set; }
        public string CompanyName { get; set; }
        public string TodaysDate { get; set; }
        public decimal Sales { get; set; }
        public decimal Collections { get; set; }
        public decimal Credit { get; set; }
        public decimal CreditTillDate { get; set; }
    }
    

}
