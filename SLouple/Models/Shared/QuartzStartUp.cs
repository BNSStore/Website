using Quartz;
using Quartz.Impl;
using SLouple.MVC.Main;
using SLouple.MVC.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SLouple.MVC.Shared
{
    public class QuartzStartUp
    {
        public static void StartUp(){
            ISchedulerFactory schedFact = new StdSchedulerFactory();
            IScheduler sched = schedFact.GetScheduler();
            sched.Start();

            //Shift Notification Job
            IJobDetail shiftNotificationJob = JobBuilder.Create<ShiftNotificationJob>()
                .WithIdentity("shiftNotificationJob", "store")
                .Build();

            //Shift Notification Trigger
            ITrigger shiftNotificationTrigger = TriggerBuilder.Create()
                .WithIdentity("shiftNotificationTrigger", "store")
                .StartNow()
                .WithSchedule(
                    CronScheduleBuilder
                    .DailyAtHourAndMinute(18,00)
                    .InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time")))
              .Build();

            sched.ScheduleJob(shiftNotificationJob, shiftNotificationTrigger);
        }
        
    }
}