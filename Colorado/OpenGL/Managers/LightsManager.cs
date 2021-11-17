using Colorado.GeometryDataStructures.Colors;
using Colorado.GeometryDataStructures.Primitives;
using Colorado.OpenGL.Enumerations;
using Colorado.OpenGL.OpenGLWrappers.Geometry;
using Colorado.OpenGL.OpenGLWrappers.Light;
using Colorado.OpenGL.Structures;
using System.Collections.Generic;
using System.Linq;

namespace Colorado.OpenGL.Managers
{
    public class LightsManager
    {
        #region Private fields

        private readonly Dictionary<LightType, Light> lightTypeToLightMap;

        #endregion Private fields

        #region Constructor

        public LightsManager()
        {
            lightTypeToLightMap = new Dictionary<LightType, Light>()
            {
                { LightType.Light0, Light.GetDefault(LightType.Light0) },
                { LightType.Light1, Light.GetDefault(LightType.Light1) },
                { LightType.Light2, Light.GetDefault(LightType.Light2) },
                { LightType.Light3, Light.GetDefault(LightType.Light3) },
                { LightType.Light4, Light.GetDefault(LightType.Light4) },
                { LightType.Light5, Light.GetDefault(LightType.Light5) },
                { LightType.Light6, Light.GetDefault(LightType.Light6) },
                { LightType.Light7, Light.GetDefault(LightType.Light7) },
            };
            EnableLight(LightType.Light0);
            IsLightingEnabled = true;
        }

        #endregion Constructor

        #region Properties

        public Light this[LightType lightType]
        {
            get
            {
                return lightTypeToLightMap.TryGetValue(lightType, out Light light) ? light : null;
            }
        }

        public IEnumerable<Light> Lights => lightTypeToLightMap.Values;

        public bool IsLightingEnabled { get; set; }

        #endregion Properties

        #region Public logic

        public void EnableLight(LightType lightType)
        {
            OpenGLLightWrapper.EnableLight(lightType);
            if (this[lightType] != null)
            {
                this[lightType].IsEnabled = true;
            }
        }

        public void DisableLight(LightType lightType)
        {
            OpenGLLightWrapper.DisableLight(lightType);
            if (this[lightType] != null)
            {
                this[lightType].IsEnabled = false;
            }
        }

        public void ConfigureEnabledLights()
        {
            if (IsLightingEnabled)
            {
                EnableLighting();
                foreach (Light light in lightTypeToLightMap.Values.Where(l => l.IsEnabled))
                {
                    ConfigurateLight(light);
                }
            }
            else
            {
                DisableLighting();
            }
        }

        public void DrawLightsSources()
        {
            if (IsLightingEnabled)
            {
                foreach (Light light in lightTypeToLightMap.Values.Where(l => l.IsDrawn))
                {
                    OpenGLGeometryWrapper.DrawPoint(Point.ZeroPoint + light.Direction * 10, RGB.WhiteColor, 20);
                }
            }
        }

        #endregion Public logic

        #region Private logic

        private void EnableLighting()
        {
            OpenGLGlobalLightWrapper.EnableLighting();
        }

        public void DisableLighting()
        {
            OpenGLGlobalLightWrapper.DisableLighting();
        }

        private void ConfigurateLight(Light light)
        {
            OpenGLLightWrapper.EnableLight(light.LightType);
            OpenGLLightWrapper.SetLigthParameter(light.LightType, LightParameter.ConstantAttenuation, 1);
            OpenGLLightWrapper.SetLigthParameter(light.LightType, LightParameter.LinearAttenuation, 0);
            OpenGLLightWrapper.SetLigthParameter(light.LightType, LightParameter.QuadraticAttenuation, 0);

            OpenGLLightWrapper.SetAmbientColor(light.LightType, light.Ambient);
            OpenGLLightWrapper.SetDiffuseColor(light.LightType, light.Diffuse);
            OpenGLLightWrapper.SetSpecularColor(light.LightType, light.Specular);
            OpenGLLightWrapper.SetLigthDirection(light.LightType, light.Direction);
        }

        #endregion Private logic
    }
}
