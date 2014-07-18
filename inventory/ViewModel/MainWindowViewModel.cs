using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace inventory.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly BackgroundWorker worker = new BackgroundWorker();
        private ObservableCollection<DealersViewModelBase> _delarsMenu;
        private ObservableCollection<ProductsViewModelBase> _ProductMenu;
        private ObservableCollection<TransactionViewModelBase> _transactionMenu;
        public ObservableCollection<DealersViewModelBase> DelarsMenu
        {
            get { return this._delarsMenu; }
            set
            {
                _delarsMenu = value;
                RaisedPropertyChanged("DelarsMenu");
            }

        }

        public ObservableCollection<ProductsViewModelBase> ProductMenu
        {
            get { return this._ProductMenu; }
            set
            {
                _ProductMenu = value;
                RaisedPropertyChanged("ProductMenu");
            }
        }

        public ObservableCollection<TransactionViewModelBase> TransactionMenu
        {
            get { return this._transactionMenu; }

            set
            {
                _transactionMenu = value;
                RaisedPropertyChanged("TransactionMenu");
            }
        }

        public MainWindowViewModel()
        {
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();


        }
        ObservableCollection<DealersViewModelBase> temp_delarsMenu = new ObservableCollection<DealersViewModelBase>();
        ObservableCollection<ProductsViewModelBase> temp_ProductMenu = new ObservableCollection<ProductsViewModelBase>();
        ObservableCollection<TransactionViewModelBase> temp_transactionMenu = new ObservableCollection<TransactionViewModelBase>();
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            #region Dealer Menu
            temp_delarsMenu.Add(new ModifyDealersViewModel());
            temp_delarsMenu.Add(new AddDealersViewModel());
            #endregion

            /////////////////////////////////////////////////////////////////////
            #region Product Menu

            temp_ProductMenu.Add(new CategoryViewModel());
            temp_ProductMenu.Add(new AddSubCategoryViewModel());
            temp_ProductMenu.Add(new AddProductViewModel());
            temp_ProductMenu.Add(new ModifyProductViewModel());
            temp_ProductMenu.Add(new ProductStockEntryViewModel());
            temp_ProductMenu.Add(new SellProductsViewModel());
            #endregion
            //////////////////////////////////////////////////////////////////////
            #region Transaction Menu
            temp_transactionMenu.Add(new TransactionSummaryViewModel());
            #endregion
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DelarsMenu = temp_delarsMenu;
            ProductMenu = temp_ProductMenu;
            TransactionMenu = temp_transactionMenu;
        }

    }
}
