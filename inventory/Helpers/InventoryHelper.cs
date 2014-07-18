using inventory.View.Alerts;
using inventory.ViewModel;
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
        public static string SellProducts = "Sell Products";
        public static string Transactions = "Transactions";
        #endregion

        #region Icons
        public static string SubCategoryIcon = "/Files/SubCategory.png";
        public static string CategoryIcon = "/Files/category.png";
        public static string AddDealerIcon = "/Files/dealer.jpg";
        public static string ModifyDealerIcon = "/Files/ModifyDealer.jpg";
        public static string AddProductIcon = "/Files/AddProduct.png";
        public static string ModifyProductIcon = "/Files/ModifyProduct.png";
        public static string StockEntryIcon = "/Files/StockEntry.png";
        public static string SellProductsIcon = "/Files/sellproducts.png";
        public static string TransactionsIcon = "/Files/Transaction.jpg";
        #endregion


        public static string ImageNA = "/Files/NA.png";

        public static string GetSaveFilePath()
        {
            string DirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string FolderName = "InventoryFiles";
            DirectoryPath += @"\" + FolderName;
            if (!Directory.Exists(DirectoryPath))
            {
                Directory.CreateDirectory(DirectoryPath);
            }
            return DirectoryPath;
        }

        public enum TransactionType
        {
            Credit = 1,
            Debit = 2
        }


        public static readonly GrowlNotifiactions growlNotifications = new GrowlNotifiactions(); // Glow Notification Object

        public static void SimpleAlert(string _Title, string _Message)
        {
            InventoryHelper.growlNotifications.AddNotification(new Notification { Title = _Title, ImageUrl = "pack://application:,,,/Files/notification-icon.png", Message = _Message });

        }

        public static void SuccessAlert(string _Title, string _Message)
        {
            InventoryHelper.growlNotifications.AddNotification(new Notification { Title = _Title, ImageUrl = "pack://application:,,,/Files/Success.png", Message = _Message });

        }

        public static DateTime GetFirstDateOfWeek(DateTime dayInWeek, DayOfWeek RunningDay)  //RunningDay is Schedular Running day 
        {
            DateTime firstDayInWeek = dayInWeek.Date;
            while (firstDayInWeek.DayOfWeek != RunningDay)
                firstDayInWeek = firstDayInWeek.AddDays(-1);

            return firstDayInWeek;
        }
        public static DateTime GetLastDateOfWeek(DateTime dayInWeek, DayOfWeek RunningDay)
        {
            DateTime lastDayInWeek = dayInWeek.Date;
            while (lastDayInWeek.DayOfWeek != RunningDay)
                lastDayInWeek = lastDayInWeek.AddDays(1);

            return lastDayInWeek;
        }

        public static DateTime GetLastDayOfMonth()
        {
            DateTime firstOfNextMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1);
            DateTime lastOfThisMonth = firstOfNextMonth.AddDays(-1);
            return lastOfThisMonth;
        }

        public static DateTime GetLastWeekdayOfMonth(DateTime date, DayOfWeek day)         // Get last WeekdayofMonth according to day
        {
            DateTime lastDayOfMonth = new DateTime(date.Year, date.Month, 1).AddMonths(1).AddDays(-1);
            int wantedDay = (int)day;
            int lastDay = (int)lastDayOfMonth.DayOfWeek;
            return lastDayOfMonth.AddDays(lastDay >= wantedDay ? wantedDay - lastDay : wantedDay - lastDay - 7);
        }

        public static DateTime GetMonthFirstday(DateTime date)
        {
            DateTime FirstDay = new DateTime(date.Year, date.Month, 1);
            return FirstDay;
        }

        public static DateTime GetMonthLastday(DateTime date)
        {
            DateTime firstDayOfTheMonth = new DateTime(date.Year, date.Month, 1);
            return firstDayOfTheMonth.AddMonths(1).AddDays(-1);
        }

        public static bool CheckWeekEndDay()
        {
            DateTime currentdate = DateTime.Now.Date;
            DateTime lastDayInWeek = GetLastDateOfWeek(DateTime.Now, DayOfWeek.Sunday);
            if (currentdate == lastDayInWeek)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        public static bool CheckMonthWeekEndDay()
        {
            DateTime currentdate = DateTime.Now.Date;
            DateTime monthlastweekend = GetLastWeekdayOfMonth(DateTime.Now, DayOfWeek.Sunday);
            if (currentdate == monthlastweekend)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static string DynamicConnectionString
        {
            get
            {
                return @"metadata=res://*/Inventory.csdl|res://*/Inventory.ssdl|res://*/Inventory.msl;provider=System.Data.SqlClient;provider connection string=';data source=" + ServerConnection.Default.ServerName + ";initial catalog=" + ServerConnection.Default.DatabaseName + ";user id=" + ServerConnection.Default.UserName + ";password=" + ServerConnection.Default.Password + ";MultipleActiveResultSets=True;App=EntityFramework';";
            }
        }



    }


}


