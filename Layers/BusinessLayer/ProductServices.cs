using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using EntityLayer;

namespace BusinessLayer
{
    public class ProductServices
    {
        public static int AddUpdateProduct(product ob)
        {
            return ProductProvider.AddUpdateProduct(ob);
        }

        public static List<product> GetAllProduct(int ProductId)
        {
            return ProductProvider.GetAllProduct(ProductId);
        }


        public static List<ProductEntity> GetProductsByName(string ProductName)
        {
            return ProductProvider.GetProductsByName(ProductName);
        }
    }
}
