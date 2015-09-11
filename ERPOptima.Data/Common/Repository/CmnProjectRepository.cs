using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Common.Repository
{

    #region interface


    public interface ICmnProjectRepository : IRepository<CmnProject> {

        int AddEntity(CmnProject objCmnProject);
       
    
    }

    #endregion
    public class CmnProjectRepository : BaseRepository<CmnProject>, ICmnProjectRepository
    {

        public CmnProjectRepository(IDatabaseFactory databaseFactory)

            : base(databaseFactory)
            {

            }

        public int AddEntity(CmnProject objCmnProject)
        {
            int Id = 1;
            CmnProject last = DataContext.CmnProjects.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = last.Id + 1;
            }
            objCmnProject.Id = Id;
            base.Add(objCmnProject);
            return Id;
        }
        

       
    }
}
