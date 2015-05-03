using Quartz;
using SLouple.MVC.Store;
using SLouple.MVC.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace SLouple.MVC.Controllers
{
    public class StoreController : AdvancedController
    {
        public StoreController()
        {
            subdomain = "Store";
        }

        public ActionResult Default()
        {
            return Products();
        }

        public ActionResult Products()
        {
            string title = "Products";
            Load(title);
            if (ids != null)
            {
                try
                {
                    int productID = ids.ContainsKey("productID") ? Convert.ToInt32(ids["productID"]) : -1;
                    int categoryID = ids.ContainsKey("categoryID") ? Convert.ToInt32(ids["categoryID"]) : -1;
                    string keyword = ids.ContainsKey("keyword") ? Convert.ToString(ids["keyword"]) : null;
                    decimal minPrice = ids.ContainsKey("minPrice") ? Convert.ToDecimal(ids["minPrice"]) : -1;
                    decimal maxPrice = ids.ContainsKey("maxPrice") ? Convert.ToDecimal(ids["maxPrice"]) : -1;
                    ViewBag.products = Product.GetProducts(productID, keyword, categoryID, minPrice, maxPrice);
                }
                catch
                {
                    ViewBag.products = Product.GetAllProducts();
                }
            }
            else
            {
                ViewBag.products = Product.GetAllProducts();
            }
            ViewBag.categories = Product.GetProductCategories();
            return View(title);
        }

        public ActionResult Schedule()
        {
            string title = "Schedule";
            Load(title);
            Dictionary<DateTime, Dictionary<int, char>> schedule;
            SqlStoredProcedures sqlSP = new SqlStoredProcedures();
            if (user == null || (ids != null && ids.ContainsKey("showAll") && Convert.ToBoolean(ids["showAll"]) == true))
            {
                schedule = sqlSP.StoreSelectSchedule(-1);
            }
            else
            {
                schedule = sqlSP.StoreSelectSchedule(user.userID);
            }
            List<char> stores = new List<char>();
            foreach (Dictionary<int, char> shifts in schedule.Values)
            {
                foreach (char store in shifts.Values)
                {
                    if (!stores.Contains(store))
                    {
                        stores.Add(store);
                    }
                }
            }
            ViewBag.stores = stores;
            ViewBag.schedule = schedule;
            return View(title);
        }

        public ActionResult ShiftRequest()
        {
            string title = "ShiftRequest";
            Load(title);
            if (postData == null || user == null)
            {
                return Content("failed");
            }
            try
            {
                SqlStoredProcedures sqlSP = new SqlStoredProcedures();
                string method = postData["method"];
                switch (method)
                {
                    case "add":
                        int recieverID = sqlSP.StoreGetEmployeeID(postData["firstName"], postData["lastName"]);
                        sqlSP.StoreAddShiftRequest(user.userID, recieverID, Convert.ToDateTime(postData["date"]));
                        break;
                    case "accept":
                        int senderID = Convert.ToInt32(postData["senderID"]);
                        DateTime date = Convert.ToDateTime(postData["date"]);
                        sqlSP.StoreAcceptShiftRequest(senderID, user.userID, date);
                        break;
                    case "decline":
                        sqlSP.StoreDeclineShiftRequest(Convert.ToInt32(postData["senderID"]), user.userID, Convert.ToDateTime(postData["date"]));
                        break;
                    default: break;
                }
                return Content("success");
            }
            catch
            {
                return Content("failed");
            }
            
        }

        public ActionResult Sales()
        {
            string title = "Sales";
            Load(title);

            SqlStoredProcedures sqlSP = new SqlStoredProcedures();
            var shifts = sqlSP.StoreGetCurrentShifts();


            if (user == null || !user.IsManager() || !shifts.ContainsKey(user.userID))
            {
                return RedirectToAction("Home", "Main");
            }

            foreach (KeyValuePair<int, char> shift in shifts){
                if (shift.Key == user.userID)
                {
                    ViewBag.store = shift.Value;
                    break;
                }
            }

            ViewBag.categories = Product.GetProductCategories();
            ViewBag.products = Product.GetAllProducts();
            return View(title);

        }

        public ActionResult Marking()
        {
            string title = "Marking";
            Load(title);

            if (user == null || !user.IsManager())
            {
                return RedirectToAction("Home", "Main");
            }

            if (postData != null && postData.ContainsKey("userID") && postData.ContainsKey("date") && postData.ContainsKey("mark"))
            {
                try
                {
                    int userID = Convert.ToInt32(postData["userID"]);
                    DateTime date = Convert.ToDateTime(postData["date"]);
                    int mark = Convert.ToInt32(postData["mark"]);
                    string comment = postData.ContainsKey("comment") ? postData["comment"] : null;

                    SqlStoredProcedures sqlSP = new SqlStoredProcedures();
                    sqlSP.StoreUpdateMarkAndComment(userID, date, mark, comment);

                    return Content("success");
                }
                catch
                {
                    return Content("failed");
                }
            }
            else
            {
                SqlStoredProcedures sqlSP = new SqlStoredProcedures();

                ViewBag.unmarkedSchedule = sqlSP.StoreSelectUnmarkedSchedule(user.userID);

                return View(title);
            }
        }

        public ActionResult ControlPanel()
        {
            string title = "ControlPanel";
            Load(title);
            if (user == null || !user.IsEmployee())
            {
                return RedirectToAction("Home", "Main");
            }
            if (postData != null && postData.ContainsKey("method"))
            {
                try
                {
                    string method = postData["method"];
                    switch (method)
                    {
                        case "AddShift": CPAddShift(); break;
                        case "DelShift": CPDelShift(); break;
                        case "AddProduct": ; break;
                        case "DelProduct": ; break;
                        case "ChangeProductPrice": ; break;
                        case "ChangeProductOnSalePrice": ; break;
                        case "ChangeProductEmployeePrice": ; break;
                        case "AddEmployee": ; break;
                        case "DelEmployee": ; break;
                        case "ChangeHomeSlide": ; break;
                        case "DownloadSales": return CPDownloadSales();
                        default: return Content("method not found");
                    }
                    return Content("success");
                }
                catch
                {
                    return Content("failed");
                }
            }
            else
            {
                return View(title);
            }
        }

        #region Control Panel Methods
        private void CPAddShift()
        {
            if (user.GetStoreGroupName() == "Human Resources" || user.IsManager())
            {
                string firstName = postData["firstName"];
                string lastName = postData["lastName"];
                DateTime date = Convert.ToDateTime(postData["date"]);
                char store = Convert.ToChar(postData["store"].ToUpper());
                SqlStoredProcedures sqlSP = new SqlStoredProcedures();
                sqlSP.StoreAddShift(firstName, lastName, date, store);
            }
            else
            {
                throw new NoPermissionException();
            }
        }

        private void CPDelShift()
        {
            if (user.GetStoreGroupName() == "Human Resources" || user.IsManager())
            {
                string firstName = postData["firstName"];
                string lastName = postData["lastName"];
                DateTime date = Convert.ToDateTime(postData["date"]);
                SqlStoredProcedures sqlSP = new SqlStoredProcedures();
                sqlSP.StoreDelShift(firstName, lastName, date);
            }
            else
            {
                throw new NoPermissionException();
            }
        }

        private ActionResult CPDownloadSales()
        {
            if (user.GetStoreGroupName() == "Accounting" || user.IsManager())
            {
                SqlStoredProcedures sqlSP = new SqlStoredProcedures();

                DateTime startDate = Convert.ToDateTime(postData["startDate"]);
                DateTime endDate = Convert.ToDateTime(postData["endDate"]);

                List<Sale> saleCounts = sqlSP.StoreSelectSales(startDate, endDate);
                StringBuilder sb = new StringBuilder();
                DateTime date = DateTime.Now;

                sb.AppendLine("\"Date\",\"Store\",\"Product Name\",\"Count\",\"Employee Count\"");
                foreach(Sale saleCount in saleCounts){
                    if(date != null && date != saleCount.date){
                        sb.AppendLine();
                    }
                    date = saleCount.date;
                    sb.Append("\"" + CSV.Escape(saleCount.date.ToShortDateString()) + "\"" + ",");
                    sb.Append("\"" + CSV.Escape(Convert.ToString(saleCount.store)) + "\"" + ",");
                    sb.Append("\"" + CSV.Escape(Convert.ToString(saleCount.productName)) + "\"" + ",");
                    sb.Append("\"" + CSV.Escape(Convert.ToString(saleCount.count)) + "\"" + ",");
                    sb.Append("\"" + CSV.Escape(Convert.ToString(saleCount.employeeCount)) + "\"");
                    sb.Append("\n");
                }

                byte[] fileBytes = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, postData["startDate"] + "-" + postData["endDate"] + ".csv");
            }
            else
            {
                throw new NoPermissionException();
            }
        }

        #endregion



    }

    
}