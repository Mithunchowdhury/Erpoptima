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
    public interface IHrmEmployeeRepository : IRepository<HrmEmployee>
    {
        int AddEntity(HrmEmployee objHrmEmployee);
    }
    #endregion
    public class HrmEmployeeRepository : BaseRepository<HrmEmployee>, IHrmEmployeeRepository
    {
        public HrmEmployeeRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
       
        public int AddEntity(HrmEmployee objHrmEmployee)
        {
            int Id = 1;
            HrmEmployee last = DataContext.HrmEmployees.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = last.Id + 1;
            }
            objHrmEmployee.Id = Id;
            base.Add(objHrmEmployee);
            return Id;
        }
       

    }

}
