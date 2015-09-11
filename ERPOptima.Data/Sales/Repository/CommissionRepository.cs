using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales.Repository
{
    public interface ICommissionRepository : IRepository<SlsCommission>
    {        
        DbSet<SlsCommission> GetTableContext();
    }
    public class CommissionRepository : BaseRepository<SlsCommission>, ICommissionRepository
    {
        public CommissionRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }


        public DbSet<SlsCommission> GetTableContext()
        {
            return DataContext.SlsCommissions;            
        }


    }

    public partial class SlsCommissionViewModel
    {
        public int Id { get; set; }
        public  Nullable<int> SlsDitributorId { get; set; }
        public  Nullable<int> SlsDealerId { get; set; }
        public int MonthFrom { get; set; }
        public int MonthTo { get; set; }
        public int YearFrom { get; set; }
        public int YearTo { get; set; }
        public decimal NetSaleAmount { get; set; }
        public decimal CommissionPercentage { get; set; }
        public decimal Commission { get; set; }
        public System.DateTime Date { get; set; }
        public string ChequeNo { get; set; }
        public string Bank { get; set; }
        public string Remarks { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }


        public System.DateTime From { get; set; }
        public System.DateTime To { get; set; }
        public int Party { get; set; }
        public int PartyType { get; set; }
    }

    
}
