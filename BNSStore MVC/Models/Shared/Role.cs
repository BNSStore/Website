using SLouple.MVC.Account;
using System;
using System.Collections.Generic;

namespace SLouple.MVC.Shared
{
    public class Role : IEquatable<Role>
    {
        private Provider provider;
        private int? roleID;
        private string roleName;
        private List<User> users;

        public Role(Provider provider, int roleID)
        {
            this.provider = provider;
            this.roleID = roleID;
        }

        public Role(Provider provider, string roleName)
        {
            this.provider = provider;
            this.roleName = roleName;
        }

        public Provider GetProvider()
        {
            return provider;
        }

        public int GetRoleID(){
            if (roleID == null)
            {
                roleID = SLSqlSPs.Permission.GetRoleID(roleName);
            }
            return roleID.Value;
        }

        public string GetRoleName()
        {
            if (roleName == null)
            {
                roleName = SLSqlSPs.Permission.GetRoleName(roleID.Value);
            }
            return roleName;
        }

        public List<User> GetUsers()
        {
            if (users == null)
            {
                users = SLSqlSPs.Permission.SelectUserFromRole(GetRoleName());
            }
            return users;
        }

        public bool HasUser(User user)
        {
            return users.Contains(user);
        }

        public override bool Equals(object other)
        {
            if (other.GetType() == typeof(Role))
            {
                return Equals((Role)other);
            }
            else
            {
                return false;
            }
        }

        public bool Equals(Role other)
        {
            if(other == null)
            {
                return false;
            }
            if (roleID == null)
            {
                return other.GetRoleName() == GetRoleName() && other.GetProvider() == GetProvider();
            }
            else
            {
                return other.GetRoleID() == GetRoleID() && other.GetProvider() == GetProvider();
            }
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + GetRoleID().GetHashCode();
                hash = hash * 23 + GetProvider().GetHashCode();
                return hash;
            }
        }

        public static bool operator ==(Role self, Role other)
        {
            if (object.ReferenceEquals(self, null) || object.ReferenceEquals(other, null))
            {
                return (object.ReferenceEquals(self, null) && object.ReferenceEquals(other, null));
            }
            return self.Equals(other);
        }

        public static bool operator !=(Role self, Role other)
        {
            return !(self == other);
        }
    }
}