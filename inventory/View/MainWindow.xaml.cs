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
using System.Windows.Shapes;

namespace inventory.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();

        }

        private void CloseCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void CommandBinding_CanExecute_1(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void MiniMize_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        public void Drag_Window(object sender, MouseButtonEventArgs e)
        {

            this.DragMove();

        }

        private void ListBoxDelears_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //pnlcontent.Content = "{Binding ElementName=ListBoxDelears, Path=SelectedItem}";
            pnlcontent.Content = ListBoxDelears.SelectedItem;
        }


    }
}
