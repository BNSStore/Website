using SLouple.MVC.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SLouple.MVC.Main
{
    public class Ticket
    {
        public int ticketID;
        public string ticketTitle;
        public string ticketCategoryName;
        public int ticketCategoryID;
        public int rating;
        public string comment;
        public string email;
        public string phoneNumber;
        public string name;

        public Ticket(string ticketTitle, int ticketCategoryID, int rating, string comment, string name, string email, string phoneNumber)
        {
            this.ticketTitle = ticketTitle;
            this.ticketCategoryID = ticketCategoryID;
            this.rating = rating;
            this.comment = comment;
            this.name = name;
            this.phoneNumber = phoneNumber;
            this.email = email;
        }

        public void AddTicket()
        {
            TicketTitleCheck();
            TicketCategoryIDCheck();
            TicketRatingCheck();
            TicketCommentCheck();
            TicketNameCheck();
            TicketPhoneNumberCheck();
            TicketEmailCheck();
            SqlStoredProcedures sp = new SqlStoredProcedures();
            sp.SupportAddTicket(this.ticketTitle, this.ticketCategoryID, this.rating, this.comment, this.email, this.phoneNumber, this.name);
        }

        public static Dictionary<int, string> GetAllTicketCategoryies()
        {
            SqlStoredProcedures sp = new SqlStoredProcedures();
            return sp.SupportSelectTicketCategories();
        }

        private void TicketTitleCheck()
        {
            if (ticketTitle == null || ticketTitle == "")
            {
                throw new ArgumentException();
            }
        }

        private void TicketCategoryIDCheck()
        {
            if (ticketCategoryID < 0)
            {
                throw new ArgumentException();
            }
        }

        private void TicketRatingCheck()
        {
            if (rating < 0)
            {
                throw new ArgumentException();
            }
        }

        private void TicketCommentCheck()
        {
            if (comment == null || comment == "")
            {
                throw new ArgumentException();
            }
        }

        private void TicketNameCheck()
        {
            if (name == null)
            {
                throw new ArgumentException();
            }
        }

        private void TicketEmailCheck()
        {
            if (email == null)
            {
                throw new ArgumentException();
            }
        }

        private void TicketPhoneNumberCheck()
        {
            if (phoneNumber == null)
            {
                throw new ArgumentException();
            }
        }
    }
}