using System.Collections.ObjectModel;
using System.Windows;

namespace TreeViewMultiDemo
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            SelectedNodes = new ObservableCollection<TreeViewModel>();
            SelectedNodes.CollectionChanged += SelectedNodes_CollectionChanged;
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TreeView1.ItemsSource = TreeViewModel.SetTree("Top Level");
        }
        public ObservableCollection<TreeViewModel> SelectedNodes { get; set; }

        void SelectedNodes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            NumberOfSelectedNodes.Text = SelectedNodes.Count.ToString();
        }

    }
}
