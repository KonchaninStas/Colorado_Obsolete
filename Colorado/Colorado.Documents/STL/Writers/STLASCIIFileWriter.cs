using Colorado.Common.ProgressTracking;
using Colorado.Documents.Properties;
using Colorado.GeometryDataStructures.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Colorado.Documents.STL.Writers
{
    public static class STLASCIIFileWriter
    {
        #region Constants

        private const string startOfFile = "solid";
        private const string endOfFile = "endsolid";
        private const string startLoop = "outer loop";
        private const string endLoop = "endloop";
        private const string vertex = "vertex";
        private const string endFacet = "endfacet";
        private const string startFacet = "facet normal";

        #endregion Constants

        #region Internal logic

        public static void Write(IEnumerable<Triangle> triangles, string pathToFile)
        {
            try
            {
                ProgressTracker.Instance.Init(triangles.Count());
                using (var sw = new StreamWriter(pathToFile, false))
                {
                    sw.WriteLine(startOfFile);
                    foreach (Triangle triangle in triangles)
                    {
                        sw.WriteLine($"{startFacet} {triangle.Normal.X} {triangle.Normal.Y} {triangle.Normal.Z}");
                        sw.WriteLine(startLoop);
                        WriteVertex(sw, triangle.FirstVertex.Position);
                        WriteVertex(sw, triangle.SecondVertex.Position);
                        WriteVertex(sw, triangle.ThirdVertex.Position);
                        sw.WriteLine();
                        sw.WriteLine(endLoop);
                        sw.WriteLine(endFacet);
                        ProgressTracker.Instance.NextStep(Resources.SavingTriangles);
                    }
                    sw.WriteLine(endOfFile);
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion Internal logic

        #region Private logic

        private static void WriteVertex(StreamWriter sw, Point vertexOfTriangle)
        {
            sw.WriteLine($"{vertex} {vertexOfTriangle.X} {vertexOfTriangle.Y} {vertexOfTriangle.Z}");
        }

        #endregion Private logic
    }
}
