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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace inventory.ViewModel
{
    public class AddProductViewModel : ProductsViewModelBase, IDataErrorInfo
    {
        public IList<category> temp_loadtime_Category;
        private readonly BackgroundWorker worker = new BackgroundWorker();
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            temp_loadtime_Category = CategoryServices.GetAllCategory(0);
        }
        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Category = temp_loadtime_Category;
        }
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
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
            //this._Category = CategoryServices.GetAllCategory(0);
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


        private sub_category _SelectedSubCategory;

        public sub_category SelectedSubCategory
        {
            get
            {

                return _SelectedSubCategory;
            }
            set
            {
                _SelectedSubCategory = value;
                RaisedPropertyChanged("SelectedSubCategory");
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
            set
            {
                _selectedPath = value;
                RaisedPropertyChanged("SelectedPath");
            }
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
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif|BMP Files (*.bmp)|*.bmp";


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

        private ICommand _clickCommand;
        public ICommand ClickCommand
        {
            get
            {
                if (_clickCommand == null)
                {
                    _clickCommand = new RelayCommand(new Action<object>(SaveProduct));
                }
                return _clickCommand;
            }
            set
            {
                _clickCommand = value;
                RaisedPropertyChanged("ClickCommand");

            }
        }

        public void SaveProduct(object parameter)
        {
            try
            {
                string path = InventoryHelper.GetSaveFilePath() + "\\" + ProductName + System.IO.Path.GetExtension(SelectedPath);
                if (SelectedPath != InventoryHelper.ImageNA)
                {
                    System.IO.File.Copy(SelectedPath, path, true);
                }
                product temp = new product();
                temp.product_name = ProductName.Trim();
                if (!ProductServices.CheckProductNameAvailable(temp.product_name))
                {
                    InventoryHelper.SimpleAlert("Warning", "Product Name is Already taken");
                    return;
                }
                temp.brand = Brand == null ? "" : Brand;
                temp.sub_category = SelectedSubCategory.id;
                temp.category = SelectedCategory.id;
                temp.sell_price = SellingPrice;
                temp.cost_price = CostPrice;
                temp.image_url = SelectedPath == InventoryHelper.ImageNA ? "" : path;
                temp.weight = Weight == null ? "" : Weight;
                temp.Stock = 0;
                temp.status = true;
                int productId = ProductServices.AddUpdateProduct(temp);
                if (productId > 0)
                {
                   // MessageBox.Show("Product Added");
                    InventoryHelper.SuccessAlert("Success", "Product Added");
                    Brand = "";
                    SellingPrice = 0;
                    CostPrice = 0;
                    Weight = "";
                    SelectedPath = InventoryHelper.ImageNA;
                    ProductName = "";
                    SelectedSubCategory = null;
                    SelectedCategory = null;
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Product Entry Fails:" + ex.ToString());
            }

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

                    case "ProductName":
                        if (string.IsNullOrEmpty(ProductName))
                            result = "Product Name is required!";
                        break;

                    case "SellingPrice":
                        if ((SellingPrice == 0) || (SellingPrice < 0))
                        {
                            result = "Selling Price is must be greater than zero";
                        }
                        break;

                    case "CostPrice":
                        if ((CostPrice == 0) || (CostPrice < 0))
                        {
                            result = "CostPrice must be greater than zero";
                        }
                        break;

                    case "SelectedCategory":
                        if (SelectedCategory == null)
                        {
                            result = "Select Category";
                        }
                        break;
                    case "SelectedSubCategory":
                        if (SelectedSubCategory == null)
                        {
                            result = "Select SubCategory";
                        }
                        break;
                };

                return result;

            }

        }




    }
}
