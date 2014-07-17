using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory.ViewModel
{
    public class ConfigurationWindowViewModel : ViewModelBase
    {
        private string _username;
        private string _password;
        private string _DatabaseName;
        private string _ServerName;

        public string UserName
        {
            get
            {
                _username = ServerConnection.Default.UserName;
                return _username;
            }
            set
            {
                _username = value;
                ServerConnection.Default["UserName"] = value;
                ServerConnection.Default.Save();
                RaisedPropertyChanged("UserName");
            }
        }

        public string Password
        {
            get
            {
                _password = ServerConnection.Default.Password;
                return _password;
            }
            set
            {
                _password = value;
                ServerConnection.Default["Password"] = value;
                ServerConnection.Default.Save();
                RaisedPropertyChanged("Password");
            }
        }

        public string DatabaseName
        {
            get
            {
                _DatabaseName = ServerConnection.Default.DatabaseName;
                return _DatabaseName;
            }
            set
            {
                _DatabaseName = value;
                ServerConnection.Default["DatabaseName"] = value;
                ServerConnection.Default.Save();
                RaisedPropertyChanged("DatabaseName");
            }
        }

        public string ServerName
        {
            get
            {
                _ServerName = ServerConnection.Default.ServerName;
                return _ServerName;
            }
            set
            {
                _DatabaseName = value;
                ServerConnection.Default["ServerName"] = value;
                ServerConnection.Default.Save();
                RaisedPropertyChanged("ServerName");
            }
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
