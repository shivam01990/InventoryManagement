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


namespace inventory.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
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
        public void ValidateClient(object parameter)
        {
            var passwordBox = (PasswordBox)parameter;
            Password = passwordBox.Password;
            bool flag = UserServices.CheckLogin(UserName, Password);
            if (flag == true)
            {

                //MessageBox.Show("Login Successful");
                MainWindow ob = new MainWindow();
                this.Close = true;
                ob.ShowDialog();
            }
            else
            {
                MessageBox.Show("Fails");
            }
        }

        public void CancelClient(object parameter)
        {
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

    }
}
