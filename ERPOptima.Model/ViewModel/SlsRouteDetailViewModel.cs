using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Model.ViewModel
{
    public class SlsRouteDetailViewModel
    {
        public int Id { get; set; }
        public int SlsRouteId { get; set; }
        public int PartyType { get; set; }
        public Nullable<int> SlsDistributorId { get; set; }
        public Nullable<int> SlsRetailerId { get; set; }
        public Nullable<int> SlsCorporateClientId { get; set; }
        public Nullable<int> SlsDealerId { get; set; }


        public string Name { get; set; }
        public string Code { get; set; }
    }
}
