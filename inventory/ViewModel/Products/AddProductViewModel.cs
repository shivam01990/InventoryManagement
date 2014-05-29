using inventory.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory.ViewModel
{
    public class AddProductViewModel:ProductsViewModelBase
    {
        public override string Name
        {
            get { return InventoryHelper.AddProduct; }
        }

        public override string Icon
        {
            get { return InventoryHelper.AddProductIcon; }
        }
    }
}
