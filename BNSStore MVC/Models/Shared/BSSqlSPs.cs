using BNSStore.MVC.Store;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Data.Entity;
using System.Runtime.CompilerServices;
using BNSStore.MVC.Support;
using SLouple.MVC.Shared;
using SLouple.MVC.Account;

namespace BNSStore.MVC.Shared
{
    public class BSSqlSPs
    {
        protected const string ProviderName = "BNSStore";
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["BNSStore"].ConnectionString;

        public static class dbo {

            private static SqlSP GetSqlSP([CallerMemberName] string name = "")
            {
                return new SqlSP("dbo.usp" + name, new Sql(connectionString));
            }


            [Obsolete]
            public static int GenerateRandomInt(int max, int min)
            {
                SqlSP sqlSP = GetSqlSP();
                sqlSP.AddPar("Max", SqlDbType.Int, 0, max, false);
                sqlSP.AddPar("Min", SqlDbType.Int, 0, min, false);
                sqlSP.AddPar("RandomInt", SqlDbType.Int, 0, null, true);
                return sqlSP.GetOutputParValue<int>("RandomInt");
            }


            [Obsolete]
            public static string GenerateRandomString(int length, string list, char letterCase, bool numbers)
            {
                SqlSP sqlSP = GetSqlSP();
                sqlSP.AddPar("Length", SqlDbType.SmallInt, 0, length, false);
                sqlSP.AddPar("List", SqlDbType.NVarChar, -1, list, false);
                sqlSP.AddPar("LetterCase", SqlDbType.Char, 1, letterCase, false);
                sqlSP.AddPar("Numbers", SqlDbType.Bit, 0, numbers, false);
                sqlSP.AddPar("RandomString", SqlDbType.NVarChar, -1, null, true);
                return sqlSP.GetOutputParValue<string>("RandomString");
            }

        }

        public static class Lang
        {
            private static SqlSP GetSqlSP([CallerMemberName] string name = "")
            {
                return new SqlSP("Lang.usp" + name, new Sql(connectionString));
            }

            public static void AddTranslation(int langID, string keyword, string context)
            {
                SqlSP sqlSP = GetSqlSP();
                sqlSP.AddPar("LangID", SqlDbType.TinyInt, langID);
                sqlSP.AddPar("Keyword", SqlDbType.VarChar, 100, keyword, false);
                sqlSP.AddPar("Context", SqlDbType.NVarChar, -1, context, false);
                sqlSP.Run();
            }

            public static string GetLangCode(int langID)
            {
                SqlSP sqlSP = GetSqlSP();
                sqlSP.AddPar("LangID", SqlDbType.TinyInt, langID);
                sqlSP.AddPar("LangCode", SqlDbType.VarChar, 7, null, true);
                return sqlSP.GetOutputParValue<string>("LangCode");
            }

            public static int GetLangID(string langName)
            {
                SqlSP sqlSP = GetSqlSP();
                sqlSP.AddPar("LangID", SqlDbType.TinyInt, 0, null, true);
                sqlSP.AddPar("LangName", SqlDbType.VarChar, 100, langName, false);
                return sqlSP.GetOutputParValue<int>("LangID");
            }

            public static string GetLangName(int langID)
            {
                SqlSP sqlSP = GetSqlSP();
                sqlSP.AddPar("LangID", SqlDbType.TinyInt, 0, langID, false);
                sqlSP.AddPar("LangName", SqlDbType.VarChar, 100, null, true);
                return sqlSP.GetOutputParValue<string>("LangName");
            }


            public static string GetLangNameNative(int langID)
            {
                SqlSP sqlSP = GetSqlSP();
                sqlSP.AddPar("LangID", SqlDbType.TinyInt, 0, langID, false);
                sqlSP.AddPar("LangNameNative", SqlDbType.NVarChar, -1, null, true);
                return sqlSP.GetOutputParValue<string>("LangNameNative");
            }


