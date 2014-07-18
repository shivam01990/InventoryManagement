using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using BusinessLayer;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows;
using inventory.Helpers;
using System.ComponentModel;

namespace inventory.ViewModel
{
    public class CategoryViewModel : ProductsViewModelBase
    {
        public IList<category> temp_loadtime_Category;
        private readonly BackgroundWorker worker = new BackgroundWorker();
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            temp_loadtime_Category = CategoryServices.GetAllCategory(0);
        }
        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Category = temp_loadtime_Category;
        }
        public override string Name
        {
            get { return InventoryHelper.Category; }
        }

        public override string Icon
        {
            get { return InventoryHelper.CategoryIcon; }
        }

        public CategoryViewModel()
        {
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
            //BindGrid();
        }

        protected void BindGrid()
        {
            this._Category = CategoryServices.GetAllCategory(0);
        }

        private IList<category> _Category;
        public IList<category> Category
        {
            get { return _Category; }
            set
            {
                _Category = value;
                RaisedPropertyChanged("Category");
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
                    _addCommand = new RelayCommand(new Action<object>(AddCatagory));
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
                    _updateCommand = new RelayCommand(new Action<object>(UpdateCatagory));
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

        public void DeleteCatagory(object parameter)
        {
            bool flag = false;
            int Category_id = 0;
            TextBox tb = (TextBox)parameter;
            int.TryParse(tb.Text, out Category_id);
            if (Category_id > 0)
            {
                flag = CategoryServices.DeleteCategory(Category_id);
            }
            if (flag == true)
            {
                BindGrid();
                RaisedPropertyChanged("Category");
                //MessageBox.Show("Category Deleted");
                InventoryHelper.SuccessAlert("Success", "Category Deleted");
            }
            else
            {
                //MessageBox.Show("Sub-category are associated with Category ");
                InventoryHelper.SimpleAlert("Warning", "Sub-category are associated with Category");
            }
        }

        public void UpdateCatagory(object parameter)
        {
            int category_id = 0;
            TextBox tb = (TextBox)parameter;
            int.TryParse(tb.Text, out category_id);
            if (category_id > 0)
            {
                category ob_Category = _Category.Where(s => s.id == category_id).First();
                List<category> temp_list = CategoryServices.GetAllCategoryByName(ob_Category.category_name.Trim());
                if (temp_list.Count == 0 || temp_list.Count == 1)
                {
                    if (temp_list.Count == 1)
                    {
                        category temp = temp_list.FirstOrDefault();
                        if (temp.id != ob_Category.id)
                        {
                            BindGrid();
                            RaisedPropertyChanged("Category");
                            //MessageBox.Show("Category Name alredy Exist.");
                            InventoryHelper.SimpleAlert("Warning", "Category Name alredy Exist.");
                            return;
                        }

                    }
                    int temp_Category_id = CategoryServices.AddUpdateCategory(ob_Category);
                    if (temp_Category_id == ob_Category.id)
                    {
                        //MessageBox.Show("Category " + ob_Category.category_name + " is Updated");
                        InventoryHelper.SuccessAlert("Success", "Category " + ob_Category.category_name + " is Updated");
                        BindGrid();
                        RaisedPropertyChanged("Category");
                    }
                }
                else
                {

                    //MessageBox.Show("Category Name already Exist");
                    InventoryHelper.SimpleAlert("Warning", "Category Name already Exist");
                   

                }

            }
        }

        public void AddCatagory(object parameter)
        {
            string category_name;
            TextBox tb = (TextBox)parameter;
            category_name = tb.Text;
            if (category_name != "")
            {
                List<category> temp_list = CategoryServices.GetAllCategoryByName(category_name.Trim());
                if (temp_list.Count == 0)
                {
                    category ct = new category();
                    ct.id = 0;
                    ct.category_name = category_name;
                    int temp_Category_id = CategoryServices.AddUpdateCategory(ct);
                    if (temp_Category_id != 0)
                    {
                        InventoryHelper.SuccessAlert("Success", "Category " + category_name + " is Created");
                       // MessageBox.Show("Category " + category_name + " is Created");
                        BindGrid();
                        RaisedPropertyChanged("Category");
                    }
                }
            }
        }
    }
}
