using inventory.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace inventory.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            this.DataContext = new ViewModel.LoginViewModel();
        }

        private void CloseCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            InventoryHelper.growlNotifications.Close();
            this.Close();
            
        }

        //protected override void OnClosed(System.EventArgs e)
        //{

        //    InventoryHelper.growlNotifications.Close();
        //    base.OnClosed(e);
        //}

    }
}
