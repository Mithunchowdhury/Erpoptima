using ERPOptima.Model.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;


namespace ERPOptima.Model.Sales
{
    public partial class SlsDefectDetail
    {
        public int Id { get; set; }
        public int SlsDefectId { get; set; }
        public int SlsProductId { get; set; }
        public decimal Quantity { get; set; }
        public int SlsUnitId { get; set; }
        public string Reason { get; set; }
        public decimal ReplacedQuantity { get; set; }
        public decimal AdjustedAmount { get; set; }
        public virtual SlsDefect SlsDefect { get; set; }
        public virtual SlsProduct SlsProduct { get; set; }
        public virtual SlsUnit SlsUnit { get; set; }
    }
    
}
