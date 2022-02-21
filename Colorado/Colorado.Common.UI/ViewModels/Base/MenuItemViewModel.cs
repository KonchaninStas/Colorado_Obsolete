﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Colorado.Common.UI.ViewModels.Base
{
    public class MenuItemViewModel : ViewModelBase
    {
        #region Constructors

        public MenuItemViewModel(string displayName)
        {
            DisplayName = displayName;
            MenuItems = new ObservableCollection<MenuItemViewModel>();
        }

        public MenuItemViewModel(string displayName, IEnumerable<MenuItemViewModel> children) : this(displayName)
        {

            MenuItems = new ObservableCollection<MenuItemViewModel>(children);
        }

        public MenuItemViewModel(string displayName, ICommand command)
            : this(displayName)
        {
            Command = command;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Returns the menu item content.
        /// </summary>
        public string DisplayName { get; }

        /// <summary>
        /// Returns the command which will be called if the current menu item clicked.
        /// </summary>
        public ICommand Command { get; }

        /// <summary>
        /// Returns a collection of the child menu items.
        /// </summary>
        public ObservableCollection<MenuItemViewModel> MenuItems { get; }

        #endregion Properties

        #region Public logic

        /// <summary>
        /// Updates binding values for the current view model.
        /// </summary>
        public override void UpdateView()
        {
            OnPropertyChanged(nameof(DisplayName));
            OnPropertyChanged(nameof(Command));
            OnPropertyChanged(nameof(MenuItems));
        }

        #endregion Public logic
    }
}
