using Colorado.GeometryDataStructures.Colors;
using Colorado.OpenGL.Enumerations;
using Colorado.OpenGL.OpenGLLibrariesAPI;
using Colorado.OpenGL.OpenGLLibrariesAPI.Light;

namespace Colorado.OpenGL.OpenGLWrappers.Light
{
    public static class OpenGLGlobalLightWrapper
    {
        public static void EnableLighting()
        {
            OpenGLAPI.Enable((int)OpenGLCapability.Lighting);
        }

        public static void DisableLighting()
        {
            OpenGLAPI.Disable((int)OpenGLCapability.Lighting);
        }

        public static bool IsLightingEnabled()
        {
            return OpenGLAPI.IsEnabled((int)OpenGLCapability.Lighting);
        }

        public static void SetLightModel(LightModel lightModel, bool enable)
        {
            OpenGLGlobalLightAPI.LightModeli((int)lightModel, enable ? 1 : 0);
        }

        /// <summary>
        /// (0.2, 0.2, 0.2,1.0)
        /// </summary>
        /// <param name="ambientColor"></param>
        public static void SetLightModelAmbientColor(RGB ambientColor)
        {
            OpenGLGlobalLightAPI.LightModelfv((int)LightModel.Ambient,
                ambientColor.ToFloat4Array());
        }
    }
}
