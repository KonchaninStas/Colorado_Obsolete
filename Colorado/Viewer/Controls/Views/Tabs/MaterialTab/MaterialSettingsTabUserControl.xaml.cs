using Colorado.OpenGLWinForm;
using Colorado.Viewer.Controls.ViewModels.Tabs.MaterialTab;
using System.Windows.Controls;

namespace Colorado.Viewer.Controls.Views.Tabs.MaterialTab
{
    /// <summary>
    /// Interaction logic for MaterialSettingsTabUserControl.xaml
    /// </summary>
    public partial class MaterialSettingsTabUserControl : UserControl
    {
        public MaterialSettingsTabUserControl(IRenderingControl renderingControl)
        {
            DataContext = new MaterialSettingsTabViewModel(renderingControl);
            InitializeComponent();  
        }
    }
}
