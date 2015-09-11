using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace ERPOptima.Web.Authorization
{
    public class CustomPrincipal : ICustomPrincipal
    {
        private CustomPrincipal() { }

        private CustomPrincipal(ICustomIdentity identity)
        {
            this.Identity = identity;
        }

        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            if (string.IsNullOrEmpty(role))
            {
                throw new ArgumentException("Role is null");
            }
            return ((ICustomIdentity)Identity).IsInRole(role);
        }


        public static void Logout()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie != null)
            {
                FormsAuthentication.SignOut();
                HttpContext.Current.Request.Cookies.Remove(FormsAuthentication.FormsCookieName);
            }
            HttpContext.Current.User =
                new GenericPrincipal(new GenericIdentity(""), new string[] { });
        }


        public static void Login(string userName)
        {
            var identity = CustomIdentity.GetCustomIdentity(userName);
            
                HttpContext.Current.User = new CustomPrincipal(identity);
                FormsAuthenticationTicket ticket =
                       new FormsAuthenticationTicket(
                           1, identity.Name, DateTime.Now, DateTime.Now.AddMinutes(30), false,
                           identity.ToJson(), FormsAuthentication.FormsCookiePath);
                string encryptedTicket = FormsAuthentication.Encrypt(ticket);

                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                cookie.Path = FormsAuthentication.FormsCookiePath;
                HttpContext.Current.Response.Cookies.Add(cookie);
            
        }

        public static bool Login(string cookieString,bool json)
        {
            ICustomIdentity identity = CustomIdentity.FromJson(cookieString);
            if (identity.IsAuthenticated)
            {
                HttpContext.Current.User = new CustomPrincipal(identity);
            }
            return identity.IsAuthenticated;
        }
    }
}