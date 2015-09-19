using System;
using BNSStore.MVC.Shared;

namespace BNSStore.MVC.Store
{
    public class Product : IEquatable<Product>
    {
        private int? productID;
        private string productName;
        private decimal? price;
        private decimal? employeePrice;
        private decimal? onSalePrice;
        private bool? onSale;
        private Category category;
        private bool? online;

        public Product(int productID)
        {
            this.productID = productID;
        }

        public Product(string productName)
        {
            this.productName = productName;
        }

        public Product(int productID, string productName, decimal productPrice, decimal employeePrice, Category category, bool online)
        {
            this.productID = productID;
            this.productName = productName;
            this.price = productPrice;
            this.employeePrice = employeePrice;
            this.onSale = false;
            this.onSalePrice = null;
            this.category = category;
            this.online = online;
        }

        public Product(int productID, string productName, decimal productPrice, decimal employeePrice, decimal onSalePrice, Category category, bool online)
        {
            this.productID = productID;
            this.productName = productName;
            this.price = productPrice;
            this.employeePrice = employeePrice;
            this.onSale = true;
            this.onSalePrice = onSalePrice;
            this.category = category;
            this.online = online;
        }

        public int GetProductID()
        {
            if (this.productID == null)
            {
                this.productID = BSSqlSPs.Store.GetProductID(this.productName);
            }
            return productID.Value;
        }

        public string GetProductName()
        {
            if (this.productName == null)
            {
                this.productName = BSSqlSPs.Store.GetProductName(this.productID.Value);
            }
            return productName;
        }

        public decimal GetPrice()
        {
            if (GetOnSalePrice() != null)
            {
                return GetOnSalePrice().Value;
            }
            else
            {
                if (this.price == null)
                {
                    this.price = BSSqlSPs.Store.GetProductPrice(GetProductID());
                }
                return price.Value;
            }
        }

        public decimal GetEmployeePrice()
        {
            if (GetOnSalePrice() != null)
            {
                return GetOnSalePrice().Value;
            }
            else
            {
                if (this.employeePrice == null)
                {
                    this.employeePrice = BSSqlSPs.Store.GetProductEmployeePrice(GetProductID());
                }
                return employeePrice.Value;
            }
        }

        public bool IsOnSale()
        {
            if(this.onSale == null)
            {
                GetOnSalePrice();
            }
            return this.onSale.Value;
        }

        public decimal? GetOnSalePrice()
        {
            if (this.onSalePrice == null && this.onSale == null)
            {
                this.onSalePrice = BSSqlSPs.Store.GetProductOnSalePrice(GetProductID());
                this.onSale = GetOnSalePrice() != null;
            }
            return this.onSalePrice.Value;
        }

        public Category GetCategory()
        {
            if (this.category == null)
            {
                this.category = new Category(BSSqlSPs.Store.GetProductCategoryID(GetProductID()));
            }
            return this.category;
        }

        public bool IsOnline()
        {
            if (this.online == null)
            {
                this.online = BSSqlSPs.Store.IsProductOnline(GetProductID());
            }
            return this.online.Value;
        }

        public override bool Equals(object other)
        {
            if (other.GetType() == typeof(Product))
            {
                return Equals((Product)other);
            }
            else
            {
                return false;
            }
        }

        public bool Equals(Product other)
        {
            if(other == null)
            {
                return false;
            }
            if (productID == null)
            {
                return other.GetProductName() == GetProductName();
            }
            else
            {
                return other.GetProductID() == GetProductID();
            }

        }

        public override int GetHashCode()
        {
            unchecked
            {
                return GetProductID().GetHashCode();
            }
        }

        public static bool operator ==(Product self, Product other)
        {
            if (object.ReferenceEquals(self, null) || object.ReferenceEquals(other, null))
            {
                return (object.ReferenceEquals(self, null) && object.ReferenceEquals(other, null));
            }
            return self.Equals(other);
        }

        public static bool operator !=(Product self, Product other)
        {
            return !(self == other);
        }
    }
}