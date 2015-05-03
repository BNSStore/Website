using Microsoft.AspNet.SignalR;
using SLouple.MVC.Account;
using SLouple.MVC.Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SLouple.MVC.Store
{
    public class SalesHub : Hub
    {
        
        public static readonly TimeSpan startTime = new TimeSpan(9, 0, 0);
        public static readonly TimeSpan endTime = new TimeSpan(15, 0, 0);

        protected static Dictionary<string, int> onlineUsers = new Dictionary<string, int>();
        protected static ArrayList uniqueOnlineUsers = new ArrayList();
        private static Dictionary<string, char> userStoreDic = new Dictionary<string, char>();

        public void Login(int userID, string sessionToken, char store)
        {
            //Valid User and Is Manager
            SLUser user = new SLUser(userID, null, null, GetIPAddress(), sessionToken);
            if (user == null || !user.IsManager())
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
            SqlStoredProcedures sqlSP = new SqlStoredProcedures();
            var shift = sqlSP.StoreGetCurrentShifts();
            if (!shift.ContainsKey(userID) && Char.ToLower(shift[userID]) != Char.ToLower(store))
            {
                return;
            }

            //Add User
            onlineUsers.Add(Context.ConnectionId, userID);
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
                int userID = onlineUsers[Context.ConnectionId];
                onlineUsers.Remove(Context.ConnectionId);
                string key = onlineUsers.FirstOrDefault(x => x.Value == userID).Key;
                if (key == null || key == "")
                {
                    uniqueOnlineUsers.Remove(userID);
                }
            }
            return base.OnDisconnected(stopCalled);
        }

        public void UpdateAllSaleCounts()
        {
            char store = userStoreDic[Context.ConnectionId];
            SqlStoredProcedures sqlSP = new SqlStoredProcedures();
            List<Sale> counts = sqlSP.StoreSelectSales(store);
            foreach (Sale count in counts)
            {
                Clients.Client(Context.ConnectionId).UpdateSaleCount(count.productID, count.count, count.employeeCount);
            }
            decimal total = sqlSP.StoreGetSaleTotal(store);
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
            SqlStoredProcedures sqlSP = new SqlStoredProcedures();
            sqlSP.StoreUpdateSaleCount(Char.ToUpper(store), productID, count, employeeCount);
            decimal total = sqlSP.StoreGetSaleTotal(store);

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
            SqlStoredProcedures sqlSP = new SqlStoredProcedures();
            total = sqlSP.StoreGetSaleTotal(store);
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