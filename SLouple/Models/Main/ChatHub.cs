using System;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Collections.Generic;
using System.Collections;
using System.Threading.Tasks;
using System.Linq;
using SLouple.MVC.Account;
using SLouple.MVC.Shared;

namespace SLouple.MVC.Main
{
    public class ChatHub : Hub
    {
        protected static Dictionary<string, int> onlineUsers = new Dictionary<string, int>();
        protected static List<int> uniqueOnlineUsers = new List<int>();
        private static List<Message> chatLog = new List<Message>();

        public virtual void Login(int userID, string sessionToken)
        {
            User user = new User(userID, null, null, GetIPAddress(), sessionToken);
            if (user != null)
            {
                onlineUsers.Add(Context.ConnectionId, userID);
                if (!uniqueOnlineUsers.Contains(userID))
                {
                    uniqueOnlineUsers.Add(userID);
                }
                LoggedIn();
            }
        }

        public void LoggedIn()
        {
            SqlStoredProcedures sqlSP = new SqlStoredProcedures();
            for (int i = chatLog.Count - 50 - 1 < 0 ? 0 : chatLog.Count - 50 - 1; i < chatLog.Count; i++)
            {
                Message message = chatLog[i];
                string type = message.type;
                if (message.userID == onlineUsers[Context.ConnectionId])
                {
                    type = "self";
                }
                Clients.Client(Context.ConnectionId).recieveMessage(sqlSP.UserGetDisplayName(message.userID), message.context, type, message.dateTime);
            }
            UpdateOnlineUserList();
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
                    OnUniqueUserDisconnected();
                }
            }
            return base.OnDisconnected(stopCalled);
        }

        public void OnUniqueUserDisconnected()
        {

            UpdateOnlineUserList();
        }

        public void SendMessage(string context)
        {
            if (!onlineUsers.ContainsKey(Context.ConnectionId))
            {
                return;
            }

            int userID = onlineUsers[Context.ConnectionId];
            string displayName = User.GetDisplayName(userID);
            string type = "normal";

            SqlStoredProcedures sqlSP = new SqlStoredProcedures();
             if (sqlSP.StoreIsManager(userID))
            {
                type = "manager";
            }
            else if (sqlSP.StoreIsEmployee(userID))
            {
                type = "employee";
            }
            else if (userID == 33)
            {
                type = "system";
            }

            Message message = new Message(userID, context, type);
            chatLog.Add(message);
            if (chatLog.Count > 100)
            {
                chatLog.RemoveRange(0, 10);
            }

            foreach (KeyValuePair<string, int> onlineUser in onlineUsers)
            {
                if (onlineUser.Value == userID)
                {
                    Clients.Client(onlineUser.Key).recieveMessage(displayName, context, "self", message.dateTime);
                }
                else
                {
                    Clients.Client(onlineUser.Key).recieveMessage(displayName, context, type, message.dateTime);
                }
            }
        }

        public void UpdateOnlineUserList()
        {
            ArrayList displayNames = new ArrayList();
            foreach (int userID in uniqueOnlineUsers)
            {
                displayNames.Add(User.GetDisplayName(userID));
            }
            Clients.All.updateOnlineUserList(displayNames.ToArray());
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
