using Microsoft.AspNet.SignalR;
using System.Collections.Generic;
using System.Collections;
using System.Threading.Tasks;
using System.Linq;
using SLouple.MVC.Account;
using SLouple.MVC.Shared;

namespace BNSStore.MVC.Main
{
    public class ChatHub : Hub
    {
        protected static Dictionary<string, User> onlineUsers = new Dictionary<string, User>();
        protected static List<User> uniqueOnlineUsers = new List<User>();
        private static List<Message> chatLog = new List<Message>();

        public virtual void Login(int userID, string sessionToken)
        {
            User user = new User(userID);
            user.SignInWithSessionToken(sessionToken, GetIPAddress());
            if (user.GetSessionToken() != null)
            {
                onlineUsers.Add(Context.ConnectionId, new User(userID));
                if (!uniqueOnlineUsers.Contains(new User(userID)))
                {
                    uniqueOnlineUsers.Add(new User(userID));
                }
                LoggedIn();
            }
        }

        public void LoggedIn()
        {
            for (int i = chatLog.Count - 50 - 1 < 0 ? 0 : chatLog.Count - 50 - 1; i < chatLog.Count; i++)
            {
                Message message = chatLog[i];
                string type = message.type;
                if (message.user == onlineUsers[Context.ConnectionId])
                {
                    type = "self";
                }
                Clients.Client(Context.ConnectionId).recieveMessage(SLSqlSPs.Account.GetDisplayName(message.user.GetUserID()), message.context, type, message.dateTime);
            }
            UpdateOnlineUserList();
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

            User user = onlineUsers[Context.ConnectionId];
            string displayName = user.GetDisplayName();
            string type = "normal";
            if (user.HasRole(new Role(MVCApp.CurrentProvider, "Store.Admin")))
            {
                type = "admin";
            }else if (user.HasRole(new Role(MVCApp.CurrentProvider, "Store.Manager")))
            {
                type = "manager";
            }
            else if (user.HasRole(new Role(MVCApp.CurrentProvider, "Store.Employee")))
            {
                type = "employee";
            }

            Message message = new Message(user, context, type);
            chatLog.Add(message);
            if (chatLog.Count > 100)
            {
                chatLog.RemoveRange(0, 10);
            }
            foreach (KeyValuePair<string, User> onlineUser in onlineUsers)
            {
                if (onlineUser.Value == user)
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
            foreach (User user in uniqueOnlineUsers)
            {
                displayNames.Add(user.GetDisplayName());
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
