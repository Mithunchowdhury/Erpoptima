using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Service.Sales
{
    public interface IReportInvoiceService
    {
        IList<SlsDelivery> GetAll(int companyId);
    }
    public class ReportInvoiceService: IReportInvoiceService
    {
        private IReportInvoiceRepository _reportInvoiceRepository;
        private IUnitOfWork _UnitOfWork;

         public ReportInvoiceService(IReportInvoiceRepository ReportInvoiceRepository, IUnitOfWork unitOfWork)
        {
            this._reportInvoiceRepository = ReportInvoiceRepository;
            this._UnitOfWork = unitOfWork;
        }

         public IList<SlsDelivery> GetAll(int companyId)
         {

             return _reportInvoiceRepository.GetAll(companyId);
         }
    }
}
