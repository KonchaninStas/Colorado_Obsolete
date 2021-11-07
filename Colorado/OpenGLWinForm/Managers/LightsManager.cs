using Colorado.Common.Collections;
using Colorado.GeometryDataStructures.Colors;
using Colorado.GeometryDataStructures.GeometryStructures.Geometry3D;
using Colorado.GeometryDataStructures.Primitives;
using Colorado.OpenGL.Enumerations;
using Colorado.OpenGL.Interfaces;
using Colorado.OpenGL.OpenGLWrappers;
using Colorado.OpenGLWinForm.Enumerations;
using Colorado.OpenGLWinForm.RenderingControlStructures;
using System;

namespace Colorado.OpenGLWinForm.Managers
{
    internal class LightsManager : ILightsManager
    {
        #region Private fields

        private readonly ViewCamera viewCamera;

        #endregion Private fields

        #region Constructor

        public LightsManager(ViewCamera viewCamera)
        {
            this.viewCamera = viewCamera;
        }

        #endregion Constructor

        #region Public logic

        public void CreateHeadLight()
        {
            OpenGLWrapper.EnableLight(LightType.Light0);
            OpenGLWrapper.SetLightParameter(LightType.Light0, LightParameter.ConstantAttenuation, 1.0f);
            OpenGLWrapper.SetLightParameter(LightType.Light0, LightParameter.LinearAttenuation, 0.0f);
            OpenGLWrapper.SetLightParameter(LightType.Light0, LightParameter.QuadraticAttenuation, 0.0f);
            var color = new float[]
            {
                0.5f,
                0.2f,
                0.2f,
                1.0f
            };

            OpenGLWrapper.SetLightParameter(LightType.Light0, LightColorType.Ambient, color);
            color[0] = 1.0f;
            color[1] = 1.0f;
            color[2] = 1.0f;
            OpenGLWrapper.SetLightParameter(LightType.Light0, LightColorType.Diffuse, color);
            OpenGLWrapper.SetLightParameter(LightType.Light0, LightColorType.Specular, color);

            var position = new float[] { 0, 0, 0, 1 };
            if (viewCamera.CameraType == CameraType.Orthographic)
            {
                Point cameraOrigin = viewCamera.Origin;
                Vector cameraDir = viewCamera.ViewDirection;
                cameraOrigin = cameraOrigin - cameraDir * 1E10;
                position[0] = (float)cameraOrigin.X;
                position[1] = (float)cameraOrigin.Y;
                position[2] = (float)cameraOrigin.Z;
                position[3] = 1.0f;
            }
            else
            {
                Point cameraOrigin = viewCamera.Origin;
                position[0] = (float)cameraOrigin.X;
                position[1] = (float)cameraOrigin.Y;
                position[2] = (float)cameraOrigin.Z;
                position[3] = 1.0f;
            }
            OpenGLWrapper.SetLightParameter(LightType.Light0, LightParameter.Position, 0f);
        }

        public RGBA GetLightedColor(RGBA vertexColor, Vector vertexNormal)
        {
            var lightColor = new RGBA(1, 0, 0);
            double ambientStrength = 0.4f;
            RGBA ambient = lightColor * ambientStrength;

            double diff = Math.Max(vertexNormal.DotProduct(Vector.XAxis.Inverse), 0.0);
            RGBA diffuse = lightColor * diff;
            return (ambient + diffuse) * vertexColor;
        }

        public double[] GetLightedColors(Mesh mesh)
        {
            var RGBColorsValuesArray = new DynamicArray<double>(mesh.VerticesColors.Length * 3);

            for (int i = 0; i < mesh.VerticesColors.Length; i++)
            {
                RGBA color = GetLightedColor(mesh.VerticesColors[i], mesh.VerticesNormals[i]);
                RGBColorsValuesArray.Add(color.Red);
                RGBColorsValuesArray.Add(color.Green);
                RGBColorsValuesArray.Add(color.Blue);
            }
            return RGBColorsValuesArray.Array;
        }

        #endregion Public logic
    }
}
