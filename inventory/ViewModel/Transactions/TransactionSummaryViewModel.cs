using BusinessLayer;
using EntityLayer;
using inventory.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory.ViewModel
{
    public class TransactionSummaryViewModel : TransactionViewModelBase
    {
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
            TransactionPurchaseList = SellingHistoryServices.GetAllDebitTransaction(null, null);
            TransactionSellList = SellingHistoryServices.GetAllCreditTransaction(null, null);
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
    }
}
