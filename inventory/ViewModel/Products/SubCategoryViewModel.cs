using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using BusinessLayer;
using EntityLayer;
using inventory.Model;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows;
using inventory.Helpers;
using System.ComponentModel;

namespace inventory.ViewModel
{
    public class AddSubCategoryViewModel : ProductsViewModelBase
    {
        public List<category> temp_loadtime_Categorylst;
        private readonly BackgroundWorker worker = new BackgroundWorker();
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            temp_loadtime_Categorylst=CategoryServices.GetAllCategory(0);
        }
        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Categorylst = temp_loadtime_Categorylst;
        }
        public override string Name
        {
            get { return InventoryHelper.SubCategory; }
        }

        public override string Icon
        {
            get { return InventoryHelper.SubCategoryIcon; }
        }
        public AddSubCategoryViewModel()
        {
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
           // Categorylst = CategoryServices.GetAllCategory(0);
            //this._lstSubCategory = SubcategoryModel.GetAllSubCategoryEntity(0, 0);
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
                BindGrid();
            }
        }

        protected void BindGrid()
        {
            this._lstSubCategory = SubcategoryModel.GetAllSubCategoryEntity(0, _SelectedCategory.id);
            RaisedPropertyChanged("SelectedCategory");
            RaisedPropertyChanged("lstSubCategory");

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

        #region Button Command

        private ICommand _addCommand;
        public ICommand AddCommand
        {
            get
            {
                if (_addCommand == null)
                {
                    _addCommand = new RelayCommand(new Action<object>(AddSubCatagory));
                }
                return _addCommand;
            }
            set
            {
                _addCommand = value;
                RaisedPropertyChanged("AddCommand");

            }
        }

        private ICommand _updateCommand;
        public ICommand UpdateCommand
        {
            get
            {
                if (_updateCommand == null)
                {
                    _updateCommand = new RelayCommand(new Action<object>(UpdateSubCatagory));
                }
                return _updateCommand;
            }
            set
            {
                _updateCommand = value;
                RaisedPropertyChanged("UpdateCommand");

            }
        }

        private ICommand _deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                {
                    _deleteCommand = new RelayCommand(new Action<object>(DeleteCatagory));
                }
                return _deleteCommand;
            }
            set
            {
                _updateCommand = value;
                RaisedPropertyChanged("DeleteCommand");

            }
        }
        #endregion

        protected void AddSubCatagory(object parameter)
        {
            string sub_category_name;
            TextBox tb = (TextBox)parameter;
            sub_category_name = tb.Text;
            if ((sub_category_name != "") && (SelectedCategory != null))
            {
                List<sub_category> temp_list = SubCategoryServices.GetAllSubCategoryByName(sub_category_name.Trim(), SelectedCategory.id);
                if (temp_list.Count == 0)
                {
                    sub_category ct = new sub_category();
                    ct.id = 0;
                    ct.subcategory_name = sub_category_name;
                    ct.category = SelectedCategory.id;
                    int temp_SubCategory_id = SubCategoryServices.AddUpdateSubCategory(ct);
                    if (temp_SubCategory_id != 0)
                    {
                       // MessageBox.Show("Category " + sub_category_name + " is Created");
                        InventoryHelper.SuccessAlert("Success", "Category " + sub_category_name + " is Created");
                        BindGrid();


                    }
                }
            }
        }


        protected void UpdateSubCatagory(object parameter)
        {
            int Sub_category_id = 0;
            TextBox tb = (TextBox)parameter;
            int.TryParse(tb.Text, out Sub_category_id);
            if ((Sub_category_id > 0) && (SelectedCategory != null))
            {
                SubCategoryEntity ob_sub_Category = _lstSubCategory.Where(s => s.id == Sub_category_id).FirstOrDefault();
                List<sub_category> temp_list = SubCategoryServices.GetAllSubCategoryByName(ob_sub_Category.sub_category_name.Trim(), SelectedCategory.id);
                if (temp_list.Count == 0 || temp_list.Count == 1)
                {
                    if (temp_list.Count == 1)
                    {
                        sub_category temp = temp_list.FirstOrDefault();
                        if (temp.id != ob_sub_Category.id)
                        {
                            BindGrid();
                            //RaisedPropertyChanged("Category");
                            //MessageBox.Show("SubCategory Name alredy Exist.");
                            InventoryHelper.SimpleAlert("Warning", "SubCategory Name alredy Exist.");
                            return;
                        }

                    }
                    sub_category temp_ob_sub_Category = SubCategoryServices.GetSubCategory(ob_sub_Category.id);
                    temp_ob_sub_Category.subcategory_name = ob_sub_Category.sub_category_name;
                    int temp_sub_Category_id = SubCategoryServices.AddUpdateSubCategory(temp_ob_sub_Category);
                    if (temp_sub_Category_id == ob_sub_Category.id)
                    {
                        //MessageBox.Show("Sub-Category " + ob_sub_Category.sub_category_name + " is Updated");
                        InventoryHelper.SuccessAlert("Success", "Sub-Category " + ob_sub_Category.sub_category_name + " is Updated");
                        BindGrid();
                        //RaisedPropertyChanged("Category");
                    }
                }
                else
                {

                   // MessageBox.Show("Category Name already Exist");
                    InventoryHelper.SimpleAlert("Warning", "Category Name already Exist");

                }

            }
        }

        protected void DeleteCatagory(object parameter)
        {
            bool flag = false;
            int Sub_Category_id = 0;
            TextBox tb = (TextBox)parameter;
            int.TryParse(tb.Text, out Sub_Category_id);
            if (Sub_Category_id > 0)
            {
                flag = SubCategoryServices.DeleteSubCategory(Sub_Category_id);
            }
            if (flag == true)
            {
                BindGrid();
                //MessageBox.Show("Category Deleted");
                InventoryHelper.SuccessAlert("Success", "Category Deleted");
            }
            else
            {
                InventoryHelper.SimpleAlert("Warning", "Sub-category are associated with Category");
                //MessageBox.Show("Sub-category are associated with Category ");
            }
        }
    }
}
