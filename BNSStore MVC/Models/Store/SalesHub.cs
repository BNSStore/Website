using Microsoft.AspNet.SignalR;
using BNSStore.MVC.Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SLouple.MVC.Account;
using SLouple.MVC.Shared;

namespace BNSStore.MVC.Store
{
    public class SalesHub : Hub
    {
        
        public static readonly TimeSpan startTime = new TimeSpan(9, 0, 0);
        public static readonly TimeSpan endTime = new TimeSpan(15, 0, 0);

        protected static Dictionary<string, User> onlineUsers = new Dictionary<string, User>();
        protected static ArrayList uniqueOnlineUsers = new ArrayList();
        private static Dictionary<string, char> userStoreDic = new Dictionary<string, char>();

        public void Login(int userID, string sessionToken, char store)
        {
            //Valid User and Is Manager
            User user = new User(userID);
            user.SignInWithSessionToken(sessionToken, GetIPAddress());
            if (user.GetSessionToken() == null || !user.HasPolicy(new Policy(MVCApp.CurrentProvider, "Store.Sales.Access")))
            {
                return;
            }
            
            //Time between 9am to 3pm
            //TimeSpan time = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Pacific Standard Time").TimeOfDay;
            //if (time <= startTime || time >= endTime)
            //{
            //    return;
            //}
            
            
            //Working Today And In the Correct Store
            List<Shift> shifts = BSSqlSPs.Store.SelectSchedule(startDate: DateTime.Now, endDate: DateTime.Now);
            if (!shifts.Any(x => (x.user == user && Char.ToLower(x.store) == Char.ToLower(store))))
            {
                return;
            }

            //Add User
            onlineUsers.Add(Context.ConnectionId, user);
            if (!uniqueOnlineUsers.Contains(userID))
            {
                uniqueOnlineUsers.Add(userID);
            }
            userStoreDic.Add(Context.ConnectionId, Char.ToLower(store));
            Clients.Client(Context.ConnectionId).LoginSuccess();
            UpdateAllSaleCounts();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            if (onlineUsers.ContainsKey(Context.ConnectionId))
            {
                User user = onlineUsers[Context.ConnectionId];
                onlineUsers.Remove(Context.ConnectionId);
                string key = onlineUsers.FirstOrDefault(x => x.Value == user).Key;
                if (key == null || key == "")
                {
                    uniqueOnlineUsers.Remove(user);
                }
            }
            return base.OnDisconnected(stopCalled);
        }

        public void UpdateAllSaleCounts()
        {
            char store = userStoreDic[Context.ConnectionId];
            List<Sale> sales = BSSqlSPs.Store.SelectSale(store);
            foreach (Sale sale in sales)
            {
                Clients.Client(Context.ConnectionId).UpdateSaleCount(sale.GetProductID(), sale.GetCount(), sale.GetEmployeeCount());
            }
            decimal total = BSSqlSPs.Store.GetSaleTotal(store);
            Clients.Client(Context.ConnectionId).UpdateTotal(Convert.ToDouble(total).ToString("N2"));
        }

        public void UpdateSaleCount(int productID, int count, int employeeCount)
        {
            if (productID > 999 || count > 999 || employeeCount > 999)
            {
                return;
            }
            if (productID < 0 || count < 0 || employeeCount < 0)
            {
                return;
            }
            char store = userStoreDic[Context.ConnectionId];
            BSSqlSPs.Store.UpdateSaleCount(Char.ToUpper(store), productID, count, employeeCount);
            decimal total = BSSqlSPs.Store.GetSaleTotal(store);

            foreach (KeyValuePair<string, char> userStorePair in userStoreDic)
            {
                if (userStorePair.Value == store)
                {
                    Clients.Client(userStorePair.Key).UpdateSaleCount(productID, count, employeeCount);
                    Clients.Client(userStorePair.Key).UpdateTotal(Convert.ToDouble(total).ToString("N2"));
                }
            }
        }

        public void UpdateTotal()
        {
            char store = userStoreDic[Context.ConnectionId];
            decimal total;
            total = BSSqlSPs.Store.GetSaleTotal(store);
            Convert.ToDouble(total).ToString("N2");
            foreach (KeyValuePair<string, char> userStorePair in userStoreDic)
            {
                if (userStorePair.Value == userStoreDic[Context.ConnectionId])
                {
                    Clients.Client(userStorePair.Key).UpdateTotal(Convert.ToDouble(total).ToString("N2"));
                }
            }
        }

        protected string GetIPAddress()
        {
            string ipAddress;
            object tempObject;

            Context.Request.Environment.TryGetValue("server.RemoteIpAddress", out tempObject);

            if (tempObject != null)
            {
                ipAddress = (string)tempObject;
            }
            else
            {
                ipAddress = "";
            }

            return ipAddress;
        }

    }
}