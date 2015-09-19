using SLouple.MVC.Account;
using System;
using System.Collections.Generic;

namespace SLouple.MVC.Shared
{
    public class Policy : IEquatable<Policy>
    {
        private Provider provider;
        private int? policyID;
        private string policyName;
        private List<User> users;

        public Policy(Provider provider, int policyID)
        {
            this.provider = provider;
            this.policyID = policyID;
        }

        public Policy(Provider provider, string policyName)
        {
            this.provider = provider;
            this.policyName = policyName;
        }

        public Provider GetProvider()
        {
            return provider;
        }

        public int GetPolicyID(){
            if (policyID == null)
            {
                policyID = SLSqlSPs.Permission.GetPolicyID(policyName);
            }
            return policyID.Value;
        }

        public string GetPolicyName()
        {
            if (policyName == null)
            {
                policyName = SLSqlSPs.Permission.GetPolicyName(policyID.Value);
            }
            return policyName;
        }

        public List<User> GetUsers()
        {
            if (users == null)
            {
                users = SLSqlSPs.Permission.SelectUserFromPolicy(GetPolicyName());
            }
            return users;
        }

        public override bool Equals(object other)
        {
            if (other.GetType() == typeof(Policy))
            {
                return Equals((Policy)other);
            }
            else
            {
                return false;
            }
        }

        public bool Equals(Policy other)
        {
            if(other == null)
            {
                return false;
            }
            if (policyID == null)
            {
                return other.GetPolicyName() == GetPolicyName() && other.GetProvider() == GetProvider();
            }
            else
            {
                return other.GetPolicyID() == GetPolicyID() && other.GetProvider() == GetProvider();
            }
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + GetPolicyID().GetHashCode();
                hash = hash * 23 + GetProvider().GetHashCode();
                return hash;
            }
        }

        public static bool operator ==(Policy self, Policy other)
        {
            if (object.ReferenceEquals(self, null) || object.ReferenceEquals(other, null))
            {
                return (object.ReferenceEquals(self, null) && object.ReferenceEquals(other, null));
            }
            return self.Equals(other);
        }

        public static bool operator !=(Policy self, Policy other)
        {
            return !(self == other);
        }
    }
}