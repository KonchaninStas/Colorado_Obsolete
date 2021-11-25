using Colorado.Common.UI.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Colorado.Common.UI.ViewModels.Base
{
    public class BaseTreeItemViewModel : ViewModelBase
    {
        private bool isExpanded;
        private bool isSelected;

        public BaseTreeItemViewModel(string displayName, bool isExpanded)
        {
            MenuItems = new ObservableCollection<MenuItemViewModel>();
            
            this.isExpanded = isExpanded;
            DisplayName = displayName;
            Children = new ObservableCollection<BaseTreeItemViewModel>();
            Children.CollectionChanged += Children_CollectionChangedCallback;
        }

        public BaseTreeItemViewModel(string displayName) : this(displayName, false)
        {
        }

        private void Children_CollectionChangedCallback(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(ExpanderVisibility));
        }

        public Visibility ContextMenuVisible
        {
            get
            {
                return MenuItems.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
            }
        }


        public ObservableCollection<MenuItemViewModel> MenuItems { get; }

        public string DisplayName { get; }

        public ObservableCollection<BaseTreeItemViewModel> Children { get; }

        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                isExpanded = Children.Count == 0 ? false : value;

                OnPropertyChanged(nameof(IsExpanded));
            }
        }

        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                isSelected = value;

                OnPropertyChanged(nameof(IsSelected));
            }
        }

        public Visibility ExpanderVisibility
        {
            get
            {
                return Children.Count > 0 ? Visibility.Visible : Visibility.Hidden;
            }
        }

        public ICommand ExpandCommand
        {
            get { return new CommandHandler<MouseButtonEventArgs>(Expand); }
        }

        private void Expand(MouseButtonEventArgs args)
        {
            IsExpanded = !IsExpanded;
        }
    }
}
