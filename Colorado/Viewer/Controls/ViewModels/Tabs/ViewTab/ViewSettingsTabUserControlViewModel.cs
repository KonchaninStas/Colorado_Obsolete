using Colorado.Common.UI.Commands;
using Colorado.OpenGLWinForm;
using Colorado.Viewer.Controls.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Colorado.Viewer.Controls.ViewModels.Tabs.ViewTab
{
    public class ViewSettingsTabUserControlViewModel : ViewerBaseViewModel
    {
        public ViewSettingsTabUserControlViewModel(IRenderingControl renderingControl) : base(renderingControl)
        {
            renderingControl.ViewCamera.SettingsChanged += (s, args) => OnPropertyChanged(nameof(FieldOfView));
        }

        public bool IsPerspectiveViewChecked
        {
            get
            {
                return renderingControl.ViewCamera.CameraType == OpenGLWinForm.Enumerations.CameraType.Perspective;
            }
            set
            {
                renderingControl.ViewCamera.CameraType = OpenGLWinForm.Enumerations.CameraType.Perspective;
                renderingControl.RefreshView();
                OnPropertyChanged(nameof(IsPerspectiveViewChecked));
                OnPropertyChanged(nameof(IsOrthographicViewChecked));
                OnPropertyChanged(nameof(FieldOfViewSettingsVisible));
            }
        }

        public bool IsOrthographicViewChecked
        {
            get
            {
                return renderingControl.ViewCamera.CameraType == OpenGLWinForm.Enumerations.CameraType.Orthographic;
            }
            set
            {
                renderingControl.ViewCamera.CameraType = OpenGLWinForm.Enumerations.CameraType.Orthographic;
                renderingControl.RefreshView();
                OnPropertyChanged(nameof(IsPerspectiveViewChecked));
                OnPropertyChanged(nameof(IsOrthographicViewChecked));
                OnPropertyChanged(nameof(FieldOfViewSettingsVisible));
            }
        }

        public Visibility FieldOfViewSettingsVisible
        {
            get
            {
                return IsPerspectiveViewChecked ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public double FieldOfView
        {
            get
            {
                return renderingControl.ViewCamera.VerticalFieldOfViewInDegrees;
            }
            set
            {
                renderingControl.ViewCamera.VerticalFieldOfViewInDegrees = value; ;
                OnPropertyChanged(nameof(FieldOfView));
                renderingControl.RefreshView();
            }
        }

        public ICommand RestoreView
        {
            get
            {
                return new CommandHandler(() =>
                {
                    renderingControl.ViewCamera.ResetToDefault();
                    renderingControl.RefreshView();
                });
            }
        }
    }
}
