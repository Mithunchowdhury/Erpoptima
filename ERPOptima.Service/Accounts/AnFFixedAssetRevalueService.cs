using ERPOptima.Data.Accounts.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Accounts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Service.Accounts
{
    public interface IAnFFixedAssetRevalueService
    {
        IList<FxdRevaluation> GetAll(int companyId);
        //string GetLastCode(int companyId, string prefix, string offcode);
        IList<FxdAcquisition> GetFxdNAcquisition(int companyId);
        IList<FxdAcquisition> GetByIdAcquisition(int Id);
        Operation SaveFxdRevalue(FxdRevaluation objfixedAssetEntry);
        IList<FxdRevaluation> RevaluationList();
        FxdRevaluation GetById(int Id);
        Operation Delete(FxdRevaluation objFxdRevaluation);
    }
    public class AnFFixedAssetRevalueService : IAnFFixedAssetRevalueService
    {
        private AnFFixedAssetRevalueRepository anFFixedAssetRevalueRepository;
        private Data.Infrastructure.UnitOfWork unitOfWork;

        public AnFFixedAssetRevalueService(AnFFixedAssetRevalueRepository anFFixedAssetRevalueRepository, Data.Infrastructure.UnitOfWork unitOfWork)
        {
            // TODO: Complete member initialization
            //this.anFFixedAssetRepository = anFFixedAssetRepository;
            this.anFFixedAssetRevalueRepository = anFFixedAssetRevalueRepository;
            this.unitOfWork = unitOfWork;
        }

        //public AnFFixedAssetService(AnFFixedAssetRepository anFFixedAssetRepository, Data.Infrastructure.UnitOfWork unitOfWork)
        //{
        //    // TODO: Complete member initialization
        //    this.anFFixedAssetRepository = anFFixedAssetRepository;
        //    this.unitOfWork = unitOfWork;
        //}



        public IList<FxdRevaluation> GetAll(int companyId)
        {
            return anFFixedAssetRevalueRepository.GetAll(companyId);
        }

        //auto generate code for RefNo
        //public string GetLastCode(int companyId, string prefix, string offcode)
        //{
        //    string code = prefix + "-" + "RVL" + "-" + offcode + "-" + DateTime.Now.ToString("yy") + "-" + DateTime.Now.ToString("MM") + "/" + anFFixedAssetRevalueRepository.GetLastCode(companyId).ToString();
        //    return code;
        //}

        public IList<FxdAcquisition> GetFxdNAcquisition(int companyId)
        {
            //return anFFixedAssetRevalueRepository.GetFxdNAcquisition(companyId);
            //IList<FxdAcquisition> list = null;
            //DataTable dt = new DataTable();
            IList<FxdAcquisition> list = new List<FxdAcquisition>();

            //SqlParameter[] paramsToStore = new SqlParameter[3];
            //paramsToStore[0] = new SqlParameter("@EmployeeId", employeeId);
            //paramsToStore[1] = new SqlParameter("@StartDate", startDate);
            //paramsToStore[2] = new SqlParameter("@EndDate", endDate);

            list = anFFixedAssetRevalueRepository.GetFxdNAcquisition(companyId).ToList();

            //list = list.Where(t => t.SecCompanyId == companyId).ToList();


            return list;
        }

        public IList<FxdAcquisition> GetByIdAcquisition(int Id)
        {
            IList<FxdAcquisition> list = new List<FxdAcquisition>();
            list = anFFixedAssetRevalueRepository.GetByIdAcquisition(Id).ToList();
            return list;
        }

        public Operation SaveFxdRevalue(FxdRevaluation objFxdAcquisition)
        {
            Operation objOperation = new Operation { Success = true, Message = "Saved successfully." };

            int Id = anFFixedAssetRevalueRepository.AddEntity(objFxdAcquisition);
            objOperation.OperationId = Id;

            try
            {
                unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                objOperation.Success = false;
                objOperation.Message = "Save not successful.";
            }
            return objOperation;
        }

        public IList<FxdRevaluation> RevaluationList()
        {
            IList<FxdRevaluation> list = anFFixedAssetRevalueRepository.GetAllWithoutId().ToList();
            return list;
        }

        public FxdRevaluation GetById(int Id)
        {

            return anFFixedAssetRevalueRepository.GetById(Id);
        }

        public Operation Delete(FxdRevaluation objFxdRevaluation)
        {
            Operation objOperation = new Operation { Success = true, Message = "Deleted successfully." };
            anFFixedAssetRevalueRepository.Delete(objFxdRevaluation);

            try
            {
                unitOfWork.Commit();
            }
            catch (Exception)
            {

                objOperation.Success = false;
                objOperation.Message = "Delete not successful.";
            }
            return objOperation;
        }
    }
}
