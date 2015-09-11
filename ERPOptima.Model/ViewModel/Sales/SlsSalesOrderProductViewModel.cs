using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Model.ViewModel.Sales
{
    public class SlsSalesOrderProductViewModel
    {
      public int Id { get; set; }
      public int SlsSalesOrderId { get; set; }
      public int SlsProductId { get; set; }
	  public string SlsProductName { get; set; }
	  public decimal SalesOrderQuantity { get; set; }	  
      public int SlsUnitId { get; set; }
	  public string SlsUnitName { get; set; }
	  public decimal Rate { get; set; }	  
	  public decimal Price { get; set; }	  
	  public decimal Discount { get; set; }
      public decimal Total { get; set; }	  
	  
    }
}
