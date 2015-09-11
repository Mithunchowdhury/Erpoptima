using ERPOptima.Data.Infrastructure;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales.Repository
{
    #region Interface

    public interface IThanaRepository : IRepository<SlsThana>
    {
        IList<SlsThana> GetCostCenters(int companyId);
        int AddEntity(SlsThana objSlsThana);
        SlsThana GetCostCenterById(long nullable);
        IList<SlsThana> GetThanasForRegion(int regionId);
        IList<SlsThana> GetThanasForOffice(int officeId);
        IList<SlsThana> GetThanasForDistrict(int districtId);
    }

    #endregion

    public class ThanaRepository : BaseRepository<SlsThana>, IThanaRepository
    {
        public ThanaRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public IList<SlsThana> GetCostCenters(int companyId)
        {
            return DataContext.SlsThanas.ToList();
        }
        public int AddEntity(SlsThana objSlsThana)
        {
            int Id = 1;
            SlsThana last = DataContext.SlsThanas.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objSlsThana.Id = Id;
            base.Add(objSlsThana);
            return Id;

        }

        public SlsThana GetCostCenterById(long nullable)
        {
            var a = from v in DataContext.SlsThanas
                    where v.Id == nullable
                    select v;
            return DataContext.SlsThanas.Where(x => x.Id == nullable).FirstOrDefault();
        }

        public IList<SlsThana> GetThanasForRegion(int regionId)
        {
            IList<SlsThana> list = new List<SlsThana>();
            DataTable dt = null;
            try
            {
                dt = new DataTable();
                SqlParameter[] paramsToStore = new SqlParameter[1];
                paramsToStore[0] = new SqlParameter("@RegionId", SqlDbType.Int);
                paramsToStore[0].Value = regionId;

                dt = GetFromStoredProcedure(SPList.Thana.GetThanasForRegion, paramsToStore);
            }
            catch (Exception ex)
            {
                //log exception.
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                list = dt.DataTableToList<SlsThana>();
            }
            return list;
        }

        public IList<SlsThana> GetThanasForOffice(int officeId)
        {
            IList<SlsThana> list = new List<SlsThana>();
            DataTable dt = null;
            try
            {
                dt = new DataTable();
                SqlParameter[] paramsToStore = new SqlParameter[1];
                paramsToStore[0] = new SqlParameter("@OfficeId", SqlDbType.Int);
                paramsToStore[0].Value = officeId;

                dt = GetFromStoredProcedure(SPList.Thana.GetThanasForOffice, paramsToStore);
            }
            catch (Exception ex)
            {
                //log exception.
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                list = dt.DataTableToList<SlsThana>();
            }
            return list;
        }

        public IList<SlsThana> GetThanasForDistrict(int districtId)
        {
            IList<SlsThana> list = new List<SlsThana>();
            DataTable dt = null;
            try
            {
                dt = new DataTable();
                SqlParameter[] paramsToStore = new SqlParameter[1];
                paramsToStore[0] = new SqlParameter("@DistrictId", SqlDbType.Int);
                paramsToStore[0].Value = districtId;

                dt = GetFromStoredProcedure(SPList.Thana.GetThanasForDistrict, paramsToStore);
            }
            catch (Exception ex)
            {
                //log exception.
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                list = dt.DataTableToList<SlsThana>();
            }
            return list;
        }

    }
}
