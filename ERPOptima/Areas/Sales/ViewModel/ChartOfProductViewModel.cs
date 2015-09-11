using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Optima.Areas.Sales.ViewModel
{
    public class ChartOfProductViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsProduct { get; set; }
        public bool NoCredit { get; set; }
        public Nullable<int> SlsProductId { get; set; }
        public int Level { get; set; }
        public string Description { get; set; }
        


    }
}