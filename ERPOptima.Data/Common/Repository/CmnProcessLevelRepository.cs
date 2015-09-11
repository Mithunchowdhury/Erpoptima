using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;


namespace ERPOptima.Data.Common.Repository
{

    #region interface
    public interface ICmnProcessLevelRepository : IRepository<CmnProcessLevel>
    {
        IList<CmnProcessLevel> GetProcessLevel();
        int AddEntity(CmnProcessLevel objCmnProcessLevel);
    }

    #endregion
    public class CmnProcessLevelRepository : BaseRepository<CmnProcessLevel>, ICmnProcessLevelRepository
    {

        public CmnProcessLevelRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {


        }

        public IList<CmnProcessLevel> GetProcessLevel()
        {
            return DataContext.CmnProcessLevels.ToList();
        }

        public int AddEntity(CmnProcessLevel objCmnProcessLevel)
        {
            int Id = 1;
            CmnProcessLevel last = DataContext.CmnProcessLevels.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objCmnProcessLevel.Id = Id;
            base.Add(objCmnProcessLevel);
            return Id;

        }








       
    }
}
