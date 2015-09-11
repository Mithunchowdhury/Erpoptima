using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales.Repository
{
    #region interface
    public interface ITransferDetailRepository : IRepository<SlsTransferDetail>
    {

        List<SlsTransferDetail> GetTransferDetailByTransferId(int transferId);

        int AddEntity(SlsTransferDetail objSlsTransferDetail);


    }


    #endregion
    public class TransferDetailRepository : BaseRepository<SlsTransferDetail>, ITransferDetailRepository
    {

        public TransferDetailRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
            {

            }
        public int AddEntity(SlsTransferDetail objSlsTransferDetail)
        {
            int Id = 1;
            SlsTransferDetail last = DataContext.SlsTransferDetails.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objSlsTransferDetail.Id = Id;
            base.Add(objSlsTransferDetail);
            return Id;

        }


        public List<SlsTransferDetail> GetTransferDetailByTransferId(int transferId)
        {
            return DataContext.SlsTransferDetails.Where(r => r.SlsTransferId == transferId).ToList();

        }




    }
}
