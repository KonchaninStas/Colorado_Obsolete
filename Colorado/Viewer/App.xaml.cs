using Colorado.Common.UI.Handlers;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace Colorado.Viewer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            MessageViewHandler.ShowExceptionMessage(Common.UI.Properties.Resources.ViewerTitle,
                Viewer.Properties.Resources.UnhandledExceptionOccured);
        }

        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs args)
        {
            MessageViewHandler.ShowExceptionMessage(Common.UI.Properties.Resources.ViewerTitle, args.Exception);
            args.Handled = true;
        }
    }
}
