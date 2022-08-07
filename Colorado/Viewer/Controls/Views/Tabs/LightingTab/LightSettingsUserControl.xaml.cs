using Colorado.OpenGL.Structures;
using Colorado.OpenGLWinForm;
using Colorado.Viewer.Controls.ViewModels.Tabs.LightingTab;
using System.Windows.Controls;

namespace Colorado.Viewer.Controls.Views.Tabs.LightingTab
{
    /// <summary>
    /// Interaction logic for LightSettingsUserControl.xaml
    /// </summary>
    public partial class LightSettingsUserControl : UserControl
    {
        public LightSettingsUserControl(IRenderingControl renderingControl, Light light)
        {
            DataContext = new LightSettingsUserControlViewModel(renderingControl, light);
            InitializeComponent();
        }
    }
}
