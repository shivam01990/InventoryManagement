using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory.Helpers
{
    public class InventoryHelper
    {
        #region MenuTitle
        public static string SubCategory = "Sub Category";
        public static string Category = "Category";
        public static string AddDealer = "Add Dealers";
        public static string ModifyDealer = "Modify Dealer";
        public static string AddProduct = "Add Product";
        public static string ModifyProduct = "Modify Products";
        public static string StockEntry = "Stock Entry";
        #endregion

        #region Icons
        public static string SubCategoryIcon = "/Files/SubCategory.png";
        public static string CategoryIcon = "/Files/category.png";
        public static string AddDealerIcon = "/Files/dealer.jpg";
        public static string ModifyDealerIcon = "/Files/ModifyDealer.jpg";
        public static string AddProductIcon = "/Files/AddProduct.png";
        public static string ModifyProductIcon = "/Files/ModifyProduct.png";
        public static string StockEntryIcon = "/Files/StockEntry.png";
        #endregion


        public static string ImageNA = "/Files/NA.png";

        public static string GetSaveFilePath()
        {
           string DirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string FolderName="InventoryFiles";
            DirectoryPath +=@"\"+ FolderName;
            if (!Directory.Exists(DirectoryPath))
            {
                Directory.CreateDirectory(DirectoryPath);
            }
           return DirectoryPath;
        }

    }
}
