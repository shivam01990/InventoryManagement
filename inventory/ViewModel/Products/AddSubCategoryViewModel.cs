using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using BusinessLayer;
using EntityLayer;
using inventory.Model;

namespace inventory.ViewModel
{
    public class AddSubCategoryViewModel : ProductsViewModelBase
    {
        public override string Name
        {
            get { return "Sub-Category"; }
        }

        public AddSubCategoryViewModel()
        {
            this._Categorylst = CategoryServices.GetAllCategory(0);
           // this._lstSubCategory = SubcategoryModel.GetAllSubCategoryEntity(0, 0);
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

        private category _SelectedCategory;
        public category SelectedCategory
        {
            get
            {
                return _SelectedCategory;
            }
            set
            {
                _SelectedCategory = value;
                this._lstSubCategory = SubcategoryModel.GetAllSubCategoryEntity(0, _SelectedCategory.id);
                RaisedPropertyChanged("SelectedCategory");
                RaisedPropertyChanged("lstSubCategory");
            }
        }

        private SubCategoryEntity _SelectedSubCategory;
        public SubCategoryEntity SelectedSubCategory
        {
            get
            {
                return _SelectedSubCategory;
            }
            set
            {
                _SelectedSubCategory = value;
                if (_SelectedSubCategory != null)
                {
                    sub_category temp = SubCategoryServices.GetSubCategory(_SelectedSubCategory.id);
                    _SelectedCategory = CategoryServices.GetCategory((int)temp.category);
                }
                RaisedPropertyChanged("SelectedCategory");
                RaisedPropertyChanged("SelectedSubCategory");
                RaisedPropertyChanged("Categorylst");
            }
        }

        private List<SubCategoryEntity> _lstSubCategory;
        public List<SubCategoryEntity> lstSubCategory
        {
            get
            {
                return _lstSubCategory;
            }
            set
            {
                _lstSubCategory = value;
                RaisedPropertyChanged("lstSubCategory");
            }
        }
    }
}
