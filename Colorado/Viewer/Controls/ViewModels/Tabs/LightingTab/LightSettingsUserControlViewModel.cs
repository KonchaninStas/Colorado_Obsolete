using Colorado.Common.UI.ViewModels.Controls;
using Colorado.OpenGL.Structures;
using Colorado.OpenGLWinForm;
using Colorado.Viewer.Controls.ViewModels.Common;
using Colorado.Viewer.Properties;

namespace Colorado.Viewer.Controls.ViewModels.Tabs.LightingTab
{
    public class LightSettingsUserControlViewModel : ViewerBaseViewModel
    {
        #region Private fields

        private readonly Light light;

        #endregion Private fields

        #region Constructor

        public LightSettingsUserControlViewModel(IRenderingControl renderingControl, Light light) : base(renderingControl)
        {
            this.light = light;
            AmbientColorViewModel = new RGBColorPickerUserControlViewModel(Resources.LightAmbientColor, light.Ambient);
            AmbientColorViewModel.ColorChanged += (s, a) => renderingControl.RefreshView();

            DiffuseColorViewModel = new RGBColorPickerUserControlViewModel(Resources.LightDiffuseColor, light.Diffuse);
            DiffuseColorViewModel.ColorChanged += (s, a) => renderingControl.RefreshView();

            SpecularColorViewModel = new RGBColorPickerUserControlViewModel(Resources.LightSpecularColor, light.Specular);
            SpecularColorViewModel.ColorChanged += (s, a) => renderingControl.RefreshView();
        }

        #endregion Constructor

        #region Properties

        public bool IsLightEnabled
        {
            get
            {
                return light.IsEnabled;
            }
            set
            {
                if (value)
                {
                    renderingControl.LightsManager.EnableLight(light.LightType);
                }
                else
                {
                    renderingControl.LightsManager.DisableLight(light.LightType);
                }
                OnPropertyChanged(nameof(IsLightEnabled));
                renderingControl.RefreshView();
            }
        }

        public string Header => light.ToString();

        public double AzimuthInDegrees
        {
            get
            {
                return light.AzimuthAngleInDegrees;
            }
            set
            {
                light.AzimuthAngleInDegrees = value;
                OnPropertyChanged(nameof(AzimuthInDegrees));
            }
        }

        public double AltitudeInDegrees
        {
            get
            {
                return light.AltitudeAngleInDegrees;
            }
            set
            {
                light.AltitudeAngleInDegrees = value;
                OnPropertyChanged(nameof(AltitudeInDegrees));
            }
        }

        public RGBColorPickerUserControlViewModel AmbientColorViewModel { get; }

        public RGBColorPickerUserControlViewModel DiffuseColorViewModel { get; }

        public RGBColorPickerUserControlViewModel SpecularColorViewModel { get; }

        #endregion Properties
    }
}
