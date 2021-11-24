using Colorado.Common.UI.ViewModels.Controls;
using Colorado.GeometryDataStructures.Colors;
using Colorado.OpenGLWinForm;
using Colorado.Viewer.Controls.ViewModels.Common;
using Colorado.Viewer.Properties;

namespace Colorado.Viewer.Controls.ViewModels.Tabs.RenderingTab
{
    public class RenderingSettingsTabUserControlViewModel : ViewerBaseViewModel
    {
        #region Constructor

        public RenderingSettingsTabUserControlViewModel(IRenderingControl renderingControl) : base(renderingControl)
        {
            BackgroundColorViewModel = new RGBColorPickerUserControlViewModel(Resources.BackgroundColorSettings,
                renderingControl.BackgroundColor, RGB.BackgroundDefaultColor);
            BackgroundColorViewModel.ColorChanged += (s, a) => renderingControl.RefreshView();

            GridColorViewModel = new RGBColorPickerUserControlViewModel(Resources.GridColorSettings,
               renderingControl.GeometryRenderer.GridPlane.Color, RGB.GridDefaultColor);
            GridColorViewModel.ColorChanged += (s, a) => renderingControl.RefreshView();
            DrawLights = false;

            TargetPointColorViewModel = new RGBColorPickerUserControlViewModel(Resources.TargetPointColorSettings,
               renderingControl.GeometryRenderer.TargetPointColor, RGB.TargetPointDefaultColor);
            TargetPointColorViewModel.ColorChanged += (s, a) => renderingControl.RefreshView();
        }

        #endregion Constructor

        #region Properties

        public RGBColorPickerUserControlViewModel BackgroundColorViewModel { get; }

        #region Lights

        public bool DrawLights
        {
            get
            {
                return renderingControl.LightsManager.DrawLights;
            }
            set
            {
                renderingControl.LightsManager.DrawLights = value;
                renderingControl.RefreshView();
            }
        }

        public double LightSourceDrawDiameter
        {
            get
            {
                return renderingControl.LightsManager.LightSourceDrawDiameter;
            }
            set
            {
                renderingControl.LightsManager.LightSourceDrawDiameter = value;
                renderingControl.RefreshView();
            }
        }

        #endregion Lights

        #region Coordinate system

        public bool DrawCoordinateSystem
        {
            get
            {
                return renderingControl.GeometryRenderer.DrawCoordinateSystem;
            }
            set
            {
                renderingControl.GeometryRenderer.DrawCoordinateSystem = value;
                renderingControl.RefreshView();
            }
        }

        public double CoordinateSystemAxisLength
        {
            get
            {
                return renderingControl.GeometryRenderer.CoordinateSystemAxisLength;
            }
            set
            {
                renderingControl.GeometryRenderer.CoordinateSystemAxisLength = value;
                renderingControl.RefreshView();
            }
        }

        #endregion Coordinate system

        #region Grid

        public RGBColorPickerUserControlViewModel GridColorViewModel { get; }

        public bool IsGridVisible
        {
            get
            {
                return renderingControl.GeometryRenderer.GridPlane.Visible;
            }
            set
            {
                renderingControl.GeometryRenderer.GridPlane.Visible = value;
                renderingControl.RefreshView();
            }
        }

        public int GridSpace
        {
            get
            {
                return renderingControl.GeometryRenderer.GridPlane.Space;
            }
            set
            {
                renderingControl.GeometryRenderer.GridPlane.Space = value;
                renderingControl.RefreshView();
            }
        }

        #endregion Grid

        #region Target point

        public bool DrawTargetPoint
        {
            get
            {
                return renderingControl.GeometryRenderer.DrawTargetPoint;
            }
            set
            {
                renderingControl.GeometryRenderer.DrawTargetPoint = value;
                renderingControl.RefreshView();
            }
        }

        public RGBColorPickerUserControlViewModel TargetPointColorViewModel { get; }

        #endregion Target point

        #endregion Properties
    }
}
