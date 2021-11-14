using Colorado.Common.UI.ViewModels.Base;
using Colorado.OpenGL.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.Viewer.Controls.ViewModels.Tabs.AppearanceTab
{
    public class LightSettingsUserControlViewModel : ViewModelBase
    {
        private readonly Light light;

        public LightSettingsUserControlViewModel(Light light)
        {
            this.light = light;

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
    }
}
