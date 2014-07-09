using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BusinessLayer
{
    public class SellingHistoryServices
    {
        public static int AddUpdateSellingHistory(selling_history ob)
        {
            return SellingHistoryProvider.AddUpdateSellingHistory(ob);
        }

        public static bool AddBulkSellingHistory(List<selling_history> lst_sellinghistory)
        {
            return SellingHistoryProvider.AddBulkSellingHistory(lst_sellinghistory);
        }

        public static decimal GetOverAllBalance(DateTime? StartDate, DateTime? EndDate)
        {
            return SellingHistoryProvider.GetOverAllBalance(StartDate, EndDate);
        }
    }
}
