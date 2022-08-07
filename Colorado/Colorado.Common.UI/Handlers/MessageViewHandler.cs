using Colorado.Common.UI.Enumerations;
using Colorado.Common.UI.Properties;
using Colorado.Common.UI.ViewModels.WindowViewModels;
using Colorado.Common.UI.Windows;
using System;

namespace Colorado.Common.UI.Handlers
{
    public static class MessageViewHandler
    {
        #region Public logic

        public static void ShowInformationMessage(string title, string message)
        {
            new MessageView(title, message, MessageViewType.Information).ShowDialog();
        }

        public static void ShowWarningMessage(string title, string message)
        {
            new MessageView(title, message, MessageViewType.Warning).ShowDialog();
        }

        public static void ShowExceptionMessage(string title, string message, Exception exception)
        {
            new MessageView(title, message + Environment.NewLine + string.Format(Resources.SmthWentWrong, exception.Message), MessageViewType.Warning).ShowDialog();
        }

        public static void ShowExceptionMessage(string title, string message)
        {
            new MessageView(title, message, MessageViewType.Warning).ShowDialog();
        }

        public static void ShowExceptionMessage(string title, Exception exception)
        {
            new MessageView(title, string.Format(Resources.SmthWentWrong, exception.Message), MessageViewType.Warning).ShowDialog();
        }

        public static MessageViewResult ShowConfirmationMessage(string title, string message)
        {
            var messageView = new MessageView(title, message, MessageViewType.Confirmation);
            messageView.ShowDialog();
            return ((MessageViewModel)messageView.DataContext).MessageViewResult;
        }

        #endregion Public logic
    }
}