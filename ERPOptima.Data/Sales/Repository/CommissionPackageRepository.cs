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
    public interface ICommissionPackageRepository : IRepository<SlsCommissionPackage>
    {        
        DbSet<SlsCommissionPackage> GetTableContext();
    }
    public class CommissionPackageRepository : BaseRepository<SlsCommissionPackage>, ICommissionPackageRepository
    {
        public CommissionPackageRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }


        public DbSet<SlsCommissionPackage> GetTableContext()
        {
            return DataContext.SlsCommissionPackages;            
        }


    }

    
}
