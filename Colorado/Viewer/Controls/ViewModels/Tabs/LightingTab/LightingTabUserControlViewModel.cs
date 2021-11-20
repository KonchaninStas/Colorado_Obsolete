using Colorado.OpenGLWinForm;
using Colorado.Viewer.Controls.ViewModels.Common;
using Colorado.Viewer.Controls.Views.Tabs.LightingTab;
using System.Collections.Generic;
using System.Linq;

namespace Colorado.Viewer.Controls.ViewModels.Tabs.LightingTab
{
    public class LightingTabUserControlViewModel : ViewerBaseViewModel
    {
        #region Constructor

        public LightingTabUserControlViewModel(IRenderingControl renderingControl) : base(renderingControl)
        {
            LightSettingsUserControls = renderingControl.LightsManager.Lights.Select(
                l => new LightSettingsUserControl(renderingControl, l));
        }

        #endregion Constructor

        #region Properties

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

        public IEnumerable<LightSettingsUserControl> LightSettingsUserControls { get; }

        #endregion Properties
    }
}
