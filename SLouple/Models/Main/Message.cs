using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SLouple.MVC.Main
{
    public class Message
    {
        public int userID;
        public string context;
        public string dateTime;
        public string type;

        public Message(int userID, string context, string type)
        {
            this.userID = userID;
            this.context = context;
            this.dateTime = DateTime.Now.ToString("MM.dd HH:mm:ss");
            this.type = type;
        }
    }
}