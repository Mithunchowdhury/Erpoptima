using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Common
{
    public partial class CmnCurrency
    {       

        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public Nullable<int> CmnCountryId { get; set; }
        public Nullable<decimal> ExchangeRate { get; set; }
        public string Symbol { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual CmnCountry CmnCountry { get; set; }



	
    }
}
