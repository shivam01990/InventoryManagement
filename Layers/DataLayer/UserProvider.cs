using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class UserProvider
    {
        public static bool CheckLogin(string userName, string Password)
        {
            bool flag = false;
            using (InventoryEntities db = new InventoryEntities())
            {
                int count = (from u in db.Users where u.UserName==userName && u.Password==Password select u ).Count();
                if (count != 0)
                    flag = true;
            }
            return flag;
        }
    }
}
