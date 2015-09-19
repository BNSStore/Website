using BNSStore.MVC.Shared;

namespace BNSStore.MVC.Support
{
    public class TicketCategory
    {
        private int? categoryID;
        private string categoryName;

        public TicketCategory(int categoryID)
        {
            this.categoryID = categoryID;
        }

        public TicketCategory(string categoryName)
        {
            this.categoryName = categoryName;
        }

        public int GetCategoryID()
        {
            if (this.categoryID == null)
            {
                this.categoryID = BSSqlSPs.Supoort.GetTicketCategoryID(this.categoryName);
            }
            return this.categoryID.Value;
        }

        public string GetCategoryName()
        {
            if (this.categoryName == null)
            {
                this.categoryName = BSSqlSPs.Supoort.GetTicketCategoryName(this.categoryID.Value);
            }
            return this.categoryName;

        }
    }
}