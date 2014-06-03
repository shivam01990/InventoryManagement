using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BusinessLayer
{
    public class ProductServices
    {
        public static int AddUpdateProduct(product ob)
        {
            return ProductProvider.AddUpdateProduct(ob);
        }
    }
}
