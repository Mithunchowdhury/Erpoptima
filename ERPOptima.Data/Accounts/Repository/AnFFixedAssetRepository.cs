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
    public interface IAnFFixedAssetRepository : IRepository<FxdAsset>
    {
        int AddEntity(FxdAsset objFxdAsset);
        IList<FxdAsset> GetAll(int companyId);
        int GetLastCode(int companyId);
        //IList<FxdAcquisition> GetFxdNAcquisition(int companyId);
    } 
    
    #endregion

    public class AnFFixedAssetRepository : BaseRepository<FxdAsset>, IAnFFixedAssetRepository
    {
        public AnFFixedAssetRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
       
        public int AddEntity(FxdAsset objFxdAsset)
        {
            int Id = 1;
            FxdAsset last = DataContext.FxdAssets.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objFxdAsset.Id = Id;
            base.Add(objFxdAsset);
            return Id;
        }       

        public IList<FxdAsset> GetAll(int companyId)
        {
            return DataContext.FxdAssets.Where(fa => fa.SecCompanyId == companyId).ToList();
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

        //public IList<FxdAcquisition> GetFxdNAcquisition(int companyId)
        //{
        //    return DataContext.FxdAcquisitions.Include("FxdAcquisitions.FxdAssets").Where(ac => ac.SecCompanyId == companyId).ToList();
        //    //return DataContext.AnFAdjustments.Include("AnFAdvance").Include("AnFAdvance.HrmEmployee").ToList();
        //}
               
       
        
    }
    
    //faruk start
    #region Interface
    public interface IAnFFixedAssetRepositoryf : IRepository<FxdAcquisition>
    {
        IList<FxdAcquisition> GetFxdNAcquisition(int companyId);
    }
    #endregion

    public class AnFFixedAssetRepositoryf : BaseRepository<FxdAcquisition>, IAnFFixedAssetRepositoryf
    {
        public AnFFixedAssetRepositoryf(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
            

        public IList<FxdAcquisition> GetFxdNAcquisition(int companyId)
        {
            return DataContext.FxdAcquisitions.Include("FxdAcquisitions.FxdAssets").Where(ac => ac.SecCompanyId == companyId).ToList();
            //return DataContext.AnFAdjustments.Include("AnFAdvance").Include("AnFAdvance.HrmEmployee").ToList();
        }
    }
    //faruk end
}
