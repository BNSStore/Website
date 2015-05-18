using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SLouple.MVC.Shared;

namespace SLouple.MVC.Store
{
    public class Product
    {
        private int? productID;
        private string productName;
        private decimal? price;
        private decimal? employeePrice;
        private decimal? onSalePrice;
        private bool? onSale;
        private int? categoryID;
        private bool? online;

        public Product(int productID)
        {
            this.productID = productID;
        }

        public Product(string productName)
        {
            this.productName = productName;
        }

        public Product(int productID, string productName, decimal productPrice, decimal employeePrice, int categoryID, bool online)
        {
            this.productID = productID;
            this.productName = productName;
            this.price = productPrice;
            this.employeePrice = employeePrice;
            this.onSale = false;
            this.onSalePrice = null;
            this.categoryID = categoryID;
            this.online = online;
        }

        public Product(int productID, string productName, decimal productPrice, decimal employeePrice, decimal onSalePrice, int categoryID, bool online)
        {
            this.productID = productID;
            this.productName = productName;
            this.price = productPrice;
            this.employeePrice = employeePrice;
            this.onSale = true;
            this.onSalePrice = onSalePrice;
            this.categoryID = categoryID;
            this.online = online;
        }

        public int GetProductID()
        {
            if (productID == null)
            {
                SqlStoredProcedures sqlSP = new SqlStoredProcedures();
                productID = sqlSP.StoreGetProductID(productName);
            }
            return (int)productID;
        }

        public string GetProductName()
        {
            if (productName == null)
            {
                SqlStoredProcedures sqlSP = new SqlStoredProcedures();
                productName = sqlSP.StoreGetProductName((int)productID);
            }
            return productName;
        }

        public bool IsOnSale()
        {
            if (onSale == null)
            {
                SqlStoredProcedures sqlSP = new SqlStoredProcedures();
                onSale = sqlSP.StoreGetOnSalePrice(GetProductID()) != null;
            }
            return (bool)onSale;
        }

        public decimal GetPrice()
        {
            if (GetOnSalePrice() != null)
            {
                return (decimal)GetOnSalePrice();
            }
            else
            {
                if (price == null)
                {
                    SqlStoredProcedures sqlSP = new SqlStoredProcedures();
                    price = sqlSP.StoreGetProductPrice(GetProductID());
                }
                return (decimal)price;
            }
        }

        public decimal GetEmployeePrice()
        {
            if (GetOnSalePrice() != null)
            {
                return (decimal)GetOnSalePrice();
            }
            else
            {
                if (employeePrice == null)
                {
                    SqlStoredProcedures sqlSP = new SqlStoredProcedures();
                    employeePrice = sqlSP.StoreGetProductEmployeePrice(GetProductID());
                }
                return (decimal)employeePrice;
            }
        }

        public decimal? GetOnSalePrice()
        {
            if (IsOnSale())
            {
                if (onSalePrice == null)
                {
                    SqlStoredProcedures sqlSP = new SqlStoredProcedures();
                    onSalePrice = sqlSP.StoreGetOnSalePrice(GetProductID());
                }
                
                return (decimal)onSalePrice;
            }
            else
            {
                return null;
            }
        }

        public int GetCategoryID()
        {
            if (categoryID == null)
            {
                SqlStoredProcedures sqlSP = new SqlStoredProcedures();
                categoryID = sqlSP.StoreGetProductCategoryID(GetProductID());
            }
            return (int)categoryID;
        }

        public bool IsOnline()
        {
            if (online == null)
            {
                SqlStoredProcedures sqlSP = new SqlStoredProcedures();
                online = sqlSP.StoreIsProductOnline(GetProductID());
            }
            return (bool)online;
        }

        public static List<Product> GetProducts(int productID, string keyword, int categoryID, decimal minPrice, decimal maxPrice)
        {
            SqlStoredProcedures sqlSP = new SqlStoredProcedures();
            return sqlSP.StoreSelectProducts(productID, keyword, categoryID, minPrice, maxPrice);
        }

        public static List<Product> GetAllProducts()
        {
            SqlStoredProcedures sqlSP = new SqlStoredProcedures();
            return sqlSP.StoreSelectProducts(-1, null, -1, -1, -1);
        }

        public static Dictionary<int,string> GetProductCategories()
        {
            SqlStoredProcedures sqlSP = new SqlStoredProcedures();
            return sqlSP.StoreSelectProductCategories();
        }
    }
}