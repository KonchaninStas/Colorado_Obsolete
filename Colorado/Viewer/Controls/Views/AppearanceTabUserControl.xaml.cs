using Colorado.OpenGL.Managers;
using Colorado.OpenGLWinForm;
using Colorado.Viewer.Controls.ViewModels.Tabs;
using System.Windows.Controls;

namespace Colorado.Viewer.Controls.Views
{
    /// <summary>
    /// Interaction logic for AppearanceTabUserControl.xaml
    /// </summary>
    public partial class AppearanceTabUserControl : UserControl
    {
        public AppearanceTabUserControl(IRenderingControl renderingControl)
        {
            InitializeComponent();
            DataContext = new AppearanceTabUserControlViewModel(renderingControl);
        }
    }
}
