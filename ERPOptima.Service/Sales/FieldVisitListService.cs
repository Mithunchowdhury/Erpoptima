using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Service.Sales
{
    
    
    public interface IFieldVisitListService
    {

        Collection<FieldVisitList> GetFieldVisitList(int employeeId, DateTime? startDate, DateTime? endDate);


    }
    public class FieldVisitListService : IFieldVisitListService
    {
        private IFieldVisitListRepository _FieldVisitListRepository;
        private IUnitOfWork _UnitOfWork;
        public FieldVisitListService(IFieldVisitListRepository fieldVisitListRepository, IUnitOfWork unitOfWork)
        {
            this._FieldVisitListRepository = fieldVisitListRepository;
            this._UnitOfWork = unitOfWork;

        }



        public Collection<FieldVisitList> GetFieldVisitList(int employeeId, DateTime? startDate, DateTime? endDate)
        {
            Collection<FieldVisitList> list = null;
            DataTable dt = new DataTable();

            SqlParameter[] paramsToStore = new SqlParameter[3];
            paramsToStore[0] = new SqlParameter("@EmployeeId", employeeId);
            paramsToStore[1] = new SqlParameter("@StartDate", startDate);
            paramsToStore[2] = new SqlParameter("@EndDate", endDate);


            try
            {
                dt = _FieldVisitListRepository.GetFromStoredProcedure(SPList.Report.GetSlsFieldVisits, paramsToStore);
            }
            catch (Exception ex)
            {
            }
            if (dt != null)
            {
                list = new Collection<FieldVisitList>();
                foreach (DataRow row in dt.Rows)
                {
                    list.Add((FieldVisitList)ERPOptima.Lib.Utilities.Helper.FillTo(row, typeof(FieldVisitList)));
                }
            }
            return list;
        }



    }

}
