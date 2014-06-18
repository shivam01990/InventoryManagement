using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer;

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

        public static List<product> GetAllProduct(int ProductId)
        {
            List<product> _product = null;
            using (InventoryEntities db = new InventoryEntities())
            {
                _product = (from u in db.products where ((ProductId == null) || (u.id == ProductId) || (ProductId == 0)) select u).ToList();
            }
            return _product;
        }

        public static List<ProductEntity> GetProductsByName(string ProductName)
        {
            List<ProductEntity> lst_product = null;
            using (InventoryEntities db = new InventoryEntities())
            {
                lst_product = (from p in db.products
                               join c in db.categories on p.category equals c.id
                               join s in db.sub_category on p.sub_category equals s.id
                               where (p.product_name.Contains(ProductName)
                                  && (ProductName == ""))
                               select new ProductEntity
                               {
                                   id = p.id,
                                   product_name = p.product_name,
                                   image_url = p.image_url,
                                   brand = p.brand,
                                   category = c.category_name,
                                   sub_category = s.subcategory_name,
                                   weight = p.weight,
                                   cost_price = p.cost_price,
                                   sell_price = p.sell_price,
                                   status = p.status
                               }).ToList();


            }
            return lst_product;

        }



    }
}
