using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace ERPOptima.Web.Authorization
{
    public class CustomIdentity : ICustomIdentity
    {
      
        public static CustomIdentity GetCustomIdentity(string userName)
        {
            CustomIdentity identity = new CustomIdentity();
            
                identity.IsAuthenticated = true;
                identity.Name = userName;
                return identity;
        }

        private CustomIdentity() { }

        public string AuthenticationType
        {
            get { return "Custom"; }
        }

        public bool IsAuthenticated { get; private set; }

        public string Name { get; private set; }

        private string[] Roles { get; set; }

        public bool IsInRole(string role)
        {
            if (string.IsNullOrEmpty(role))
            {
                throw new ArgumentException("Role is null");
            }
            return Roles.Where(one => one.ToUpper().Trim() == role.ToUpper().Trim()).Any();
        }

        public string ToJson()
        {
            string returnValue = string.Empty;
            IdentityRepresentation representation = new IdentityRepresentation()
            {
                IsAuthenticated = this.IsAuthenticated,
                Name = this.Name
            };
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            returnValue = serializer.Serialize(representation);
            return returnValue;
        }

        public static ICustomIdentity FromJson(string cookieString)
        {

            IdentityRepresentation serializedIdentity = null;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializedIdentity = serializer.Deserialize<IdentityRepresentation>(cookieString);
            CustomIdentity identity = new CustomIdentity()
            {
                IsAuthenticated = serializedIdentity.IsAuthenticated,
                Name = serializedIdentity.Name,
                Roles = serializedIdentity.Roles
                    .Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries)
            };
            return identity;
        }

    }
}