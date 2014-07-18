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
    public class ProductStockEntryViewModel : ProductsViewModelBase, IDataErrorInfo
    {
        public List<dealer> temp_Loadtime_Dealers;
        public List<category> temp_loadtime_Category;
        private readonly BackgroundWorker worker = new BackgroundWorker();
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            temp_Loadtime_Dealers = DelarServices.GetAllDelars(0);
            temp_loadtime_Category = CategoryServices.GetAllCategory(0);
        }
        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Dealers = temp_Loadtime_Dealers;
            Category = temp_loadtime_Category;
        }
        public override string Name
        {
            get { return InventoryHelper.StockEntry; }
        }

        public override string Icon
        {
            get { return InventoryHelper.StockEntryIcon; }
        }

        public ProductStockEntryViewModel()
        {
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
            //Dealers = DelarServices.GetAllDelars(0);
            //Category = CategoryServices.GetAllCategory(0);
        }

        private List<dealer> _Dealers;
        public List<dealer> Dealers
        {
            get
            {
                return _Dealers;
            }
            set
            {
                _Dealers = value;
                RaisedPropertyChanged("Dealers");
            }
        }


        private dealer _SelectedDealer;
        public dealer SelectedDealer
        {
            get
            {
                return _SelectedDealer;
            }
            set
            {
                _SelectedDealer = value;
                RaisedPropertyChanged("SelectedDealer");
            }
        }


        private List<category> _Category;
        public List<category> Category
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
                if (_SelectedSubCategory != null)
                {
                    Products = ProductServices.GetProductBySubcategory(_SelectedSubCategory.id);
                }

                return _SelectedSubCategory;
            }
            set
            {
                _SelectedSubCategory = value;
                RaisedPropertyChanged("SelectedSubCategory");
            }
        }


        private List<sub_category> _SubCategory;
        public List<sub_category> SubCategory
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

        private List<product> _Products;
        public List<product> Products
        {
            get
            {
                return _Products;
            }
            set
            {
                _Products = value;
                RaisedPropertyChanged("Products");
            }
        }

        private product _SelectedProduct;
        public product SelectedProduct
        {
            get
            {
                if (_SelectedProduct != null)
                {
                    CostPrice = _SelectedProduct.cost_price;
                    SellingPrice = _SelectedProduct.sell_price;
                }
                return _SelectedProduct;
            }
            set
            {
                _SelectedProduct = value;
                RaisedPropertyChanged("SelectedProduct");
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
                Amount = _Qantity * _CostPrice;
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


        private int _Qantity;
        public int Quantity
        {
            get
            {
                return _Qantity;
            }
            set
            {
                _Qantity = value;
                RaisedPropertyChanged("Quantity");
                Amount = _Qantity * _CostPrice;
            }

        }


        private decimal _Amount;
        public decimal Amount
        {
            get
            {
                return _Amount;
            }
            set
            {
                _Amount = value;
                RaisedPropertyChanged("Amount");
            }
        }


        private string _Remarks;
        public string Remarks
        {
            get
            {
                return _Remarks;
            }
            set
            {
                _Remarks = value;
                RaisedPropertyChanged("Remarks");
            }
        }

        private System.Windows.Controls.ComboBoxItem _PaymentType;
        public System.Windows.Controls.ComboBoxItem PaymentType
        {
            get
            {
                return _PaymentType;
            }
            set
            {
                _PaymentType = value;
                RaisedPropertyChanged("PaymentType");
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

                    case "SelectedDealer":
                        if (SelectedDealer == null)
                            result = "Please Select Dealer";
                        break;

                    case "SelectedCategory":
                        if (SelectedCategory == null)
                        {
                            result = "Please Select Category";
                        }
                        break;

                    case "SelectedSubCategory":
                        if (SelectedSubCategory == null)
                        {
                            result = "Please Select Sub Category";
                        }
                        break;
                    case "SelectedProduct":
                        if (SelectedProduct == null)
                        {
                            result = "Please select Product";
                        }
                        break;
                    case "Quantity":
                        if (Quantity <= 0)
                        {
                            result = "Please input Quantity";
                        }
                        break;
                };

                return result;

            }

        }

        private ICommand _clickCommand;
        public ICommand ClickCommand
        {
            get
            {
                if (_clickCommand == null)
                {
                    _clickCommand = new RelayCommand(new Action<object>(AddStock));
                }
                return _clickCommand;
            }
            set
            {
                _clickCommand = value;
                RaisedPropertyChanged("ClickCommand");

            }
        }

        protected void AddStock(object parameter)
        {
            try
            {
                selling_history ob = new selling_history();
                ob.id = 0;
                ob.dealer_id = SelectedDealer.id;
                ob.product_id = SelectedProduct.id;
                ob.quantity = Quantity;
                ob.credit = 0;
                ob.debit = Amount;
                ob.transaction_type = (int)InventoryHelper.TransactionType.Debit;
                ob.customer_info = "";
                ob.payment_type = PaymentType == null ? "" : PaymentType.Content.ToString();
                ob.payment_date = DateTime.Now;
                ob.customer_name = "";
                ob.remarks = Remarks;
                SellingHistoryServices.AddUpdateSellingHistory(ob);
                ProductServices.UpdateProductStock(ob.product_id);
                //MessageBox.Show("Stock Added");
                InventoryHelper.SuccessAlert("Success", "Stock Added");
                Initialize();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurse::" + ex.ToString());
            }
        }

        protected void Initialize()
        {
            SelectedDealer = null;
            SelectedCategory = null;
            SelectedSubCategory = null;
            SelectedProduct = null;
            Remarks = "";
            CostPrice = 0;
            SellingPrice = 0;
            Amount = 0;
            PaymentType = null;
            Quantity = 0;

        }


    }
}
