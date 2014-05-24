using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BusinessLayer
{
    public class UserServices
    {
        public static bool CheckLogin(string userName, string Password)
        {
            return UserProvider.CheckLogin(userName, Password);
        }

        
    }
}
