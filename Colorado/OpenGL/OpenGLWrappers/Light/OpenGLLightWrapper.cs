using Colorado.GeometryDataStructures.Colors;
using Colorado.GeometryDataStructures.Primitives;
using Colorado.OpenGL.Enumerations;
using Colorado.OpenGL.OpenGLLibrariesAPI;
using Colorado.OpenGL.OpenGLLibrariesAPI.Light;

namespace Colorado.OpenGL.OpenGLWrappers.Light
{
    public static class OpenGLLightWrapper
    {
        #region Public logic

        public static void EnableLight(LightType lightType)
        {
            OpenGLAPI.Enable((int)lightType);
        }

        public static void DisableLight(LightType lightType)
        {
            OpenGLAPI.Disable((int)lightType);
        }

        /// <summary>
        ///  (0.0, 0.0, 0.0, 1.0)
        /// </summary>
        /// <param name="faceSide"></param>
        /// <param name="materialColorType"></param>
        /// <param name="value"></param>
        public static void SetAmbientColor(LightType lightType, RGB ambientColor)
        {
            SetLightParameter(lightType, LightColorType.Ambient, ambientColor);
        }

        /// <summary>
        /// (1.0, 1.0, 1.0, 1.0) for LIGHT0 and (0.0, 0.0, 0.0, 1.0) for other ones.
        /// </summary>
        /// <param name="faceSide"></param>
        /// <param name="diffuseColor"></param>
        public static void SetDiffuseColor(LightType lightType, RGB diffuseColor)
        {
            SetLightParameter(lightType, LightColorType.Diffuse, diffuseColor);
        }

        /// <summary>
        /// (1.0, 1.0, 1.0, 1.0) for LIGHT0 and (0.0, 0.0, 0.0, 1.0) for other ones.
        /// </summary>
        /// <param name="faceSide"></param>
        /// <param name="specularColor"></param>
        public static void SetSpecularColor(LightType lightType, RGB specularColor)
        {
            SetLightParameter(lightType, LightColorType.Specular, specularColor);
        }

        /// <summary>
        /// (0.0, 0.0, 1.0, 0.0)
        /// </summary>
        /// <param name="position"></param>
        public static void SetLightPosition(LightType lightType, Point position)
        {
            OpenGLLightAPI.Lightfv((int)lightType, (int)LightParameter.Position,
                position.FloatArray);
        }

        public static void SetLigthDirection(LightType lightType, Vector lightDirection)
        {
            OpenGLLightAPI.Lightfv((int)lightType, (int)LightParameter.Position,
                lightDirection.FloatArray);
        }

        public static void SetLigthParameter(LightType lightType, LightParameter lightParameter, double value)
        {
            OpenGLLightAPI.Lightf((int)lightType, (int)lightParameter, (float)value);
        }

        #endregion Public logic

        #region Private logic

        private static void SetLightParameter(LightType lightType, LightColorType lightColorType,
            RGB color)
        {
            OpenGLLightAPI.Lightfv((int)lightType, (int)lightColorType, color.ToFloat4Array());
        }

        #endregion Private logic
    }
}
