using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Accounts.Repository
{


    #region Interface

    public interface IAnFCostCenterRepository : IRepository<AnFCostCenter>
    {
        IList<AnFCostCenter> GetCostCenters(int companyId);
       //AnFCostCenter GetLastCode(int companyId);
        int AddEntity(AnFCostCenter objAnFCostCenter);



        AnFCostCenter GetCostCenterById(long nullable);
    }

    #endregion

    public class AnFCostCenterRepository : BaseRepository<AnFCostCenter>, IAnFCostCenterRepository
    {
        public AnFCostCenterRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public IList<AnFCostCenter> GetCostCenters(int companyId)
        {
            return DataContext.AnFCostCenters.Where(ac => ac.CmnCompanyId == companyId).ToList();
        }
        //public AnFCostCenter GetLastCode(int companyId)
        //{
        //    return DataContext.AnFCostCenters.Where(ac => ac.CmnCompanyId == companyId).OrderByDescending(x => x.Id).FirstOrDefault();
        //}

        public int AddEntity(AnFCostCenter objAnFCostCenter)
        {
            int Id = 1;
            AnFCostCenter last = DataContext.AnFCostCenters.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objAnFCostCenter.Id = Id;
            base.Add(objAnFCostCenter);
            return Id;

        }

        public AnFCostCenter GetCostCenterById(long nullable)
        {
            var a = from v in DataContext.AnFCostCenters
                    where v.Id == nullable
                    select v;
            return DataContext.AnFCostCenters.Where( x => x.Id == nullable).FirstOrDefault();
        }

    }

}
