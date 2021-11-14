using Colorado.Common.UI.ViewModels.Base;
using Colorado.OpenGLWinForm;
using Colorado.Viewer.Controls.Views.AppearanceTab;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Colorado.Viewer.Controls.ViewModels.Tabs.AppearanceTab
{
    public class AppearanceTabUserControlViewModel : ViewModelBase
    {
        private readonly IRenderingControl renderingControl;

        public AppearanceTabUserControlViewModel(IRenderingControl renderingControl)
        {
            this.renderingControl = renderingControl;
            Lights = renderingControl.LightsManager.Lights.Select(l => new LightSettingsUserControl(l));
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

        public IEnumerable<LightSettingsUserControl> Lights { get; }
    }
}