            public static string GetTranslation(int langID, string keyword)
            {
                SqlSP sqlSP = GetSqlSP();
                sqlSP.AddPar("LangID", SqlDbType.TinyInt, 0, langID, false);
                sqlSP.AddPar("Keyword", SqlDbType.VarChar, 100, keyword, false);
                sqlSP.AddPar("Context", SqlDbType.NVarChar, -1, null, true);
                return sqlSP.GetOutputParValue<string>("Context");
            }

            public static List<string> SelectLangName()
            {
                SqlSP sqlSP = GetSqlSP();
                return sqlSP.GetOutputColumn<string>("LangName");
            }

            public static Dictionary<string, string> SelectLangNameNative()
            {
                SqlSP sqlSP = GetSqlSP();
                var langNames = sqlSP.GetOutputColumn<string>("LangName");
                var langNamesNative = sqlSP.GetOutputColumn<string>("LangNameNative");
                return langNames.ToDictionary(x => x, x => langNamesNative[langNames.IndexOf(x)]);
            }

            public static List<string> SelectTranslation(int langID, List<string> keywords)
            {
                SqlSP sqlSP = GetSqlSP();
                sqlSP.AddPar("LangID", SqlDbType.TinyInt, 0, langID, false);
                var keywordsDic = new Dictionary<string, ArrayList>();
                keywordsDic.Add("Keyword", new ArrayList(keywords));
                sqlSP.AddTablePar("Keywords", keywordsDic);
                return sqlSP.GetOutputColumn<string>("Context");
            }

        }

        public static class Store
        {
            private static SqlSP GetSqlSP([CallerMemberName] string name = "")
            {
                return new SqlSP("Store.usp" + name, new Sql(connectionString));
            }


            public static void AcceptShiftRequest(int senderID, int recieverID, DateTime date)
            {
                SqlSP sqlSP = GetSqlSP();
                sqlSP.AddPar("SenderID", SqlDbType.Int, 0, senderID, false);
                sqlSP.AddPar("RecieverID", SqlDbType.Int, 0, recieverID, false);
                sqlSP.AddPar("Date", SqlDbType.Date, 0, date, false);
                sqlSP.Run();
            }


            public static void AddCategory(string categoryName)
            {
                SqlSP sqlSP = GetSqlSP();
                sqlSP.AddPar("CategoryName", SqlDbType.VarChar, 100, categoryName, false);
                sqlSP.Run();
            }


            public static void AddMarkAndComment(DateTime date, int userID, char store, int mark, string comment)
            {
                SqlSP sqlSP = GetSqlSP();
                sqlSP.AddPar("Date", SqlDbType.Date, 0, date, false);
                sqlSP.AddPar("UserID", SqlDbType.Int, 0, userID, false);
                sqlSP.AddPar("Store", SqlDbType.Char, 1, store, false);
                sqlSP.AddPar("Mark", SqlDbType.TinyInt, 0, mark, false);
                sqlSP.AddPar("Comment", SqlDbType.NVarChar, -1, comment, false);
                sqlSP.Run();
            }

            public static void AddProduct(string productName, decimal productPrice, decimal employeePrice, int categoryID, bool online)
            {
                SqlSP sqlSP = GetSqlSP();
                sqlSP.AddPar("ProductName", SqlDbType.VarChar, 100, productName, false);
                sqlSP.AddPar("ProductPrice", SqlDbType.SmallMoney, 0, productPrice, false);
                sqlSP.AddPar("EmployeePrice", SqlDbType.SmallMoney, 0, employeePrice, false);
                sqlSP.AddPar("CategoryID", SqlDbType.TinyInt, 0, categoryID, false);
                sqlSP.AddPar("Online", SqlDbType.Bit, 100, online, false);
                sqlSP.Run();
            }


            public static void AddShift(int userID, DateTime date, char store)
            {
                SqlSP sqlSP = GetSqlSP();
                sqlSP.AddPar("UserID", SqlDbType.Int, 0, userID, false);
                sqlSP.AddPar("Date", SqlDbType.Date, 0, date, false);
                sqlSP.AddPar("Store", SqlDbType.Char, 1, store, false);
                sqlSP.Run();
            }


