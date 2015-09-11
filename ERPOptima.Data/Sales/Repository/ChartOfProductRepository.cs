using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales.Repository
{
    #region Interface

    public interface IChartOfProductRepository : IRepository<SlsProduct>
    {
        IList<SlsProduct> GetAll(int companyId);
        SlsProduct GetById(int Id);
        int AddEntity(SlsProduct objSlsProduct);
        IList<SlsProduct> GetProducts(int CompanyId);
        IList<SlsProducts> GetProductsByReqId(int requisitionId, int CompanyId);
        IList<SlsProduct> GetCategories(int CompanyId);
        IList<SlsProduct> GetSubCategories(int categoryId, int CompanyId);
        IList<SlsProduct> GetBySlsProductId(int SlsProductId);

    }



    #endregion
    public class ChartOfProductRepository : BaseRepository<SlsProduct>, IChartOfProductRepository
    {
        
        public ChartOfProductRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
            {

            }

        public IList<SlsProduct> GetAll(int companyId)
        {
            return DataContext.SlsProducts.Where(p => p.SecCompanyId == companyId).ToList();
        }

        public SlsProduct GetById(int Id)
        {
            return DataContext.SlsProducts.Where(p => p.Id == Id).FirstOrDefault();
        }
        public int AddEntity(SlsProduct objSlsProduct)
        {
            int Id = 1;
            SlsProduct last = DataContext.SlsProducts.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objSlsProduct.Id = Id;
            base.Add(objSlsProduct);
            return Id;

        }

        public IList<SlsProduct> GetProducts(int companyId)
        {
            return DataContext.SlsProducts.Where(p => p.SecCompanyId == companyId && p.IsProduct==true).ToList();
        }

        public IList<SlsProducts> GetProductsByReqId(int requisitionId, int companyId)
        {
            var list = (from p in DataContext.SlsProducts
                        join r in DataContext.InvRequisitionDetails on p.Id equals r.SlsProductId
                        where p.SecCompanyId == companyId && p.IsProduct == true && r.InvRequisitionId == requisitionId
                        select new SlsProducts
                              {
                                  Id = p.Id,
                                  Name = p.Name,
                                  Code = p.Code,
                                  IsProduct = p.IsProduct,
                                  NoCredit = p.NoCredit,
                                  SlsProductId = p.SlsProductId,
                                  Level = p.Level,
                                  Description = p.Description,
                                  SecCompanyId = p.SecCompanyId,
                                  CreatedBy = p.CreatedBy,
                                  CreatedDate=p.CreatedDate,
                                  ModifiedBy = p.ModifiedBy,
                                  ModifiedDate=p.ModifiedDate
                              }).ToList();
            return list;

        }

        public IList<SlsProduct> GetCategories(int companyId)
        {
            return DataContext.SlsProducts.Where(p => p.SecCompanyId == companyId && p.IsProduct==false && p.Level==1).ToList();
        }

        public IList<SlsProduct> GetSubCategories(int categoryId, int companyId)
        {
            return DataContext.SlsProducts.Where(p => p.SecCompanyId == companyId && p.IsProduct == false && p.Level == 2 && p.SlsProductId == categoryId).ToList();
        }
        public IList<SlsProduct> GetBySlsProductId(int SlsProductId)
        {
            return DataContext.SlsProducts.Where(p => p.SlsProductId == SlsProductId).ToList();
        }



    }



}
