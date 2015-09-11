using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Service.Sales
{
    
    public interface IOfficeService
    {
        IEnumerable<OfficeViewModel> GetAll();
        DataTable GetOfficeByEmployee(int? employeeId, int regionId);
        SlsOffice GetById(int Id);
        Operation Save(SlsOffice obj);
        Operation Delete(SlsOffice obj);
        Operation Update(SlsOffice obj);

        SlsOffice GetUserOffice(int userId);
    }

    public class OfficeService : IOfficeService
    {
        private IOfficeRepository _officeRepository;
        private IUnitOfWork _unitOfWork;


        public OfficeService(IOfficeRepository officeRepository, IUnitOfWork unitOfWork)
        {
            this._officeRepository = officeRepository;
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<OfficeViewModel> GetAll()
        {
            try
            {
                Collection<OfficeViewModel> list = null;
                DataTable dt = _officeRepository.GetFromStoredProcedure(SPList.Office.GetAllSlsOffices, null);
                
                if (dt != null)
                {
                    list = new Collection<OfficeViewModel>();
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add((OfficeViewModel)ERPOptima.Lib.Utilities.Helper.FillTo(row, typeof(OfficeViewModel)));
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }            
        }
        public DataTable GetOfficeByEmployee(int? employeeId,int regionId)
        {
            try
            {
                SqlParameter[] paramsToStore = new SqlParameter[2];
                paramsToStore[0] = new SqlParameter("@HrmEmployeeId", employeeId);
                paramsToStore[1] = new SqlParameter("@SlsRegionId", regionId);

                DataTable dt = _officeRepository.GetFromStoredProcedure(SPList.Office.GetOfficeByEmployee, paramsToStore);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SlsOffice GetById(int Id)
        {
            SlsOffice obj = _officeRepository.GetById(Id);
            return obj;
        }
        public SlsOffice GetUserOffice(int userId)
        {
            SlsOffice obj = _officeRepository.GetUserOffice(userId);
            return obj;
        }
        public Operation Update(SlsOffice obj)
        {
            Operation objOperation = new Operation { Success = true, OperationId = obj.Id };
            _officeRepository.Update(obj);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                objOperation.Success = false;

            }
            return objOperation;
        }

        public Operation Delete(SlsOffice obj)
        {
            Operation objOperation = new Operation { Success = true, OperationId = obj.Id };
            _officeRepository.Delete(obj);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {

                objOperation.Success = false;
            }
            return objOperation;
        }

        public Operation Save(SlsOffice obj)
        {
            Operation objOperation = new Operation { Success = true };

            long Id = _officeRepository.AddEntity(obj);
            objOperation.OperationId = Id;

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                objOperation.Success = false;
            }
            return objOperation;
        }
    }

    
}
