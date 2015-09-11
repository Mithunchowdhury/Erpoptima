using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales.Repository
{
    #region Interface

    public interface IProductReceiveRepository : IRepository<InvProductReceive>
    {
        // IList<SlsCollectionTarget> GetAll();
        int AddEntity(InvProductReceive obj);
       // InvProductReceive GetAllByIssue(int issueId);

        InvProductReceive GetByIssue(int issueId);
    }

    #endregion
    public class ProductReceiveRepository : BaseRepository<InvProductReceive>, IProductReceiveRepository
    {
      public ProductReceiveRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
         public int AddEntity(InvProductReceive objProductReceive)
        {
            
            int Id = 1;
            InvProductReceive last = null;
            try
            {
                last = DataContext.InvProductReceives.OrderByDescending(x => x.Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                //Possibly can occur when no data exists in table.
            }
            if (last != null)
            {
                Id = last.Id + 1;

            }
            objProductReceive.Id = Id;
            base.Add(objProductReceive);
            return Id;
    }
        public InvProductReceive GetByIssue(int issueId)
        {
            InvProductReceive PrReceive = new InvProductReceive();
            PrReceive = DataContext.InvProductReceives.Where(x => x.InvIssueId == issueId).FirstOrDefault();
            return PrReceive;
        }

 }
}