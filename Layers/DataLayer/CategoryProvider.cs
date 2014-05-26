using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class CategoryProvider
    {
        public static List<category> GetAllCategory(int CategoryId)
        {
            List<category> _category = null;
            using (InventoryEntities db = new InventoryEntities())
            {
                _category = (from u in db.categories where ((CategoryId == null) || (u.id == CategoryId) || (CategoryId == 0)) select u).ToList();
            }
            return _category;
        }

        public static List<category> GetAllCategoryByName(string Category_Name)
        {
            List<category> _Category = null;
            using (InventoryEntities db = new InventoryEntities())
            {
                _Category = (from u in db.categories where (u.category_name == Category_Name) select u).ToList();
            }
            return _Category;
        }

        public static int AddUpdateCategory(category ob)
        {
            int _Categoryid = 0;
            using (InventoryEntities db = new InventoryEntities())
            {
                if (ob.id > 0)
                {
                    category temp = db.categories.Where(u => u.id == ob.id).FirstOrDefault();
                    if (temp != null)
                    {
                        temp.id = ob.id;
                        temp.category_name = ob.category_name;

                    }

                }
                else
                {
                    db.categories.Add(ob);
                }
                int x = db.SaveChanges();
                if (x > 0)
                {
                    _Categoryid = ob.id;
                }

            }
            return _Categoryid;
        }

        public static bool DeleteCategory(int Category_id)
        {
            try
            {
                bool flag = false;
                using (InventoryEntities db = new InventoryEntities())
                {
                    category temp = db.categories.Where(u => u.id == Category_id).FirstOrDefault();
                    if (temp != null)
                    {
                        db.categories.Remove(temp);
                        db.SaveChanges();
                        flag = true;
                    }
                }
                return flag;
            }
            catch
            {
                return false;

            }
        }

    }
}
