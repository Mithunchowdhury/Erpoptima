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
    public interface IAnFFixedAssetRevalueRepository : IRepository<FxdRevaluation>
    {
        int AddEntity(FxdRevaluation objFxdRevaluation);
        IList<FxdRevaluation> GetAll(int companyId);
        int GetLastCode(int companyId);
        IList<FxdAcquisition> GetFxdNAcquisition(int companyId);
        IList<FxdAcquisition> GetByIdAcquisition(int Id);
        IList<FxdRevaluation> GetAllWithoutId();
    }
    #endregion
    public class AnFFixedAssetRevalueRepository : BaseRepository<FxdRevaluation>, IAnFFixedAssetRevalueRepository
    {
        public AnFFixedAssetRevalueRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public int AddEntity(FxdRevaluation objFxdRevaluation)
        {
            int Id = 1;
            FxdRevaluation last = DataContext.FxdRevaluations.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objFxdRevaluation.Id = Id;
            base.Add(objFxdRevaluation);
            return Id;
        }

        public IList<FxdRevaluation> GetAll(int companyId)
        {
            return DataContext.FxdRevaluations.Include("FxdAsset").Where(fa => fa.SecCompanyId == companyId).ToList();
        }

        public int GetLastCode(int companyId)
        {

            int SL = 1;
            FxdRevaluation last = null;
            try
            {
                last = DataContext.FxdRevaluations.Where(r => r.SecCompanyId == companyId).OrderByDescending(x => x.Id).FirstOrDefault();
            }
            catch (Exception ex)
            {

            }
            if (last != null)
            {
                SL = int.Parse(last.RefNo.Split('/')[1]) + 1;

            }
            return SL;

        }//end of GetLastCode

        public IList<FxdAcquisition> GetFxdNAcquisition(int companyId)
        {
            return DataContext.FxdAcquisitions.Include("FxdAsset").Where(t => t.SecCompanyId == companyId).ToList();
        }

        public IList<FxdAcquisition> GetByIdAcquisition(int Id)
        {
            return DataContext.FxdAcquisitions.Include("FxdAsset").Where(t => t.Id == Id).ToList();
        }

        public IList<FxdRevaluation> GetAllWithoutId()
        {
            return DataContext.FxdRevaluations.Include("FxdAcquisition").Include("FxdAcquisition.FxdAsset").ToList();
        }
    }

    
}
