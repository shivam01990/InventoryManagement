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
        public static List<SubCategoryEntity> GetAllSubCategoryEntity(int subCategoryId,int CategoryId)
        {
            return SubCategoryProvider.GetAllSubCategoryEntity(subCategoryId,CategoryId);
        }
        public static List<sub_category> GetAllSubCategory(int subCategoryId)
        {
            return SubCategoryProvider.GetAllSubCategory(subCategoryId);
        }

        public static sub_category GetSubCategory(int SubCategoryId)
        {
            return SubCategoryProvider.GetAllSubCategory(SubCategoryId).FirstOrDefault();
        }
    }
}
