using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BusinessLayer
{
    public class CategoryServices
    {

        public static List<category> GetAllCategory(int CategoryId)
        {
            return CategoryProvider.GetAllCategory(CategoryId);
        }

        public static category GetCategory(int CategoryId)
        {
            return CategoryProvider.GetAllCategory(CategoryId).FirstOrDefault();
        }

        public static List<category> GetAllCategoryByName(string Category_Name)
        {
            return CategoryProvider.GetAllCategoryByName(Category_Name);
        }

        public static int AddUpdateCategory(category ob)
        {
            return CategoryProvider.AddUpdateCategory(ob);
        }

        public static bool DeleteCategory(int Category_id)
        {
            return CategoryProvider.DeleteCategory(Category_id);
        }
    }
}
