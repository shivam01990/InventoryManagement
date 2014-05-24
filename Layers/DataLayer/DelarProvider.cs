using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DelarProvider
    {
        public static List<dealer> GetAllDelars(int Delarid)
        {
            List<dealer> _delars = null;
            using (InventoryEntities db = new InventoryEntities())
            {
                _delars = (from u in db.dealers where ((Delarid == null) || (u.id == Delarid) || (Delarid == 0)) select u).ToList();
            }
            return _delars;
        }

        public static List<dealer> GetDealerByName(string Delar_Name)
        {
            List<dealer> _delars = null;
            using (InventoryEntities db = new InventoryEntities())
            {
                _delars = (from u in db.dealers where (u.dealer_name == Delar_Name) select u).ToList();
            }
            return _delars;
        }

        public static int AddUpdateDealer(dealer ob)
        {
            int delarid = 0;
            using (InventoryEntities db = new InventoryEntities())
            {
                if (ob.id > 0)
                {
                    dealer temp = db.dealers.Where(u => u.id == ob.id).FirstOrDefault();
                    if (temp != null)
                    {
                        temp.id = ob.id;
                        temp.dealer_name = ob.dealer_name;
                        temp.dealer_address = ob.dealer_address;
                    }

                }
                else
                {
                    db.dealers.Add(ob);
                }
                int x = db.SaveChanges();
                if (x > 0)
                {
                    delarid = ob.id;
                }

            }
            return delarid;
        }

        public static bool DeleteDealer(int Dealer_id)
        {
            bool flag =false;
            using (InventoryEntities db = new InventoryEntities())
            {
                dealer temp = db.dealers.Where(u => u.id == Dealer_id).FirstOrDefault();
                if (temp != null)
                {
                    db.dealers.Remove(temp);
                    db.SaveChanges();
                    flag = true;
                }
            }
            return flag;
        }
    }
}
