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
    public interface ISalesReturnListReportService
    {
        DataTable GetSalesReturnListReport(int salesRetunrId);
    }

   public class SalesReturnListReportService : ISalesReturnListReportService
   {
           private ISalesReturnRepository _salesReturnRepository;
           private IUnitOfWork _UnitOfWork;

           public SalesReturnListReportService(ISalesReturnRepository salesReturnRepository, IUnitOfWork unitOfWork)
       {
           this._salesReturnRepository = salesReturnRepository;
           this._UnitOfWork = unitOfWork;

       }
           public DataTable GetSalesReturnListReport(int salesRetunrId)
           {
               DataTable dt = new DataTable();

               SqlParameter[] paramsToStore = new SqlParameter[1];
               paramsToStore[0] = new SqlParameter("@id", salesRetunrId);

               try
               {
                   dt = _salesReturnRepository.GetFromStoredProcedure(SPList.Report.RptSalesReturnById, paramsToStore);
               }
               catch (Exception)
               {
               }

               return dt;
           }
   }
}
