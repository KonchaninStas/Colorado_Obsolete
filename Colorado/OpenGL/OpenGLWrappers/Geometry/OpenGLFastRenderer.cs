using Colorado.Common.Helpers;
using Colorado.GeometryDataStructures.GeometryStructures.Geometry2D;
using Colorado.GeometryDataStructures.GeometryStructures.Geometry3D;
using Colorado.OpenGL.Enumerations.Geometry;
using Colorado.OpenGL.Interfaces;
using Colorado.OpenGL.OpenGLLibrariesAPI;
using System;
using System.Linq;

namespace Colorado.OpenGL.OpenGLWrappers.Geometry
{
    public static unsafe class OpenGLFastRenderer
    {
        public static void DrawMeshRgb(Mesh mesh, ILightsManager lightsManager)
        {
            double[] verticesValues = mesh.VerticesValuesArray;
            double[] normalsValues = mesh.NormalsValuesArray;
            double[] colorsValues = lightsManager.GetLightedColors(mesh);

            fixed (double* cachedPoints = verticesValues)
            fixed (double* cachedNormals = normalsValues)
            fixed (double* cachedColors = colorsValues)
                DrawTrianglesRgb(mesh.VerticesCount, cachedPoints, cachedNormals, cachedColors);
        }


        public static void DrawLinesRgb(Line[] lines)
        {
            double[] verticesValues = ArrayHelper.MergeArrays(lines.Select(l => l.VerticesValuesArray));
            double[] colorsValues = ArrayHelper.MergeArrays(lines.Select(l => l.RGBColorsValuesArray));

            fixed (double* cachedPoints = verticesValues)
            fixed (double* cachedColors = colorsValues)
                DrawLinesRgb(lines.Length * 2, cachedPoints, cachedColors);
        }

        public static void DrawLinesRgb(int nVertices, double* vertices, double* colors)
        {
            EnableClientState(ArrayType.Vertex);
            EnableClientState(ArrayType.Color);
            VertexPointer((IntPtr)vertices);
            ColorPointerRGB((IntPtr)colors);
            DrawArrays(GeometryType.Line, nVertices);
            DisableClientState(ArrayType.Vertex);
            DisableClientState(ArrayType.Color);
        }

        public static void DrawTrianglesRgb(int nVertices, double* vertices, double* normals, double* colors)
        {
            EnableClientState(ArrayType.Vertex);
            EnableClientState(ArrayType.Normal);
            EnableClientState(ArrayType.Color);

            VertexPointer((IntPtr)vertices);
            NormalPointer((IntPtr)normals);
            ColorPointerRGB((IntPtr)colors);

            DrawArrays(GeometryType.Triangle, nVertices);

            DisableClientState(ArrayType.Vertex);
            DisableClientState(ArrayType.Normal);
            DisableClientState(ArrayType.Color);
        }

        private static void EnableClientState(ArrayType arrayType)
        {
            OpenGLGeometryAPI.EnableClientState((int)arrayType);
        }

        private static void DisableClientState(ArrayType arrayType)
        {
            OpenGLGeometryAPI.DisableClientState((int)arrayType);
        }

        private const int vertexSize = 3;
        private const int normalSize = 3;

        private static void VertexPointer(IntPtr firstVertexPointer)
        {
            VertexPointer(DataType.Double, 0, firstVertexPointer);
        }

        private static void NormalPointer(IntPtr firstNormalPointer)
        {
            NormalPointer(DataType.Double, 0, firstNormalPointer);
        }

        private static void VertexPointer(DataType dataType, int stride, IntPtr firstVertexPointer)
        {
            OpenGLGeometryAPI.VertexPointer(vertexSize, (int)dataType, stride, firstVertexPointer);
        }

        private static void NormalPointer(DataType dataType, int stride, IntPtr firstVertexPointer)
        {
            OpenGLGeometryAPI.NormalPointer((int)dataType, stride, firstVertexPointer);
        }

        private static void ColorPointerRGB(IntPtr firstColorComponentPointer)
        {
            ColorPointer(ColorComponentSize.WithoutAlpha, DataType.Double, 0, firstColorComponentPointer);
        }

        private static void ColorPointerRGBA(IntPtr firstColorComponentPointer)
        {
            ColorPointer(ColorComponentSize.WithAlpha, DataType.Double, 4, firstColorComponentPointer);
        }


        private static void ColorPointer(ColorComponentSize colorComponentSize, DataType dataType, int stride, IntPtr firstColorComponentPointer)
        {
            OpenGLGeometryAPI.ColorPointer((int)colorComponentSize, (int)dataType, stride, firstColorComponentPointer);
        }

        private static void DrawArrays(GeometryType geometryType, int count)
        {
            OpenGLGeometryAPI.DrawArrays((int)geometryType, 0, count);
        }
    }
}
