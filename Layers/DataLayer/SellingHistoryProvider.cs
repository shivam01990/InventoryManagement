using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class SellingHistoryProvider
    {
        public static int AddUpdateSellingHistory(selling_history ob)
        {
            int _id = 0;
            using (InventoryEntities db = new InventoryEntities())
            {
                if (ob.id > 0)
                {
                    selling_history temp = db.selling_history.Where(u => u.id == ob.id).FirstOrDefault();
                    if (temp != null)
                    {
                        temp.id = ob.id;
                        temp.dealer_id = ob.dealer_id;
                        temp.product_id = ob.product_id;
                        temp.quantity = ob.quantity;
                        temp.credit = ob.credit;
                        temp.debit = ob.debit;
                        temp.transaction_type = ob.transaction_type;
                        temp.customer_info = ob.customer_info;
                        temp.payment_type = ob.payment_type;
                        temp.payment_date = ob.payment_date;
                        temp.customer_name = ob.customer_name;
                        temp.remarks = ob.remarks;
                    }

                }
                else
                {
                    db.selling_history.Add(ob);
                }
                int x = db.SaveChanges();
                if (x > 0)
                {
                    _id = ob.id;
                }

            }
            return _id;
        }

        public static bool AddBulkSellingHistory(List<selling_history> lst_sellinghistory)
        {
            bool flag = false;
            using (InventoryEntities db = new InventoryEntities())
            {
                foreach (selling_history item in lst_sellinghistory)
                {
                    db.selling_history.Add(item);
                }

                int x = db.SaveChanges();
                if (x > 0)
                {
                    flag = true;
                }

            }
            return flag;
        }


        public static decimal GetOverAllBalance(DateTime? StartDate, DateTime? EndDate)
        {
            decimal balance = 0;
            using (InventoryEntities db = new InventoryEntities())
            {
                balance = (from s in db.selling_history
                           where (((s.payment_date >= StartDate) || (StartDate == null))
                           && ((s.payment_date <= EndDate) || (EndDate == null)))
                           select (s.credit - s.debit)).Sum();
            }
            return balance;
        }
    }
}