            public static void AddShiftRequest(int senderID, int recieverID, DateTime date)
            {
                SqlSP sqlSP = GetSqlSP();
                sqlSP.AddPar("SenderID", SqlDbType.Int, 0, senderID, false);
                sqlSP.AddPar("RecieverID", SqlDbType.Int, 0, recieverID, false);
                sqlSP.AddPar("Date", SqlDbType.Date, 0, date, false);
                sqlSP.Run();
            }


            public static void ChangeCategoryName(int categoryID, string newCategoryName)
            {
                SqlSP sqlSP = GetSqlSP();
                sqlSP.AddPar("CategoryID", SqlDbType.TinyInt, 0, categoryID, false);
                sqlSP.AddPar("NewCategoryName", SqlDbType.VarChar, 100, newCategoryName, false);
                sqlSP.Run();
            }


            public static void ChangeProductCategory(int productID, int categoryID)
            {
                SqlSP sqlSP = GetSqlSP();
                sqlSP.AddPar("ProductID", SqlDbType.SmallInt, 0, productID, false);
                sqlSP.AddPar("CategoryID", SqlDbType.TinyInt, 0, categoryID, false);
                sqlSP.Run();
            }


            public static void ChangeProductEmployeePrice(int productID, decimal employeePrice)
            {
                SqlSP sqlSP = GetSqlSP();
                sqlSP.AddPar("ProductID", SqlDbType.SmallInt, 0, productID, false);
                sqlSP.AddPar("EmployeePrice", SqlDbType.SmallMoney, 0, employeePrice, false);
                sqlSP.Run();
            }


            public static void ChangeProductName(int productID, string productName)
            {
                SqlSP sqlSP = GetSqlSP();
                sqlSP.AddPar("ProductID", SqlDbType.SmallInt, 0, productID, false);
                sqlSP.AddPar("ProductName", SqlDbType.VarChar, 100, productName, false);
                sqlSP.Run();
            }


            public static void ChangeProductOnSalePrice(int productID, decimal? onSalePrice)
            {
                SqlSP sqlSP = GetSqlSP();
                sqlSP.AddPar("ProductID", SqlDbType.SmallInt, 0, productID, false);
                sqlSP.AddPar("OnSalePrice", SqlDbType.SmallMoney, 0, onSalePrice, false);
                sqlSP.Run();
            }


            public static void ChangeProductPrice(int productID, decimal productPrice)
            {
                SqlSP sqlSP = GetSqlSP();
                sqlSP.AddPar("ProductID", SqlDbType.SmallInt, 0, productID, false);
                sqlSP.AddPar("ProductPrice", SqlDbType.SmallMoney, 0, productPrice, false);
                sqlSP.Run();
            }


            public static void DeclineShiftRequest(int senderID, int recieverID, DateTime date)
            {
                SqlSP sqlSP = GetSqlSP();
                sqlSP.AddPar("SenderID", SqlDbType.Int, 0, senderID, false);
                sqlSP.AddPar("RecieverID", SqlDbType.Int, 0, recieverID, false);
                sqlSP.AddPar("Date", SqlDbType.Date, 0, date, false);
                sqlSP.Run();
            }


            public static void DelCategory(int categoryID)
            {
                SqlSP sqlSP = GetSqlSP();
                sqlSP.AddPar("CategoryID", SqlDbType.TinyInt, 0, categoryID, false);
                sqlSP.Run();
            }


            public static void DelProduct(int productID)
            {
                SqlSP sqlSP = GetSqlSP();
                sqlSP.AddPar("ProductID", SqlDbType.SmallInt, 0, productID, false);
                sqlSP.Run();
            }


            public static void DelShift(int userID, DateTime date)
            {
                SqlSP sqlSP = GetSqlSP();
                sqlSP.AddPar("UserID", SqlDbType.Int, 0, userID, false);
                sqlSP.AddPar("Date", SqlDbType.Date, 0, date, false);
                sqlSP.Run();
            }


