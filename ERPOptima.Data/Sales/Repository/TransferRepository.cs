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
    public interface ITransferRepository : IRepository<SlsTransfer>
    {

        List<SlsTransfer> GetAll(int companyId);
        int GetRefNo(int companyId);
        int GetLastId(SlsTransfer objSlsTransfer);


    }


    #endregion
    public class TransferRepository : BaseRepository<SlsTransfer>, ITransferRepository
    {
        
        public TransferRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
            {

            }

        public int GetRefNo(int companyId)
        {

            int SL = 1;
            SlsTransfer last = DataContext.SlsTransfers.Where(r => r.SecCompanyId == companyId).OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                SL = int.Parse(last.RefNo.Split('/')[1]) + 1;

            }
            return SL;

        }//end of GetRefNo

        public int GetLastId(SlsTransfer objSlsTransfer)
        {
            int Id = 1;
            SlsTransfer last = DataContext.SlsTransfers.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }         
            return Id;

        }


        public List<SlsTransfer> GetAll(int companyId)
        {
            return DataContext.SlsTransfers.Where(r => r.SecCompanyId == companyId).ToList();           

        }








    }
}
