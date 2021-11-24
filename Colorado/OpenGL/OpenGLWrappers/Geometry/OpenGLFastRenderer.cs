using Colorado.Common.Helpers;
using Colorado.GeometryDataStructures.GeometryStructures.Geometry2D;
using Colorado.GeometryDataStructures.GeometryStructures.Geometry3D;
using Colorado.OpenGL.Enumerations.Geometry;
using Colorado.OpenGL.Managers;
using Colorado.OpenGL.Managers.Materials;
using Colorado.OpenGL.OpenGLLibrariesAPI.Geometry;
using Colorado.OpenGL.OpenGLWrappers.View;
using System;
using System.Linq;

namespace Colorado.OpenGL.OpenGLWrappers.Geometry
{
    public static unsafe class OpenGLFastRenderer
    {
        #region Constants

        private const int vertexSize = 3;
        private const int normalSize = 3;

        #endregion Constants

        #region Public logic

        public static void DrawMesh(Mesh mesh, GeometryDataStructures.Colors.Material globalMaterial)
        {
            MaterialsManager.SetMaterial(globalMaterial ?? mesh.Material);
            double[] verticesValues = mesh.VerticesValuesArray;
            double[] normalsValues = mesh.NormalsValuesArray;
            byte[] colorsValues = mesh.VerticesColorsValuesArray;

            OpenGLMatrixOperationWrapper.ApplyTransform(mesh.Transform, () =>
            {
                fixed (double* cachedPoints = verticesValues)
                fixed (double* cachedNormals = normalsValues)
                fixed (byte* cachedColors = colorsValues)
                {
                    DrawTrianglesRgb(mesh.VerticesCount, cachedPoints, cachedNormals, cachedColors);
                }
            });
        }

        public static void DrawLinesRgb(Line[] lines)
        {
            double[] verticesValues = ArrayHelper.MergeArrays(lines.Select(l => l.VerticesValuesArray));
            byte[] colorsValues = ArrayHelper.MergeArrays(lines.Select(l => l.RGBColorsValuesArray));

            fixed (double* cachedPoints = verticesValues)
            fixed (byte* cachedColors = colorsValues)
            {
                DrawLines(lines.Length * 2, cachedPoints, cachedColors);
            }
        }

        #endregion Public logic

        #region Private logic

        private static void DrawLines(int nVertices, double* vertices, byte* colors)
        {
            EnableClientState(ArrayType.Vertex);
            EnableClientState(ArrayType.Color);
            VertexPointer((IntPtr)vertices);
            ColorPointerRGB((IntPtr)colors);
            DrawArrays(OpenGLGeometryType.Line, nVertices);
            DisableClientState(ArrayType.Vertex);
            DisableClientState(ArrayType.Color);
        }

        public static void DrawTrianglesRgb(int nVertices, double* vertices, double* normals, byte* colors)
        {
            EnableClientState(ArrayType.Vertex);
            EnableClientState(ArrayType.Normal);
            EnableClientState(ArrayType.Color);

            VertexPointer((IntPtr)vertices);
            NormalPointer((IntPtr)normals);
            ColorPointerRGB((IntPtr)colors);

            DrawArrays(OpenGLGeometryType.Triangle, nVertices);

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
            ColorPointer(ColorComponentSize.WithoutAlpha, DataType.UnsignedByte, 0, firstColorComponentPointer);
        }

        private static void ColorPointerRGBA(IntPtr firstColorComponentPointer)
        {
            ColorPointer(ColorComponentSize.WithAlpha, DataType.Float, 4, firstColorComponentPointer);
        }


        private static void ColorPointer(ColorComponentSize colorComponentSize, DataType dataType, int stride, IntPtr firstColorComponentPointer)
        {
            OpenGLGeometryAPI.ColorPointer((int)colorComponentSize, (int)dataType, stride, firstColorComponentPointer);
        }

        private static void DrawArrays(OpenGLGeometryType geometryType, int count)
        {
            OpenGLGeometryAPI.DrawArrays((int)geometryType, 0, count);
        }

        #endregion Private logic
    }
}
