using inventory.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace inventory.View
{
    /// <summary>
    /// Interaction logic for TransactionSummary.xaml
    /// </summary>
    public partial class TransactionSummary : UserControl
    {
        public TransactionSummary()
        {
            InitializeComponent();
            this.DataContext = new TransactionSummaryViewModel();
        }
    }
}
