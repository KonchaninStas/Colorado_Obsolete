using Colorado.Common.UI.Commands;
using Colorado.Common.UI.ViewModels.Base;
using Colorado.GeometryDataStructures.Colors;
using Colorado.OpenGL.Structures;
using Colorado.OpenGLWinForm;
using Colorado.Viewer.Controls.Views.Common;
using Colorado.Viewer.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace Colorado.Viewer.Controls.ViewModels.Tabs.AppearanceTab
{
    public class LightSettingsUserControlViewModel : ViewModelBase
    {
        #region Private fields

        private readonly IRenderingControl renderingControl;
        private readonly Light light;

        #endregion Private fields

        #region Constructor

        public LightSettingsUserControlViewModel(IRenderingControl renderingControl, Light light)
        {
            this.renderingControl = renderingControl;
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

        #region Commands

        public ICommand RefreshViewCommand
        {
            get { return new CommandHandler(() => NewMethod()); }
        }

        private void NewMethod()
        {
            renderingControl.RefreshView();
        }

        #endregion Commands
    }
}
