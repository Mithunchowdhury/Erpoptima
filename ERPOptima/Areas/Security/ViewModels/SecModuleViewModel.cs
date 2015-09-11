using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Optima.Areas.Security.ViewModels
{
    public class SecModuleDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime? InitialDate { get; set; }

        public string Status { get; set; }
    }
}