using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;

namespace inventory.Model
{
    public class SubcategoryModel
    {
        public static List<SubCategoryEntity> GetAllSubCategoryEntity(int subCategoryId,int CategoryId)
        {
            return SubCategoryServices.GetAllSubCategoryEntity(subCategoryId,CategoryId);
        }
    }
}
