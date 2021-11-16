using Colorado.Common.UI.ViewModels.Base;
using Colorado.GeometryDataStructures.Colors;
using Colorado.OpenGL.Structures;
using Colorado.OpenGLWinForm;
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
        private readonly IRenderingControl renderingControl;
        private readonly Light light;

        public LightSettingsUserControlViewModel(IRenderingControl renderingControl, Light light)
        {
            this.renderingControl = renderingControl;
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
                renderingControl.RefreshView();
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
                renderingControl.RefreshView();
            }
        }

        public Color AmbientColor
        {
            get
            {
                return light.Ambient.ToColor();
            }
            set
            {
                light.Ambient = new RGB(value.R, value.G, value.B);
                OnPropertyChanged(nameof(AmbientColor));
                renderingControl.RefreshView();
            }
        }

        public Color DiffuseColor
        {
            get
            {
                return light.Diffuse.ToColor();
            }
            set
            {
                light.Diffuse = new RGB(value.R, value.G, value.B);
                OnPropertyChanged(nameof(DiffuseColor));
                renderingControl.RefreshView();
            }
        }

        public Color SpecularColor
        {
            get
            {
                return light.Specular.ToColor();
            }
            set
            {
                light.Specular = new RGB(value.R, value.G, value.B);
                OnPropertyChanged(nameof(SpecularColor));
                renderingControl.RefreshView();
            }
        }
    }
}
