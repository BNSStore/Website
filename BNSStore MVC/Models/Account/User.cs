using BNSStore.MVC.Shared;
using SLouple.MVC.Shared;
using System;
using System.Collections.Generic;
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
        private Lang mainLang;
        private List<Role> roles;
        private List<Policy> policies;

        public User(int userID)
        {
            ValidateUserID(userID);
            this.userID = userID;
        }

        public User(string username)
        {
            ValidateUsername(username);
            this.username = username;
        }

        public void SignInWithPassword(string password, string ip)
        {
            try
            {
                ValidatePassword(password);
                ValidateIP(ip);
                string passwordHash = Hash.HashString(password, SLSqlSPs.Account.GetPasswordSalt(GetUserID()), new SHA3.SHA3Managed(512));
                this.sessionToken = SLSqlSPs.Account.Login(GetUserID(), ip, passwordHash, null, false);
            }
            catch
            {
                this.sessionToken = null;
            }
        }

        public void SignInWithSessionToken(string sessionToken, string ip)
        {
            try
            {
                ValidateSessionToken(sessionToken);
                ValidateIP(ip);
                this.sessionToken = SLSqlSPs.Account.Login(GetUserID(), ip, null, sessionToken, false);
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
                userID = SLSqlSPs.Account.GetUserID(username);
            }
            return userID.Value;
        }

        public string GetUsername()
        {
            if (username == null)
            {
                username = SLSqlSPs.Account.GetUsername(userID.Value);
            }
            return username;
        }

        public Lang GetMainLang()
        {
            if (mainLang == null)
            {
                mainLang = SLSqlSPs.Account.GetMainLang(GetUserID());
            }
            return mainLang;
        }

        public string GetDisplayName()
        {
            if (displayName == null)
            {
                displayName = SLSqlSPs.Account.GetDisplayName(GetUserID());
            }
            return displayName;
        }

        public string GetFullName()
        {
            if (fullName == null)
            {
                fullName = SLSqlSPs.Account.GetFullName(GetUserID());
            }

            return fullName;
        }

        public List<Role> GetRoles()
        {
            if (roles == null)
            {
                roles = SLSqlSPs.Permission.SelectRoleFromUser(GetUserID());
            }
            return roles;
        }

        public List<Policy> GetPolicies()
        {
            if (policies == null)
            {
                policies = SLSqlSPs.Permission.SelectPolicyFromUser(GetUserID());
            }
            return policies;
        }

        public bool HasRole(Role role)
        {
            return SLSqlSPs.Permission.DoesUserContainRole(GetUserID(), role.GetRoleName(), role.GetProvider().GetProviderName());
        }

        public bool HasPolicy(Policy policy)
        {
            return SLSqlSPs.Permission.DoesUserContainPolicy(GetUserID(), policy.GetPolicyName(), policy.GetProvider().GetProviderName());
        }

        public override bool Equals(object other)
        {
            if (other.GetType() == typeof(User))
            {
                return Equals((User)other);
            }
            else
            {
                return false;
            }
        }

        public bool Equals(User other)
        {
            if(other == null)
            {
                return false;
            }
            if (userID == null)
            {
                return other.GetUsername() == GetUsername();
            }
            else
            {
                return other.GetUserID() == GetUserID();
            }
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return GetUserID().GetHashCode();
            }
        }

        public static bool operator ==(User self, User other)
        {
            if (object.ReferenceEquals(self, null) || object.ReferenceEquals(other, null))
            {
                return (object.ReferenceEquals(self, null) && object.ReferenceEquals(other, null));
            }
            return self.Equals(other);
        }

        public static bool operator !=(User self, User other)
        {
            return !(self == other);
        }

        public static User CreateUser(string username, string displayName, string password, string emailAddress, string verifyString, string langName, string ip)
        {
            ValidateUsername(username);
            ValidateDisplayName(displayName);
            ValidatePassword(password);
            ValidateEmail(emailAddress);
            ValidateVerifyString(emailAddress, verifyString);
            ValidateLangName(langName);
            ValidateIP(ip);

            string passwordSalt = Hash.GenerateSalt(64);
            string passwordHash = Hash.HashString(password, passwordSalt, new SHA3.SHA3Managed(512));
            try
            {
                int userID = SLSqlSPs.Account.CreateUser(username, displayName, passwordSalt, passwordHash, emailAddress, verifyString, langName, ip);
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

        private static void ValidateVerifyString(string emailAddress, string verifyString)
        {
            if(!SLSqlSPs.Account.IsVerifyStringValid(emailAddress, verifyString))
            {
                throw new ArgumentException("Not valid verifyString");
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
        #endregion
    }
}
