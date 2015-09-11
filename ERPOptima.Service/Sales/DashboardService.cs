using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Service.Sales
{
    public interface IDashboardService
    {
        IEnumerable<RegionWiseSales> GetRegionWiseSales();
    }
    public class DashboardService : IDashboardService    
    {
        private IDashboardRepository _repository;
        private IUnitOfWork _unitOfWork;


        public DashboardService(IDashboardRepository 
            repository, IUnitOfWork unitOfWork)
        {
            this._repository = repository;
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<RegionWiseSales> GetRegionWiseSales()
        {
            try
            {
                IList<RegionWiseSales> list = new List<RegionWiseSales>();
                SqlParameter[] paramsToStore = new SqlParameter[1];
               
                DataTable dt = _repository.GetFromStoredProcedure(SPList.DashboardChart.RegionWiseSales, null);

                if (dt != null && dt.Rows.Count > 0)
                {
                    list = dt.DataTableToList<RegionWiseSales>();                    
                }                

                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }

    //ViewModel for RegionWiseSales

    public class RegionWiseSales
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OfficeCount { get; set; }
        public decimal Sales { get; set; }
        public decimal SalesTotal { get; set; }
        public decimal SalesPer { get; set; }
    }
}
