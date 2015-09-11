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

    public interface IHrmDepartmentRepository : IRepository<HrmDepartment>
    {
        int AddEntity(HrmDepartment objHrmDepartment);
    }

    #endregion


    public class HrmDepartmentRepository : BaseRepository<HrmDepartment>, IHrmDepartmentRepository
    {

        public HrmDepartmentRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public int AddEntity(HrmDepartment objHrmDepartment)
        {
            int Id = 1;
            HrmDepartment last = DataContext.HrmDepartments.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objHrmDepartment.Id = Id;
            base.Add(objHrmDepartment);
            return Id;

        }

    }
}
