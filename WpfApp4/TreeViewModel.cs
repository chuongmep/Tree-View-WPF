using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using WpfApp4;

namespace TreeView
{
    public class TreeViewModel : ViewModelBase
    {
        public static MainWindow FrMainWindow;
        TreeViewModel(string name)
        {
            Name = name;
            Children = new List<TreeViewModel>();
        }

        #region Properties

        public string Name { get; private set; }
        public List<TreeViewModel> Children { get; private set; }
        public bool IsInitiallySelected { get; private set; }

        bool? _isChecked = false;
        TreeViewModel _parent;
        
        #region IsChecked

        public bool? IsChecked
        {
            get { return _isChecked; }
            set { SetIsChecked(value, true, true); }
        }

        void SetIsChecked(bool? value, bool updateChildren, bool updateParent)
        {
            if (value == _isChecked) return;

            _isChecked = value;

            if (updateChildren && _isChecked.HasValue) Children.ForEach(c => c.SetIsChecked(_isChecked, true, false));

            if (updateParent && _parent != null) _parent.VerifyCheckedState();

            OnPropertyChanged(nameof(IsChecked));
        }

        void VerifyCheckedState()
        {
            bool? state = null;

            for (int i = 0; i < Children.Count; ++i)
            {
                bool? current = Children[i].IsChecked;
                if (i == 0)
                {
                    state = current;
                }
                else if (state != current)
                {
                    state = null;
                    break;
                }
            }

            SetIsChecked(state, false, true);
        }

        #endregion

        #endregion

        void Initialize()
        {
            foreach (TreeViewModel child in Children)
            {
                child._parent = this;
                child.Initialize();
            }
        }

        public static List<TreeViewModel> SetTree(string topLevelName)
        {
            List<TreeViewModel> treeView = new List<TreeViewModel>();
            TreeViewModel tv = new TreeViewModel(topLevelName);

            treeView.Add(tv);

            //Perform recursive method to build treeview 
  
            #region Test Data
            //Doing this below for this example, you should do it dynamically 
            //***************************************************
            TreeViewModel tvChild4 = new TreeViewModel("Child4");

            tv.Children.Add(new TreeViewModel("Child1"));
            tv.Children.Add(new TreeViewModel("Child2"));
            tv.Children.Add(new TreeViewModel("Child3"));
            tv.Children.Add(tvChild4);
            tv.Children.Add(new TreeViewModel("Child5"));

            TreeViewModel grtGrdChild2 = (new TreeViewModel("GrandChild4-2"));

            tvChild4.Children.Add(new TreeViewModel("GrandChild4-1"));
            tvChild4.Children.Add(grtGrdChild2);
            tvChild4.Children.Add(new TreeViewModel("GrandChild4-3"));

            grtGrdChild2.Children.Add(new TreeViewModel("GreatGrandChild4-2-1"));
            //***************************************************
            #endregion

            tv.Initialize();

            return treeView;
        }

        void GetTree()
        {
            //select = recursive method to check each tree view item for selection (if required)
            MessageBox.Show("Im here");
            TreeViewModel root = (TreeViewModel)FrMainWindow.treeView1.Items[0];
            MessageBox.Show(root.Name);
            //List<string> selected = new List<string>(TreeViewModel.GetTree());
            //return selected;
            
            //***********************************************************
            //From your window capture selected your treeview control like:   TreeViewModel root = (TreeViewModel)TreeViewControl.Items[0];
            //                                                                List<string> selected = new List<string>(TreeViewModel.GetTree());
            //***********************************************************
        }

        public ICommand Check => new RelayCommand(GetTree);
    }
}
