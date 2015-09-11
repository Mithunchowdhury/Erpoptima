using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Common.Repository
{

    #region Interface

    public interface ICmnBusinessRepository : IRepository<CmnBusiness>
    {
        //IList<CmnBusiness> GetCmnBusinesses(int companyId);
        //CmnBusiness GetLastCode(int companyId);
        int AddEntity(CmnBusiness objCmnBusiness);



        CmnBusiness GetCmnBusinessById(long nullable);
    }

    #endregion


    //public class CmnBusinessRepository : BaseRepository<CmnBusiness>, ICmnBusinessRepository
    //{
    //    public CmnBusinessRepository(IDatabaseFactory databaseFactory)
    //        : base(databaseFactory)
    //    {

    //    }
    //    //public IList<CmnBusiness> GetCmnBusinesses(int companyId)
    //    //{
    //    //    return DataContext.CmnBusinesses.Where(ac => ac.CmnCompanyId == companyId).ToList();
    //    //}
    //    //public AnFCostCenter GetLastCode(int companyId)
    //    //{
    //    //    return DataContext.AnFCostCenters.Where(ac => ac.CmnCompanyId == companyId).OrderByDescending(x => x.Id).FirstOrDefault();
    //    //}

    //    //public int AddEntity(CmnBusiness objAnFCostCenter)
    //    //{
    //    //    int Id = 1;
    //    //    CmnBusiness last = DataContext.CmnBusinesses.OrderByDescending(x => x.Id).FirstOrDefault();

    //    //    if (last != null)
    //    //    {
    //    //        Id = last.Id + 1;

    //    //    }
    //    //    objAnFCostCenter.Id = Id;
    //    //    base.Add(objAnFCostCenter);
    //    //    return Id;

    //    //}

    //    //public CmnBusiness GetCmnBusinessById(long nullable)
    //    //{
    //    //    var a = from v in DataContext.AnFCostCenters
    //    //            where v.Id == nullable
    //    //            select v;
    //    //    return DataContext.CmnBusinesses.Where(x => x.Id == nullable).FirstOrDefault();
    //    //}

    //}
}

