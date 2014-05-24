using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory.ViewModel
{
    public class MainWindowViewModel:ViewModelBase
    {
        private readonly ObservableCollection<DealersViewModelBase> _delarsMenu;

        public ObservableCollection<DealersViewModelBase> DelarsMenu
        {
            get { return this._delarsMenu; }
        }

        public MainWindowViewModel()
        {
            this._delarsMenu = new ObservableCollection<DealersViewModelBase>();
            this._delarsMenu.Add(new ModifyDealersViewModel());
            this._delarsMenu.Add(new AddDealersViewModel());
            
        }

    }
}
