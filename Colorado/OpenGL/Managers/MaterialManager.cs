using Colorado.GeometryDataStructures.Colors;
using Colorado.OpenGL.Enumerations;
using Colorado.OpenGL.OpenGLWrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.OpenGL.Managers
{
    internal static class MaterialManager
    {
        public static void SetMaterial(Material material)
        {
            OpenGLWrapper.EnableCapability(OpenGLCapability.Lighting);
            OpenGLWrapper.EnableLight(LightType.Light0);
            OpenGLWrapper.SetLightModel(LightModel.TwoSide, true);
            float[] color = new float[4];
            color[3] = 1.0f - material.Transparency;
            OpenGLWrapper.SetMaterial(FaceSide.Front, MaterialColorType.Shininess, material.Shininess * 128);
            OpenGLWrapper.SetMaterial(FaceSide.Back, MaterialColorType.Shininess, material.Shininess * 128);
            color[0] = material.Diffuse.Red;
            color[1] = material.Diffuse.Green;
            color[2] = material.Diffuse.Blue;
            OpenGLWrapper.SetMaterial(FaceSide.Front, MaterialColorType.Diffuse, color);
            OpenGLWrapper.SetMaterial(FaceSide.Back, MaterialColorType.Diffuse, color);
            color[0] = material.Specular.Red;
            color[1] = material.Specular.Green;
            color[2] = material.Specular.Blue;
            OpenGLWrapper.SetMaterial(FaceSide.Front, MaterialColorType.Specular, color);
            OpenGLWrapper.SetMaterial(FaceSide.Back, MaterialColorType.Diffuse, color);
            color[0] = material.Ambient.Red;
            color[1] = material.Ambient.Green;
            color[2] = material.Ambient.Blue;
            OpenGLWrapper.SetMaterial(FaceSide.Front, MaterialColorType.Emission, color);
            OpenGLWrapper.SetMaterial(FaceSide.Back, MaterialColorType.Diffuse, color);
            //if (((ICwTriTesselation)tesselation).OneSided)
            //{
            //OpenGLWrapper.SetMaterial(FaceSide.Back, MaterialColorType.Shininess, material.Shininess * 128);
            //color[0] = material.Diffuse.Red;
            //color[1] = material.Diffuse.Green;
            //color[2] = material.Diffuse.Blue;
            //gl.Materialfv(gl.BACK, gl.DIFFUSE, color);
            //color[0] = material.SpecularColor.R / 255.0f;
            //color[1] = material.SpecularColor.G / 255.0f;
            //color[2] = material.SpecularColor.B / 255.0f;
            //gl.Materialfv(gl.BACK, gl.SPECULAR, color);
            //color[0] = material.EmmisiveColor.R / 255.0f;
            //color[1] = material.EmmisiveColor.G / 255.0f;
            //color[2] = material.EmmisiveColor.B / 255.0f;
            //gl.Materialfv(gl.BACK, gl.EMISSION, color);
            //}
            //else
            //{
            //OpenGLWrapper.SetMaterial(FaceSide.Back, MaterialColorType.Shininess, 0);
            //color[0] = 0.3f;
            //color[1] = 0.3f;
            //color[2] = 0.3f;
            //OpenGLWrapper.SetMaterial(FaceSide.Back, MaterialColorType.Diffuse, color);
            //color[0] = 0f;
            //color[1] = 0f;
            //color[2] = 0f;
            //OpenGLWrapper.SetMaterial(FaceSide.Back, MaterialColorType.Specular, color);
            //OpenGLWrapper.SetMaterial(FaceSide.Back, MaterialColorType.Emission, color);
            //}
        }
    }
}
