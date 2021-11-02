using Colorado.Common.Helpers;
using Colorado.GeometryDataStructures.Colors;
using Colorado.GeometryDataStructures.GeometryStructures.Geometry2D;
using Colorado.GeometryDataStructures.GeometryStructures.Geometry3D;
using Colorado.OpenGL.Enumerations.Geometry;
using Colorado.OpenGL.OpenGLLibrariesAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.OpenGL.OpenGLWrappers.Geometry
{
    public static unsafe class OpenGLFastRenderer
    {
        public static void DrawMeshRgb(Mesh mesh)
        {
            double[] verticesValues = mesh.VerticesValuesArray;
            byte[] colorsValues = mesh.RGBColorsValuesArray;

            fixed (double* cachedPoints = verticesValues)
            fixed (byte* cachedColors = colorsValues)
                DrawTrianglesRgb(mesh.VerticesCount, cachedPoints, cachedColors);
        }


        public static void DrawLinesRgb(Line[] lines)
        {
            double[] verticesValues = ArrayHelper.MergeArrays(lines.Select(l => l.VerticesValuesArray));
            byte[] colorsValues = ArrayHelper.MergeArrays(lines.Select(l => l.RGBColorsValuesArray));

            fixed (double* cachedPoints = verticesValues)
            fixed (byte* cachedColors = colorsValues)
                DrawLinesRgb(lines.Length * 2, cachedPoints, cachedColors);
        }

        public static void DrawLinesRgb(int nVertices, double* vertices, byte* colors)
        {
            EnableClientState(ArrayType.Vertex);
            EnableClientState(ArrayType.Color);
            VertexPointer((IntPtr)vertices);
            ColorPointerRGB((IntPtr)colors);
            DrawArrays(GeometryType.Line, nVertices);
            DisableClientState(ArrayType.Vertex);
            DisableClientState(ArrayType.Color);
        }

        public static void DrawTrianglesRgb(int nVertices, double* vertices, byte* colors)
        {
            EnableClientState(ArrayType.Vertex);
            EnableClientState(ArrayType.Color);
            VertexPointer((IntPtr)vertices);
            ColorPointerRGB((IntPtr)colors);
            DrawArrays(GeometryType.Triangle, nVertices);
            DisableClientState(ArrayType.Vertex);
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

        private const int vectexSize = 3;

        private static void VertexPointer(IntPtr firstVertexPointer)
        {
            VertexPointer(DataType.Double, 0, firstVertexPointer);
        }

        private static void VertexPointer(DataType dataType, int stride, IntPtr firstVertexPointer)
        {
            OpenGLGeometryAPI.VertexPointer(vectexSize, (int)dataType, stride, firstVertexPointer);
        }

        private static void ColorPointerRGB(IntPtr firstColorComponentPointer)
        {
            ColorPointer(ColorComponentSize.WithoutAlpha, DataType.UnsignedByte, 0, firstColorComponentPointer);
        }

        private static void ColorPointerRGBA(IntPtr firstColorComponentPointer)
        {
            ColorPointer(ColorComponentSize.WithAlpha, DataType.UnsignedByte, 4, firstColorComponentPointer);
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
