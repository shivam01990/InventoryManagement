using DataLayer;
using inventory.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer;
using BusinessLayer;
using System.Windows.Input;
using inventory.Controls;
using System.Windows;
using inventory.View;

namespace inventory.ViewModel
{
    public class ModifyProductViewModel : ProductsViewModelBase
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



        private ICommand _EditCommand;
        public ICommand EditCommand
        {
            get
            {
                if (_EditCommand == null)
                {
                    _EditCommand = new RelayCommand(new Action<object>(EditProduct));
                }
                return _EditCommand;
            }
            set
            {
                _EditCommand = value;
                RaisedPropertyChanged("EditCommand");

            }
        }

        protected void EditProduct(object parameter)
        {
            int ProductId = (int)parameter;
            EditProduct ob = new EditProduct();
            ob.DataContext = new EditProductViewModel(ProductId);
            bool? result;
            result = ob.ShowDialog();
            if (result == true)
            {
                ProductEntity temp = (from p in Products where (p.id == ProductId) select p).First();
                List<ProductEntity> lsttemp = Products;
                lsttemp.Remove(temp);
                temp = ProductServices.GetProductEntityById(ProductId);
                lsttemp.Add(temp);
                Products = null;
                Products = lsttemp;
            }
        }


        private ICommand _DeleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (_DeleteCommand == null)
                {
                    _DeleteCommand = new RelayCommand(new Action<object>(DeleteProduct));
                }
                return _DeleteCommand;
            }
            set
            {
                _DeleteCommand = value;
                RaisedPropertyChanged("DeleteCommand");

            }
        }

        protected void DeleteProduct(object Parameter)
        {
            int ProductId = (int)Parameter;
            product ob = ProductServices.GetProduct(ProductId);
            ob.status = false;
            int x = ProductServices.AddUpdateProduct(ob);
            if (x > 0)
            {
                ProductEntity temp = (from p in Products where (p.id == ProductId) select p).First();
                List<ProductEntity> lsttemp = Products;
                lsttemp.Remove(temp);
                Products = null;
                Products = lsttemp;
                //MessageBox.Show("Product Deleted");
                InventoryHelper.SuccessAlert("Success", "Product Deleted");
            }
        }


        private ICommand _ClickCommand;
        public ICommand ClickCommand
        {
            get
            {
                if (_ClickCommand == null)
                {
                    _ClickCommand = new RelayCommand(new Action<object>(SearchProduct), new Predicate<object>(Can_Execute));
                }
                return _ClickCommand;
            }
            set
            {
                _ClickCommand = value;
                RaisedPropertyChanged("ClickCommand");

            }
        }
        protected bool Can_Execute(object parameter)
        {
            return true;
        }

        protected void SearchProduct(object parameter)
        {
            AutoCompleteTextBox txt_searchBox = (AutoCompleteTextBox)parameter;
            Products = ProductServices.GetProductsByName(txt_searchBox.Text.Trim());
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
