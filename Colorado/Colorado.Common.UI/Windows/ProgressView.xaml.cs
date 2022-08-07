using Colorado.Common.UI.ViewModels.WindowViewModels;
using System;
using System.Windows;

namespace Colorado.Common.UI.Windows
{
    /// <summary>
    /// Interaction logic for ProgressView.xaml
    /// </summary>
    public partial class ProgressView : Window
    {
        public ProgressView(string title)
        {
            InitializeComponent();
            DataContext = new ProgressViewModel(title)
            {
                CloseAction = new Action(Close)
            };
        }
    }
}
