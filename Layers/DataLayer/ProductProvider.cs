using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ProductProvider
    {
        public static int AddUpdateProduct(product ob)
        {
            int _Productid = 0;
            using (InventoryEntities db = new InventoryEntities())
            {
                if (ob.id > 0)
                {
                    product temp = db.products.Where(u => u.id == ob.id).FirstOrDefault();
                    if (temp != null)
                    {
                        temp.id = ob.id;
                        temp.product_name = ob.product_name;
                        temp.category = ob.category;
                        temp.image_url = ob.image_url;
                        temp.brand = ob.brand;
                        temp.sub_category = ob.sub_category;
                        temp.sell_price = ob.sell_price;
                        temp.cost_price = ob.sell_price;
                        temp.weight = ob.weight;
                        temp.status = ob.status;
                    }

                }
                else
                {
                    db.products.Add(ob);
                }
                int x = db.SaveChanges();
                if (x > 0)
                {
                    _Productid = ob.id;
                }

            }
            return _Productid;
        }
    }
}
