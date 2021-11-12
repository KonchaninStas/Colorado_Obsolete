using Colorado.GeometryDataStructures.Colors;
using Colorado.GeometryDataStructures.GeometryStructures.BaseGeometryStructures;
using Colorado.GeometryDataStructures.GeometryStructures.Geometry2D;
using Colorado.GeometryDataStructures.GeometryStructures.Geometry3D;
using Colorado.GeometryDataStructures.Primitives;
using Colorado.OpenGL.Enumerations.Geometry;
using Colorado.OpenGL.Interfaces;
using Colorado.OpenGL.OpenGLLibrariesAPI;
using Colorado.OpenGL.OpenGLWrappers.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.OpenGL.OpenGLWrappers
{
    public static class OpenGLGeometryWrapper
    {
        public static void DrawGeometryObject(GeometryObject geometryObject, ILightsManager lightsManager)
        {
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
                        OpenGLFastRenderer.DrawMeshRgb(geometryObject as Mesh, lightsManager);

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

        public static void DrawLine(Line line, RGB startPoint, RGB endPoint)
        {
            DrawingGeometryWrapper(GeometryType.Line, () =>
            {
                SetActiveColorWithoutAlpha(startPoint);
                AppendVertex(line.StartPoint);
                SetActiveColorWithoutAlpha(endPoint);
                AppendVertex(line.EndPoint);
            });
        }

        public static void DrawLine(Line line, RGB color)
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

        public static void DrawPoint(Point point, RGB color, float pointSize)
        {
            SetPointSize(pointSize);
            DrawingGeometryWrapper(GeometryType.Point, () =>
                 {
                     SetActiveColorWithoutAlpha(color);
                     AppendVertex(point);
                 });
            SetDefaultPointSize();
        }

        public static void DrawMeshes(IEnumerable<Mesh> meshes)
        {
            DrawingGeometryWrapper(GeometryType.Triangle, () =>
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

        private static void DrawMesh(Mesh mesh)
        {
            //OpenGLFastRenderer.DrawMeshRgb(mesh);
            DrawingGeometryWrapper(GeometryType.Triangle, () =>
            {
                SetActiveColorWithoutAlpha(RGB.RedColor);
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

        private static void SetPointSize(float pointSize)
        {
            OpenGLGeometryAPI.glPointSize(pointSize);
        }

        private static void SetDefaultPointSize()
        {
            OpenGLGeometryAPI.glPointSize(1f);
        }

        private static void SetActiveColorWithoutAlpha(RGB color)
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