            public static int GetCategoryID(string categoryName)
            {
                SqlSP sqlSP = GetSqlSP();
                sqlSP.AddPar("CategoryName", SqlDbType.VarChar, 100, categoryName, false);
                sqlSP.AddPar("CategoryID", SqlDbType.TinyInt, 0, null, true);
                return sqlSP.GetOutputParValue<int>("CategoryID");
            }


            public static string GetCategoryName(int categoryID)
            {
                SqlSP sqlSP = GetSqlSP();
                sqlSP.AddPar("CategoryID", SqlDbType.TinyInt, 0, categoryID, false);
                sqlSP.AddPar("CategoryName", SqlDbType.VarChar, 100, null, true);
                return sqlSP.GetOutputParValue<string>("CategoryName");
            }


            public static int GetProductCategoryID(int productID)
            {
                SqlSP sqlSP = GetSqlSP();
                sqlSP.AddPar("ProductID", SqlDbType.SmallInt, 0, productID, false);
                sqlSP.AddPar("CategoryID", SqlDbType.TinyInt, 0, null, true);
                return sqlSP.GetOutputParValue<int>("CategoryID");
            }


            public static decimal GetProductEmployeePrice(int productID)
            {
                SqlSP sqlSP = GetSqlSP();
                sqlSP.AddPar("ProductID", SqlDbType.SmallInt, 0, productID, false);
                sqlSP.AddPar("EmployeePrice", SqlDbType.SmallMoney, 0, null, true);
                return sqlSP.GetOutputParValue<decimal>("EmployeePrice");
            }


            public static int GetProductID(string productName)
            {
                SqlSP sqlSP = GetSqlSP();
                sqlSP.AddPar("ProductName", SqlDbType.VarChar, 100, productName, false);
                sqlSP.AddPar("ProductID", SqlDbType.SmallInt, 0, null, true);
                return sqlSP.GetOutputParValue<int>("ProductID");
            }


            public static string GetProductName(int productID)
            {
                SqlSP sqlSP = GetSqlSP();
                sqlSP.AddPar("ProductID", SqlDbType.SmallInt, 0, productID, false);
                sqlSP.AddPar("ProductName", SqlDbType.VarChar, 100, null, true);
                return sqlSP.GetOutputParValue<string>("ProductName");
            }


            public static decimal? GetProductOnSalePrice(int productID)
            {
                SqlSP sqlSP = GetSqlSP();
                sqlSP.AddPar("ProductID", SqlDbType.SmallInt, 0, productID, false);
                sqlSP.AddPar("OnSalePrice", SqlDbType.SmallMoney, 0, null, true);
                return sqlSP.GetOutputParValue<decimal>("OnSalePrice");
            }


            public static decimal GetProductPrice(int productID)
            {
                SqlSP sqlSP = GetSqlSP();
                sqlSP.AddPar("ProductID", SqlDbType.SmallInt, 0, productID, false);
                sqlSP.AddPar("ProductPrice", SqlDbType.SmallMoney, 0, null, true);
                return sqlSP.GetOutputParValue<decimal>("ProductPrice");
            }

            public static decimal GetSaleTotal(char store, DateTime? date = null) {
                SqlSP sqlSP = GetSqlSP();
                sqlSP.AddPar("Store", SqlDbType.Char, 1, store, false);
                sqlSP.AddPar("Date", SqlDbType.Date, 0, date, false);
                sqlSP.AddPar("Total", SqlDbType.SmallMoney, 0, null, true);
                return sqlSP.GetOutputParValue<decimal>("Total");
            }


            public static bool HasMissedShift(int userID)
            {
                SqlSP sqlSP = GetSqlSP();
                sqlSP.AddPar("UserID", SqlDbType.Int, 0, userID, false);
                sqlSP.AddPar("HasMissedShift", SqlDbType.Bit, 0, null, true);
                return sqlSP.GetOutputParValue<bool>("HasMissedShift");
            }


            public static bool IsProductOnline(int productID)
            {
                SqlSP sqlSP = GetSqlSP();
                sqlSP.AddPar("ProductID", SqlDbType.SmallInt, 0, productID, false);
                sqlSP.AddPar("Online", SqlDbType.Bit, 0, null, true);
                return sqlSP.GetOutputParValue<bool>("Online");
            }


