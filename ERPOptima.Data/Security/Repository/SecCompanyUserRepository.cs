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



    public interface ISecCompanyUserRepository : IRepository<SecCompanyUser>
    {
        int AddEntity(SecCompanyUser objSecCompanyUser);
    }

    #endregion


    public class SecCompanyUserRepository : BaseRepository<SecCompanyUser>, ISecCompanyUserRepository
    {

        public SecCompanyUserRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public int AddEntity(SecCompanyUser objSecCompanyUser)
        {
            int Id = 1;
            SecCompanyUser last = DataContext.SecCompanyUsers.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objSecCompanyUser.Id = Id;
            base.Add(objSecCompanyUser);
            return Id;

        }

    }

}
