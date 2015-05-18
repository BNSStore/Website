using SLouple.MVC.Store;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SLouple.MVC.Shared
{
    public class SqlStoredProcedures
    {
        private Sql sql { get; set; }

        public SqlStoredProcedures()
        {
            this.sql = new Sql();
        }

        public SqlStoredProcedures(string username, string password, string server, string database, bool ssl)
        {
            this.sql = new Sql(username, password, server, database, ssl);
        }

        public SqlStoredProcedures(Sql sql)
        {
            this.sql = sql;
        }

        #region User
        public int UserGetUserID(string username)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@Username", SqlDbType.VarChar, 100, username, false));
            pars.Add(Sql.GenerateSqlParameter("@UserID", SqlDbType.Int, 0, null, true));
            SqlParameterCollection parCol = sql.RunStoredProcedure("User.uspGetUserID", pars);
            int userID = Convert.ToInt32(parCol["@UserID"].Value);
            return userID;
        }

        public string UserGetDisplayName(int userID)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@UserID", SqlDbType.Int, 0, userID, false));
            pars.Add(Sql.GenerateSqlParameter("@DisplayName", SqlDbType.NVarChar, 100, null, true));
            SqlParameterCollection parCol = sql.RunStoredProcedure("User.uspGetDisplayName", pars);
            string displayName = Convert.ToString(parCol["@DisplayName"].Value);
            return displayName;
        }

        public string UserGetMainEmailAddress(int userID)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@UserID", SqlDbType.Int, 0, userID, false));
            pars.Add(Sql.GenerateSqlParameter("@EmailAddress", SqlDbType.NVarChar, 254, null, true));
            SqlParameterCollection parCol = sql.RunStoredProcedure("User.uspGetMainEmailAddress", pars);
            string emailAddress = Convert.ToString(parCol["@EmailAddress"].Value);
            return emailAddress;
        }

        public string UserGetPasswordSalt(int userID)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@UserID", SqlDbType.Int, 0, userID, false));
            pars.Add(Sql.GenerateSqlParameter("@PasswordSalt", SqlDbType.Char, 64, null, true));
            SqlParameterCollection parCol = sql.RunStoredProcedure("User.uspGetPasswordSalt", pars);
            string passwordSalt = Convert.ToString(parCol["@PasswordSalt"].Value);
            return passwordSalt;
        }

        public int UserCreateUser(string username, string displayName, string passwordSalt, string passwordHash, string emailAddress, string langName, string ip)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@UserID", SqlDbType.Int, 0, null, true));
            pars.Add(Sql.GenerateSqlParameter("@Username", SqlDbType.VarChar, 100, username, false));
            pars.Add(Sql.GenerateSqlParameter("@DisplayName", SqlDbType.NVarChar, 100, displayName, false));
            pars.Add(Sql.GenerateSqlParameter("@PasswordSalt", SqlDbType.Char, 64, passwordSalt, false));
            pars.Add(Sql.GenerateSqlParameter("@PasswordHash", SqlDbType.Char, 128, passwordHash, false));
            pars.Add(Sql.GenerateSqlParameter("@EmailAddress", SqlDbType.NVarChar, 254, emailAddress, false));
            pars.Add(Sql.GenerateSqlParameter("@LangName", SqlDbType.VarChar, 100, langName, false));
            pars.Add(Sql.GenerateSqlParameter("@IP", SqlDbType.VarChar, 45, ip, false));
            pars.Add(Sql.GenerateSqlParameter("@ProviderName", SqlDbType.VarChar, 100, Sql.ProviderName, false));
            SqlParameterCollection parCol = sql.RunStoredProcedure("User.uspCreateUser", pars);
            int userID = Convert.ToInt32(parCol["@UserID"].Value);
            return userID;
        }

        public string UserLogin(int userID, string passwordHash, string ip, string sessionToken)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@UserID", SqlDbType.Int, 0, userID, false));
            pars.Add(Sql.GenerateSqlParameter("@IP", SqlDbType.VarChar, 45, ip, false));
            pars.Add(Sql.GenerateSqlParameter("@PasswordHash", SqlDbType.VarChar, 128, passwordHash, false));
            pars.Add(Sql.GenerateSqlParameter("@SessionToken", SqlDbType.VarChar, 32, sessionToken, false));
            pars.Add(Sql.GenerateSqlParameter("@OutputSessionToken", SqlDbType.VarChar, 32, null, true));
            SqlParameterCollection parCol = sql.RunStoredProcedure("User.uspLogin", pars);
            string outputSessionToken = Convert.ToString(parCol["@OutputSessionToken"].Value);
            if (outputSessionToken == "")
            {
                outputSessionToken = null;
            }
            return outputSessionToken;
        }

        public int UserGetMainLang(int userID)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@UserID", SqlDbType.Int, 0, userID, false));
            pars.Add(Sql.GenerateSqlParameter("@LangID", SqlDbType.TinyInt, 0, null, true));
            SqlParameterCollection parCol = sql.RunStoredProcedure("User.uspGetMainLang", pars);
            int langID = Convert.ToInt32(parCol["@LangID"].Value);
            return langID;
        }

        public bool UserEmailExist(string emailAddress)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@EmailAddress", SqlDbType.NVarChar, 254, emailAddress, false));
            pars.Add(Sql.GenerateSqlParameter("@Exist", SqlDbType.Bit, 0, null, true));
            SqlParameterCollection parCol = sql.RunStoredProcedure("User.uspEmailExist", pars);
            bool exist = Convert.ToBoolean(parCol["@Exist"].Value);
            return exist;
        }

        public bool UserUsernameExist(string username)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@Username", SqlDbType.VarChar, 100, username, false));
            pars.Add(Sql.GenerateSqlParameter("@Exist", SqlDbType.Bit, 0, null, true));
            SqlParameterCollection parCol = sql.RunStoredProcedure("User.uspUsernameExist", pars);
            bool exist = Convert.ToBoolean(parCol["@Exist"].Value);
            return exist;
        }

        public string UserAddEmail(int userID, string emailAddress, bool main)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@UserID", SqlDbType.Int, 0, userID, false));
            pars.Add(Sql.GenerateSqlParameter("@EmailAddress", SqlDbType.NVarChar, 254, emailAddress, false));
            pars.Add(Sql.GenerateSqlParameter("@Main", SqlDbType.Bit, 0, main, false));
            pars.Add(Sql.GenerateSqlParameter("@VerifyString", SqlDbType.VarChar, 64, null, true));
            SqlParameterCollection parCol = sql.RunStoredProcedure("User.uspAddEmailAddress", pars);
            string verifyString = Convert.ToString(parCol["@VerifyString"].Value);
            return verifyString;
        }

        public string UserGetEmailVerifyString(string emailAddress)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@EmailAddress", SqlDbType.NVarChar, 254, emailAddress, false));
            pars.Add(Sql.GenerateSqlParameter("@VerifyString", SqlDbType.VarChar, 64, null, true));
            SqlParameterCollection parCol = sql.RunStoredProcedure("User.uspGetEmailVerifyString", pars);
            string verifyString = Convert.ToString(parCol["@VerifyString"].Value);
            return verifyString;

        }

        public bool UserIsEmailVerified(string emailAddress)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@EmailAddress", SqlDbType.NVarChar, 254, emailAddress, false));
            pars.Add(Sql.GenerateSqlParameter("@Verified", SqlDbType.Bit, 0, null, true));
            SqlParameterCollection parCol = sql.RunStoredProcedure("User.uspIsEmailVerified", pars);
            bool verified = Convert.ToBoolean(parCol["@Verified"].Value);
            return verified;
        }

        public bool UserVerifyEmail(string emailAddress, string verifyString)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@EmailAddress", SqlDbType.NVarChar, 254, emailAddress, false));
            pars.Add(Sql.GenerateSqlParameter("@VerifyString", SqlDbType.VarChar, 64, verifyString, false));
            pars.Add(Sql.GenerateSqlParameter("@Verified", SqlDbType.Bit, 0, null, true));
            SqlParameterCollection parCol = sql.RunStoredProcedure("User.uspVerifyEmail", pars);
            bool verified = Convert.ToBoolean(parCol["@Verified"].Value);
            return verified;
        }

        public bool UserIsEmailSub(int userID)
        {
            bool isEmailSub = UserIsEmailSub(userID, null);
            return isEmailSub;
        }

        public bool UserIsEmailSub(string emailAddress)
        {
            bool isEmailSub = UserIsEmailSub(-1, emailAddress);
            return isEmailSub;
        }

        public bool UserIsEmailSub(int userID, string emailAddress)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            if (userID > 0)
            {
                pars.Add(Sql.GenerateSqlParameter("@UserID", SqlDbType.Int, 0, userID, false));
            }
            pars.Add(Sql.GenerateSqlParameter("@EmailAddress", SqlDbType.NVarChar, 254, emailAddress, false));
            pars.Add(Sql.GenerateSqlParameter("@IsEmailSub", SqlDbType.Bit, 0, null, true));
            SqlParameterCollection parCol = sql.RunStoredProcedure("User.uspIsEmailSub", pars);
            bool isEmailSub = Convert.ToBoolean(parCol["@IsEmailSub"].Value);
            return isEmailSub;
        }

        public void UserAddEmailSub(int userID)
        {
            UserAddEmailSub(userID, null);
        }

        public void UserAddEmailSub(string emailAddress)
        {
            UserAddEmailSub(-1, emailAddress);
        }

        public void UserAddEmailSub(int userID, string emailAddress)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            if (userID > 0)
            {
                pars.Add(Sql.GenerateSqlParameter("@UserID", SqlDbType.Int, 0, userID, false));
            }
            pars.Add(Sql.GenerateSqlParameter("@EmailAddress", SqlDbType.NVarChar, 254, emailAddress, false));
            SqlParameterCollection parCol = sql.RunStoredProcedure("User.uspAddEmailSub", pars);
        }

        public void UserDelEmailSub(int userID)
        {
            UserDelEmailSub(userID, null);
        }

        public void UserDelEmailSub(string emailAddress)
        {
            UserDelEmailSub(-1, emailAddress);
        }

        public void UserDelEmailSub(int userID, string emailAddress)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            if (userID > 0)
            {
                pars.Add(Sql.GenerateSqlParameter("@UserID", SqlDbType.Int, 0, userID, false));
            }
            pars.Add(Sql.GenerateSqlParameter("@EmailAddress", SqlDbType.NVarChar, 254, emailAddress, false));
            SqlParameterCollection parCol = sql.RunStoredProcedure("User.uspDelEmailSub", pars);
        }


        #endregion

        #region Lang

        public int LangGetLangID(string langName)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@LangID", SqlDbType.TinyInt, 0, null, true));
            pars.Add(Sql.GenerateSqlParameter("@LangName", SqlDbType.VarChar, 100, langName, false));
            SqlParameterCollection parCol = sql.RunStoredProcedure("Lang.uspGetLangID", pars);
            int langID = Convert.ToInt32(parCol["@LangID"].Value);
            return langID;
        }

        public string LangGetLangName(int langID)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@LangID", SqlDbType.TinyInt, 0, langID, false));
            pars.Add(Sql.GenerateSqlParameter("@LangName", SqlDbType.VarChar, 100, null, true));
            SqlParameterCollection parCol = sql.RunStoredProcedure("Lang.uspGetLangName", pars);
            string langName = Convert.ToString(parCol["@LangName"].Value);
            return langName;
        }

        public string LangGetLangCode(int langID)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@LangID", SqlDbType.TinyInt, 0, langID, false));
            pars.Add(Sql.GenerateSqlParameter("@LangCode", SqlDbType.VarChar, 7, null, true));
            SqlParameterCollection parCol = sql.RunStoredProcedure("Lang.uspGetLangCode", pars);
            string langCode = Convert.ToString(parCol["@LangCode"].Value);
            return langCode;
        }

        public string LangGetTranslation(int langID, string providerName, string keywords)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@LangID", SqlDbType.TinyInt, 0, langID, false));
            pars.Add(Sql.GenerateSqlParameter("@ProviderName", SqlDbType.VarChar, 100, providerName, false));
            pars.Add(Sql.GenerateSqlParameter("@Keywords", SqlDbType.VarChar, -1, keywords, false));
            pars.Add(Sql.GenerateSqlParameter("@Context", SqlDbType.NVarChar, -1, null, true));
            SqlParameterCollection parCol = sql.RunStoredProcedure("Lang.uspGetTranslation", pars);
            string context = Convert.ToString(parCol["@Context"].Value);
            return context;
        }

        public void LangAddTranslation(string langName, string providerName, string keyword, string context)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@LangName", SqlDbType.VarChar, 100, langName, false));
            pars.Add(Sql.GenerateSqlParameter("@ProviderName", SqlDbType.VarChar, 100, providerName, false));
            pars.Add(Sql.GenerateSqlParameter("@Keyword", SqlDbType.VarChar, 100, keyword, false));
            pars.Add(Sql.GenerateSqlParameter("@Context", SqlDbType.NVarChar, -1, context, false));
            SqlParameterCollection parCol = sql.RunStoredProcedure("Lang.uspAddTranslation", pars);
        }

        public List<string> LangSelectLangList()
        {
            return sql.RunStoredProcedure("Lang.uspSelectLangList", null, new string[] { "LangName" })["LangName"].ConvertAll(obj => obj.ToString());
        }

        public string LangGetLangNameNative(string langName)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@LangName", SqlDbType.VarChar, 100, langName, false));
            pars.Add(Sql.GenerateSqlParameter("@LangNameNative", SqlDbType.NVarChar, -1, null, true));
            SqlParameterCollection parCol = sql.RunStoredProcedure("Lang.uspGetLangNameNative", pars);
            string langNameNative = Convert.ToString(parCol["@LangNameNative"].Value);
            return langNameNative;
        }

        public Dictionary<string, string> LangSelectLangListNative()
        {
            Dictionary<string, List<object>> columns = sql.RunStoredProcedure("Lang.uspSelectLangListNative", null, new string[] { "LangName", "LangNameNative" });
            Dictionary<string, string> langNameNative = new Dictionary<string, string>();
            for (int i = 0; i < columns["LangName"].Count; i++)
            {
                langNameNative.Add(Convert.ToString(columns["LangName"][i]), Convert.ToString(columns["LangNameNative"][i]));
            }
            return langNameNative;
        }

        #endregion

        #region Store

        #region Product

        #region Product Control

        public void StoreDelProduct(string productName)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@ProductName", SqlDbType.VarChar, 100, productName, false));
            SqlParameterCollection parCol = sql.RunStoredProcedure("Store.uspDelProduct", pars);
        }

        public void StoreAddProduct(string productName, decimal productPrice, decimal employeePrice, string categoryName, bool online)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@ProductName", SqlDbType.VarChar, 100, productName, false));
            pars.Add(Sql.GenerateSqlParameter("@ProductPrice", SqlDbType.SmallMoney, 0, productPrice, false));
            pars.Add(Sql.GenerateSqlParameter("@EmployeePrice", SqlDbType.SmallMoney, 0, employeePrice, false));
            pars.Add(Sql.GenerateSqlParameter("@CategoryName", SqlDbType.VarChar, 100, categoryName, false));
            pars.Add(Sql.GenerateSqlParameter("@Online", SqlDbType.Bit, 0, online, false));
            SqlParameterCollection parCol = sql.RunStoredProcedure("Store.uspAddProduct", pars);
        }

        public void StoreAddCategory(string categoryName)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@CategoryName", SqlDbType.VarChar, 100, categoryName, false));
            SqlParameterCollection parCol = sql.RunStoredProcedure("Store.uspAddCategory", pars);
        }

        public void StoreDelCategory(string categoryName)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@CategoryName", SqlDbType.VarChar, 100, categoryName, false));
            SqlParameterCollection parCol = sql.RunStoredProcedure("Store.uspDelCategory", pars);
        }

        public void StoreChangeCategoryName(string categoryName, string newCategoryName)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@CategoryName", SqlDbType.VarChar, 100, categoryName, false));
            pars.Add(Sql.GenerateSqlParameter("@NewCategoryName", SqlDbType.VarChar, 100, newCategoryName, false));
            SqlParameterCollection parCol = sql.RunStoredProcedure("Store.uspChangeCategoryName", pars);
        }

        public void StoreChangeProductCategory(string productName, string categoryName)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@ProductName", SqlDbType.VarChar, 100, productName, false));
            pars.Add(Sql.GenerateSqlParameter("@CategoryName", SqlDbType.VarChar, 100, categoryName, false));
            SqlParameterCollection parCol = sql.RunStoredProcedure("Store.uspChangeProductCategory", pars);
        }

        public void StoreChangeProductName(string productName, string newProductName)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@ProductName", SqlDbType.VarChar, 100, productName, false));
            pars.Add(Sql.GenerateSqlParameter("@NewProductName", SqlDbType.VarChar, 100, newProductName, false));
            SqlParameterCollection parCol = sql.RunStoredProcedure("Store.uspChangeProductName", pars);
        }

        public void StoreChangeProductPrice(string productName, decimal productPrice)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@ProductName", SqlDbType.VarChar, 100, productName, false));
            pars.Add(Sql.GenerateSqlParameter("@ProductPrice", SqlDbType.SmallMoney, 0, productPrice, false));
            SqlParameterCollection parCol = sql.RunStoredProcedure("Store.uspChangeProductPrice", pars);
        }

        public void StoreChangeProductEmployeePrice(string productName, decimal employeePrice)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@ProductName", SqlDbType.VarChar, 100, productName, false));
            pars.Add(Sql.GenerateSqlParameter("@EmployeePrice", SqlDbType.SmallMoney, 0, employeePrice, false));
            SqlParameterCollection parCol = sql.RunStoredProcedure("Store.uspChangeProductEmployeePrice", pars);
        }

        public void StoreChangeProductOnSalePrice(string productName, decimal? onSalePrice)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@ProductName", SqlDbType.VarChar, 100, productName, false));
            pars.Add(Sql.GenerateSqlParameter("@OnSalePrice", SqlDbType.SmallMoney, 0, onSalePrice, false));
            SqlParameterCollection parCol = sql.RunStoredProcedure("Store.uspChangeProductOnSalePrice", pars);
        }

        #endregion

        public int StoreGetProductID(string productName)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@ProductName", SqlDbType.VarChar, 100, productName, false));
            pars.Add(Sql.GenerateSqlParameter("@ProductID", SqlDbType.Int, 0, null, true));
            SqlParameterCollection parCol = sql.RunStoredProcedure("Store.uspGetProductID", pars);
            int productID = Convert.ToInt32(parCol["@ProductID"].Value);
            return productID;
        }

        public string StoreGetProductName(int productID)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@ProductName", SqlDbType.VarChar, 100, null, true));
            pars.Add(Sql.GenerateSqlParameter("@ProductID", SqlDbType.Int, 0, productID, false));
            SqlParameterCollection parCol = sql.RunStoredProcedure("Store.uspGetProductName", pars);
            string productName = Convert.ToString(parCol["@ProductName"].Value);
            return productName;
        }

        public decimal StoreGetProductPrice(int productID)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@ProductID", SqlDbType.Int, 0, productID, false));
            pars.Add(Sql.GenerateSqlParameter("@ProductPrice", SqlDbType.SmallMoney, 0, null, true));
            SqlParameterCollection parCol = sql.RunStoredProcedure("Store.uspGetProductPrice", pars);
            decimal productPrice = Convert.ToDecimal(parCol["@ProductPrice"].Value);
            return productPrice;
        }

        public decimal StoreGetProductEmployeePrice(int productID)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@ProductID", SqlDbType.Int, 0, productID, false));
            pars.Add(Sql.GenerateSqlParameter("@EmployeePrice", SqlDbType.SmallMoney, 0, null, true));
            SqlParameterCollection parCol = sql.RunStoredProcedure("Store.uspGetProductEmployeePrice", pars);
            decimal employeePrice = Convert.ToDecimal(parCol["@EmployeePrice"].Value);
            return employeePrice;
        }

        public decimal? StoreGetOnSalePrice(int productID)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@ProductID", SqlDbType.Int, 0, productID, false));
            pars.Add(Sql.GenerateSqlParameter("@OnSalePrice", SqlDbType.SmallMoney, 0, null, true));
            SqlParameterCollection parCol = sql.RunStoredProcedure("Store.uspGetProductOnSalePrice", pars);
            if (parCol["@OnSalePrice"].Value == DBNull.Value)
            {
                return null;
            }
            else
            {
                decimal onSalePrice = Convert.ToDecimal(parCol["@OnSalePrice"].Value);
                return onSalePrice;
            }
        }

        public int StoreGetProductCategoryID(int productID)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@ProductID", SqlDbType.Int, 0, productID, false));
            pars.Add(Sql.GenerateSqlParameter("@CategoryID", SqlDbType.TinyInt, 0, null, true));
            SqlParameterCollection parCol = sql.RunStoredProcedure("Store.uspGetCategoryID", pars);
            int categoryID = Convert.ToInt32(parCol["@CategoryID"].Value);
            return categoryID;
        }

        public bool StoreIsProductOnline(int productID)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@ProductID", SqlDbType.Int, 0, productID, false));
            pars.Add(Sql.GenerateSqlParameter("@Online", SqlDbType.Bit, 0, null, true));
            SqlParameterCollection parCol = sql.RunStoredProcedure("Store.uspIsProductOnline", pars);
            bool online = Convert.ToBoolean(parCol["@Online"].Value);
            return online;
        }

        public List<Product> StoreSelectProducts(int productID, string keyword, int categoryId, decimal minPrice, decimal maxPrice)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            if (productID > 0)
            {
                pars.Add(Sql.GenerateSqlParameter("@ProductID", SqlDbType.SmallInt, 0, productID, false));
            }
            if (keyword != null)
            {
                pars.Add(Sql.GenerateSqlParameter("@Keyword", SqlDbType.VarChar, 100, keyword, false));
            }
            if (categoryId > 0)
            {
                pars.Add(Sql.GenerateSqlParameter("@CategoryID", SqlDbType.TinyInt, 0, categoryId, false));
            }
            if (minPrice > 0)
            {
                pars.Add(Sql.GenerateSqlParameter("@MinPrice", SqlDbType.SmallMoney, 0, minPrice, false));
            }
            if (maxPrice > 0)
            {
                pars.Add(Sql.GenerateSqlParameter("@MaxPrice", SqlDbType.SmallMoney, 0, maxPrice, false));
            }
            List<Product> products = new List<Product>();
            Dictionary<string, List<object>> columns = sql.RunStoredProcedure("Store.uspSelectProducts", pars, new string[] { "ProductID", "ProductName", "ProductPrice", "EmployeePrice", "OnSalePrice", "CategoryID", "Online" });
            for (int i = 0; i < columns["ProductID"].Count; i++)
            {
                if (columns["OnSalePrice"][i] == DBNull.Value)
                {
                    products.Add(new Product(
                        Convert.ToInt32(columns["ProductID"][i]),
                        Convert.ToString(columns["ProductName"][i]),
                        Convert.ToDecimal(columns["ProductPrice"][i]),
                        Convert.ToDecimal(columns["EmployeePrice"][i]),
                        Convert.ToInt32(columns["CategoryID"][i]),
                        Convert.ToBoolean(columns["Online"][i])
                        ));
                }
                else
                {
                    products.Add(new Product(
                        Convert.ToInt32(columns["ProductID"][i]),
                        Convert.ToString(columns["ProductName"][i]),
                        Convert.ToDecimal(columns["ProductPrice"][i]),
                        Convert.ToDecimal(columns["EmployeePrice"][i]),
                        Convert.ToDecimal(columns["OnSalePrice"][i]),
                        Convert.ToInt32(columns["CategoryID"][i]),
                        Convert.ToBoolean(columns["Online"][i])
                        ));
                }
            }
            return products;
        }

        public Dictionary<int, string> StoreSelectProductCategories()
        {
            Dictionary<int, string> categories = new Dictionary<int, string>();
            Dictionary<string, List<object>> columns = sql.RunStoredProcedure("Store.uspSelectProductCategories", null, new string[] { "CategoryID", "CategoryName" });
            for (int i = 0; i < columns["CategoryID"].Count; i++)
            {
                categories.Add(Convert.ToInt32(columns["CategoryID"][i]), Convert.ToString(columns["CategoryName"][i]));
            }
            return categories;
        }

        public int StoreGetCategoryID(string categoryName){
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@CategoryID", SqlDbType.TinyInt, 0, null, true));
            pars.Add(Sql.GenerateSqlParameter("@CategoryName", SqlDbType.VarChar, 100, categoryName, false));
            SqlParameterCollection parCol = sql.RunStoredProcedure("Store.uspGetCategoryID", pars);
            int categoryID = Convert.ToInt32(parCol["@CategoryID"].Value);
            return categoryID;
        }

        public string StoreGetCategoryName(int categoryID){
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@CategoryID", SqlDbType.TinyInt, 0, categoryID, false));
            pars.Add(Sql.GenerateSqlParameter("@CategoryName", SqlDbType.VarChar, 100, null, true));
            SqlParameterCollection parCol = sql.RunStoredProcedure("Store.uspGetCategoryName", pars);
            string categoryName = Convert.ToString(parCol["@CategoryID"].Value);
            return categoryName;
        }

        #endregion

        #region Sales

        public List<Sale> StoreSelectSales(char store)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@Store", SqlDbType.Char, 1, store, false));
            List<Sale> counts = new List<Sale>();
            Dictionary<string, List<object>> columns = sql.RunStoredProcedure("Store.uspSelectAllSaleCounts", pars, new string[] { "ProductID", "Count", "EmployeeCount" });
            for (int i = 0; i < columns["ProductID"].Count; i++)
            {
                counts.Add(new Sale(
                    Convert.ToInt32(columns["ProductID"][i]),
                    Convert.ToInt32(columns["Count"][i]),
                    Convert.ToInt32(columns["EmployeeCount"][i])
                    ));
            }
            return counts;
        }

        public List<Sale> StoreSelectSales(DateTime startDate, DateTime endDate)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@StartDate", SqlDbType.Date, 0, startDate, false));
            pars.Add(Sql.GenerateSqlParameter("@EndDate", SqlDbType.Date, 0, endDate, false));

            List<Sale> saleCounts = new List<Sale>();
            Dictionary<string, List<object>> columns = sql.RunStoredProcedure("Store.uspSelectSales", pars, new string[] { "Date", "Store", "ProductName", "Count", "EmployeeCount" });
            for (int i = 0; i < columns["Date"].Count; i++)
            {
                saleCounts.Add(new Sale(
                    Convert.ToDateTime(columns["Date"][i]),
                    Convert.ToString(columns["ProductName"][i]),
                    Convert.ToChar(columns["Store"][i]),
                    Convert.ToInt32(columns["Count"][i]),
                    Convert.ToInt32(columns["EmployeeCount"][i])
                    ));
            }
            return saleCounts;
        }

        public void StoreUpdateSaleCount(char store, int productID, int count, int employeeCount)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@Store", SqlDbType.Char, 1, store, false));
            pars.Add(Sql.GenerateSqlParameter("@ProductID", SqlDbType.SmallInt, 0, productID, false));
            pars.Add(Sql.GenerateSqlParameter("@Count", SqlDbType.SmallInt, 100, count, false));
            pars.Add(Sql.GenerateSqlParameter("@EmployeeCount", SqlDbType.SmallInt, 100, employeeCount, false));
            SqlParameterCollection parCol = sql.RunStoredProcedure("Store.uspUpdateSaleCount", pars);
        }

        public decimal StoreGetSaleTotal(char store)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@Store", SqlDbType.Char, 1, store, false));
            pars.Add(Sql.GenerateSqlParameter("@Total", SqlDbType.SmallMoney, 0, null, true));
            SqlParameterCollection parCol = sql.RunStoredProcedure("Store.uspGetSaleTotal", pars);
            decimal total = Convert.ToDecimal(parCol["@Total"].Value);
            return total;
        }

        #endregion

        #region Schedule

        public void StoreAddShift(int userID, DateTime date, char store)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@UserID", SqlDbType.Int, 0, userID, false));
            pars.Add(Sql.GenerateSqlParameter("@Date", SqlDbType.Date, 0, date, false));
            pars.Add(Sql.GenerateSqlParameter("@Store", SqlDbType.Char, 1, store, false));
            SqlParameterCollection parCol = sql.RunStoredProcedure("Store.uspAddShift", pars);
        }

        public void StoreAddShift(string firstName, string lastName, DateTime date, char store)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@FirstName", SqlDbType.NVarChar, 100, firstName, false));
            pars.Add(Sql.GenerateSqlParameter("@LastName", SqlDbType.NVarChar, 100, lastName, false));
            pars.Add(Sql.GenerateSqlParameter("@Date", SqlDbType.Date, 0, date, false));
            pars.Add(Sql.GenerateSqlParameter("@Store", SqlDbType.Char, 1, store, false));
            SqlParameterCollection parCol = sql.RunStoredProcedure("Store.uspAddShift", pars);

        }

        public void StoreDelShift(int userID, DateTime date)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@UserID", SqlDbType.Int, 0, userID, false));
            pars.Add(Sql.GenerateSqlParameter("@Date", SqlDbType.Date, 0, date, false));
            SqlParameterCollection parCol = sql.RunStoredProcedure("Store.uspDelShift", pars);
        }

        public void StoreDelShift(string firstName, string lastName, DateTime date)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@FirstName", SqlDbType.NVarChar, 100, firstName, false));
            pars.Add(Sql.GenerateSqlParameter("@LastName", SqlDbType.NVarChar, 100, lastName, false));
            pars.Add(Sql.GenerateSqlParameter("@Date", SqlDbType.Date, 0, date, false));
            SqlParameterCollection parCol = sql.RunStoredProcedure("Store.uspDelShift", pars);
        }

        public Dictionary<int, char> StoreGetCurrentShifts()
        {
            Dictionary<int, char> shifts = new Dictionary<int, char>();
            Dictionary<string, List<object>> columns = sql.RunStoredProcedure("Store.uspGetCurrentShifts", null, new string[] { "Date", "UserID", "Store" });
            for (int i = 0; i < columns["Date"].Count; i++)
            {
                shifts.Add(Convert.ToInt32(columns["UserID"][i]), Convert.ToChar(columns["Store"][i]));
            }
            return shifts;
        }

        public Dictionary<int, char> StoreGetNextShifts()
        {
            Dictionary<int, char> shifts = new Dictionary<int, char>();
            Dictionary<string, List<object>> columns = sql.RunStoredProcedure("Store.uspGetNextShifts", null, new string[] { "Date", "UserID", "Store" });
            for (int i = 0; i < columns["Date"].Count; i++)
            {
                shifts.Add(Convert.ToInt32(columns["UserID"][i]), Convert.ToChar(columns["Store"][i]));
            }
            return shifts;
        }

        public Dictionary<DateTime, Dictionary<int, char>> StoreSelectSchedule(int userID)
        {
            Dictionary<DateTime, Dictionary<int, char>> shifts = new Dictionary<DateTime, Dictionary<int, char>>();

            List<SqlParameter> pars = new List<SqlParameter>();
            if (userID > 0)
            {
                pars.Add(Sql.GenerateSqlParameter("@UserID", SqlDbType.Int, 0, userID, false));
            }

            Dictionary<string, List<object>> columns = sql.RunStoredProcedure("Store.uspSelectSchedule", pars, new string[] { "Date", "UserID", "Store" });
            for (int i = 0; i < columns["Date"].Count; i++)
            {
                DateTime shiftDate;
                DateTime.TryParse(Convert.ToString(columns["Date"][i]), out shiftDate);
                if (!shifts.ContainsKey(shiftDate))
                {
                    shifts.Add(shiftDate, new Dictionary<int, char>());
                }
                shifts[shiftDate].Add(Convert.ToInt32(columns["UserID"][i]), Convert.ToChar(columns["Store"][i]));
            }
            return shifts;
        }

        public Dictionary<DateTime, Dictionary<string, char>> StoreSelectScheduleWithDisplayName(int userID)
        {
            Dictionary<DateTime, Dictionary<string, char>> shifts = new Dictionary<DateTime, Dictionary<string, char>>();

            List<SqlParameter> pars = new List<SqlParameter>();
            if (userID > 0)
            {
                pars.Add(Sql.GenerateSqlParameter("@UserID", SqlDbType.Int, 0, userID, false));
            }

            Dictionary<string, List<object>> columns = sql.RunStoredProcedure("Store.uspSelectSchedule", pars, new string[] { "Date", "UserID", "Store" });
            for (int i = 0; i < columns["Date"].Count; i++)
            {
                DateTime shiftDate;
                DateTime.TryParse(Convert.ToString(columns["Date"][i]), out shiftDate);
                if (!shifts.ContainsKey(shiftDate))
                {
                    shifts.Add(shiftDate, new Dictionary<string, char>());
                }
                string displayName = this.UserGetDisplayName(Convert.ToInt32(columns["UserID"][i]));
                shifts[shiftDate].Add(displayName, Convert.ToChar(columns["Store"][i]));
            }
            return shifts;
        }

        public List<Shift> StoreSelectUnmarkedSchedule(int userID)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@UserID", SqlDbType.Int, 0, userID, false));
            List<Shift> schedule = new List<Shift>();
            Dictionary<string, List<object>> columns = sql.RunStoredProcedure("Store.uspSelectUnmarkedSchedule", pars, new string[] { "Date", "UserID", "Store" });
            for (int i = 0; i < columns["Date"].Count; i++)
            {
                schedule.Add(new Shift(
                    Convert.ToDateTime(columns["Date"][i]),
                    Convert.ToInt32(columns["UserID"][i]),
                    Convert.ToChar(columns["Store"][i])
                    ));
            }
            return schedule;
        }

        public void StoreUpdateMarkAndComment(int userID, DateTime date, int mark, string comment)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@UserID", SqlDbType.Int, 0, userID, false));
            pars.Add(Sql.GenerateSqlParameter("@Date", SqlDbType.Date, 0, date, false));
            pars.Add(Sql.GenerateSqlParameter("@Mark", SqlDbType.TinyInt, 0, mark, false));
            pars.Add(Sql.GenerateSqlParameter("@Comment", SqlDbType.NVarChar, -1, comment, false));
            SqlParameterCollection parCol = sql.RunStoredProcedure("Store.uspUpdateMarkAndComment", pars);
        }

        #endregion

        #region Employee

        public int StoreGetEmployeeGroupID(int userID)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@UserID", SqlDbType.Int, 0, userID, false));
            pars.Add(Sql.GenerateSqlParameter("@GroupID", SqlDbType.TinyInt, 0, null, true));
            SqlParameterCollection parCol = sql.RunStoredProcedure("Store.uspGetEmployeeGroupID", pars);
            int groupID = Convert.ToInt32(parCol["@GroupID"].Value);
            return groupID;
        }

        public string StoreGetGroupName(int groupID)
        {
            List<SqlParameter> pars = new List<SqlParameter>();

            pars.Add(Sql.GenerateSqlParameter("@GroupID", SqlDbType.TinyInt, 0, groupID, false));
            pars.Add(Sql.GenerateSqlParameter("@GroupName", SqlDbType.VarChar, 50, null, true));
            SqlParameterCollection parCol = sql.RunStoredProcedure("Store.uspGetGroupName", pars);
            string groupName = Convert.ToString(parCol["@GroupName"].Value);
            return groupName;
        }

        public int StoreGetEmployeeID(string firstName, string lastName)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@FirstName", SqlDbType.NVarChar, 100, firstName, false));
            pars.Add(Sql.GenerateSqlParameter("@LastName", SqlDbType.NVarChar, 100, lastName, false));
            pars.Add(Sql.GenerateSqlParameter("@UserID", SqlDbType.Int, 0, null, true));
            SqlParameterCollection parCol = sql.RunStoredProcedure("Store.uspGetEmployeeID", pars);
            int userID = Convert.ToInt32(parCol["@UserID"].Value);
            return userID;
        }

        public bool StoreIsEmployee(int userID)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@UserID", SqlDbType.Int, 0, userID, false));
            pars.Add(Sql.GenerateSqlParameter("@IsEmployee", SqlDbType.Bit, 0, null, true));
            SqlParameterCollection parCol = sql.RunStoredProcedure("Store.uspIsEmployee", pars);
            bool isEmployee = Convert.ToBoolean(parCol["@IsEmployee"].Value);
            return isEmployee;
        }

        public bool StoreIsManager(int userID)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@UserID", SqlDbType.Int, 0, userID, false));
            pars.Add(Sql.GenerateSqlParameter("@IsManager", SqlDbType.Bit, 0, null, true));
            SqlParameterCollection parCol = sql.RunStoredProcedure("Store.uspIsManager", pars);
            bool isManager = Convert.ToBoolean(parCol["@IsManager"].Value);
            return isManager;
        }

        public bool StoreHasMissedShift(int userID)
        {
            List<SqlParameter> pars = new List<SqlParameter>();

            pars.Add(Sql.GenerateSqlParameter("@UserID", SqlDbType.Int, 0, userID, false));
            pars.Add(Sql.GenerateSqlParameter("@HasMissedShift", SqlDbType.Bit, 0, null, true));
            SqlParameterCollection parCol = sql.RunStoredProcedure("Store.uspHasMissedShift", pars);
            bool hasMissedShift = Convert.ToBoolean(parCol["@HasMissedShift"].Value);
            return hasMissedShift;
        }

        #endregion

        #region ShiftRequest

        public void StoreAddShiftRequest(int senderID, int recieverID, DateTime date)
        {
            List<SqlParameter> pars = new List<SqlParameter>();

            pars.Add(Sql.GenerateSqlParameter("@SenderID", SqlDbType.Int, 0, senderID, false));
            pars.Add(Sql.GenerateSqlParameter("@RecieverID", SqlDbType.Int, 0, recieverID, false));
            pars.Add(Sql.GenerateSqlParameter("@Date", SqlDbType.Date, 0, date, false));
            SqlParameterCollection parCol = sql.RunStoredProcedure("Store.uspAddShiftRequest", pars);
        }

        public Dictionary<int, DateTime> StoreSelectShiftRequest(int recieverID)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@RecieverID", SqlDbType.Int, 0, recieverID, false));
            Dictionary<int, DateTime> shiftRequests = new Dictionary<int, DateTime>();
            Dictionary<string, List<object>> columns = sql.RunStoredProcedure("Store.uspSelectShiftRequest", pars, new string[] { "SenderID", "Date" });
            for (int i = 0; i < columns["SenderID"].Count; i++)
            {
                shiftRequests.Add(
                    Convert.ToInt32(columns["SenderID"][i]),
                    Convert.ToDateTime(columns["Date"][i])
                    );
            }
            return shiftRequests;
        }

        public void StoreAcceptShiftRequest(int senderID, int recieverID, DateTime date)
        {
            List<SqlParameter> pars = new List<SqlParameter>();

            pars.Add(Sql.GenerateSqlParameter("@SenderID", SqlDbType.Int, 0, senderID, false));
            pars.Add(Sql.GenerateSqlParameter("@RecieverID", SqlDbType.Int, 0, recieverID, false));
            pars.Add(Sql.GenerateSqlParameter("@Date", SqlDbType.Date, 0, date, false));
            SqlParameterCollection parCol = sql.RunStoredProcedure("Store.uspAcceptShiftRequest", pars);
        }

        public void StoreDeclineShiftRequest(int senderID, int recieverID, DateTime date)
        {
            List<SqlParameter> pars = new List<SqlParameter>();

            pars.Add(Sql.GenerateSqlParameter("@SenderID", SqlDbType.Int, 0, senderID, false));
            pars.Add(Sql.GenerateSqlParameter("@RecieverID", SqlDbType.Int, 0, recieverID, false));
            pars.Add(Sql.GenerateSqlParameter("@Date", SqlDbType.Date, 0, date, false));
            SqlParameterCollection parCol = sql.RunStoredProcedure("Store.uspDeclineShiftRequest", pars);
        }

        #endregion

        #endregion

        #region Support

        public void SupportAddTicket(string ticketTitle, int ticketCategoryID, int rating, string comment, string email, string phoneNumber, string name)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(Sql.GenerateSqlParameter("@TicketTitle", SqlDbType.NVarChar, 200, ticketTitle, false));
            pars.Add(Sql.GenerateSqlParameter("@TicketCategoryID", SqlDbType.TinyInt, 0, ticketCategoryID, false));
            pars.Add(Sql.GenerateSqlParameter("@Rating", SqlDbType.TinyInt, 0, rating, false));
            pars.Add(Sql.GenerateSqlParameter("@Comment", SqlDbType.NVarChar, -1, comment, false));
            if (email != null && email.Length < 254)
            {
                pars.Add(Sql.GenerateSqlParameter("@Email", SqlDbType.NVarChar, 254, email, false));
            }
            if (phoneNumber != null && phoneNumber.Length < 15)
            {
                pars.Add(Sql.GenerateSqlParameter("@PhoneNumber", SqlDbType.VarChar, 15, phoneNumber, false));
            }
            if (name != null && name.Length < 100)
            {
                pars.Add(Sql.GenerateSqlParameter("@Name", SqlDbType.NVarChar, 100, name, false));
            }
            SqlParameterCollection parCol = sql.RunStoredProcedure("Support.uspAddTicket", pars);
        }

        public Dictionary<int, string> SupportSelectTicketCategories()
        {
            Dictionary<int, string> categories = new Dictionary<int, string>();
            Dictionary<string, List<object>> columns = sql.RunStoredProcedure("Support.uspSelectTicketCategories", null, new string[] { "TicketCategoryID", "TicketCategoryName" });
            for (int i = 0; i < columns["TicketCategoryID"].Count; i++)
            {
                categories.Add(Convert.ToInt32(columns["TicketCategoryID"][i]), Convert.ToString(columns["TicketCategoryName"][i]));
            }
            return categories;
        }

        #endregion

    }
}
