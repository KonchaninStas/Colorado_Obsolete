using Colorado.OpenGLWinForm;
using Colorado.Viewer.Controls.ViewModels.Tabs.ViewTab;
using System.Windows.Controls;

namespace Colorado.Viewer.Controls.Views.Tabs.ViewTab
{
    /// <summary>
    /// Interaction logic for ViewSettingsTab.xaml
    /// </summary>
    public partial class ViewSettingsTabUserControl : UserControl
    {
        public ViewSettingsTabUserControl(IRenderingControl renderingControl)
        {
            DataContext = new ViewSettingsTabUserControlViewModel(renderingControl);
            InitializeComponent();
        }
    }
}
