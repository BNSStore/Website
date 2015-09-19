using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BNSStore.MVC.Store
{
    public class Sale : Product
    {
        private DateTime date { get; set; }
        private char store { get; set; }
        private int count { get; set; }
        private int employeeCount { get; set; }

        public Sale(int productID, int count, int employeeCount)
            : base(productID)
        {
            this.count = count;
            this.employeeCount = employeeCount;
        }

        public Sale(string productName, int count, int employeeCount)
            : base(productName)
        {
            this.count = count;
            this.employeeCount = employeeCount;
        }

        public Sale(DateTime date, int productID, char store, int count, int employeeCount)
            : base(productID)
        {
            this.date = date;
            this.store = store;
            this.count = count;
            this.employeeCount = employeeCount;
        }

        public Sale(DateTime date, string productName, char store, int count, int employeeCount)
            : base(productName)
        {
            this.date = date;
            this.store = store;
            this.count = count;
            this.employeeCount = employeeCount;
        }

        public DateTime GetDate()
        {
            return date;
        }

        public char GetStore()
        {
            return store;
        }

        public int GetCount()
        {
            return count;
        }

        public int GetEmployeeCount()
        {
            return employeeCount;
        }

        
    }
}