            public static List<Category> SelectProductCategory()
            {
                SqlSP sqlSP = GetSqlSP();
                return sqlSP.GetOutputColumn<int>("CategoryID").Select(x => new Category(x)).ToList();
            }


            public static List<Product> SelectProduct(int? productID = null, string keyword = null, int? categoryID = null, decimal? minPrice = null, decimal? maxPrice = null, bool? online = null)
            {
                SqlSP sqlSP = GetSqlSP();
                sqlSP.AddPar("ProductID", SqlDbType.SmallInt, 0, productID, false);
                sqlSP.AddPar("Keyword", SqlDbType.VarChar, 100, keyword, false);
                sqlSP.AddPar("CategoryID", SqlDbType.TinyInt, 0, categoryID, false);
                sqlSP.AddPar("MinPrice", SqlDbType.SmallMoney, 0, minPrice, false);
                sqlSP.AddPar("MaxPrice", SqlDbType.SmallMoney, 0, maxPrice, false);
                sqlSP.AddPar("Online", SqlDbType.Bit, 0, online, false);
                return sqlSP.GetOutputColumn<int>("ProductID").Select(x => new Product(x)).ToList();
            }


            public static List<Sale> SelectSale(char? store = null, DateTime? startDate = null, DateTime? endDate = null)
            {
                SqlSP sqlSP = GetSqlSP();
                sqlSP.AddPar("Store", SqlDbType.Char, 1, store, false);
                sqlSP.AddPar("StartDate", SqlDbType.Date, 0, startDate, false);
                sqlSP.AddPar("EndDate", SqlDbType.Date, 0, endDate, false);
                List<DateTime> dates = sqlSP.GetOutputColumn<DateTime>("Date");
                List<int> productIDs = sqlSP.GetOutputColumn<int>("ProductID");
                List<char> stores = sqlSP.GetOutputColumn<char>("Store");
                List<int> counts = sqlSP.GetOutputColumn<int>("Count");
                List<int> employeeCounts = sqlSP.GetOutputColumn<int>("EmployeeCount");
                return dates.Select(x =>
                new Sale(
                    x,
                    productIDs[dates.IndexOf(x)],
                    stores[dates.IndexOf(x)],
                    counts[dates.IndexOf(x)],
                    employeeCounts[dates.IndexOf(x)]
                    )
                ).ToList();
            }


            public static List<Shift> SelectSchedule(int? userID = null, DateTime? startDate = null, DateTime? endDate = null)
            {
                SqlSP sqlSP = GetSqlSP();
                sqlSP.AddPar("UserID", SqlDbType.Int, 0, userID, false);
                sqlSP.AddPar("StartDate", SqlDbType.Date, 0, startDate, false);
                sqlSP.AddPar("EndDate", SqlDbType.Date, 0, endDate, false);
                List<DateTime> dates = sqlSP.GetOutputColumn<DateTime>("Date");
                List<int> userIDs = sqlSP.GetOutputColumn<int>("UserID");
                List<char> stores = sqlSP.GetOutputColumn<char>("Store");
                return dates.Select(
                    x => new Shift(
                        x,
                        new User(userIDs[dates.IndexOf(x)]),
                        stores[dates.IndexOf(x)]
                        )
                    ).ToList();
            }


            public static List<ShiftRequest> SelectShiftRequest(int recieverID)
            {
                SqlSP sqlSP = GetSqlSP();
                sqlSP.AddPar("RecieverID", SqlDbType.Int, 0, recieverID, false);
                List<DateTime> dates = sqlSP.GetOutputColumn<DateTime>("Date");
                List<int> senderIDs = sqlSP.GetOutputColumn<int>("SenderID");
                List<char> recieverIDs = sqlSP.GetOutputColumn<char>("RecieverID");
                return dates.Select(
                    x => new ShiftRequest(
                        x,
                        senderIDs[dates.IndexOf(x)],
                        recieverID
                        )
                    ).ToList();
            }


