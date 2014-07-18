using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using BusinessLayer;
using DataLayer;
using System.Windows.Controls;
using inventory.View;
using inventory.View.Alerts;
using inventory.Helpers;


namespace inventory.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly BackgroundWorker worker = new BackgroundWorker();
        private string _username;
        private string _password;

        public string UserName
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                RaisedPropertyChanged("UserName");
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                RaisedPropertyChanged("Password");
            }
        }

        public LoginViewModel()
        {

        }



        private ICommand _clickCommand;
        private ICommand _clickCancel;
        private ICommand _ClickConfig;

        public ICommand ClickConfig
        {
            get
            {
                if (_ClickConfig == null)
                {
                    _ClickConfig = new RelayCommand(new Action<object>(OpenConfigWindow));

                     
                }
                return _ClickConfig;
            }
            set
            {
                _ClickConfig = value;
                RaisedPropertyChanged("ClickConfig");
            }
        }

        public ICommand ClickCommand
        {
            get
            {
                if (_clickCommand == null)
                {
                    _clickCommand = new RelayCommand(new Action<object>(ValidateClient));
                }
                return _clickCommand;
            }
            set
            {
                _clickCommand = value;
                RaisedPropertyChanged("ClickCommand");

            }
        }

        public ICommand ClickCancel
        {
            get
            {
                if (_clickCancel == null)
                {
                    _clickCancel = new RelayCommand(new Action<object>(CancelClient));
                }
                return _clickCancel;
            }
            set
            {
                _clickCancel = value;
                RaisedPropertyChanged("ClickCancel");

            }
        }

        public void OpenConfigWindow(object parameter)
        {
            ConfigurationWindow ob = new ConfigurationWindow();
            ob.ShowDialog();
        }

        public void ValidateClient(object parameter)
        {
            try
            {
                DataLayer.Constants.DynamicConnectionString = InventoryHelper.DynamicConnectionString;
                var passwordBox = (PasswordBox)parameter;
                Password = passwordBox.Password;
                bool flag = UserServices.CheckLogin(UserName, Password);
                if (flag == true)
                {
                    worker.DoWork += worker_DoWork;
                    worker.RunWorkerCompleted += worker_RunWorkerCompleted;
                    worker.RunWorkerAsync();
                    // MessageBox.Show("Login Successful");
                    InventoryHelper.SuccessAlert("Success", "Login Successful.");

                    MainWindow ob = new MainWindow();
                    this.Close = true;
                    ob.ShowDialog();

                }
                else
                {
                    //MessageBox.Show("Fails");
                    InventoryHelper.growlNotifications.AddNotification(new Notification { Title = "Warning", ImageUrl = "pack://application:,,,/Files/notification-icon.png", Message = "Login Fails." });

                }
            }
            catch
            {
                InventoryHelper.SimpleAlert("Warning", " Please Check Configuration Settings");
            }
        }

        public void CancelClient(object parameter)
        {
            InventoryHelper.growlNotifications.Close();
            this.Close = true;
        }
        private bool _close;
        public bool Close
        {
            get
            {
                return _close;
            }
            set
            {

                _close = value;
                RaisedPropertyChanged("Close");
            }
        }

        private Decimal OverAllBalance;
        private Decimal MonthlyBalance;
        private string EmptyStockProductList = "";
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            OverAllBalance = SellingHistoryProvider.GetOverAllBalance(null, null);
            DateTime date = DateTime.Now;
            DateTime FirstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            DateTime LastDayOfMonth = new DateTime(date.Year, date.Month, 1).AddMonths(1).AddDays(-1);
            MonthlyBalance = SellingHistoryProvider.GetOverAllBalance(FirstDayOfMonth, LastDayOfMonth);

            foreach (product item in ProductServices.GetEmptyStockList())
            {
                EmptyStockProductList += item.product_name + ",";

            }
            if (EmptyStockProductList.Length != 0)
            {
                EmptyStockProductList = EmptyStockProductList.Substring(0, EmptyStockProductList.LastIndexOf(','));
            }
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            InventoryHelper.SimpleAlert("OverAll Balance", "Your Over All Balance is " + OverAllBalance + ".");
            InventoryHelper.SimpleAlert("Monthly Balance", "Your Monthly Balance is " + MonthlyBalance + ".");


            if (EmptyStockProductList.Length != 0)
            {
                InventoryHelper.SimpleAlert("Products Out of Stock", "Products " + EmptyStockProductList + " are Out of Stock");

            }
        }

    }
}
