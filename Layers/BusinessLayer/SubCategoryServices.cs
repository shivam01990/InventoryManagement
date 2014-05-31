using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BusinessLayer
{
    public class SubCategoryServices
    {
        public static List<SubCategoryEntity> GetAllSubCategoryEntity(int subCategoryId, int CategoryId)
        {
            return SubCategoryProvider.GetAllSubCategoryEntity(subCategoryId, CategoryId);
        }
        public static List<sub_category> GetAllSubCategory(int subCategoryId, int CategoryId)
        {
            return SubCategoryProvider.GetAllSubCategory(subCategoryId,CategoryId);
        }

        public static sub_category GetSubCategory(int SubCategoryId)
        {
            return SubCategoryProvider.GetAllSubCategory(SubCategoryId,0).FirstOrDefault();
        }

        public static List<sub_category> GetAllSubCategoryByName(string Category_Name, int CategoryId)
        {
            return SubCategoryProvider.GetAllSubCategoryByName(Category_Name, CategoryId);
        }

        public static int AddUpdateSubCategory(sub_category ob)
        {
            return SubCategoryProvider.AddUpdateSubCategory(ob);
        }

        public static bool DeleteSubCategory(int Category_id)
        {
            return SubCategoryProvider.DeleteSubCategory(Category_id);
        }
    }
}
