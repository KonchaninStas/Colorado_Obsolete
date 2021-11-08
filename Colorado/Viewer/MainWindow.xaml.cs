using Colorado.Documents;
using Colorado.Documents.STL;
using Colorado.Viewer.ViewModels;
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
        }
    }
}
