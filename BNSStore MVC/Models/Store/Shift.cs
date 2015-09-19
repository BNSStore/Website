using SLouple.MVC.Account;
using System;

namespace BNSStore.MVC.Store
{
    public class Shift
    {
        public DateTime date { get; set; }
        public User user { get; set; }
        public char store { get; set; }
        public int? mark { get; set; }
        public string comment { get; set; }

        public Shift(DateTime date, User user, char store)
        {
            this.date = date;
            this.user = user;
            this.store = store;
        }

        public Shift(DateTime date, User user, char store, int mark, string comment)
        {
            this.date = date;
            this.user = user;
            this.store = store;
            this.mark = mark;
            this.comment = comment;
        }
    }
}