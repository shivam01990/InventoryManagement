using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BusinessLayer
{
    public class DelarServices
    {
        public static List<dealer> GetAllDelars(int Delarid)
        {
            return DelarProvider.GetAllDelars(Delarid);
        }

        public static dealer GetDelar(int Delarid)
        {
            return DelarProvider.GetAllDelars(Delarid).FirstOrDefault();
        }

        public static List<dealer> GetDelarByName(string Delar_Name)
        {
            return DelarProvider.GetDealerByName(Delar_Name);
        }

        public static int AddUpdateDealer(dealer ob)
        {
            return DelarProvider.AddUpdateDealer(ob);
        }

        public static bool DeleteDealer(int Dealer_id)
        {
            return DelarProvider.DeleteDealer(Dealer_id);
        }
    }
}
