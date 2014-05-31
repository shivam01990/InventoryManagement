using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer;

namespace DataLayer
{
    public class SubCategoryProvider
    {
        public static List<SubCategoryEntity> GetAllSubCategoryEntity(int subCategoryId,int CategoryId)
        {
            List<SubCategoryEntity> _subcategory = null;
            using (InventoryEntities db = new InventoryEntities())
            {
                _subcategory = (from u in db.sub_category
                                join c in db.categories on u.category equals c.id
                                where ((subCategoryId == null) || (u.id == subCategoryId) || (subCategoryId == 0)&&
                                ((CategoryId==0)||(c.id==CategoryId)||(CategoryId==null)))
                                select new SubCategoryEntity
                                {
                                    id = u.id,
                                    category_name = c.category_name,
                                    sub_category_name = u.subcategory_name
                                }).ToList();
            }
            return _subcategory;
        }
        public static List<sub_category> GetAllSubCategory(int subCategoryId,int CategoryId)
        {
            List<sub_category> _category = null;
            using (InventoryEntities db = new InventoryEntities())
            {
                _category = (from u in db.sub_category where (((subCategoryId == null) || (u.id == subCategoryId) || (subCategoryId == 0))
                                 &&((CategoryId==0)||(u.category==CategoryId)||(CategoryId==null))) select u).ToList();
            }
            return _category;
        }

        public static List<sub_category> GetAllSubCategoryByName(string Category_Name,int CategoryId)
        {
            List<sub_category> _Category = null;
            using (InventoryEntities db = new InventoryEntities())
            {
                _Category = (from u in db.sub_category where ((u.subcategory_name == Category_Name)&&(u.category==CategoryId)) select u).ToList();
            }
            return _Category;
        }

        public static int AddUpdateSubCategory(sub_category ob)
        {
            int _Categoryid = 0;
            using (InventoryEntities db = new InventoryEntities())
            {
                if (ob.id > 0)
                {
                    sub_category temp = db.sub_category.Where(u => u.id == ob.id).FirstOrDefault();
                    if (temp != null)
                    {
                        temp.id = ob.id;
                        temp.subcategory_name = ob.subcategory_name;
                        temp.category = ob.category;

                    }

                }
                else
                {
                    db.sub_category.Add(ob);
                }
                int x = db.SaveChanges();
                if (x > 0)
                {
                    _Categoryid = ob.id;
                }

            }
            return _Categoryid;
        }

        public static bool DeleteSubCategory(int Category_id)
        {
            bool flag = false;
            using (InventoryEntities db = new InventoryEntities())
            {
                sub_category temp = db.sub_category.Where(u => u.id == Category_id).FirstOrDefault();
                if (temp != null)
                {
                    db.sub_category.Remove(temp);
                    db.SaveChanges();
                    flag = true;
                }
            }
            return flag;
        }
    }
}
