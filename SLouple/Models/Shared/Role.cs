using SLouple.MVC.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SLouple.MVC.Shared
{
    public class Role : IEquatable<Role>
    {
        private int? roleID;
        private string roleName;
        private List<User> users;

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

        public List<User> GetUsers()
        {
            if (users == null)
            {
                SqlStoredProcedures sqlSP = new SqlStoredProcedures();
                users = sqlSP.PermissionSelectUserFromUserRole(this.GetRoleID());
            }
            return users;
        }

        public bool HasUser(User user)
        {
            return users.Contains(user);
        }

        public bool Equals(Role other)
        {
            if (roleID == null)
            {
                return other.GetRoleName() == GetRoleName();
            }
            else
            {
                return other.GetRoleID() == GetRoleID();
            }
        }
    }
}