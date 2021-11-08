using Colorado.GeometryDataStructures.Colors;
using Colorado.GeometryDataStructures.GeometryStructures.Geometry3D;
using Colorado.GeometryDataStructures.Primitives;

namespace Colorado.OpenGL.Interfaces
{
    public interface ILightsManager
    {
        RGB GetLightedColor(RGB vertexColor, Vector vertexNormal);

        double[] GetLightedColors(Mesh mesh);
    }
}
