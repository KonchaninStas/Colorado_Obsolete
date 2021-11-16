using Colorado.OpenGL.Structures;
using Colorado.OpenGLWinForm;
using Colorado.Viewer.Controls.ViewModels.Tabs.AppearanceTab;
using System.Windows.Controls;

namespace Colorado.Viewer.Controls.Views.AppearanceTab
{
    /// <summary>
    /// Interaction logic for LightSettingsUserControl.xaml
    /// </summary>
    public partial class LightSettingsUserControl : UserControl
    {
        public LightSettingsUserControl(IRenderingControl renderingControl, Light light)
        {
            var x =  new LightSettingsUserControlViewModel(renderingControl, light);
            DataContext = x;
            InitializeComponent();
            ambientColorPicker.DataContext = x.AmbientColorViewModel;
        }
    }
}
