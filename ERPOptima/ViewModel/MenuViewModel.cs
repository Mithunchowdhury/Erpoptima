using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Optima.ViewModel
{
    public class MenuViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }

        public int? SecResourcesId { get; set; }

        public int R_Value { get; set; }

        public int? SecModuleId { get; set; }
    }
}