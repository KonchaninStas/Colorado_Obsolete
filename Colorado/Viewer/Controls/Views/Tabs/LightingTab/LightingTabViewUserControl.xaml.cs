using Colorado.OpenGLWinForm;
using Colorado.Viewer.Controls.ViewModels.Tabs.LightingTab;
using System.Windows.Controls;

namespace Colorado.Viewer.Controls.Views.Tabs.LightingTab
{
    /// <summary>
    /// Interaction logic for LightingTabViewUserControl.xaml
    /// </summary>
    public partial class LightingTabViewUserControl : UserControl
    {
        public LightingTabViewUserControl(IRenderingControl renderingControl)
        {
            DataContext = new LightingTabUserControlViewModel(renderingControl);
            InitializeComponent();
        }
    }
}
