using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SLouple.MVC.Store
{
    public class SaleCount
    {
        public DateTime date { get; set; }
        public int productID { get; set; }
        public string productName { get; set; }
        public char store { get; set; }
        public int count { get; set; }
        public int employeeCount { get; set; }

        public SaleCount(int productID, int count, int employeeCount)
        {
            this.productID = productID;
            this.count = count;
            this.employeeCount = employeeCount;
        }

        public SaleCount(DateTime date, int productID, string productName, char store, int count, int employeeCount)
        {
            this.date = date;
            this.productID = productID;
            this.productName = productName;
            this.store = store;
            this.count = count;
            this.employeeCount = employeeCount;
        }

        public int GetProductID()
        {
            if (productID < 0)
            {
                productID = 1;
            }
                return productID;
        }

        public string GetProductName()
        {
            if (productName == null)
            {
                productName = "";
            }
            return productName;
        }
    }
}