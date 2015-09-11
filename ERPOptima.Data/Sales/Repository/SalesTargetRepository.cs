using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Sales;
using ERPOptima.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales.Repository
{
    #region interface
    public interface ISalesTargetRepository : IRepository<SlsSalesTarget>
    {

        List<SlsSalesTarget> GetTargetsByYear(int companyId, int month, int year);
        List<SlsSalesTarget> GetTargetsByYearNEmployeeId(int companyId, int month, int year, int employeeId);
        int GetRefNo(int companyId);
        int GetLastId(SlsSalesTarget objSlsSalesTarget);
        List<TargetList> GetTargetList(int companyId);
    }


    #endregion
    public class SalesTargetRepository : BaseRepository<SlsSalesTarget>, ISalesTargetRepository
    {

        public SalesTargetRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
            {

            }

        public int GetRefNo(int companyId)
        {

            int SL = 1;
            SlsSalesTarget last = DataContext.SlsSalesTargets.Where(r => r.SecCompanyId == companyId).OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                SL = int.Parse(last.RefNo.Split('/')[1]) + 1;

            }
            return SL;

        }//end of GetLastCode

        public int GetLastId(SlsSalesTarget objSlsSalesTarget)
        {
            int Id = 1;
            SlsSalesTarget last = DataContext.SlsSalesTargets.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
       
            return Id;

        }


        public List<SlsSalesTarget> GetTargetsByYear(int companyId, int month, int year)
        {
            return DataContext.SlsSalesTargets.Where(r => r.SecCompanyId == companyId && r.Month == month && r.Year == year).ToList();

            //var list = (from t in DataContext.SlsSalesTargets
            //            where t.SecCompanyId == companyId && t.Month == month && t.Year == year
            //            select new SlsSalesTarget
            //                     {
            //                         Id = t.Id,
            //                         RefNo = t.RefNo,
            //                         Month = t.Month,
            //                         Year = t.Year,
            //                         TargetSalesAmount = t.TargetSalesAmount,
            //                         Remarks = t.Remarks

            //                     }).ToList();
            //return list;
        }
        public List<SlsSalesTarget> GetTargetsByYearNEmployeeId(int companyId, int month, int year, int employeeId)
        {
            return DataContext.SlsSalesTargets.Where(r => r.SecCompanyId == companyId && r.Month == month && r.Year == year && r.HrmEmployeeId == employeeId).ToList();


        }

        public List<TargetList> GetTargetList(int companyId)
        {           
            var list = (from t in DataContext.SlsSalesTargets
                        join e in DataContext.HrmEmployees on t.HrmEmployeeId equals e.Id
                        where t.SecCompanyId == companyId
                        select new TargetList
                              {
                                  Id = t.Id,
                                  RefNo = t.RefNo,
                                  Month = t.Month,
                                  Year=t.Year,
                                  HrmEmployeeId=t.HrmEmployeeId,
                                  Employee = e.Name,
                                  TargetSalesAmount=t.TargetSalesAmount,
                                  Remarks=t.Remarks,
                                  SecCompanyId = t.SecCompanyId,
                                  CreatedBy=t.CreatedBy,
                                  CreatedDate=t.CreatedDate,
                                  ModifiedBy=t.ModifiedBy,
                                  ModifiedDate = t.ModifiedDate
                              }).ToList();

            foreach (TargetList tl in list)
            {
                tl.MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(tl.Month) + " " + tl.Year;
            }

            return list;          


        }





    }
}
