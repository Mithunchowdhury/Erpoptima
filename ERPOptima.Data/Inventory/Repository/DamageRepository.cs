using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Inventory.Repository
{
   public interface DamageRepository
    {
       string GetLastCode(int companyId);
       int AddEntity(InvDamage objInvDamage);
       InvDamage GetById(int Id);
       IList<InvDamage> GetAll(int companyId);
    }
   public class InvDamageRepository : BaseRepository<InvDamage>, IInvDamageRepository
   {

       public InvDamageRepository(IDatabaseFactory databaseFactory)
           : base(databaseFactory)
       {


       }
       //public int GetLastId()
       //{
       //    int Id = 1;
       //    InvDamage last = DataContext.InvDamages.OrderByDescending(x => x.Id).FirstOrDefault();

       //    if (last != null)
       //    {
       //        Id = last.Id + 1;

       //    }
       //    return Id;
       //}

       public int GetLastCode(int companyId)
       {
           int SL = 1;
           InvDamage last = DataContext.InvDamages.Where(r => r.SecCompanyId == companyId).OrderByDescending(x => x.Id).FirstOrDefault();

           if (last != null)
           {
               SL = int.Parse(last.RefNo.Split('-')[3]) + 1;

           }
           return SL;
       }


       public int AddEntity(InvDamage objInvDamage)
       {
           int Id = 1;
           InvDamage last = DataContext.InvDamages.OrderByDescending(x => x.Id).FirstOrDefault();

           if (last != null)
           {
               Id = last.Id + 1;

           }
           objInvDamage.Id = Id;
           base.Add(objInvDamage);
           return Id;
       }

       public IList<InvDamage> GetAll(int companyId)
       {
           return DataContext.InvDamages.Where(p => p.SecCompanyId == companyId).ToList();
       }

       public InvDamage GetById(int Id)
       {
           return DataContext.InvDamages.Where(p => p.Id == Id).FirstOrDefault();
       }

      



   }
}
