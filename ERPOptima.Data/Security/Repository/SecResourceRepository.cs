using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Security;

namespace ERPOptima.Data.Security.Repository
{
    #region Interface

    public interface ISecResourceRepository : IRepository<SecResource>
    { 
    
    
    }


    #endregion Interface

    public class SecResourceRepository : BaseRepository<SecResource>,ISecResourceRepository
    {

        public SecResourceRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}