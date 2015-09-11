using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales.Repository
{
    public interface IDashboardRepository : IRepository<SecUser>
    {

    }
    public class DashboardRepository : BaseRepository<SecUser>, IDashboardRepository
    {
        public DashboardRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

    }
}
