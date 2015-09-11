using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Web.Authorization
{
    public class IdentityRepresentation
    {
        private bool ia;

        public bool IsAuthenticated
        {
            get { return ia; }
            set { ia = value; }
        }

        private string n;

        public string Name
        {
            get { return n; }
            set { n = value; }
        }

        private string r;

        public string Roles
        {
            get { return r; }
            set { r = value; }
        }

        private string pm;

        public string Permissions
        {
            get { return pm; }
            set { pm = value; }
        }
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

    }
}
