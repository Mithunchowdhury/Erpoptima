using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Common;
using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Common.Repository
{
  

    #region Interface



    public interface ISecCompanyRepository : IRepository<SecCompany>
    {
        Dictionary<Int32, string> GetCompaniesIdAndName();
        IList<SecCompany> GetCmnCompanies();
        int AddEntity(SecCompany objCmnCountry);
        SecCompany GetSecCompanyById(int id);


    }

    #endregion

    public class SecCompanyRepository : BaseRepository<SecCompany>, ISecCompanyRepository
    {
        public SecCompanyRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

       public Dictionary<Int32,string> GetCompaniesIdAndName()
        {
            Dictionary<Int32, string> IdandName = new Dictionary<int, string>();
            IdandName = (from p in DataContext.SecCompanies
                         select new { Id = p.Id, Name = p.Name }).ToDictionary(o => o.Id, o => o.Name);

            return IdandName;
         
        }
       public IList<SecCompany> GetCmnCompanies()
       {
           return DataContext.SecCompanies.ToList();
       }

       public int AddEntity(SecCompany objSecCompany)
       {
           int Id = 1;
           SecCompany last = DataContext.SecCompanies.OrderByDescending(x => x.Id).FirstOrDefault();
           if (last != null)
           {
               Id = last.Id + 1;
           }
           objSecCompany.Id = Id;
           base.Add(objSecCompany);
           return Id;
       }

       public SecCompany GetSecCompanyById(int id)
       {
           //var a = from v in DataContext.AnFCostCenters
           //        where v.Id == id
           //        select v;
           return DataContext.SecCompanies.Where(x => x.Id == id).FirstOrDefault();
       }

    }
}
