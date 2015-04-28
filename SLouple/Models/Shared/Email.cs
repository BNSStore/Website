using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace SLouple.MVC.Shared
{
    public class Email : IDisposable
    {
        public const string serverAddress = "mail.bnsstore.com";
#if DEBUG
        public const int serverPort = 587;
#else
        public const int serverPort = 25;
#endif
        private const string accountEmailAddress = "account@bnsstore.com";
        private const string accountEmailPassword = "bbysd41its_";
        private const string adminEmailAddress = "admin@bnsstore.com";
        private const string adminEmailPassword = "bbysd41its_";
        private const string noReplyEmailAddress = "no-reply@bnsstore.com";
        private const string noReplyEmailPassword = "bbysd41its_";
        private const string humanResourcesEmailAddress = "humanresources@bnsstore.com";
        private const string humanResourcesEmailPassword = "hrbnsstore_";

        public SmtpClient emailClient;
        public string senderAddress;

        public Email(string senderAddress, string senderPassword)
        {
            Init(serverAddress, serverPort, senderAddress, senderPassword);
        }

        public Email(string serverAddress, int serverPort, string senderAddress, string senderPassword)
        {
            Init(serverAddress, serverPort, senderAddress, senderPassword);
        }

        private void Init(string serverAddress, int serverPort, string senderAddress, string senderPassword)
        {
            emailClient = new SmtpClient(serverAddress, serverPort);
            this.senderAddress = senderAddress;
            NetworkCredential credential = new NetworkCredential(senderAddress, senderPassword);
            emailClient.UseDefaultCredentials = false;
            emailClient.Credentials = credential;
            emailClient.EnableSsl = false;
            emailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        }

        public void Send(string recieverAddress, string subject, string body)
        {
            MailMessage message = new MailMessage(senderAddress, recieverAddress, subject, body);
            emailClient.Send(message);
        }

        public void Dispose()
        {
            emailClient.Dispose();
        }

        #region Email Accounts

        public static void SendNoReplyEmail(string recieverAddress, string subject, string body)
        {
            using (Email email = new Email(noReplyEmailAddress, noReplyEmailPassword))
            {
                email.Send(recieverAddress, subject, body);
            }
        }

        public static void SendHumanResourcesEmail(string recieverAddress, string subject, string body)
        {
            using (Email email = new Email(humanResourcesEmailAddress, humanResourcesEmailPassword))
            {
                email.Send(recieverAddress, subject, body);
            }
        }

        #endregion

        #region Emails

        public static void SendVerifyEmail(string emailAddress, string verifyString, string langName)
        {
            Lang lang = new Lang(langName);
            lang.AddKeyword("Account.Email.VerifyEmail.Subject");
            lang.AddKeyword("Account.Email.VerifyEmail.Body");
            lang.GetTranslationFromSQL();
            string subject = lang.GetTrans("Account.Email.VerifyEmail.Subject");
            string body = lang.GetTrans("Account.Email.VerifyEmail.Body");
            body = body.Replace("\\n", "\n").Replace("{URL}", "http://account.bnsstore.com/VerifyEmail/emailAddress=" + emailAddress + "/verifyString=" + verifyString + "/");
            SendNoReplyEmail(emailAddress, subject, body);
        }



        public static void SendShiftNotificationEmail(string emailAddress, string displayName, char store, string langName)
        {
            Lang lang = new Lang(langName);
            lang.AddKeyword("Store.Schedule.Notification.Email.Subject");
            lang.AddKeyword("Store.Schedule.Notification.Email.Body");
            lang.AddKeyword("Store.Name." + store.ToString().ToUpper());
            lang.GetTranslationFromSQL();
            string subject = lang.GetTrans("Store.Schedule.Notification.Email.Subject");
            string body = lang.GetTrans("Store.Schedule.Notification.Email.Body");
            string storeName = lang.GetTrans("Store.Name." + store.ToString().ToUpper());
            body = body.Replace("\\n", "\n");
            body = body.Replace("{DisplayName}", displayName);
            body = body.Replace("{StoreName}", storeName);
            SendHumanResourcesEmail(emailAddress, subject, body);
        }
        #endregion
    }
}