using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SLouple.MVC.Shared;

namespace SLouple.MVC.Store
{
    public class Product
    {
        public int productID;
        public string productName;
        public decimal productPrice;
        public decimal employeePrice;
        public decimal onSalePrice;
        public bool isOnSale;
        public int categoryID;
        public bool online;

        public Product(int productID, string productName, decimal productPrice, decimal employeePrice, int categoryID, bool online)
        {
            this.productID = productID;
            this.productName = productName;
            this.productPrice = productPrice;
            this.employeePrice = employeePrice;
            this.isOnSale = false;
            this.onSalePrice = -1;
            this.categoryID = categoryID;
            this.online = online;
        }

        public Product(int productID, string productName, decimal productPrice, decimal employeePrice, decimal onSalePrice, int categoryID, bool online)
        {
            this.productID = productID;
            this.productName = productName;
            this.productPrice = productPrice;
            this.employeePrice = employeePrice;
            this.isOnSale = true;
            this.onSalePrice = onSalePrice;
            this.categoryID = categoryID;
            this.online = online;
        }

        public static List<Product> GetProducts(int productID, string keyword, int categoryID, decimal minPrice, decimal maxPrice)
        {
            SqlStoredProcedures sp = new SqlStoredProcedures();
            return sp.StoreSelectProducts(productID, keyword, categoryID, minPrice, maxPrice);
        }

        public static List<Product> GetAllProducts()
        {
            SqlStoredProcedures sp = new SqlStoredProcedures();
            return sp.StoreSelectProducts(-1, null, -1, -1, -1);
        }

        public static Dictionary<int,string> GetProductCategories()
        {
            SqlStoredProcedures sp = new SqlStoredProcedures();
            return sp.StoreSelectProductCategories();
        }
    }
}