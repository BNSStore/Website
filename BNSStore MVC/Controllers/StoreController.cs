using Quartz;
using BNSStore.MVC.Store;
using BNSStore.MVC.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.IO;
using SLouple.MVC.Shared;

namespace BNSStore.MVC.Controllers
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
                    ViewBag.products = BSSqlSPs.Store.SelectProduct(productID: productID, keyword: keyword, categoryID: categoryID, minPrice: minPrice, maxPrice: maxPrice);
                }
                catch
                {
                    ViewBag.products = BSSqlSPs.Store.SelectProduct();
                }
            }
            else
            {
                ViewBag.products = BSSqlSPs.Store.SelectProduct();
            }
            ViewBag.categories = BSSqlSPs.Store.SelectProductCategory();
            return View(title);
        }

        public ActionResult Schedule()
        {
            string title = "Schedule";
            Load(title);
            List<Shift> schedule;
            if (user == null || (ids != null && ids.ContainsKey("showAll") && Convert.ToBoolean(ids["showAll"]) == true))
            {
                schedule = BSSqlSPs.Store.SelectSchedule();
            }
            else
            {
                schedule = BSSqlSPs.Store.SelectSchedule(userID: user.GetUserID());
            }
            List<char> stores = new List<char>();
            foreach (Shift shift in schedule)
            {
                if (!stores.Contains(shift.store))
                {
                    stores.Add(shift.store);
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
                string method = postData["method"];
                switch (method)
                {
                    case "add":
                        BSSqlSPs.Store.AddShiftRequest(user.GetUserID(), Convert.ToInt32(postData["recieverID"]), Convert.ToDateTime(postData["date"]));
                        break;
                    case "accept":
                        BSSqlSPs.Store.AcceptShiftRequest(Convert.ToInt32(postData["senderID"]), user.GetUserID(), Convert.ToDateTime(postData["date"]));
                        break;
                    case "decline":
                        BSSqlSPs.Store.DeclineShiftRequest(Convert.ToInt32(postData["senderID"]), user.GetUserID(), Convert.ToDateTime(postData["date"]));
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

            List<Shift> schedule = BSSqlSPs.Store.SelectSchedule(startDate: DateTime.Now, endDate: DateTime.Now);

            if (user == null || !user.HasPolicy(new Policy(MVCApp.CurrentProvider, "Store.Sales.Access")) || !schedule.Any(x => x.user == user))
            {
                return RedirectToAction("Home", "Main");
            }

            foreach (Shift shift in schedule)
            {
                if (shift.user == user)
                {
                    ViewBag.store = shift.store;
                    break;
                }
            }

            ViewBag.categories = BSSqlSPs.Store.SelectProductCategory();
            ViewBag.products = BSSqlSPs.Store.SelectProduct();
            return View(title);

        }

        public ActionResult Marking()
        {
            string title = "Marking";
            Load(title);

            if (user == null || !user.HasPolicy(new Policy(MVCApp.CurrentProvider, "Store.Marking.Add")))
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
                    
                    BSSqlSPs.Store.UpdateMarkAndComment(userID, date, mark, comment);

                    return Content("success");
                }
                catch
                {
                    return Content("failed");
                }
            }
            else
            {

                ViewBag.unmarkedSchedule = BSSqlSPs.Store.SelectUnmarkedSchedule(user.GetUserID());

                return View(title);
            }
        }

        public ActionResult ControlPanel()
        {
            string title = "ControlPanel";
            Load(title);
            if (user == null || !user.HasPolicy(new Policy(MVCApp.CurrentProvider, "Store.ControlPanel.Access")))
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

        #region Product

        private void CPAddProduct()
        {
            if (user.HasPolicy(new Policy(MVCApp.CurrentProvider, "Store.Product.Add")))
            {
                string productName = postData["productName"];
                decimal productPrice = Convert.ToDecimal(postData["productPrice"]);
                decimal employeePrice = Convert.ToDecimal(postData["employeePrice"]);
                string categoryName = postData["categoryName"];
                BSSqlSPs.Store.AddProduct(productName, productPrice, employeePrice, new Category(categoryName).GetCategoryID(), false);
            }
            else
            {
                throw new NoPermissionException();
            }

        }

        private void CPDelProduct()
        {
            if (user.HasPolicy(new Policy(MVCApp.CurrentProvider, "Store.Product.Del")))
            {
                string productName = postData["productName"];
                BSSqlSPs.Store.DelProduct(new Product(productName).GetProductID());
            }
            else
            {
                throw new NoPermissionException();
            }
        }

        private void CPChangeProductPrice()
        {
            if (user.HasPolicy(new Policy(MVCApp.CurrentProvider, "Store.Product.Update")))
            {
                string productName = postData["productName"];
                decimal productPrice = Convert.ToDecimal(postData["productPrice"]);
                BSSqlSPs.Store.ChangeProductPrice(new Product(productName).GetProductID(), productPrice);
            }
            else
            {
                throw new NoPermissionException();
            }
        }

        private void CPChangeProductOnSalePrice()
        {
            if (user.HasPolicy(new Policy(MVCApp.CurrentProvider, "Store.Product.Update")))
            {
                string productName = postData["productName"];
                decimal? onSalePrice = Convert.ToDecimal(postData["onSalePrice"]);
                if (onSalePrice == null && onSalePrice <= 0)
                {
                    onSalePrice = null;
                }
                BSSqlSPs.Store.ChangeProductOnSalePrice(new Product(productName).GetProductID(), onSalePrice);
            }
            else
            {
                throw new NoPermissionException();
            }
        }

        private void CPChangeProductEmployeePrice()
        {
            if (user.HasPolicy(new Policy(MVCApp.CurrentProvider, "Store.Product.Update")))
            {
                string productName = postData["productName"];
                decimal employeePrice = Convert.ToDecimal(postData["employeePrice"]);
                BSSqlSPs.Store.ChangeProductEmployeePrice(new Product(productName).GetProductID(), employeePrice);
            }
            else
            {
                throw new NoPermissionException();
            }
        }

        private void CPChangeProductName()
        {
            if (user.HasPolicy(new Policy(MVCApp.CurrentProvider, "Store.Product.Update")))
            {
                string productName = postData["productName"];
                string newProductName = postData["newProductName"];
                BSSqlSPs.Store.ChangeProductName(new Product(productName).GetProductID(), newProductName);
            }
            else
            {
                throw new NoPermissionException();
            }
        }

        private void CPChangeProductCategory()
        {
            if (user.HasPolicy(new Policy(MVCApp.CurrentProvider, "Store.Product.Update")))
            {
                string productName = postData["productName"];
                string categoryName = postData["categoryName"];
                BSSqlSPs.Store.ChangeProductCategory(new Product(productName).GetProductID(), new Category(categoryName).GetCategoryID());
            }
            else
            {
                throw new NoPermissionException();
            }
        }

        private void CPChangeProductImage()
        {
            if (user.HasPolicy(new Policy(MVCApp.CurrentProvider, "Store.Product.Update")))
            {
                string productName = postData["productName"];
                HttpPostedFileBase file = Request.Files[0];
                string path = Path.Combine(Server.MapPath("~/Resources/Store/Images/Products"), new Product(productName).GetProductID() + ".jpg");
                file.SaveAs(path);
            }
            else
            {
                throw new NoPermissionException();
            }
        }

        private void CPAddCategory()
        {
            if (user.HasPolicy(new Policy(MVCApp.CurrentProvider, "Store.Category.Add")))
            {
                string categoryName = postData["categoryName"];
                BSSqlSPs.Store.AddCategory(categoryName);
            }
            else
            {
                throw new NoPermissionException();
            }
        }

        private void CPDelCategory()
        {
            if (user.HasPolicy(new Policy(MVCApp.CurrentProvider, "Store.Category.Del")))
            {
                string categoryName = postData["categoryName"];
                BSSqlSPs.Store.DelCategory(new Category(categoryName).GetCategoryID());
            }
            else
            {
                throw new NoPermissionException();
            }
        }

        private void CPChangeCategoryName()
        {
            if (user.HasPolicy(new Policy(MVCApp.CurrentProvider, "Store.Category.Update")))
            {
                string categoryName = postData["categoryName"];
                string newCategoryName = postData["newCategoryName"];
                BSSqlSPs.Store.ChangeCategoryName(new Category(categoryName).GetCategoryID(), newCategoryName);
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
            if (user.HasPolicy(new Policy(MVCApp.CurrentProvider, "Store.Shift.Add")))
            {
                int userID = Convert.ToInt32(postData["userID"]);
                DateTime date = Convert.ToDateTime(postData["date"]);
                char store = Convert.ToChar(postData["store"].ToUpper());
                BSSqlSPs.Store.AddShift(userID, date, store);
            }
            else
            {
                throw new NoPermissionException();
            }
        }

        private void CPDelShift()
        {
            if (user.HasPolicy(new Policy(MVCApp.CurrentProvider, "Store.Shift.Del")))
            {
                int userID = Convert.ToInt32(postData["userID"]);
                DateTime date = Convert.ToDateTime(postData["date"]);
                BSSqlSPs.Store.DelShift(userID, date);
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
            if (user.HasPolicy(new Policy(MVCApp.CurrentProvider, "Store.Sales.Download")))
            {

                DateTime startDate = Convert.ToDateTime(postData["startDate"]);
                DateTime endDate = Convert.ToDateTime(postData["endDate"]);

                List<Sale> saleCounts = BSSqlSPs.Store.SelectSale(startDate: startDate, endDate: endDate);
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