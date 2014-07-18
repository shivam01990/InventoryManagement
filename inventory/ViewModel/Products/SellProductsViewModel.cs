using BusinessLayer;
using DataLayer;
using EntityLayer;
using inventory.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace inventory.ViewModel
{
    class SellProductsViewModel : ProductsViewModelBase
    {
        private readonly BackgroundWorker worker = new BackgroundWorker();
        public List<category> temp_loadtime_Category;
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
            get { return InventoryHelper.SellProducts; }
        }

        public override string Icon
        {
            get { return InventoryHelper.SellProductsIcon; }
        }


        public SellProductsViewModel()
        {
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
            //Category = CategoryServices.GetAllCategory(0);
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


        private IList<product> _Products;
        public IList<product> Products
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
                    //CostPrice = _SelectedProduct.cost_price;
                    SellingPrice = _SelectedProduct.sell_price;
                    MaxQuantity = _SelectedProduct.Stock == null ? 0 : (int)_SelectedProduct.Stock;
                }
                return _SelectedProduct;
            }
            set
            {
                _SelectedProduct = value;
                RaisedPropertyChanged("SelectedProduct");
            }
        }




        private List<ProductSellingEntity> _SellingItems;
        public List<ProductSellingEntity> SellingItems
        {
            get
            {
                return _SellingItems;
            }
            set
            {
                _SellingItems = value;
                RaisedPropertyChanged("SellingItems");
            }
        }

        private string _CustomerName;
        public string CustomerName
        {
            get
            {
                return _CustomerName;
            }
            set
            {
                _CustomerName = value;
                RaisedPropertyChanged("CustomerName");
            }
        }

        private string _CustomerInfo;
        public string CustomerInfo
        {
            get
            {
                return _CustomerInfo;
            }
            set
            {
                _CustomerInfo = value;
                RaisedPropertyChanged("CustomerInfo");
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

        private int _Quantity;
        public int Quantity
        {
            get
            {
                return _Quantity;
            }
            set
            {
                _Quantity = value;
                RaisedPropertyChanged("Quantity");
            }
        }

        private int _MaxQuantity;
        public int MaxQuantity
        {
            get
            {
                return _MaxQuantity;
            }
            set
            {
                _MaxQuantity = value;
                RaisedPropertyChanged("MaxQuantity");
            }
        }

        private decimal _TotalAmount;
        public decimal TotalAmount
        {
            get
            {
                return _TotalAmount;
            }
            set
            {
                _TotalAmount = value;
                RaisedPropertyChanged("TotalAmount");
            }
        }

        private ICommand _SubmitCommand;
        public ICommand SubmitCommand
        {
            get
            {
                if (_SubmitCommand == null)
                {
                    _SubmitCommand = new RelayCommand(new Action<object>(SubmitSellingList));
                }
                return _SubmitCommand;
            }
            set
            {
                _SubmitCommand = value;
                RaisedPropertyChanged("SubmitCommand");
            }
        }

        protected void SubmitSellingList(object parameter)
        {
            try
            {
                if (SellingItems != null)
                {
                    if (SellingItems.Count != 0)
                    {
                        List<selling_history> ob_sellinghistory = new List<selling_history>();
                        foreach (ProductSellingEntity item in SellingItems)
                        {
                            ob_sellinghistory.Add(Converttosellinghistory(item));
                        }
                        bool flag = SellingHistoryServices.AddBulkSellingHistory(ob_sellinghistory);
                        if (flag == true)
                        {
                            // MessageBox.Show("Transaction Complete");
                            InventoryHelper.SuccessAlert("Success", "Transaction Complete");
                            try
                            {
                                foreach (ProductSellingEntity item in SellingItems)
                                {
                                    ProductServices.UpdateProductStock(item.ProductId);
                                }
                            }
                            catch
                            { }

                        }
                        Initialize();
                    }

                }
            }
            catch
            {
                InventoryHelper.SimpleAlert("Warning", "Transaction Fails");
                // MessageBox.Show("Transaction Fails"); }
            }
        }


        protected void Initialize()
        {
            TotalAmount = 0;
            SellingPrice = 0;
            Remarks = "";
            SellingItems = null;
            SelectedCategory = null;
            SelectedSubCategory = null;
            SelectedProduct = null;
            PaymentType = null;
            CustomerInfo = "";
            CustomerName = "";
        }

        private ICommand _AddCommand;
        public ICommand AddCommand
        {
            get
            {
                if (_AddCommand == null)
                {
                    _AddCommand = new RelayCommand(new Action<object>(AddSellingList));
                }
                return _AddCommand;
            }
            set
            {
                _AddCommand = value;
                RaisedPropertyChanged("AddCommand");

            }
        }

        protected void AddSellingList(object parameter)
        {
            if (SelectedProduct != null)
            {
                ProductSellingEntity Item = null;
                Item = CoverttoProductSellingEntity(SelectedProduct);
                List<ProductSellingEntity> temp_Selling_lst = new List<ProductSellingEntity>();
                if (SellingItems != null)
                {
                    temp_Selling_lst = SellingItems;
                    if (temp_Selling_lst.Where(p => p.ProductId == Item.ProductId).Count() != 0)
                    {
                        int temp_quantity = temp_Selling_lst.Where(p => p.ProductId == Item.ProductId).FirstOrDefault().Quantity;
                        Quantity += temp_quantity;
                        if (!checkQuantity())
                        {
                            return;
                        }
                        Item = CoverttoProductSellingEntity(SelectedProduct);
                        temp_Selling_lst.Remove(temp_Selling_lst.Where(p => p.ProductId == Item.ProductId).FirstOrDefault());
                    }
                }
                if (!checkQuantity())
                {
                    return;
                }
                temp_Selling_lst.Add(Item);
                Quantity = 0;
                SellingItems = null;
                SellingItems = temp_Selling_lst;
                TotalAmount = SellingItems.Sum(s => s.Amount);
            }
        }

        public bool checkQuantity()
        {
            if (Quantity > MaxQuantity)
            {
                MessageBox.Show("Quantity is greater than Stock");
                return false;
            }
            return true;
        }


        protected ProductSellingEntity CoverttoProductSellingEntity(product ob)
        {

            ProductSellingEntity tempproduct = new ProductSellingEntity();
            tempproduct.ProductId = ob.id;
            tempproduct.ProductName = ob.product_name;
            tempproduct.Quantity = Quantity;
            tempproduct.SellingPrice = ob.sell_price;
            tempproduct.Amount = tempproduct.SellingPrice * Quantity;
            return tempproduct;
        }

        protected selling_history Converttosellinghistory(ProductSellingEntity ob)
        {
            selling_history temphistory = new selling_history();
            temphistory.dealer_id = null;
            temphistory.product_id = ob.ProductId;
            temphistory.quantity = ob.Quantity;
            temphistory.credit = ob.Amount;
            temphistory.debit = 0;
            temphistory.transaction_type = (int)InventoryHelper.TransactionType.Credit;
            temphistory.customer_info = CustomerInfo;
            temphistory.payment_type = PaymentType.Content == null ? "" : PaymentType.Content.ToString();
            temphistory.payment_date = DateTime.Now;
            temphistory.customer_name = CustomerName;
            temphistory.remarks = Remarks;

            return temphistory;
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

        //private decimal _CostPrice;

        //public decimal CostPrice
        //{
        //    get
        //    {
        //        return _CostPrice;
        //    }
        //    set
        //    {
        //        _CostPrice = value;
        //        RaisedPropertyChanged("CostPrice");
        //       // Amount = _Qantity * _CostPrice;
        //    }

        //}

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

    }
}
