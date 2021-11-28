using Colorado.GeometryDataStructures.Colors;
using Colorado.GeometryDataStructures.GeometryStructures.BaseGeometryStructures;
using Colorado.GeometryDataStructures.GeometryStructures.Geometry2D;
using Colorado.GeometryDataStructures.GeometryStructures.Geometry3D;
using Colorado.GeometryDataStructures.Primitives;
using Colorado.OpenGL.Enumerations.Geometry;
using Colorado.OpenGL.OpenGLLibrariesAPI.Geometry;
using System;
using System.Collections.Generic;

namespace Colorado.OpenGL.OpenGLWrappers.Geometry
{
    public static class OpenGLGeometryWrapper
    {
        #region Public logic

        public static void DrawLine(Line line)
        {
            DrawingGeometryWrapper(OpenGLGeometryType.Line, () =>
            {
                AppendVertex(line.StartPoint);
                AppendVertex(line.EndPoint);
            });
        }

        public static void DrawLine(Line line, RGB startPoint, RGB endPoint)
        {
            DrawingGeometryWrapper(OpenGLGeometryType.Line, () =>
            {
                SetActiveColorWithoutAlpha(startPoint);
                AppendVertex(line.StartPoint);
                SetActiveColorWithoutAlpha(endPoint);
                AppendVertex(line.EndPoint);
            });
        }

        public static void DrawLine(Line line, RGB color, float lineWidth)
        {
            SetLineWidth(lineWidth);
            DrawingGeometryWrapper(OpenGLGeometryType.Line, () =>
            {
                SetActiveColorWithoutAlpha(color);

                AppendVertex(line.StartPoint);
                AppendVertex(line.EndPoint);
            });

            ResetToDefaultLineWidth();
        }

        public static void DrawPoint(Point point, RGB color, float pointSize)
        {
            SetPointSize(pointSize);
            DrawingGeometryWrapper(OpenGLGeometryType.Point, () =>
            {
                SetActiveColorWithoutAlpha(color);
                AppendVertex(point);
            });
            SetDefaultPointSize();
        }

        public static void DrawMeshes(IEnumerable<Mesh> meshes)
        {
            DrawingGeometryWrapper(OpenGLGeometryType.Triangle, () =>
            {
                foreach (Mesh mesh in meshes)
                {
                    SetActiveColorWithoutAlpha(RGB.RedColor);
                    foreach (Triangle triangle in mesh.Triangles)
                    {
                        AppendNormal(triangle.Normal);
                        AppendVertex(triangle.FirstVertex);
                        AppendVertex(triangle.SecondVertex);
                        AppendVertex(triangle.ThirdVertex);
                    }
                }
            });
        }

        public static void DrawMeshes(IList<Mesh> meshes)
        {
            for (int i = 0; i < meshes.Count; i++)
            {
                //glBindVertexArray(meshes[i].VAO);
                //glDrawElementsInstanced(GL_TRIANGLES, meshes[i].indices.size(), GL_UNSIGNED_INT, 0, amount);
                //glBindVertexArray(0);

            }
        }

        public static void ResetToDefaultLineWidth()
        {
            OpenGLGeometryAPI.LineWidth(1);
        }

        public static void SetLineWidth(float lineWidth)
        {
            OpenGLGeometryAPI.LineWidth(lineWidth);
        }

        public static void SetPointSize(float pointSize)
        {
            OpenGLGeometryAPI.glPointSize(pointSize);
        }

        public static void SetDefaultPointSize()
        {
            OpenGLGeometryAPI.glPointSize(1f);
        }

        #endregion Public logic

        #region Private logic

        private static void DrawWireframeMesh(Mesh mesh)
        {
            foreach (Triangle triangle in mesh.Triangles)
            {
                DrawingGeometryWrapper(OpenGLGeometryType.Line, () =>
                {
                    SetActiveColorWithoutAlpha(RGB.RedColor);
                    AppendVertex(triangle.Center);
                    AppendVertex(triangle.Center + triangle.Normal * 10);
                });
                DrawingGeometryWrapper(OpenGLGeometryType.LineLoop, () =>
                {
                    SetActiveColorWithoutAlpha(new RGB(204, 204, 204));
                    AppendVertex(triangle.FirstVertex);
                    AppendVertex(triangle.SecondVertex);
                    AppendVertex(triangle.ThirdVertex);

                });
            }
        }

        private static void AppendNormal(Vector normal)
        {
            OpenGLGeometryAPI.glNormal3d(normal.X, normal.Y, normal.Z);
        }


        private static void AppendVertex(Point point)
        {
            OpenGLGeometryAPI.glVertex3d(point.X, point.Y, point.Z);
        }

        private static void AppendVertex(Vertex vertex)
        {
            OpenGLGeometryAPI.glVertex3d(vertex.Position.X, vertex.Position.Y, vertex.Position.Z);
        }

        private static void SetActiveColorWithoutAlpha(RGB color)
        {
            OpenGLGeometryAPI.glColor3fv(color.ToFloat3Array());
        }

        private static void DrawingGeometryWrapper(OpenGLGeometryType geometryType, Action drawAction)
        {
            BeginDrawingGeometry(geometryType);
            drawAction.Invoke();
            EndDrawingGeometry();
        }

        private static void BeginDrawingGeometry(OpenGLGeometryType geometryType)
        {
            OpenGLGeometryAPI.glBegin((uint)geometryType);
        }

        private static void EndDrawingGeometry()
        {
            OpenGLGeometryAPI.glEnd();
        }

        #endregion Private logic
    }
}