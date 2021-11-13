using Colorado.Common.UI.ViewModels.Base;
using Colorado.OpenGLWinForm;

namespace Colorado.Viewer.Controls.ViewModels.Tabs.AppearanceTab
{
    public class AppearanceTabUserControlViewModel : ViewModelBase
    {
        private readonly IRenderingControl renderingControl;

        public AppearanceTabUserControlViewModel(IRenderingControl renderingControl)
        {
            this.renderingControl = renderingControl;
        }

        public bool IsLightingEnabled
        {
            get
            {
                return renderingControl.LightsManager.IsLightingEnabled;
            }
            set
            {
                renderingControl.LightsManager.IsLightingEnabled = value;
                renderingControl.RefreshView();
                OnPropertyChanged(nameof(IsLightingEnabled));
            }
        }
    }
}
