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

        public static product GetProduct(int ProductId)
        {
            return ProductProvider.GetAllProduct(ProductId).FirstOrDefault();
        }


        public static List<ProductEntity> GetProductsByName(string ProductName)
        {
            return ProductProvider.GetProductsByName(ProductName);
        }

        public static List<ProductEntity> GetProductsEntityById(int ProductId)
        {
            return ProductProvider.GetProductsEntityById(ProductId);
        }

        public static ProductEntity GetProductEntityById(int ProductId)
        {
            return ProductProvider.GetProductsEntityById(ProductId).FirstOrDefault();
        }

        public static List<product> GetProductBySubcategory(int subcategoryId)
        {
            return ProductProvider.GetProductBySubcategory(subcategoryId);
        }

        public static int UpdateProductStock(int ProductId)
        {
            return ProductProvider.UpdateProductStock(ProductId);
        }

        public static List<product> GetEmptyStockList()
        {
            return ProductProvider.GetEmptyStockList();
        }


        public static bool CheckProductNameAvailable(string ProductName)
        {
            return ProductProvider.CheckProductNameAvailable(ProductName);
        }
    }
}
