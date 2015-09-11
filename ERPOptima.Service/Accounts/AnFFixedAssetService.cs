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
    public interface IAnFFixedAssetService
    {
        IList<FxdAsset> GetAll(int companyId);
        string GetLastCode(int companyId, string prefix, string offcode);
        IList<FxdAcquisition> GetFxdNAcquisition(int companyId);
        DataTable GetAllAcquisition(int companyId);
        Operation SaveAcquisition(FxdAcquisition objfixedAssetEntry);

    }

    public class AnFFixedAssetService : IAnFFixedAssetService
    {
        private AnFFixedAssetRepository anFFixedAssetRepository;
        private AnFFixedAcquisitionRepository anFFixedAcquisitionRepository;
        private AnFFixedAssetRevalueRepository anFFixedAssetRevalueRepository;
        private Data.Infrastructure.UnitOfWork unitOfWork;

        public AnFFixedAssetService(AnFFixedAssetRepository anFFixedAssetRepository, AnFFixedAcquisitionRepository anFFixedAcquisitionRepository, AnFFixedAssetRevalueRepository anFFixedAssetRevalueRepository, Data.Infrastructure.UnitOfWork unitOfWork)
        {
            // TODO: Complete member initialization
            this.anFFixedAssetRepository = anFFixedAssetRepository;
            this.anFFixedAcquisitionRepository = anFFixedAcquisitionRepository;
            this.anFFixedAssetRevalueRepository = anFFixedAssetRevalueRepository;
            this.unitOfWork = unitOfWork;
        }

        public IList<FxdAsset> GetAll(int companyId)
        {
            return anFFixedAssetRepository.GetAll(companyId);
        }

        //auto generate code for RefNo
        public string GetLastCode(int companyId, string prefix, string offcode)
        {
            string code = prefix + "-" + "RVL" + "-" + offcode + "-" + DateTime.Now.ToString("yy") + "-" + DateTime.Now.ToString("MM") + "/" + anFFixedAssetRepository.GetLastCode(companyId).ToString();
            return code;
        }

        public IList<FxdAcquisition> GetFxdNAcquisition(int companyId)
        {
            //return anFFixedAssetRevalueRepository.GetFxdNAcquisition(companyId);
            IList<FxdAcquisition> list = null;
            DataTable dt = new DataTable();

            //SqlParameter[] paramsToStore = new SqlParameter[3];
            //paramsToStore[0] = new SqlParameter("@EmployeeId", employeeId);
            //paramsToStore[1] = new SqlParameter("@StartDate", startDate);
            //paramsToStore[2] = new SqlParameter("@EndDate", endDate);

            list = anFFixedAssetRevalueRepository.GetFxdNAcquisition(companyId).ToList();

            //list = list.Where(t => t.SecCompanyId == companyId).ToList();


            return list;
        }

        public DataTable GetAllAcquisition(int companyId)
        {
            try
            {
                SqlParameter[] paramsToStore = new SqlParameter[1];
                paramsToStore[0] = new SqlParameter("@SlsCompanyId", companyId);
               // paramsToStore[1] = new SqlParameter("@SlsUnitId", unitId);
                DataTable dt = anFFixedAcquisitionRepository.GetFromStoredProcedure(SPList.FxdAcquisition.GetAllAcquisition, paramsToStore);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Operation SaveAcquisition(FxdAcquisition objFxdAcquisition)
        {
            Operation objOperation = new Operation { Success = true, Message = "Saved successfully." };

            int Id = anFFixedAcquisitionRepository.AddEntity(objFxdAcquisition);
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

    }
}
