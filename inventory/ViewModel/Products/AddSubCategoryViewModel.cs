using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using BusinessLayer;

namespace inventory.ViewModel
{
    public class AddSubCategoryViewModel : ProductsViewModelBase
    {
        public override string Name
        {
            get { return "Add Sub-Category"; }
        }

        public AddSubCategoryViewModel()
        {
            this._Categorylst = CategoryServices.GetAllCategory(0);
        }

        private List<category> _Categorylst;
        public List<category> Categorylst
        {
            get
            {
                return _Categorylst;
            }
            set
            {
                _Categorylst = value;
                RaisedPropertyChanged("Categorylst");
            }
        }

        public category SelectedCategory
        {
            get;
            set;
        }
    }
}
