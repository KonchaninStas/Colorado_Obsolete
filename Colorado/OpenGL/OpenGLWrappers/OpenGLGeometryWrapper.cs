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
            DrawingGeometryWrapper(GeometryType.Quads, () =>
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
                default:
                    {
                        throw new ArgumentException();
                    }
            }
        }

        public static void DrawLine(Line line)
        {
            DrawingGeometryWrapper(GeometryType.Lines, () =>
                 {
                     AppendVertex(line.StartPoint);
                     AppendVertex(line.EndPoint);
                 });
        }

        public static void DrawLine(Line line, RGBA color)
        {
            DrawingGeometryWrapper(GeometryType.Lines, () =>
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
            DrawingGeometryWrapper(GeometryType.Points, () =>
                 {
                     SetActiveColorWithoutAlpha(color);
                     AppendVertex(point);
                 });
            SetDefaultPointSize();
        }

        private static void AppendVertex(Point point)
        {
            OpenGLGeometryAPI.glVertex3d(point.X, point.Y, point.Z);
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