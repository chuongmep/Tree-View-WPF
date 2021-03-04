using System.Collections.Generic;
using System.Windows;
using TreeView;

namespace TreeViewSingleDemo
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
                    MessageBox.Show($"Name:{viewModel.Name}-Checked{viewModel.IsChecked}");
                }
            }
        }
    }
}
