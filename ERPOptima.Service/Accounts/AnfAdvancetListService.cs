using ERPOptima.Data.Accounts.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Lib.Model;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Accounts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Service.Accounts
{
    
    public interface IAnfAdvancetListService
    {
        IList<AnFAdvance> GetAdvanceList(int employeeId,int companyid, DateTime? startDate, DateTime? endDate);
        IList<AnFAdvance> GetAllAdvance(int companyId);
        IEnumerable<AnFAdvance> GetTransactionalHeadByCompanyId(int companyId);
        //Operation Save (AnFAdvance objAnFAdvance);
        //Operation Delete (AnFAdvance objAnFAdvance);
        //Operation Update(AnFAdvance objAnFAdvance);
        string GetRefNo(int companyId, string prefix, string offcode); // Add by Bably
        Operation Save(AnFAdvance objadvanceEntry);
        Operation Update(AnFAdvance objadvanceEntry);
        IList<AnFAdvance> GetByfinancialYearId(int financialYearId);
        //IList<AnFAdvance> GetByfinancialYearId(int financialYearId);

        AnFAdvance GetById(int Id);

        Operation Delete(AnFAdvance objadvanceEntry);

        DataTable GetAnFAdvanceAdjustmentReport(int anfAdvanceId);
    }
    public class AnfAdvancetListService : IAnfAdvancetListService
    {
        private IAnfAdvancetListRepository _anfAdvancetListRepository;
        private IUnitOfWork _UnitOfWork;
        public AnfAdvancetListService(IAnfAdvancetListRepository anfAdvanceListRepository, IUnitOfWork unitOfWork)
        {
            this._anfAdvancetListRepository = anfAdvanceListRepository;
            this._UnitOfWork = unitOfWork;

        }

        // For Auto generated RefNo in Advance Entry By Bably
        public string GetRefNo(int companyId, string prefix, string offcode)
        {
            string refno = prefix + "-" + "ADV" + "-" + offcode + "-" + DateTime.Now.ToString("yy") + "-" + DateTime.Now.ToString("MM") + "/" + _anfAdvancetListRepository.GetRefNo(companyId).ToString();
            return refno;
        }
        public IEnumerable<AnFAdvance> GetTransactionalHeadByCompanyId(int companyId)
        {
            return _anfAdvancetListRepository.GetMany(coa => coa.SecCompanyId == companyId);
        }

        public AnFAdvance GetById(int Id)
        {

            return _anfAdvancetListRepository.GetById(Id);
        }
        public IList<AnFAdvance> GetAdvanceList(int employeeId,int companyid, DateTime? startDate, DateTime? endDate)
        {
            IList<AnFAdvance> list = null;
            DataTable dt = new DataTable();

            //SqlParameter[] paramsToStore = new SqlParameter[3];
            //paramsToStore[0] = new SqlParameter("@EmployeeId", employeeId);
            //paramsToStore[1] = new SqlParameter("@StartDate", startDate);
            //paramsToStore[2] = new SqlParameter("@EndDate", endDate);

            list = _anfAdvancetListRepository.GetAll().ToList();
            if (employeeId == 0)
            {
                if (startDate != null && endDate != null)
                {
                    list = list.Where(t => t.SecCompanyId == companyid && t.Date >= startDate && t.Date <= endDate).ToList();
                }
                else
                {
                    list = list.Where(t => t.SecCompanyId == companyid).ToList();
                }
            }
            else
            {
                list = list.Where(t => t.HrmEmployeeId == employeeId).ToList();
            }
            //try
            //{
            //    dt = _anfAdvancetListRepository.GetFromStoredProcedure(SPList.Report.GetAnfAdvanceList, paramsToStore);
            //}
            //catch (Exception ex)
            //{
            //}
            //if (dt != null)
            //{
            //    list = new Collection<AnFAdvance>();
            //    foreach (DataRow row in dt.Rows)
            //    {
            //        list.Add((AnFAdvance)ERPOptima.Lib.Utilities.Helper.FillTo(row, typeof(AnFAdvance)));
            //    }
            //}
            return list;
        }

        public IList<AnFAdvance> GetAllAdvance(int companyId)
        {
            return _anfAdvancetListRepository.GetAllAdvance(companyId);
            //return _anfAdvancetListRepository.GetAllAdvance(ml => ml.SecCompanyId == companyId).ToList();
        }

        public IList<AnFAdvance> GetByfinancialYearId(int financialYearId)
        {
            return _anfAdvancetListRepository.GetManyWithInclude(ml => ml.CmnFinancialYearId == financialYearId, "HrmEmployee").ToList();
        }

        //public Operation Save(AnFAdvance objAnFAdvance)
        //{
        //    Operation objOperation = new Operation { Success = true };

        //    long Id = _anfAdvancetListRepository.AddEntity(objAnFAdvance);
        //    objOperation.OperationId = Id;

        //    try
        //    {
        //        _UnitOfWork.Commit();
        //    }
        //    catch (Exception ex)
        //    {
        //        objOperation.Success = false;
        //    }
        //    return objOperation;
        //}
        //public Operation Delete(AnFAdvance objAnFAdvance)
        //{
        //    Operation objOperation = new Operation { Success = true, OperationId = objAnFAdvance.Id };
        //    _anfAdvancetListRepository.Delete(objAnFAdvance);

        //    try
        //    {
        //        _UnitOfWork.Commit();
        //    }
        //    catch (Exception)
        //    {

        //        objOperation.Success = false;
        //    }
        //    return objOperation;
        //}
        //public Operation UpdateAnFCostCenter(AnFCostCenter objAnFCostCenter)
        //{
        //    Operation objOperation = new Operation { Success = true, OperationId = objAnFCostCenter.Id };
        //    _AnFCostCenterRepository.Update(objAnFCostCenter);

        //    try
        //    {
        //        _UnitOfWork.Commit();
        //    }
        //    catch (Exception)
        //    {
        //        objOperation.Success = false;

        //    }
        //    return objOperation;
        //}

        public Operation Save(AnFAdvance objadvanceEntry)
        {
            Operation objOperation = new Operation { Success = true, Message = "Saved successfully." };

            int Id = _anfAdvancetListRepository.AddEntity(objadvanceEntry);
            objOperation.OperationId = Id;

            try
            {
                _UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                objOperation.Success = false;
                objOperation.Message = "Save not successful.";
            }
            return objOperation;
        }

        public Operation Update(AnFAdvance objadvanceEntry)
        {
            Operation objOperation = new Operation { Success = true, Message = "Update successfully." };
            _anfAdvancetListRepository.Update(objadvanceEntry);

            try
            {
                _UnitOfWork.Commit();
            }
            catch (Exception)
            {
                objOperation.Success = false;
                objOperation.Message = "Update not successful.";
            }
            return objOperation;
        }

        public Operation Delete(AnFAdvance objadvanceEntry)
        {
            Operation objOperation = new Operation { Success = true, Message = "Deleted successfully." };
            _anfAdvancetListRepository.Delete(objadvanceEntry);

            try
            {
                _UnitOfWork.Commit();
            }
            catch (Exception)
            {

                objOperation.Success = false;
                objOperation.Message = "Delete not successful.";
            }
            return objOperation;
        }

        // GetAnFAdvanceAdjustmentReport added by Bably
        public DataTable GetAnFAdvanceAdjustmentReport(int anfAdvanceId)
        {
            DataTable dt = new DataTable();

            SqlParameter[] paramsToStore = new SqlParameter[1];
            paramsToStore[0] = new SqlParameter("@AnFAdvanceId", anfAdvanceId);

            try
            {
                dt = _anfAdvancetListRepository.GetFromStoredProcedure(SPList.Report.RptAnFAdvanceAdjustmentReport, paramsToStore);
            }
            catch (Exception ex)
            {
            }

            return dt;
        }

    }
}
