using Colorado.Common.UI.ViewModels.Base;
using Colorado.Common.UI.ViewModels.Controls;
using Colorado.Framework;
using Colorado.OpenGL.Managers;
using Colorado.OpenGLWinForm;
using Colorado.Viewer.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Colorado.Viewer.Controls.ViewModels.Tabs
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
