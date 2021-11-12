using Colorado.GeometryDataStructures.Colors;
using Colorado.OpenGL.Enumerations;
using Colorado.OpenGL.OpenGLLibrariesAPI.Material;
using System;

namespace Colorado.OpenGL.OpenGLWrappers.Material
{
    public static class OpenGLMaterialWrapper
    {
        #region Public logic

        /// <summary>
        ///  (0.2, 0.2, 0.2, 1.0)
        /// </summary>
        /// <param name="faceSide"></param>
        /// <param name="materialColorType"></param>
        /// <param name="value"></param>
        public static void SetAmbientColor(RGB ambientColor)
        {
            SetMaterialValues(FaceSide.FrontAndBack, MaterialColorType.Ambient, ambientColor);
        }

        /// <summary>
        /// (0.8, 0.8, 0.8, 1.0).
        /// </summary>
        /// <param name="faceSide"></param>
        /// <param name="diffuseColor"></param>
        public static void SetDiffuseColor(RGB diffuseColor)
        {
            SetMaterialValues(FaceSide.FrontAndBack, MaterialColorType.Diffuse, diffuseColor);
        }

        /// <summary>
        /// (0.0, 0.0, 0.0, 1.0)
        /// </summary>
        /// <param name="faceSide"></param>
        /// <param name="specularColor"></param>
        public static void SetSpecularColor(RGB specularColor)
        {
            SetMaterialValues(FaceSide.FrontAndBack, MaterialColorType.Specular, specularColor);
        }

        /// <summary>
        /// 0
        /// </summary>
        /// <param name="faceSide"></param>
        /// <param name="shininessIntensityValue"></param>
        public static void SetShininessIntensity(float shininessIntensityValue)
        {
            if (shininessIntensityValue < 0 || shininessIntensityValue > 128)
            {
                throw new Exception();
            }
            SetMaterialValue(FaceSide.FrontAndBack, MaterialColorType.Shininess, shininessIntensityValue);
        }

        /// <summary>
        ///  (0.0, 0.0, 0.0, 1.0).
        /// </summary>
        /// <param name="faceSide"></param>
        /// <param name="emissionColor"></param>
        public static void SetEmissionColor(RGB emissionColor)
        {
            SetMaterialValues(FaceSide.FrontAndBack, MaterialColorType.Emission, emissionColor);
        }

        /// <summary>
        ///  (0.2, 0.2, 0.2, 1.0)
        /// </summary>
        /// <param name="faceSide"></param>
        /// <param name="materialColorType"></param>
        /// <param name="value"></param>
        public static void SetAmbientColor(FaceSide faceSide, RGB ambientColor)
        {
            SetMaterialValues(faceSide, MaterialColorType.Ambient, ambientColor);
        }

        /// <summary>
        /// (0.8, 0.8, 0.8, 1.0).
        /// </summary>
        /// <param name="faceSide"></param>
        /// <param name="diffuseColor"></param>
        public static void SetDiffuseColor(FaceSide faceSide, RGB diffuseColor)
        {
            SetMaterialValues(faceSide, MaterialColorType.Diffuse, diffuseColor);
        }

        /// <summary>
        /// (0.0, 0.0, 0.0, 1.0)
        /// </summary>
        /// <param name="faceSide"></param>
        /// <param name="specularColor"></param>
        public static void SetSpecularColor(FaceSide faceSide, RGB specularColor)
        {
            SetMaterialValues(faceSide, MaterialColorType.Specular, specularColor);
        }

        /// <summary>
        /// 0
        /// </summary>
        /// <param name="faceSide"></param>
        /// <param name="shininessIntensityValue"></param>
        public static void SetShininessIntensity(FaceSide faceSide, float shininessIntensityValue)
        {
            if (shininessIntensityValue < 0 || shininessIntensityValue > 128)
            {
                throw new Exception();
            }
            SetMaterialValue(faceSide, MaterialColorType.Shininess, shininessIntensityValue);
        }

        /// <summary>
        ///  (0.0, 0.0, 0.0, 1.0).
        /// </summary>
        /// <param name="faceSide"></param>
        /// <param name="emissionColor"></param>
        public static void SetEmissionColor(FaceSide faceSide, RGB emissionColor)
        {
            SetMaterialValues(faceSide, MaterialColorType.Emission, emissionColor);
        }

        #endregion Public logic

        #region Private logic

        private static void SetMaterialValue(FaceSide faceSide,
            MaterialColorType materialColorType, float value)
        {
            OpenGLMaterialAPI.Materialf((int)faceSide, (int)materialColorType, value);
        }

        private static void SetMaterialValues(FaceSide faceSide,
            MaterialColorType materialColorType, RGB color)
        {
            SetMaterialValues(faceSide, materialColorType, color.ToFloat4Array());
        }

        private static void SetMaterialValues(FaceSide faceSide,
            MaterialColorType materialColorType, float[] values)
        {
            OpenGLMaterialAPI.Materialfv((int)faceSide, (int)materialColorType, values);
        }

        #endregion Private logic
    }
}
