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
        private readonly Material material;
        private Point lightPosition => viewCamera.Origin;

        private Vector lightDirection => viewCamera.ViewDirection;
        #endregion Private fields

        #region Constructor

        public LightsManager(ViewCamera viewCamera)
        {
            this.viewCamera = viewCamera;
            material = new Material();

        }

        #endregion Constructor

        #region Public logic

        public void CreateHeadLight()
        {
            OpenGLWrapper.EnableCapability(OpenGLCapability.Lighting);
            OpenGLWrapper.SetLightModel(LightModel.LocalViewer, true);
            OpenGLWrapper.EnableLight(LightType.Light0);
            
            OpenGLWrapper.SetLightModel(LightModel.Ambient, new RGB(0.2, 0.2, 0.2).ToFloat4Array());
            //OpenGLWrapper.SetLightParameter(LightType.Light0, LightParameter.ConstantAttenuation, 1.0f);
            //OpenGLWrapper.SetLightParameter(LightType.Light0, LightParameter.LinearAttenuation, 0.0f);
            //OpenGLWrapper.SetLightParameter(LightType.Light0, LightParameter.QuadraticAttenuation, 0.0f);

            OpenGLWrapper.SetLightParameter(LightType.Light0, LightColorType.Ambient, new RGB(0, 0, 0).ToFloat4Array());
            OpenGLWrapper.SetLightParameter(LightType.Light0, LightColorType.Diffuse, new RGB(1, 1, 1).ToFloat4Array());
            OpenGLWrapper.SetLightParameter(LightType.Light0, LightColorType.Specular, new RGB(1, 1, 1).ToFloat4Array());

            //OpenGLWrapper.SetLightParameter(LightType.Light0, LightParameter.Position, new Point(0,1,1).FloatArray);
        }

        public RGB GetLightedColor(Vertex vertex, Vector normal)
        {
            var lightColor = new RGB(1, 0, 0);
            RGB ambient = lightColor * material.Ambient;

            // diffuse 
            Vector norm = normal;
            Vector lightDir = (lightPosition - vertex.Position).UnitVector();
            double diff = Math.Max(norm.DotProduct(lightDir), 0.0);
            RGB diffuse = lightColor * (material.Diffuse * diff);

            // specular
            Vector viewDir = (viewCamera.Origin - vertex.Position).UnitVector();
            Vector reflectDir = Vector.Reflect(lightDir.Inverse, norm);
            double spec = Math.Pow(Math.Max(viewDir.DotProduct(reflectDir), 0.0), material.Shininess);
            RGB specular = lightColor * (material.Specular * spec);

            return ambient + diffuse + specular;

        }

        public RGB GetLightedColor(RGB vertexColor, Vector vertexNormal)
        {
            var lightColor = new RGB(1, 0, 0);
            double ambientStrength = 0.4f;
            RGB ambient = lightColor * ambientStrength;

            double diff = Math.Max(vertexNormal.DotProduct(Vector.XAxis.Inverse), 0.0);
            RGB diffuse = lightColor * diff;
            return (ambient + diffuse) * vertexColor;

        }

        public double[] GetLightedColors(Mesh mesh)
        {
            var RGBColorsValuesArray = new DynamicArray<double>(mesh.VerticesColors.Length * 3);

            for (int i = 0; i < mesh.VerticesCount; i++)
            {
                RGB color = GetLightedColor(mesh.VerticesColors[i], mesh.VerticesNormals[i]);
                RGBColorsValuesArray.Add(color.Red);
                RGBColorsValuesArray.Add(color.Green);
                RGBColorsValuesArray.Add(color.Blue);
            }
            return RGBColorsValuesArray.Array;
        }

        #endregion Public logic
    }
}
