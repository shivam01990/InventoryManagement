using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DataLayer;
using BusinessLayer;
using System.Windows.Forms;
using inventory.Helpers;

namespace inventory.ViewModel
{
    public class AddDealersViewModel : DealersViewModelBase
    {
        public override string Name
        {
            get { return InventoryHelper.AddDealer; }
        }

        public override string Icon
        {
            get { return InventoryHelper.AddDealerIcon; }
        }

        private string _dealeraddress;
        public string DealerAddress
        {
            get
            {
                return _dealeraddress;
            }
            set
            {
                _dealeraddress = value;
                RaisedPropertyChanged("DealerAddress");
            }
        }

        private string _dealername;
        public string DealerName
        {
            get
            {
                return _dealername;
            }
            set
            {
                _dealername = value;
                RaisedPropertyChanged("DealerName");
            }
        }

        private ICommand _addCommand;
        public ICommand AddCommand
        {
            get
            {
                if (_addCommand == null)
                {
                    _addCommand = new RelayCommand(new Action<object>(AddDealer));
                }
                return _addCommand;
            }
            set
            {
                _addCommand = value;
                RaisedPropertyChanged("AddCommand");

            }
        }

        public void AddDealer(object Parameter)
        {
            dealer ob = new dealer();
            ob.id = 0;
            ob.dealer_address = DealerAddress;
            ob.dealer_name = DealerName;
            List<dealer> temp = DelarServices.GetDelarByName(ob.dealer_name.Trim());
            if (temp.Count == 0)
            {
                int temp_dealerid = DelarServices.AddUpdateDealer(ob);
                if (temp_dealerid > 0)
                {
                   // MessageBox.Show("Dealer added successfully");
                    InventoryHelper.SuccessAlert("Success", "Dealer added successfully");
                    DealerAddress = "";
                    DealerName = "";
                }
            }
            else
            {
                InventoryHelper.SimpleAlert("Warning", "Dealer Name already Exist");                   
                //MessageBox.Show("Dealer Name already Exist");
            }

        }
    }
}
