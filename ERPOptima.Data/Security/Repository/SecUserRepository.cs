using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Common;
using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Security.Repository
{
    #region Interface



    public interface ISecUserRepository : IRepository<SecUser>
    {
      
        int AddEntity(SecUser objSecUser);
    }

    #endregion


    public class SecUserRepository : BaseRepository<SecUser>, ISecUserRepository
    {

        public SecUserRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

       
        public int AddEntity(SecUser objSecUser)
        {
            int Id = 1;
            SecUser last = DataContext.SecUsers.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objSecUser.Id = Id;
            base.Add(objSecUser);
            return Id;

        }

    }
}
