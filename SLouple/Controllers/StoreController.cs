using Quartz;
using SLouple.MVC.Store;
using SLouple.MVC.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.IO;

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
                schedule = sqlSP.StoreSelectSchedule(user.GetUserID());
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
                        sqlSP.StoreAddShiftRequest(user.GetUserID(), recieverID, Convert.ToDateTime(postData["date"]));
                        break;
                    case "accept":
                        int senderID = Convert.ToInt32(postData["senderID"]);
                        DateTime date = Convert.ToDateTime(postData["date"]);
                        sqlSP.StoreAcceptShiftRequest(senderID, user.GetUserID(), date);
                        break;
                    case "decline":
                        sqlSP.StoreDeclineShiftRequest(Convert.ToInt32(postData["senderID"]), user.GetUserID(), Convert.ToDateTime(postData["date"]));
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


            if (user == null || !user.HasPolicy(new Policy("Store.Sales.Access")) || !shifts.ContainsKey(user.GetUserID()))
            {
                return RedirectToAction("Home", "Main");
            }

            foreach (KeyValuePair<int, char> shift in shifts){
                if (shift.Key == user.GetUserID())
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

            if (user == null || !user.HasPolicy(new Policy("Store.Marking.Add")))
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

                ViewBag.unmarkedSchedule = sqlSP.StoreSelectUnmarkedSchedule(user.GetUserID());

                return View(title);
            }
        }

        public ActionResult ControlPanel()
        {
            string title = "ControlPanel";
            Load(title);
            if (user == null || !user.HasPolicy("Store.ControlPanel.Access"))
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
                        case "AddProduct": CPAddProduct(); break;
                        case "DelProduct": CPDelProduct(); break;
                        case "ChangeProductPrice": CPChangeProductPrice(); break;
                        case "ChangeProductOnSalePrice": CPChangeProductOnSalePrice(); break;
                        case "ChangeProductEmployeePrice": CPChangeProductEmployeePrice(); break;
                        case "ChangeProductName": CPChangeProductName(); break;
                        case "ChangeProductCategory": CPChangeProductCategory(); break;
                        case "ChangeProductImage": CPChangeProductImage(); break;
                        case "ChangeCategoryName": CPChangeCategoryName(); break;
                        case "AddCategory": CPAddCategory(); break;
                        case "DelCategory": CPDelCategory(); break;
                        case "AddEmployee": ; break;
                        case "DelEmployee": ; break;
                        case "ChangeHomeSlide": ; break;
                        case "DownloadSales": return CPDownloadSales();
                        default: return Content("method not found");
                    }
                    return Content("success");
                }
                catch(Exception e)
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

        #region Product

        private void CPAddProduct()
        {
            if (user.HasPolicy("Store.Product.Add"))
            {
                string productName = postData["productName"];
                decimal productPrice = Convert.ToDecimal(postData["productPrice"]);
                decimal employeePrice = Convert.ToDecimal(postData["employeePrice"]);
                string categoryName = postData["categoryName"];
                SqlStoredProcedures sqlSP = new SqlStoredProcedures();
                sqlSP.StoreAddProduct(productName, productPrice, employeePrice, categoryName, false);
            }
            else
            {
                throw new NoPermissionException();
            }

        }

        private void CPDelProduct()
        {
            if (user.HasPolicy("Store.Product.Del"))
            {
                string productName = postData["productName"];
                SqlStoredProcedures sqlSP = new SqlStoredProcedures();
                sqlSP.StoreDelProduct(productName);
            }
            else
            {
                throw new NoPermissionException();
            }
        }

        private void CPChangeProductPrice()
        {
            if (user.HasPolicy("Store.Product.Update"))
            {
                string productName = postData["productName"];
                decimal productPrice = Convert.ToDecimal(postData["productPrice"]);
                SqlStoredProcedures sqlSP = new SqlStoredProcedures();
                sqlSP.StoreChangeProductPrice(productName, productPrice);
            }
            else
            {
                throw new NoPermissionException();
            }
        }

        private void CPChangeProductOnSalePrice()
        {
            if (user.HasPolicy("Store.Product.Update"))
            {
                string productName = postData["productName"];
                decimal? onSalePrice = Convert.ToDecimal(postData["onSalePrice"]);
                if (onSalePrice != null && onSalePrice <= 0)
                {
                    onSalePrice = null;
                }
                SqlStoredProcedures sqlSP = new SqlStoredProcedures();
                sqlSP.StoreChangeProductOnSalePrice(productName, onSalePrice);
            }
            else
            {
                throw new NoPermissionException();
            }
        }

        private void CPChangeProductEmployeePrice()
        {
            if (user.HasPolicy("Store.Product.Update"))
            {
                string productName = postData["productName"];
                decimal employeePrice = Convert.ToDecimal(postData["employeePrice"]);
                SqlStoredProcedures sqlSP = new SqlStoredProcedures();
                sqlSP.StoreChangeProductEmployeePrice(productName, employeePrice);
            }
            else
            {
                throw new NoPermissionException();
            }
        }

        private void CPChangeProductName()
        {
            if (user.HasPolicy("Store.Product.Update"))
            {
                string productName = postData["productName"];
                string newProductName = postData["newProductName"];
                SqlStoredProcedures sqlSP = new SqlStoredProcedures();
                sqlSP.StoreChangeProductName(productName, newProductName);
            }
            else
            {
                throw new NoPermissionException();
            }
        }

        private void CPChangeProductCategory()
        {
            if (user.HasPolicy("Store.Product.Update"))
            {
                string productName = postData["productName"];
                string categoryName = postData["categoryName"];
                SqlStoredProcedures sqlSP = new SqlStoredProcedures();
                sqlSP.StoreChangeProductCategory(productName, categoryName);
            }
            else
            {
                throw new NoPermissionException();
            }
        }

        private void CPChangeProductImage()
        {
            if (user.HasPolicy("Store.Product.Update"))
            {
                string productName = postData["productName"];
                HttpPostedFileBase file = Request.Files[0];
                string path = Path.Combine(Server.MapPath("~/Resources/Store/Images/Products"), new SqlStoredProcedures().StoreGetProductID(productName).ToString() + ".jpg");
                file.SaveAs(path);
            }
            else
            {
                throw new NoPermissionException();
            }
        }

        private void CPAddCategory()
        {
            if (user.HasPolicy("Store.Category.Add"))
            {
                string categoryName = postData["categoryName"];
                SqlStoredProcedures sqlSP = new SqlStoredProcedures();
                sqlSP.StoreAddCategory(categoryName);
            }
            else
            {
                throw new NoPermissionException();
            }
        }

        private void CPDelCategory()
        {
            if (user.HasPolicy("Store.Category.Del"))
            {
                string categoryName = postData["categoryName"];
                SqlStoredProcedures sqlSP = new SqlStoredProcedures();
                sqlSP.StoreDelCategory(categoryName);
            }
            else
            {
                throw new NoPermissionException();
            }
        }

        private void CPChangeCategoryName()
        {
            if (user.HasPolicy("Store.Category.Update"))
            {
                string categoryName = postData["categoryName"];
                string newCategoryName = postData["newCategoryName"];
                SqlStoredProcedures sqlSP = new SqlStoredProcedures();
                sqlSP.StoreChangeCategoryName(categoryName, newCategoryName);
            }
            else
            {
                throw new NoPermissionException();
            }
        }

        #endregion

        #region Human Resources

        private void CPAddShift()
        {
            if (user.HasPolicy("Store.Shift.Add"))
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
            if (user.HasPolicy("Store.Shift.Del"))
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

        #endregion

        #region Accounting

        private ActionResult CPDownloadSales()
        {
            if (user.HasPolicy("Store.Sales.Download"))
            {
                SqlStoredProcedures sqlSP = new SqlStoredProcedures();

                DateTime startDate = Convert.ToDateTime(postData["startDate"]);
                DateTime endDate = Convert.ToDateTime(postData["endDate"]);

                List<Sale> saleCounts = sqlSP.StoreSelectSales(startDate, endDate);
                StringBuilder sb = new StringBuilder();
                DateTime date = DateTime.Now;

                sb.AppendLine("\"Date\",\"Store\",\"Product Name\",\"Count\",\"Employee Count\"");
                foreach(Sale sale in saleCounts){
                    if(date != null && date != sale.GetDate()){
                        sb.AppendLine();
                    }
                    date = sale.GetDate();
                    sb.Append("\"" + CSV.Escape(sale.GetDate().ToShortDateString()) + "\"" + ",");
                    sb.Append("\"" + CSV.Escape(Convert.ToString(sale.GetStore())) + "\"" + ",");
                    sb.Append("\"" + CSV.Escape(Convert.ToString(sale.GetProductName())) + "\"" + ",");
                    sb.Append("\"" + CSV.Escape(Convert.ToString(sale.GetCount())) + "\"" + ",");
                    sb.Append("\"" + CSV.Escape(Convert.ToString(sale.GetEmployeeCount())) + "\"");
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

        #endregion



    }

    
}