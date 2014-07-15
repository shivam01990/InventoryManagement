using BusinessLayer;
using EntityLayer;
using inventory.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace inventory.ViewModel
{
    public class TransactionSummaryViewModel : TransactionViewModelBase
    {
        private readonly BackgroundWorker worker = new BackgroundWorker();
        public override string Name
        {
            get { return InventoryHelper.Transactions; }
        }

        public override string Icon
        {
            get { return InventoryHelper.TransactionsIcon; }
        }

        public TransactionSummaryViewModel()
        {
            FromDate = null;
            ToDate = null;
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
          

        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            TransactionPurchaseList = SellingHistoryServices.GetAllDebitTransaction(FromDate, ToDate);
            TransactionSellList = SellingHistoryServices.GetAllCreditTransaction(FromDate, ToDate);
            Balance = SellingHistoryServices.GetOverAllBalance(FromDate, ToDate);
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            List<TransactionPurchaseEntity> temp_TransactionPurchaseList = TransactionPurchaseList;
            TransactionPurchaseList = null;
            List<TransactionSellingEntity> temp_TransactionSellList = TransactionSellList;
            TransactionSellList = null;
            decimal temp_Balance = Balance;
            Balance = 0;

            Balance = temp_Balance;
            TransactionSellList = temp_TransactionSellList;
            TransactionPurchaseList = temp_TransactionPurchaseList;
           
        }

        private DateTime? _FromDate;
        public DateTime? FromDate
        {
            get
            {
                return _FromDate;
            }
            set
            {
                _FromDate = value;
                RaisedPropertyChanged("FromDate");
            }
        }


        private DateTime? _ToDate;
        public DateTime? ToDate
        {
            get
            {
                return _ToDate;
            }
            set
            {
                _ToDate = value;
                RaisedPropertyChanged("ToDate");
            }
        }

        private decimal _Balance;
        public decimal Balance
        {
            get
            {
                return _Balance;
            }
            set
            {
                _Balance = value;
                RaisedPropertyChanged("Balance");
            }
        }

        private List<TransactionPurchaseEntity> _TransactionPurchaseList;
        public List<TransactionPurchaseEntity> TransactionPurchaseList
        {
            get
            {
                return _TransactionPurchaseList;
            }
            set
            {
                _TransactionPurchaseList = value;
                RaisedPropertyChanged("TransactionPurchaseList");
            }
        }

        private List<TransactionSellingEntity> _TransactionSellList;
        public List<TransactionSellingEntity> TransactionSellList
        {
            get
            {
                return _TransactionSellList;
            }
            set
            {
                _TransactionSellList = value;
                RaisedPropertyChanged("TransactionSellList");
            }
        }


        private ICommand _SearchCommand;

        public ICommand SearchCommand
        {
            get
            {
                if (_SearchCommand == null)
                {
                    _SearchCommand = new RelayCommand(new Action<object>(SearchTransactions));
                }
                return _SearchCommand;
            }
            set
            {
                _SearchCommand = value;
                RaisedPropertyChanged("SearchCommand");

            }
        }


        protected void SearchTransactions(object parameter)
        {
            worker.RunWorkerAsync();

        }


    }
}
