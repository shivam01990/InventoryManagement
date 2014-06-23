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
    }
}
