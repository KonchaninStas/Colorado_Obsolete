using Colorado.OpenGL.Enumerations;
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
            lightTypeToLightMap = new Dictionary<LightType, Light>();
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
            set
            {
                lightTypeToLightMap[lightType] = value;
            }
        }

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
            OpenGLLightWrapper.SetAmbientColor(light.LightType, light.Ambient);
            OpenGLLightWrapper.SetDiffuseColor(light.LightType, light.Diffuse);
            OpenGLLightWrapper.SetSpecularColor(light.LightType, light.Specular);
            OpenGLLightWrapper.SetLigthDirection(light.LightType, light.Direction);
        }

        #endregion Private logic
    }
}
