using BusinessLayer;
using DataLayer;
using EntityLayer;
using inventory.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory.ViewModel
{
    class SellProductsViewModel : ProductsViewModelBase
    {
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
            Category = CategoryServices.GetAllCategory(0);
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
                    MaxQuantity = _SelectedProduct.Stock==null?0:(int)_SelectedProduct.Stock;
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
