using Colorado.GeometryDataStructures.GeometryStructures.Geometry3D;
using Colorado.GeometryDataStructures.Primitives;
using System.Collections.Generic;
using System.IO;

namespace Colorado.Documents.STL.Readers
{
    internal class STLASCIIFileReader
    {
        #region Constants

        private const string solidSearchWord = "solid";
        private const string endsolidSearchWord = "endsolid";

        #endregion Constants

        private readonly string pathToStlFile;
        private readonly List<Triangle> triangles;

        public STLASCIIFileReader(string pathToStlFile)
        {
            this.pathToStlFile = pathToStlFile;
            triangles = new List<Triangle>();
        }

        public Mesh Read()
        {
            using (StreamReader txtReader = new StreamReader(pathToStlFile))
            {
                while (!txtReader.EndOfStream)
                {
                    string lineString = GetNextLine(txtReader);
                    string[] lineData = GetLineData(lineString);

                    if (lineData[0] == solidSearchWord)
                    {
                        while (lineData[0] != endsolidSearchWord)
                        {
                            lineString = GetNextLine(txtReader);
                            lineData = GetLineData(lineString);

                            if (lineData[0] == endsolidSearchWord)
                            {
                                break;
                            }

                            try
                            {
                                Vector normal = GetNormalVector(lineData);

                                txtReader.ReadLine(); // Just skip the OuterLoop line

                                triangles.Add(new Triangle(GetVertex(txtReader), GetVertex(txtReader), GetVertex(txtReader), normal));
                            }
                            catch
                            {
                                break;
                            }

                            txtReader.ReadLine(); // Just skip the endloop
                            txtReader.ReadLine(); // Just skip the endfacet

                            lineString = GetNextLine(txtReader);
                            lineData = GetLineData(lineString);
                        }
                    }
                }

                return new Mesh(triangles);
            }
        }

        private Vertex GetVertex(StreamReader txtReader)
        {
            var lineString = GetNextLine(txtReader);
            /* reduce spaces until string has proper format for split */
            while (lineString.IndexOf("  ") != -1)
            {
                lineString = lineString.Replace("  ", " ");
            }
            string[] lineData = GetLineData(lineString);

            return new Vertex(new Point(double.Parse(lineData[1]), double.Parse(lineData[2]), double.Parse(lineData[3])));
        }

        private Vector GetNormalVector(string[] lineData)
        {
            return new Vector(double.Parse(lineData[2]), double.Parse(lineData[3]), double.Parse(lineData[4]));
        }

        private string[] GetLineData(string lineString)
        {
            return lineString.Split(' ');
        }

        private string GetNextLine(StreamReader txtReader)
        {
            string nextLine = string.Empty;
            do
            {
                nextLine = txtReader.ReadLine().Trim();
            }
            while (string.IsNullOrEmpty(nextLine));


            return nextLine;
        }
    }
}
