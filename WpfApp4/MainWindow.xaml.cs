using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp4;

namespace TreeView
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            TreeViewModel.FrMainWindow = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            treeView1.ItemsSource = TreeViewModel.SetTree("Top Level");
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            TreeViewModel root = (TreeViewModel)treeView1.Items[0];
            List<TreeViewModel> treeViewModels = root.Children;
            foreach (TreeViewModel model in treeViewModels)
            {
                List<TreeViewModel> models = model.Children;
                foreach (TreeViewModel viewModel in models)
                {
                    MessageBox.Show(viewModel.IsChecked.ToString());
                }
            }
        }
    }
}
