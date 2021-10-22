using Colorado.GeometryDataStructures.Colors;
using Colorado.GeometryDataStructures.GeometryStructures.BaseGeometryStructures;
using Colorado.GeometryDataStructures.GeometryStructures.Geometry2D;
using Colorado.GeometryDataStructures.GeometryStructures.Geometry3D;
using Colorado.GeometryDataStructures.Primitives;
using Colorado.OpenGL.Enumerations.Geometry;
using Colorado.OpenGL.OpenGLLibrariesAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.OpenGL.OpenGLWrappers
{
    public static class OpenGLGeometryWrapper
    {
        public static void DrawGeometryObject(GeometryObject geometryObject)
        {
            DrawingGeometryWrapper(GeometryType.Quad, () =>
            {
                AppendVertex(new Point(-1, -1, -10));
                AppendVertex(new Point(1, -1, -10));
                AppendVertex(new Point(1, 1, -10));
                AppendVertex(new Point(-1, 1, -10));
            });
            DrawPoint(Point.ZeroPoint, new RGBA(100, 100, 100), 6);
            switch (geometryObject.GeometryType)
            {
                case GeometryDataStructures.GeometryStructures.Enumerations.GeometryType.Line:
                    {
                        DrawLine(geometryObject as Line);
                        break;
                    }
                case GeometryDataStructures.GeometryStructures.Enumerations.GeometryType.Circle:
                    {
                        DrawCircle(geometryObject as Circle);
                        break;
                    }
                case GeometryDataStructures.GeometryStructures.Enumerations.GeometryType.Sphere:
                    {
                        DrawSphere(geometryObject as Sphere);
                        break;
                    }
                case GeometryDataStructures.GeometryStructures.Enumerations.GeometryType.Mesh:
                    {
                        DrawWireframeMesh(geometryObject as Mesh);
                        break;
                    }
                default:
                    {
                        throw new ArgumentException();
                    }
            }
        }

        public static void DrawLine(Line line)
        {
            DrawingGeometryWrapper(GeometryType.Line, () =>
                 {
                     AppendVertex(line.StartPoint);
                     AppendVertex(line.EndPoint);
                 });
        }

        public static void DrawLine(Line line, RGBA color)
        {
            DrawingGeometryWrapper(GeometryType.Line, () =>
            {
                SetActiveColorWithoutAlpha(color);
                AppendVertex(line.StartPoint);
                AppendVertex(line.EndPoint);
            });
        }

        public static void DrawSphere(Sphere sphere)
        {
            throw new NotImplementedException();
        }

        public static void DrawCircle(Circle circle)
        {
            throw new NotImplementedException();
        }

        public static void DrawPoint(Point point, RGBA color, float pointSize)
        {
            SetPointSize(pointSize);
            DrawingGeometryWrapper(GeometryType.Point, () =>
                 {
                     SetActiveColorWithoutAlpha(color);
                     AppendVertex(point);
                 });
            SetDefaultPointSize();
        }

        private static void DrawMesh(Mesh mesh)
        {
            DrawingGeometryWrapper(GeometryType.Triangle, () =>
            {
                SetActiveColorWithoutAlpha(new RGBA(204, 204, 204));
                foreach (Triangle triangle in mesh.Triangles)
                {
                    AppendNormal(triangle.Normal);
                    AppendVertex(triangle.FirstVertex);
                    AppendVertex(triangle.SecondVertex);
                    AppendVertex(triangle.ThirdVertex);
                }
            });
        }

        private static void DrawWireframeMesh(Mesh mesh)
        {
            foreach (Triangle triangle in mesh.Triangles)
            {
                //DrawingGeometryWrapper(GeometryType.Line, () =>
                //{
                //    SetActiveColorWithoutAlpha(RGBA.RedColor);
                //    AppendVertex(triangle.Center);
                //    AppendVertex(triangle.Center + triangle.Normal * 10);
                //});
                DrawingGeometryWrapper(GeometryType.LineLoop, () =>
            {
                SetActiveColorWithoutAlpha(new RGBA(204, 204, 204));
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

        private static void SetPointSize(float pointSize)
        {
            OpenGLGeometryAPI.glPointSize(pointSize);
        }

        private static void SetDefaultPointSize()
        {
            OpenGLGeometryAPI.glPointSize(1f);
        }

        private static void SetActiveColorWithoutAlpha(RGBA color)
        {
            OpenGLGeometryAPI.glColor3fv(color.ToFloat3Array());
        }

        private static void DrawingGeometryWrapper(GeometryType geometryType, Action drawAction)
        {
            BeginDrawingGeometry(geometryType);
            drawAction.Invoke();
            EndDrawingGeometry();
        }

        private static void BeginDrawingGeometry(GeometryType geometryType)
        {
            OpenGLGeometryAPI.glBegin((uint)geometryType);
        }

        private static void EndDrawingGeometry()
        {
            OpenGLGeometryAPI.glEnd();
        }
    }
}