using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Security.Repository
{
    #region Interface



    public interface IMenuRepository : IRepository<SecModule>
    {

    }

    #endregion


    public class MenuRepository : BaseRepository<SecModule>, IMenuRepository
    {

        public MenuRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

    }

}
