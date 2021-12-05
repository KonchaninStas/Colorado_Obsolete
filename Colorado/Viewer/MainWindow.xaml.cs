using Colorado.Documents;
using Colorado.Documents.STL;
using Colorado.Viewer.Utilities;
using Colorado.Viewer.ViewModels;
using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;

namespace Colorado.Viewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var mainWindowViewModel = new MainWindowViewModel();
            DataContext = mainWindowViewModel;
            openGLControlWrapper.Children.Add(mainWindowViewModel.OpenGLControl);

            RestoreWindowSettings();
        }

        private void RestoreWindowSettings()
        {
            var userPrefs = new UserPreferences();

            Height = userPrefs.WindowHeight;
            Width = userPrefs.WindowWidth;
            Top = userPrefs.WindowTop;
            Left = userPrefs.WindowLeft;
            WindowState = userPrefs.WindowState;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            var userPrefs = new UserPreferences();

            userPrefs.WindowHeight = this.Height;
            userPrefs.WindowWidth = this.Width;
            userPrefs.WindowTop = this.Top;
            userPrefs.WindowLeft = this.Left;
            userPrefs.WindowState = this.WindowState;

            userPrefs.Save();
        }
    }
}
