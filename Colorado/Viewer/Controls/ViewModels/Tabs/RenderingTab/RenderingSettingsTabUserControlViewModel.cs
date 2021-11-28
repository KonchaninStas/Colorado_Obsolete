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
            GridColorViewModel = new RGBColorPickerUserControlViewModel(Resources.GridColorSettings,
               renderingControl.GeometryRenderer.GridPlane.Color, RGB.GridDefaultColor);
            GridColorViewModel.ColorChanged += (s, a) => renderingControl.RefreshView();
            DrawLights = false;
        }

        #endregion Constructor

        #region Properties

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
                return renderingControl.GeometryRenderer.CoordinateSystemRenderer.DrawCoordinateSystem;
            }
            set
            {
                renderingControl.GeometryRenderer.CoordinateSystemRenderer.DrawCoordinateSystem = value;
                renderingControl.RefreshView();
            }
        }

        public double CoordinateSystemAxisLength
        {
            get
            {
                return renderingControl.GeometryRenderer.CoordinateSystemRenderer.CoordinateSystemAxisLength;
            }
            set
            {
                renderingControl.GeometryRenderer.CoordinateSystemRenderer.CoordinateSystemAxisLength = value;
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

        #region Mesh rendering settings

        public bool DrawFillTriangles
        {
            get
            {
                return renderingControl.GeometryRenderer.MeshRenderingSettings.DrawFillTriangles;
            }
            set
            {
                renderingControl.GeometryRenderer.MeshRenderingSettings.DrawFillTriangles = value;
                renderingControl.RefreshView();
            }
        }

        public bool EnableWireframeMode
        {
            get
            {
                return renderingControl.GeometryRenderer.MeshRenderingSettings.EnableWireframeMode;
            }
            set
            {
                renderingControl.GeometryRenderer.MeshRenderingSettings.EnableWireframeMode = value;
                renderingControl.RefreshView();
            }
        }

        public bool DrawTrianglesVertices
        {
            get
            {
                return renderingControl.GeometryRenderer.MeshRenderingSettings.DrawTrianglesVertices;
            }
            set
            {
                renderingControl.GeometryRenderer.MeshRenderingSettings.DrawTrianglesVertices = value;
                renderingControl.RefreshView();
            }
        }
        
        #endregion Mesh rendering settings

        #endregion Properties
    }
}
