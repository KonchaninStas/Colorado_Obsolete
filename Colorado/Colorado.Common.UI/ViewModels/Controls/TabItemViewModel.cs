using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Colorado.Common.UI.ViewModels.Controls
{
    public class TabItemViewModel
    {
        #region Constructors

        public TabItemViewModel(string header, UserControl content)
        {
            Header = header;
            Content = content;
        }

        public TabItemViewModel(string header, UserControl content, Visibility visibility)
            : this(header, content)
        {
            Visibility = visibility;
        }

        public TabItemViewModel(string header, UserControl content, bool isSelected)
            : this(header, content)
        {
            IsSelected = isSelected;
        }

        #endregion Constructors

        #region Properties

        public string Header { get; }

        public UserControl Content { get; }

        public bool IsSelected { get; }

        public Visibility Visibility { get; set; }

        #endregion Properties
    }
}
