using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.HRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Hrm.Repository
{    
    #region Interface
    public interface IHrmDesignationRepository : IRepository<HrmDesignation>
    {

        IList<HrmDesignation> GetParentWithChild(int companyId);
        long AddEntity(HrmDesignation objHrmDesignation);
    }
    #endregion
    public class HrmDesignationRepository : BaseRepository<HrmDesignation>, IHrmDesignationRepository
    {
        public HrmDesignationRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public IList<HrmDesignation> GetParentWithChild(int companyId)
        {
            return DataContext.HrmDesignations.ToList();
        }
        public long AddEntity(HrmDesignation objHrmDesignation)
        {
            int Id = 1;
            HrmDesignation last = DataContext.HrmDesignations.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = last.Id + 1;
            }
            objHrmDesignation.Id = Id;
            base.Add(objHrmDesignation);
            return Id;
        }
    }
}
