using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales.Repository
{

    #region interface
    public interface IProductReceiveDetailRepository : IRepository<InvProductReceiveDetail>
    {

        //List<InvProductReceiveDetail> GetProductReceiveDetailByProductReceiveId(int productReceiveId);
        List<InvProductReceiveDetail> GetAll();

        int AddEntity(InvProductReceiveDetail objProductReceiveDetail);



        InvProductReceiveDetail GetAllByPrRecId(int receiveId);
    }


    #endregion
    public class ProductReceiveDetailRepository : BaseRepository<InvProductReceiveDetail>, IProductReceiveDetailRepository
    {
        public ProductReceiveDetailRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }


        public int AddEntity(InvProductReceiveDetail objInvProductReceiveDetail)
        {
            int Id = 1;
            InvProductReceiveDetail last = DataContext.InvProductReceiveDetails.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objInvProductReceiveDetail.Id = Id;
            base.Add(objInvProductReceiveDetail);
            return Id;

        }


        public InvProductReceiveDetail GetAllByPrRecId(int receiveId)
        {
            InvProductReceiveDetail PrRecDetailId = new InvProductReceiveDetail();
            PrRecDetailId = DataContext.InvProductReceiveDetails.OrderByDescending(x => x.InvProductReceiveId == receiveId).FirstOrDefault();

            return PrRecDetailId;

        }
        public List<InvProductReceiveDetail> GetAll()
        {
            return DataContext.InvProductReceiveDetails.ToList();

        }









    }
}
