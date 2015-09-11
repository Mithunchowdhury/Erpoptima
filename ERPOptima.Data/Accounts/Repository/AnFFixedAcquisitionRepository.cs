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
    public interface IAnFFixedAcuisitionRepository : IRepository<FxdAcquisition>
    {
        int AddEntity(FxdAcquisition objFxdAcquisition);
        IList<FxdAcquisition> GetAll(int companyId);
        //int GetLastCode(int companyId);
        //IList<FxdAcquisition> GetFxdNAcquisition(int companyId);
    }
    #endregion
    public class AnFFixedAcquisitionRepository : BaseRepository<FxdAcquisition>, IAnFFixedAcuisitionRepository
    {
        public AnFFixedAcquisitionRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public int AddEntity(FxdAcquisition objFxdAcquisition)
        {
            int Id = 1;
            FxdAcquisition last = DataContext.FxdAcquisitions.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objFxdAcquisition.Id = Id;
            base.Add(objFxdAcquisition);
            return Id;
        }
        public IList<FxdAcquisition> GetAll(int companyId)
        {
            return DataContext.FxdAcquisitions.ToList();
        }
    }
}
