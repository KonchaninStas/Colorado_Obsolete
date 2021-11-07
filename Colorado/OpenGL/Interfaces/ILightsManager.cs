using Colorado.GeometryDataStructures.Colors;
using Colorado.GeometryDataStructures.GeometryStructures.Geometry3D;
using Colorado.GeometryDataStructures.Primitives;

namespace Colorado.OpenGL.Interfaces
{
    public interface ILightsManager
    {
        RGBA GetLightedColor(RGBA vertexColor, Vector vertexNormal);

        double[] GetLightedColors(Mesh mesh);
    }
}
