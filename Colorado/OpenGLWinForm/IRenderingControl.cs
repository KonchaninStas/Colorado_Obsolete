using Colorado.OpenGL.Managers;

namespace Colorado.OpenGLWinForm
{
    public interface IRenderingControl
    {
        void RefreshView();

        LightsManager LightsManager { get; }
    }
}
