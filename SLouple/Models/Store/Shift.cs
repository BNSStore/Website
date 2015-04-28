using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SLouple.MVC.Store
{
    public class Shift
    {
        public DateTime date;
        public int userID;
        public char store;
        public int? mark;
        public string comment;

        public Shift(DateTime date, int userID, char store)
        {
            this.date = date;
            this.userID = userID;
            this.store = store;
        }

        public Shift(DateTime date, int userID, char store, int mark, string comment)
        {
            this.date = date;
            this.userID = userID;
            this.store = store;
            this.mark = mark;
            this.comment = comment;
        }
    }
}