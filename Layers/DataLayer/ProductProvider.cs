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
                        temp.cost_price = ob.cost_price;
                        temp.weight = ob.weight;
                        temp.status = ob.status;
                        temp.Stock = ob.Stock;
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

        public static bool CheckProductNameAvailable(string ProductName)
        {
            bool flag = false;
            using (InventoryEntities db = new InventoryEntities())
            {
                int num = (from p in db.products
                           where p.product_name == ProductName
                           select p).Count();
                if (num == 0)
                    flag = true;
            }
            return flag;
        }

        public static List<ProductEntity> GetProductsByName(string ProductName)
        {
            List<ProductEntity> lst_product = null;
            using (InventoryEntities db = new InventoryEntities())
            {
                lst_product = (from p in db.products
                               join c in db.categories on p.category equals c.id
                               join s in db.sub_category on p.sub_category equals s.id
                               where (p.product_name.Contains(ProductName) && (p.status == true)
                                  || (ProductName == "") && (p.status == true))
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
                                   status = p.status,
                                   Stock = p.Stock
                               }).ToList();


            }
            return lst_product;
        }

        public static List<ProductEntity> GetProductsEntityById(int ProductId)
        {
            List<ProductEntity> lst_product = null;
            using (InventoryEntities db = new InventoryEntities())
            {
                lst_product = (from p in db.products
                               join c in db.categories on p.category equals c.id
                               join s in db.sub_category on p.sub_category equals s.id
                               where (p.id == ProductId && (p.status == true)
                                  || (ProductId == 0) && (p.status == true))
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

        public static List<product> GetProducts(int ProductId)
        {
            List<product> lst_product = null;
            using (InventoryEntities db = new InventoryEntities())
            {
                lst_product = (from p in db.products
                               where ((p.id == ProductId) || (ProductId == 0))
                               select p).ToList();
            }
            return lst_product;
        }

        public static List<product> GetProductBySubcategory(int subcategoryId)
        {
            List<product> lst_product = null;
            using (InventoryEntities db = new InventoryEntities())
            {
                lst_product = db.products.Where(s => s.sub_category == subcategoryId && s.status == true).ToList();
            }
            return lst_product;
        }

        public static int UpdateProductStock(int ProductId)
        {
            int Currentstock = 0;
            using (InventoryEntities db = new InventoryEntities())
            {
                product temp = GetAllProduct(ProductId).FirstOrDefault();
                if (temp != null)
                {
                    int CreditStock = 0;
                    int DebitStock = 0;
                    try
                    {
                        CreditStock = db.selling_history.Where(s => s.product_id == ProductId && s.transaction_type == 2).Sum(q => q.quantity); //Debit Transactions

                    }
                    catch
                    { }
                    try
                    {
                        DebitStock = db.selling_history.Where(s => s.product_id == ProductId && s.transaction_type == 1).Sum(q => q.quantity); //Credit Transactions
                    }
                    catch
                    { }
                    Currentstock = CreditStock - DebitStock;
                    temp.Stock = Currentstock;
                    AddUpdateProduct(temp);
                }
            }
            return Currentstock;
        }

        public static List<product> GetEmptyStockList()
        {
            List<product> lstProducts = null;
            using (InventoryEntities db = new InventoryEntities())
            {
                lstProducts = (from p in db.products where ((p.Stock == 0) && (p.status == true)) select p).ToList();
            }
            return lstProducts;
        }

    }
}
