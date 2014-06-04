using BusinessLayer;
using DataLayer;
using inventory.Helpers;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory.ViewModel
{
    public class AddProductViewModel : ProductsViewModelBase, IDataErrorInfo
    {
        public override string Name
        {
            get { return InventoryHelper.AddProduct; }
        }

        public override string Icon
        {
            get { return InventoryHelper.AddProductIcon; }
        }

        public AddProductViewModel()
        {
            this._Category = CategoryServices.GetAllCategory(0);
        }

        private IList<category> _Category;
        public IList<category> Category
        {
            get { return _Category; }
            set
            {
                _Category = value;
                RaisedPropertyChanged("Category");
            }
        }

        private category _SelectedCategory;

        public category SelectedCategory
        {
            get
            {
                if (_SelectedCategory != null)
                {
                    SubCategory = SubCategoryServices.GetAllSubCategory(0, _SelectedCategory.id);
                }
                return _SelectedCategory;
            }
            set
            {
                _SelectedCategory = value;
                RaisedPropertyChanged("SelectedCategory");
            }
        }

        private IList<sub_category> _SubCategory;
        public IList<sub_category> SubCategory
        {

            get
            {
                return _SubCategory;
            }
            set
            {
                _SubCategory = value;
                RaisedPropertyChanged("SubCategory");

            }
        }

        private string _selectedPath;
        public string SelectedPath
        {
            get
            {
                if ((_selectedPath == "") || (_selectedPath == null))
                {
                    _selectedPath = InventoryHelper.ImageNA;
                }
                return _selectedPath;
            }
            set { _selectedPath = value; RaisedPropertyChanged("SelectedPath"); }
        }

        private RelayCommand _openCommand;
        public RelayCommand OpenCommand
        {
            get
            {
                if (_openCommand == null)
                {
                    _openCommand = new RelayCommand(new Action<object>(OpenFile));
                }
                return _openCommand;
            }
            set
            {
                _openCommand = value;
                RaisedPropertyChanged("OpenCommand");

            }

        }

        public void OpenFile(object ob)
        {
            OpenFileDialog dlg = new OpenFileDialog();



            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                SelectedPath = filename;
            }
        }

        private string _ProductName;
        public string ProductName
        {
            get
            {
                return _ProductName;
            }
            set
            {
                _ProductName = value;
                RaisedPropertyChanged("ProductName");
            }
        }

        private string _Brand;
        public string Brand
        {
            get
            {
                return _Brand;
            }
            set
            {
                _Brand = value;
                RaisedPropertyChanged("Brand");
            }
        }

        private string _Weight;
        public string Weight
        {
            get
            {
                return _Weight;
            }
            set
            {
                _Weight = value;
                RaisedPropertyChanged("Weight");
            }
        }

        private decimal _CostPrice;
        public decimal CostPrice
        {
            get
            {
                return _CostPrice;
            }
            set
            {
                _CostPrice = value;
                RaisedPropertyChanged("CostPrice");
            }
        }

        private decimal _SellingPrice;
        public decimal SellingPrice
        {
            get
            {
                return _SellingPrice;
            }
            set
            {
                _SellingPrice = value;
                RaisedPropertyChanged("SellingPrice");
            }
        }


        public void SaveProduct()
        {
            string path = InventoryHelper.GetSaveFilePath();
            product temp = new product();
            temp.product_name = ProductName;
            temp.brand = Brand;
            temp.category = SelectedCategory.id;
            //temp.sub_category = 
            temp.sell_price = SellingPrice;
            temp.cost_price = CostPrice;
            temp.image_url = SelectedPath;
            temp.weight = Weight;
            temp.status = true;
            ProductServices.AddUpdateProduct(temp);

        }


        public string Error
        {

            get { throw new NotImplementedException(); }

        }



        public string this[string columnName]
        {

            get
            {

                string result = string.Empty;

                switch (columnName)
                {

                    case "ProductName": if (string.IsNullOrEmpty(ProductName)) result = "Product Name is required!"; break;

                    //case "SellingPrice":
                    //    if (SellingPrice.GetType().Name.ToLower() != "decimal")
                    //    {
                    //        result = "Selling Price is Required";
                    //    }
                    //    break;

                    //case "CostPrice":
                    //    if (CostPrice == 0)
                    //    {
                    //        result = "CostPrice is Required";
                    //    }
                    //    break;

                };

                return result;

            }

        }
    }
}
