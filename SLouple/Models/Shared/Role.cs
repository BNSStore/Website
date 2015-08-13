using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SLouple.MVC.Shared
{
    public class Role
    {
        private int? roleID;
        private string roleName;

        public Role(int roleID)
        {
            this.roleID = roleID;
        }

        public Role(string roleName)
        {
            this.roleName = roleName;
        }

        public int GetRoleID(){
            if (roleID == null)
            {
                SqlStoredProcedures sqlSP = new SqlStoredProcedures();
                roleID = sqlSP.PermissionGetRoleID(roleName);
            }
            return roleID.Value;
        }

        public string GetRoleName()
        {
            if (roleName == null)
            {
                SqlStoredProcedures sqlSP = new SqlStoredProcedures();
                roleName = sqlSP.PermissionGetRoleName(roleID.Value);
            }
            return roleName;
        }
    }
}