using Colorado.Common.UI.ViewModels.Base;
using Colorado.OpenGL.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Colorado.Viewer.Controls.ViewModels.Tabs.AppearanceTab
{
    public class LightSettingsUserControlViewModel : ViewModelBase
    {
        private readonly Light light;

        public LightSettingsUserControlViewModel(Light light)
        {
            this.light = light;
            AmbientColorSettingsViewModel = new ColorSettingsUserControlViewModel(light.Ambient);
            DiffuseColorSettingsViewModel = new ColorSettingsUserControlViewModel(light.Diffuse);
            SpecularColorSettingsViewModel = new ColorSettingsUserControlViewModel(light.Specular);
        }

        public bool IsLightEnabled
        {
            get
            {
                return light.IsEnabled;
            }
            set
            {
                light.IsEnabled = value;
                OnPropertyChanged(nameof(IsLightEnabled));
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

        public ColorSettingsUserControlViewModel AmbientColorSettingsViewModel { get; }

        public ColorSettingsUserControlViewModel DiffuseColorSettingsViewModel { get; }

        public ColorSettingsUserControlViewModel SpecularColorSettingsViewModel { get; }

        public Color SelectedColor
        {
            get
            {
                return Color.FromRgb(0,255,0);
            }
            set
            {
                
                OnPropertyChanged(nameof(SelectedColor));
            }
        }
    }
}