            public static List<Shift> SelectUnmarkedSchedule(int userID)
            {
                SqlSP sqlSP = GetSqlSP();
                sqlSP.AddPar("UserID", SqlDbType.Int, 0, userID, false);
                List<DateTime> dates = sqlSP.GetOutputColumn<DateTime>("Date");
                List<int> userIDs = sqlSP.GetOutputColumn<int>("UserID");
                List<char> stores = sqlSP.GetOutputColumn<char>("Store");
                return dates.Select(
                    x => new Shift(
                        x,
                        new User(userIDs[dates.IndexOf(x)]),
                        stores[dates.IndexOf(x)]
                        )
                    ).ToList();
            }


            public static void UpdateMarkAndComment(int userID, DateTime date, int mark, string comment)
            {
                SqlSP sqlSP = GetSqlSP();
                sqlSP.AddPar("UserID", SqlDbType.Int, 0, userID, false);
                sqlSP.AddPar("Date", SqlDbType.Date, 0, date, false);
                sqlSP.AddPar("Mark", SqlDbType.TinyInt, 0, mark, false);
                sqlSP.AddPar("Comment", SqlDbType.NVarChar, -1, comment, false);
                sqlSP.Run();
            }


            public static void UpdateSaleCount(char store, int productID, int count, int employeeCount)
            {
                SqlSP sqlSP = GetSqlSP();
                sqlSP.AddPar("Store", SqlDbType.Char, 1, store, false);
                sqlSP.AddPar("ProductID", SqlDbType.SmallInt, 0, productID, false);
                sqlSP.AddPar("Count", SqlDbType.SmallInt, 0, count, false);
                sqlSP.AddPar("EmployeeCount", SqlDbType.SmallInt, 0, employeeCount, false);
                sqlSP.Run();
            }


        }

        public static class Supoort
        {
            private static SqlSP GetSqlSP([CallerMemberName] string name = "")
            {
                return new SqlSP("Support.usp" + name, new Sql(connectionString));
            }


            public static void AddTicket(string ticketTitle, int ticketCategoryID, int rating, string comment, string email, string phoneNumber, string name)
            {
                SqlSP sqlSP = GetSqlSP();
                sqlSP.AddPar("TicketTitle", SqlDbType.NVarChar, 200, ticketTitle, false);
                sqlSP.AddPar("TicketCategoryID", SqlDbType.TinyInt, 0, ticketCategoryID, false);
                sqlSP.AddPar("Rating", SqlDbType.TinyInt, 0, rating, false);
                sqlSP.AddPar("Comment", SqlDbType.NVarChar, -1, comment, false);
                sqlSP.AddPar("Email", SqlDbType.NVarChar, 254, email, false);
                sqlSP.AddPar("PhoneNumber", SqlDbType.VarChar, 15, phoneNumber, false);
                sqlSP.AddPar("Name", SqlDbType.NVarChar, 100, name, false);
                sqlSP.Run();
            }


            public static List<TicketCategory> SelectTicketCategory(){
                SqlSP sqlSP = GetSqlSP();
                return sqlSP.GetOutputColumn<int>("TicketCategoryID").Select(x => new TicketCategory(x)).ToList();
            }


            public static int GetTicketCategoryID(string categoryName)
            {
                SqlSP sqlSP = GetSqlSP();
                sqlSP.AddPar("TicketCategoryName", SqlDbType.VarChar, 100, categoryName, false);
                sqlSP.AddPar("TicketCategoryID", SqlDbType.TinyInt, 0, null, true);
                return sqlSP.GetOutputParValue<int>("TicketCategoryID");
            }


            public static string GetTicketCategoryName(int categoryID)
            {
                SqlSP sqlSP = GetSqlSP();
                sqlSP.AddPar("TicketCategoryID", SqlDbType.TinyInt, 0, categoryID, false);
                sqlSP.AddPar("TicketCategoryName", SqlDbType.VarChar, 100, null, true);
                return sqlSP.GetOutputParValue<string>("TicketCategoryName");
            }
        }
    }
}
