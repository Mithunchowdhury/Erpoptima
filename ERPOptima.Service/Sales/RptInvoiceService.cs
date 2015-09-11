using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Service.Sales
{
    public interface IRptInvoiceService
    {

        DataTable GetInvoiceList(string invoiceId);


    }
    public class RptInvoiceService : IRptInvoiceService
    {
        private IDistributorRepository _DistributorRepository;
        private IUnitOfWork _UnitOfWork;
        public RptInvoiceService(IDistributorRepository distributorRepository, IUnitOfWork unitOfWork)
        {
            this._DistributorRepository = distributorRepository;
            this._UnitOfWork = unitOfWork;

        }



        public DataTable GetInvoiceList(string invoiceId)
        {
            DataTable dt = new DataTable();

            SqlParameter[] paramsToStore = new SqlParameter[1];
            paramsToStore[0] = new SqlParameter("@InvoiceId", invoiceId);
           

            try
            {
                dt = _DistributorRepository.GetFromStoredProcedure(SPList.Report.RptGetInvoiceNo, paramsToStore);
            }
            catch (Exception)
            {
            }

            return dt;
        }



    }
}
