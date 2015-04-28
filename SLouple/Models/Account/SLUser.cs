using SLouple.MVC.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace SLouple.MVC.Account
{
    public class SLUser
    {
        public int userID { get; set; }
        public string displayName { get; set; }
        public string sessionToken { get; set; }
        private bool? employee;
        private bool? manager;
        private int? storeGroupID;
        private string storeGroupName;

        public SLUser(int userID, string username, string password, string ip, string sessionToken)
        {
            if (username != null)
            {
                ValidateUsername(username);
                SqlStoredProcedures sqlSP = new SqlStoredProcedures();
                this.userID = sqlSP.UserGetUserID(username);
                if (this.userID < 0)
                {
                    return;
                }
            }
            else
            {
                ValidateUserID(userID);
                this.userID = userID;
            }
            this.sessionToken = Login(this.userID, password, ip, sessionToken);
            this.displayName = GetDisplayName(this.userID);
        }

        public int GetLangID()
        {
            SqlStoredProcedures sqlSP = new SqlStoredProcedures();
            return sqlSP.UserGetMainLang(this.userID);
        }

        //??
        public bool SendVerifyEmail()
        {
            if (sessionToken != null)
            {

            }
            return false;
        }

        public bool IsEmployee()
        {
            if (employee == null)
            {
                SqlStoredProcedures sqlSP = new SqlStoredProcedures();
                employee = sqlSP.StoreIsEmployee(this.userID);
            }
            return (bool)(employee);
        }

        public bool IsManager()
        {
            if (manager == null)
            {
                if (!IsEmployee())
                {
                    manager = false;
                }
                else
                {
                    SqlStoredProcedures sqlSP = new SqlStoredProcedures();
                    manager = sqlSP.StoreIsManager(this.userID);
                }
            }
            return (bool)(manager);
        }

        public int GetStoreGroupID()
        {
            if (!IsEmployee())
            {
                return -1;
            }
            else if (storeGroupID != null)
            {
                return (int)storeGroupID;
            }
            else
            {
                SqlStoredProcedures sqlSP = new SqlStoredProcedures();
                storeGroupID = sqlSP.StoreGetEmployeeGroupID(userID);
                return (int)storeGroupID;
            }
        }

        public string GetStoreGroupName()
        {
            if (!IsEmployee())
            {
                return null;
            }
            else
            {
                SqlStoredProcedures sqlSP = new SqlStoredProcedures();
                storeGroupName = sqlSP.StoreGetGroupName(GetStoreGroupID());
                return storeGroupName;
            }
        }

        public static string GetDisplayName(int userID)
        {
            SqlStoredProcedures sqlSP = new SqlStoredProcedures();
            string displayName = sqlSP.UserGetDisplayName(userID);
            return displayName;
        }

        public static string Login(int userID, string password, string ip, string sessionToken)
        {
            ValidateUserID(userID);
            SqlStoredProcedures sqlSP = new SqlStoredProcedures();
            string outputSessionToken;
            if (sessionToken != null)
            {
                ValidateSessionToken(sessionToken);
                try
                {
                    outputSessionToken = sqlSP.UserLogin(userID, null, ip, sessionToken);
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                ValidatePassword(password);
                string passwordSalt = sqlSP.UserGetPasswordSalt(userID);
                string passwordHash = Hash.HashString(password, passwordSalt, new SHA3.SHA3Managed(512));
                try
                {
                    outputSessionToken = sqlSP.UserLogin(userID, passwordHash, ip, null);
                }
                catch
                {
                    return null;
                }
            }
            return outputSessionToken;
        }

        public static int CreateUser(string username, string displayName, string password, string emailAddress, string langName, string ip, string reCapResponse)
        {
            ValidateUsername(username);
            ValidateDisplayName(displayName);
            ValidatePassword(password);
            ValidateEmail(emailAddress);
            ValidateLangName(langName);
            ValidateIP(ip);
            ValidateReCapResponse(reCapResponse, ip);

            string passwordSalt = Hash.GenerateSalt(64);
            string passwordHash = Hash.HashString(password, passwordSalt, new SHA3.SHA3Managed(512));
            SqlStoredProcedures sqlSP = new SqlStoredProcedures();
            try
            {
                int userID = sqlSP.UserCreateUser(username, displayName, passwordSalt, passwordHash, emailAddress, langName, ip);
                string verifyString = sqlSP.UserGetEmailVerifyString(emailAddress);
                Email.SendVerifyEmail(emailAddress, verifyString, langName);
                return userID;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return -1;
            }
            
        }

        public static bool VerifyEmail(string emailAddress, string verifyString)
        {
            SqlStoredProcedures sqlSP = new SqlStoredProcedures();
            return sqlSP.UserVerifyEmail(emailAddress, verifyString);
        }

        #region Validations
        private static void ValidateUserID(int userID)
        {
            if(userID < 1){
                throw new ArgumentException("userID must be greater than or equal to 1");
            }
        }

        private static void ValidateUsername(string username)
        {
            if (username.Length < 4 || username.Length > 100)
            {
                throw new ArgumentException("username length must be betweem 4 to 101");
            }
            if (Encoding.UTF8.GetByteCount(username) != username.Length)
            {
                throw new ArgumentException("username must be in ASCII");
            }
            Regex r = new Regex("[^a-zA-Z0-9_-]");
            if(r.IsMatch(username))
            {
                throw new ArgumentException("username must be in a-z A-Z 0-9 and _-");
            }
        }

        private static void ValidatePassword(string password)
        {
            if (password.Length < 8 || password.Length > 100)
            {
                throw new ArgumentException("password length must be betweem 8 to 101");
            }
            if (Encoding.UTF8.GetByteCount(password) != password.Length)
            {
                throw new ArgumentException("password must be in ASCII");
            }
            
        }

        private static void ValidateDisplayName(string displayName)
        {
            if (displayName.Length > 100)
            {
                throw new ArgumentException("displayName length must be lower than 101");
            }else if (displayName == "")
            {
                throw new ArgumentException("displayName must not be blank");
            }
        }

        private static void ValidateEmail(string emailAddress)
        {
            if (emailAddress.Length > 254)
            {
                throw new ArgumentException("emailAddress length must be lower than 255");
            }
            try
            {
                var address = new System.Net.Mail.MailAddress(emailAddress);
            }
            catch
            {
                throw new ArgumentException("emailAddress must be a valid email address");
            }
        }

        private static void ValidateLangName(string langName)
        {
            if (langName.Length > 100)
            {
                throw new ArgumentException("langName length must be lower than 101");
            }
        }

        private static void ValidateIP(string ip)
        {
            if (ip.Length > 45)
            {
                throw new ArgumentException("ip length must be lower than 46");
            }
        }

        private static void ValidatePasswordHash(string passwordHash)
        {
            if (passwordHash.Length != 128)
            {
                throw new ArgumentException("passwordHash length must be 128"); ;
            }
            if (Encoding.UTF8.GetByteCount(passwordHash) != passwordHash.Length)
            {
                throw new ArgumentException("passwordHash must be in ASCII");
            }
        }

        private static void ValidateSessionToken(string sessionToken)
        {
            if (sessionToken.Length != 32)
            {
                throw new ArgumentException("sessionToken length must be 32"); ;
            }
            if (Encoding.UTF8.GetByteCount(sessionToken) != sessionToken.Length)
            {
                throw new ArgumentException("sessionToken must be in ASCII");
            }
        }

        private static void ValidateReCapResponse(string reCapResponse, string ip)
        {
            using (WebClient client = new WebClient())
            {
                string secretKey = "6LeOnAATAAAAAOQZfeSz9728PcdU0FXfdm8F4E0h";
                string url = "https://www.google.com/recaptcha/api/siteverify?";
                url += "secret=" + secretKey + "&";
                url += "response=" + reCapResponse + "&";
                url += "remoteip=" + ip;
                string responseString = client.DownloadString(url);
                if (!responseString.Contains("\"success\": true"))
                {
                    throw new ArgumentException("invalid reCapResponse");
                }
            }
        }
        #endregion
    }
}
