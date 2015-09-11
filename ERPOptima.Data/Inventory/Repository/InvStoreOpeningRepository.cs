using ERPOptima.Data.Infrastructure;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Inventory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Inventory.Repository
{

    #region interface
    public interface IInvStoreOpeningRepository : IRepository<InvStoreOpening>
    {     
        int GetLastId();
       
        DataTable GetInvStoreOpeningByInvStoreId(int storeId, int companyId);


    }

    #endregion
    public class InvStoreOpeningRepository : BaseRepository<InvStoreOpening>, IInvStoreOpeningRepository
    {

        public InvStoreOpeningRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
            {

            }

        public IList<InvStore> GetStores()
        {
            return DataContext.InvStores.ToList();
        }

        public int GetLastId() {

            int Id = 1;
            InvStoreOpening last = DataContext.InvStoreOpening.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            return Id;
        
        }//end of GetLastId


        //public List<InvStoreOpening> GetInvStoreOpeningByInvStoreId(int storeId, int companyId)
        //{

        //    var query = DataContext.InvStoreOpening.Include("SlsProduct").Where(op => op.InvStoreId == storeId && op.SlsProduct.IsProduct == true && op.SlsProduct.SecCompanyId == companyId);         
        //    List<InvStoreOpening> list = query.ToList();
        //    return list;
                   
        //}


        public DataTable GetInvStoreOpeningByInvStoreId(int storeId, int companyId)
        {
            DataTable dt = null;
            try
            {
                dt = new DataTable();

                SqlParameter[] paramsToStore = new SqlParameter[2];

                paramsToStore[0] = new SqlParameter("@InvStoreId", storeId);
                paramsToStore[1] = new SqlParameter("@SecCompanyId", companyId);

                dt = GetFromStoredProcedure(SPList.InvStoreOpening.GetInvStoreOpeningByInvStoreId, paramsToStore);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
           
        }

      




    }
}
