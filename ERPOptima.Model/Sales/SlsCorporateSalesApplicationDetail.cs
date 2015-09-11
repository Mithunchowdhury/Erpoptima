using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Sales
{
    public partial class SlsCorporateSalesApplicationDetail
    {
        public int Id { get; set; }
        public int SlsCorporateSalesApplicationId { get; set; }
        public int SlsProductId { get; set; }
        public decimal AppliedPercentage { get; set; }
        public Nullable< decimal> ApprovedPercentage { get; set; }
        public virtual SlsCorporateSalesApplication SlsCorporateSalesApplication { get; set; }
        public virtual SlsProduct SlsProduct { get; set; }
    }


    public partial class CorporateSalesDetail
    {
        public int Id { get; set; }
        public int SlsCorporateSalesApplicationId { get; set; }
        public int SlsProductId { get; set; }
        public decimal AppliedPercentage { get; set; }
        public Nullable<decimal> ApprovedPercentage { get; set; }
        public string ProductName { get; set; }

    }







}
