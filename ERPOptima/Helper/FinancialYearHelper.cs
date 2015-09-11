using ERPOptima.Data.Common.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Optima.Helper
{
    public class FinancialYearHelper
    {
        private ICmnFinancialYearService _fyService;

        public FinancialYearHelper()
        {
            var dbfactory = new DatabaseFactory();
            _fyService = new CmnFinancialYearService(new CmnFinancialYearRepository(dbfactory), new UnitOfWork(dbfactory));
        }

        public int SetFinancialYearId(int companyId, int moduleId)
        {

            int financialyearId = _fyService.GetCurrentFinancialYearId(companyId, moduleId);
            return financialyearId;
        }
    }
}