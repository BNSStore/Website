using Quartz;
using System;
using System.Collections.Generic;
using BNSStore.MVC.Shared;
using SLouple.MVC.Shared;

namespace BNSStore.MVC.Store
{
    public class ShiftNotificationJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            List<Shift> shifts = BSSqlSPs.Store.SelectSchedule(startDate: DateTime.Now.AddDays(1), endDate: DateTime.Now.AddDays(1));
            foreach(Shift shift in shifts){
                List<string> emailAddresses = SLSqlSPs.Account.SelectEmailSubFromUser(shift.user.GetUserID(), "Store.Employee.ShiftNotification", providerName: MVCApp.CurrentProvider.GetProviderName());
                foreach (string emailAddress in emailAddresses)
                {
                    string displayName = SLSqlSPs.Account.GetDisplayName(shift.user.GetUserID());
                    Lang lang = SLSqlSPs.Account.GetMainLang(shift.user.GetUserID());
                    Email.SendShiftNotificationEmail(emailAddress, shift.user, shift.store, lang);
                    System.Diagnostics.Debug.WriteLine("Sent shift notification to " + displayName + " at " + emailAddress);
                }
            }
        }
    }
}