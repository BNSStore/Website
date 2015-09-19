using SLouple.MVC.Account;
using System;

namespace BNSStore.MVC.Main
{
    public class Message
    {
        public User user { get; set; }
        public string context { get; set; }
        public string dateTime { get; set; }
        public string type { get; set; }

        public Message(User user, string context, string type)
        {
            this.user = user;
            this.context = context;
            this.dateTime = DateTime.Now.ToString("MM.dd HH:mm:ss");
            this.type = type;
        }
    }
}