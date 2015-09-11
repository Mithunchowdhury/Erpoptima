using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Sales
{
    public partial class SlsAreaConfigurationDetail
    {
        public int Id { get; set; }
        public string BasedOn { get; set; }
        public int RefId { get; set; }
        public int SlsAreaConfigurationId { get; set; }
    }
}
