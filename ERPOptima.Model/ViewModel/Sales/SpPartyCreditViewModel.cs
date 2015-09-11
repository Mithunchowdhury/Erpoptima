using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Model.ViewModel.Sales
{
    public class SpPartyCreditViewModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal CurrentCredit { get; set; }
    }
}
