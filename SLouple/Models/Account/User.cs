using SLouple.MVC.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace SLouple.MVC.Account
{
    public class User : IEquatable<User>
    {
        private int? userID;
        private string username;
        private string displayName;
        private string fullName;
        private string sessionToken;
        private Lang lang;
        private List<Role> roles;

        public User(int userID)
        {
            this.userID = userID;
        }

        public User(string username)
        {
            this.username = username;
        }

        public void SignInWithPassword(string password, string ip)
        {
            SqlStoredProcedures sqlSP = new SqlStoredProcedures();
            try
            {
                string passwordHash = Hash.HashString(password, sqlSP.UserGetPasswordSalt(GetUserID()), new SHA3.SHA3Managed(512));
                this.sessionToken = sqlSP.UserLogin(GetUserID(), passwordHash, ip, null);
            }
            catch
            {
                this.sessionToken = null;
            }
        }

        public void SignInWithSessionToken(string sessionToken, string ip)
        {
            SqlStoredProcedures sqlSP = new SqlStoredProcedures();
            try
            {
                this.sessionToken = sqlSP.UserLogin(GetUserID(), null, ip, sessionToken);
            }
            catch
            {
                this.sessionToken = null;
            }
        }

        public string GetSessionToken()
        {
            return sessionToken;
        }

        public int GetUserID()
        {
            if (userID == null)
            {
                SqlStoredProcedures sqlSP = new SqlStoredProcedures();
                userID = sqlSP.UserGetUserID(username);
            }
            return userID.Value;
        }

        public string GetUsername()
        {
            if (username == null)
            {
                SqlStoredProcedures sqlSP = new SqlStoredProcedures();
                username = sqlSP.UserGetUsername(userID.Value);
            }
            return username;
        }

        public Lang GetLang()
        {
            if (lang == null)
            {
                SqlStoredProcedures sqlSP = new SqlStoredProcedures();
                lang = new Lang(sqlSP.UserGetMainLang(GetUserID()));
            }
            return lang;
        }

        public string GetDisplayName()
        {
            if (displayName == null)
            {
                SqlStoredProcedures sqlSP = new SqlStoredProcedures();
                displayName = sqlSP.UserGetDisplayName(GetUserID());
            }
            if (HasRole("Store.Employee") || HasRole("Store.Manager"))
            {
                return displayName + " (" + GetFullName() + ")";
            }
            return displayName;
        }

        public string GetFullName()
        {
            if (fullName == null)
            {
                SqlStoredProcedures sqlSP = new SqlStoredProcedures();
                fullName = sqlSP.UserGetFullName(GetUserID());
            }

            return fullName;
        }

        public List<Role> GetRoles()
        {
            if (roles == null)
            {
                SqlStoredProcedures sqlSP = new SqlStoredProcedures();
                roles = sqlSP.PermissionSelectRoleFromUserRole(GetUserID());
            }
            return roles;
        }

        public bool HasRole(string roleName)
        {
            return HasRole(new Role(roleName));
        }

        public bool HasRole(Role role)
        {
            if (roles == null)
            {
                GetRoles();
            }
            return roles.Contains(role);
        }

        public bool HasPolicy(string policyName)
        {
            return HasPolicy(new Policy(policyName));
        }

        public bool HasPolicy(Policy policy)
        {
            SqlStoredProcedures sqlSP = new SqlStoredProcedures();
            return sqlSP.PermissionDoesUserContainPolicy(GetUserID(), policy.GetPolicyName());
        }

        public bool Equals(User other)
        {
            if (userID == null)
            {
                return other.GetUsername() == GetUsername();
            }
            else
            {
                return other.GetUserID() == GetUserID();
            }
        }

        public static User CreateUser(string username, string displayName, string password, string emailAddress, string langName, string ip, string reCapResponse)
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
                User user = new User(userID);
                user.SignInWithPassword(password, ip);
                return user;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
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
