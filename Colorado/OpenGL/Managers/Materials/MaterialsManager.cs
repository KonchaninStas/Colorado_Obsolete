using Colorado.GeometryDataStructures.Colors;
using Colorado.OpenGL.Enumerations;
using Colorado.OpenGL.OpenGLWrappers.Material;

namespace Colorado.OpenGL.Managers.Materials
{
    internal static class MaterialsManager
    {
        public static void SetMaterial(Material material)
        {
            OpenGLMaterialWrapper.SetAmbientColor(FaceSide.FrontAndBack, material.Ambient);
            OpenGLMaterialWrapper.SetDiffuseColor(FaceSide.FrontAndBack, material.Diffuse);
            OpenGLMaterialWrapper.SetSpecularColor(FaceSide.FrontAndBack, material.Specular);
            OpenGLMaterialWrapper.SetShininessIntensity(FaceSide.FrontAndBack, material.ShininessRadius);
            OpenGLMaterialWrapper.SetEmissionColor(FaceSide.FrontAndBack, material.Emission);
        }
    }
}
