using Colorado.OpenGLWinForm;
using Colorado.Viewer.Controls.ViewModels.Common;

namespace Colorado.Viewer.Controls.ViewModels.Tabs.MaterialTab
{
    public class MaterialSettingsTabViewModel : ViewerBaseViewModel
    {
        #region Constructor

        public MaterialSettingsTabViewModel(IRenderingControl renderingControl) : base(renderingControl)
        {
            GlobalMaterialSettingsUserControlViewModel = new MaterialSettingsUserControlViewModel(renderingControl);
        }

        #endregion Constructor

        #region Properties

        public MaterialSettingsUserControlViewModel GlobalMaterialSettingsUserControlViewModel { get; }

        #endregion Properties
    }
}
