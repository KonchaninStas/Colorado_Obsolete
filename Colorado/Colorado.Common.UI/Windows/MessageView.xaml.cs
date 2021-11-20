using Colorado.Common.UI.Enumerations;
using Colorado.Common.UI.ViewModels.WindowViewModels;
using System;
using System.Windows;

namespace Colorado.Common.UI.Windows
{
    /// <summary>
    /// Interaction logic for MessageView.xaml
    /// </summary>
    public partial class MessageView : Window
    {
        public MessageView(string title, string message, MessageViewType messageViewType)
        {
            InitializeComponent();
            DataContext = new MessageViewModel(title, message,
                messageViewType)
            {
                CloseAction = new Action(Close)
            };
        }
    }
}
