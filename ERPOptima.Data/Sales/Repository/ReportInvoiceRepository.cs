using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales.Repository
{
    public interface IReportInvoiceRepository : IRepository<SlsDelivery>
    {
        IList<SlsDelivery> GetAll(int companyId);
    }
    public class ReportInvoiceRepository: BaseRepository<SlsDelivery>, IReportInvoiceRepository
    {

        public ReportInvoiceRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public IList<SlsDelivery> GetAll(int companyId)
        {

            return DataContext.SlsDeliveries.ToList();
            //return DataContext.SlsDeliveries.Where(p => p.SecCompanyId == companyId && p.IsProduct == true).ToList();

        }

    }
}
