using ERPOptima.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPOptima.Helper
{
    public class SessionFactory : System.Web.Mvc.ValueProviderFactory
    {
        public override System.Web.Mvc.IValueProvider GetValueProvider(ControllerContext actionContext)
        {
            return new SessionValueProvider();
        }
    }
}