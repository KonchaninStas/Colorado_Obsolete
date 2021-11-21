using Colorado.OpenGLWinForm;
using Colorado.Viewer.Controls.ViewModels.Tabs.Rendering;
using System.Windows.Controls;

namespace Colorado.Viewer.Controls.Views.Tabs.Rendering
{
    /// <summary>
    /// Interaction logic for RenderingSettingsTabUserControl.xaml
    /// </summary>
    public partial class RenderingSettingsTabUserControl : UserControl
    {
        public RenderingSettingsTabUserControl(IRenderingControl renderingControl)
        {
            DataContext = new RenderingSettingsTabUserControlViewModel(renderingControl);
            InitializeComponent();
        }
    }
}
