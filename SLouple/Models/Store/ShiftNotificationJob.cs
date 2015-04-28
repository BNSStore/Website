using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SLouple.MVC.Shared;

namespace SLouple.MVC.Store
{
    public class ShiftNotificationJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            SqlStoredProcedures sqlSP = new SqlStoredProcedures();
            Dictionary<int, char> shifts = sqlSP.StoreGetNextShifts();
            foreach(KeyValuePair<int, char>shift in shifts){
                if (sqlSP.UserIsEmailSub(shift.Key) || sqlSP.StoreHasMissedShift(shift.Key))
                {
                    string displayName = sqlSP.UserGetDisplayName(shift.Key);
                    string langName = sqlSP.LangGetLangName(sqlSP.UserGetMainLang(shift.Key));
                    string emailAddress = sqlSP.UserGetMainEmailAddress(shift.Key);
                    Email.SendShiftNotificationEmail(emailAddress, displayName, shift.Value, langName);
                    System.Diagnostics.Debug.WriteLine("Sent shift notification to " + displayName + " at " + emailAddress);
                }
            }
        }
    }
}