using Colorado.Common.UI.ViewModels.Controls;
using Colorado.OpenGLWinForm;
using Colorado.Viewer.Controls.ViewModels.Common;
using Colorado.Viewer.Properties;

namespace Colorado.Viewer.Controls.ViewModels.Tabs.Rendering
{
    public class RenderingSettingsTabUserControlViewModel : ViewerBaseViewModel
    {
        #region Constructor

        public RenderingSettingsTabUserControlViewModel(IRenderingControl renderingControl) : base(renderingControl)
        {
            BackgroundColorViewModel = new RGBColorPickerUserControlViewModel(Resources.BackgroundColorSettings,
                renderingControl.BackgroundColor);
            BackgroundColorViewModel.ColorChanged += (s, a) => renderingControl.RefreshView();
        }

        #endregion Constructor

        #region Properties

        public RGBColorPickerUserControlViewModel BackgroundColorViewModel { get; }

        public bool DrawCoordinateSystem { get; set; }

        #endregion Properties
    }
}
