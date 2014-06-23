using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ObservableCollection<DealersViewModelBase> _delarsMenu;
        private ObservableCollection<ProductsViewModelBase> _ProductMenu;
        public ObservableCollection<DealersViewModelBase> DelarsMenu
        {
            get { return this._delarsMenu; }
        }

        public ObservableCollection<ProductsViewModelBase> ProductMenu
        {
            get { return this._ProductMenu; }
        }

        public MainWindowViewModel()
        {
            this._delarsMenu = new ObservableCollection<DealersViewModelBase>();
            this._delarsMenu.Add(new ModifyDealersViewModel());
            this._delarsMenu.Add(new AddDealersViewModel());

            this._ProductMenu = new ObservableCollection<ProductsViewModelBase>();
            this._ProductMenu.Add(new CategoryViewModel());
            this._ProductMenu.Add(new AddSubCategoryViewModel());
            this._ProductMenu.Add(new AddProductViewModel());
            this._ProductMenu.Add(new ModifyProductViewModel());
            this._ProductMenu.Add(new ProductStockEntryViewModel());

        }

    }
}
