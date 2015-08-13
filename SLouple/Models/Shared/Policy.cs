using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SLouple.MVC.Shared
{
    public class Policy
    {
        private int? policyID;
        private string policyName;

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
    }
}