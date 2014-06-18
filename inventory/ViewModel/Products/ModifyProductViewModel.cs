using DataLayer;
using inventory.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer;
using BusinessLayer;

namespace inventory.ViewModel
{
   public class ModifyProductViewModel:ProductsViewModelBase
    {
        public override string Name
        {
            get { return InventoryHelper.ModifyProduct; }
        }

        public override string Icon
        {
            get { return InventoryHelper.ModifyProductIcon; }
        }

        public ModifyProductViewModel()
        {
            Products = ProductServices.GetProductsByName("");
        }
      
        private List<ProductEntity> _Products;
        public List<ProductEntity> Products
        {
            get { return _Products; }
            set
            {
                _Products = value;
                RaisedPropertyChanged("Products");
            }
        }
    }
}
