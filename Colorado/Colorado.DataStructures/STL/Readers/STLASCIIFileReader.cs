using Colorado.Common.Exceptions;
using Colorado.GeometryDataStructures.GeometryStructures.Geometry3D;
using Colorado.GeometryDataStructures.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Colorado.DataStructures.STL.Readers
{
    internal static class STLASCIIFileReader
    {
        #region Constants

        private const string normalSearchWord = "normal";
        private const string vertexSearchWord = "vertex";

        #endregion Constants

        #region Private fields

        private static readonly Regex regex;

        #endregion Private fields

        #region Constructors

        static STLASCIIFileReader()
        {
            regex = new Regex(@"[+-]?\d+(\.\d+)?");
        }

        #endregion Constructors

        #region Internal logic

        public static Mesh Read(string pathToStlFile)
        {
            try
            {
                var triangles = new List<Triangle>();
                using (var sr = new StreamReader(pathToStlFile))
                {
                    string line;
                    Vector normal = null;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Contains(normalSearchWord))
                        {
                            normal = GetVectorFromLine(line);
                        }
                        else if (line.Contains(vertexSearchWord))
                        {
                            Vertex firstVertex = GetVertexFromLine(line);
                            Vertex secondVertex = GetVertexFromLine(sr.ReadLine());
                            Vertex thirdVertex = GetVertexFromLine(sr.ReadLine());
                            triangles.Add(new Triangle(firstVertex, secondVertex, thirdVertex, normal));
                        }
                    }
                }

                return new Mesh(triangles);
            }
            catch (Exception ex)
            {
                throw new StlFileIsInvalidException(ex);
            }
        }

        #endregion Internal logic

        #region Private logic

        private static Vertex GetVertexFromLine(string line)
        {
            return new Vertex(GetPointFromLine(line));
        }

        private static Vector GetVectorFromLine(string line)
        {
            return GetPointFromLine(line).ToVector();
        }

        private static Point GetPointFromLine(string line)
        {
            MatchCollection matches = regex.Matches(line);
            return new Point(double.Parse(matches[0].Value), double.Parse(matches[1].Value), double.Parse(matches[2].Value));
        }

        #endregion Private logic
    }
}
