using inventory.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using DataLayer;
using BusinessLayer;

namespace inventory.View
{
    /// <summary>
    /// Interaction logic for ModifyProduct.xaml
    /// </summary>
    public partial class ModifyProduct : UserControl
    {
        public ObservableCollection<AutoCompleteEntry> autoCompletionList;
        public ModifyProduct()
        {
            InitializeComponent();
            autoCompletionList = new ObservableCollection<AutoCompleteEntry>();            
            //textBox1.AddItem(new AutoCompleteEntry("Toyota Camry", "Toyota Camry", "camry", "car", "sedan"));
            //textBox1.AddItem(new AutoCompleteEntry("Toyota Corolla", "Toyota Corolla", "corolla", "car", "compact"));
            //textBox1.AddItem(new AutoCompleteEntry("Toyota Tundra", "Toyota Tundra", "tundra", "truck"));
            //textBox1.AddItem(new AutoCompleteEntry("Chevy Impala", null));  // null matching string will default with just the name
            //textBox1.AddItem(new AutoCompleteEntry("Chevy Tahoe", "Chevy Tahoe", "tahoe", "truck", "SUV"));
            //textBox1.AddItem(new AutoCompleteEntry("Chevrolet Malibu", "Chevrolet Malibu", "malibu", "car", "sedan"));
            List<product> lst_product = new List<product>();
            lst_product = ProductServices.GetAllProduct(0);

            //autoCompletionList = (from u in lst_product
            //                      select new AutoCompleteEntry { DisplayName = u.product_name });

            foreach (product item in lst_product)
            {
                autoCompletionList.Add(new AutoCompleteEntry(item.product_name, null));
            }

            textBox1.AddList(autoCompletionList);

        }
    }
}
