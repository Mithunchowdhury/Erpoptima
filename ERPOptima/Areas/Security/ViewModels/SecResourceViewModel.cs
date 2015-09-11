using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Optima.Areas.Security.ViewModels
{
    public class SecResourceViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Read { get; set; }
        public bool Add { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
        public bool Print { get; set; }
    }
}