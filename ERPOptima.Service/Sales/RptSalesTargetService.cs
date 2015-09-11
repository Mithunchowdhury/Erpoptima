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
   public interface IRptSalesTargetService
    {
       DataTable GetSalesTargetReport(int? EmployeeId);
    }
   public class RptSalesTargetService : IRptSalesTargetService
   {
       private IRptSalesTargetRepository _rptSalesTargetRepository;
       private IUnitOfWork _UnitOfWork;

       public RptSalesTargetService(IRptSalesTargetRepository rptSalesTargetRepository, IUnitOfWork unitOfWork)
       {
           this._rptSalesTargetRepository = rptSalesTargetRepository;
           this._UnitOfWork = unitOfWork;

       }
       public DataTable GetSalesTargetReport(int? EmployeeId)
       {
           DataTable dt = new DataTable();
           DateTime ToDay = DateTime.Now;
           SqlParameter[] paramsToStore = new SqlParameter[3];
           paramsToStore[0] = new SqlParameter("@EmployeeId", EmployeeId);
           paramsToStore[1] = new SqlParameter("@Year", ToDay.Year);
           paramsToStore[2] = new SqlParameter("@Month", ToDay.Month-1);



           try
           {
               dt = _rptSalesTargetRepository.GetFromStoredProcedure(SPList.Report.RptSalesTargetCollection, paramsToStore);
           }
           catch (Exception)
           {
           }

           return dt;
       }
   }
}
