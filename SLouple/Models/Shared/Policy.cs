using SLouple.MVC.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SLouple.MVC.Shared
{
    public class Policy : IEquatable<Policy>
    {
        private int? policyID;
        private string policyName;
        private List<User> users;

        public Policy(int policyID)
        {
            this.policyID = policyID;
        }

        public Policy(string policyName)
        {
            this.policyName = policyName;
        }

        public int GetPolicyID(){
            if (policyID == null)
            {
                SqlStoredProcedures sqlSP = new SqlStoredProcedures();
                policyID = sqlSP.PermissionGetPolicyID(policyName);
            }
            return policyID.Value;
        }

        public string GetPolicyName()
        {
            if (policyName == null)
            {
                SqlStoredProcedures sqlSP = new SqlStoredProcedures();
                policyName = sqlSP.PermissionGetPolicyName(policyID.Value);
            }
            return policyName;
        }

        public List<User> GetUsers()
        {
            if (users == null)
            {
                SqlStoredProcedures sqlSP = new SqlStoredProcedures();
                users = sqlSP.PermissionSelectUserFromPolicy(GetPolicyID());
            }
            return users;
        }

        public bool Equals(Policy other)
        {
            if (policyID == null)
            {
                return other.GetPolicyName() == GetPolicyName();
            }
            else
            {
                return other.GetPolicyID() == GetPolicyID();
            }
            
        }
    }
}