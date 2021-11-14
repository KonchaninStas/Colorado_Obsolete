using Colorado.OpenGL.Structures;
using Colorado.Viewer.Controls.ViewModels.Tabs.AppearanceTab;
using System.Windows.Controls;

namespace Colorado.Viewer.Controls.Views.AppearanceTab
{
    /// <summary>
    /// Interaction logic for LightSettingsUserControl.xaml
    /// </summary>
    public partial class LightSettingsUserControl : UserControl
    {
        public LightSettingsUserControl(Light light)
        {
            DataContext = new LightSettingsUserControlViewModel(light);
            InitializeComponent();
            
        }
    }
}
