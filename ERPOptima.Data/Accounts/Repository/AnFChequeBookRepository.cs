using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Accounts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Accounts.Repository
{

    #region Interface

    public interface IAnFChequeBookRepository : IRepository<AnFChequeBook>
    {
        IList<AnFChequeBook> GetChequeBooks();
        long GetLastId();
        
    }

    #endregion

    public class AnFChequeBookRepository : BaseRepository<AnFChequeBook>, IAnFChequeBookRepository
    {
        public AnFChequeBookRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public IList<AnFChequeBook> GetChequeBooks()
        {
            return DataContext.AnFChequeBooks.ToList();
        }
        //public AnFChequeBook GetLastCode(int companyId)
        //{
        //    return DataContext.AnFChequeBooks.Where(ac => ac.CmnCompanyId == companyId).OrderByDescending(x => x.Id).FirstOrDefault();
        //}
       
       
        public long GetLastId()
        {

            long Id = 1;
            AnFChequeBook last = DataContext.AnFChequeBooks.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            return Id;

        }//end 


    }
}
