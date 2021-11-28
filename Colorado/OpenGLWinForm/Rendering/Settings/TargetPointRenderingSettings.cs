using Colorado.GeometryDataStructures.Colors;

namespace Colorado.OpenGLWinForm.Rendering.Settings
{
    public class TargetPointRenderingSettings
    {
        public TargetPointRenderingSettings()
        {
            TargetPointColor = RGB.TargetPointDefaultColor;
        }

        public bool DrawTargetPoint { get; set; }

        public RGB TargetPointColor { get; }
    }
}
