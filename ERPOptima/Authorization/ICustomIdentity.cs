using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Web.Authorization
{
    public interface ICustomIdentity : IIdentity
    {
        bool IsInRole(string role);
        string ToJson();
    }
}
