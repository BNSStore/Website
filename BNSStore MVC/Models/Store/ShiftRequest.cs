using System;

namespace BNSStore.MVC.Store
{
    public class ShiftRequest
    {
        public DateTime date { get; }
        public int senderID { get; }
        public int recieverID { get; }

        public ShiftRequest(DateTime date, int senderID, int recieverID)
        {
            this.date = date;
            this.senderID = senderID;
            this.recieverID = recieverID;
        }
    }
}