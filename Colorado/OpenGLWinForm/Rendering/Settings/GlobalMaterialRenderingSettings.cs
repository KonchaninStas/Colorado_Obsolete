using Colorado.GeometryDataStructures.Colors;

namespace Colorado.OpenGLWinForm.Rendering.Settings
{
    public class GlobalMaterialRenderingSettings
    {
        public GlobalMaterialRenderingSettings()
        {
            GlobalMaterial = Material.Default;
        }

        public bool UseGlobalMaterial { get; set; }

        public Material GlobalMaterial { get; set; }
    }
}
