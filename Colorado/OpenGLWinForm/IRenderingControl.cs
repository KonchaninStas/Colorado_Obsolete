using Colorado.GeometryDataStructures.Colors;
using Colorado.OpenGL.Managers;
using Colorado.OpenGL.Managers.Materials;
using Colorado.OpenGLWinForm.Rendering;
using Colorado.OpenGLWinForm.View;

namespace Colorado.OpenGLWinForm
{
    public interface IRenderingControl
    {
        void RefreshView();

        LightsManager LightsManager { get; }

        DefaultMaterialsManager DefaultMaterialsManager { get; }

        GeometryRenderer GeometryRenderer { get; }

        RGB BackgroundColor { get; }

        Camera ViewCamera { get; }
    }
}
