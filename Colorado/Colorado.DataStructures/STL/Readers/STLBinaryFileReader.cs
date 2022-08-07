using Colorado.Common.Exceptions;
using Colorado.GeometryDataStructures.GeometryStructures.Geometry3D;
using Colorado.GeometryDataStructures.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.DataStructures.STL.Readers
{
    internal static class STLBinaryFileReader
    {
        internal static Mesh Read(string pathToStlFile)
        {
            try
            {
                var triangles = new List<Triangle>();
                int numOfMesh = 0;
                int i = 0;
                int byteIndex = 0;
                byte[] fileBytes = File.ReadAllBytes(pathToStlFile);

                byte[] temp = new byte[4];

                /* 80 bytes title + 4 byte num of triangles + 50 bytes (1 of triangular mesh)  */
                if (fileBytes.Length > 120)
                {
                    temp[0] = fileBytes[80];
                    temp[1] = fileBytes[81];
                    temp[2] = fileBytes[82];
                    temp[3] = fileBytes[83];

                    numOfMesh = BitConverter.ToInt32(temp, 0);
                    byteIndex = 84;

                    for (i = 0; i < numOfMesh; i++)
                    {
                        triangles.Add(new Triangle(GetVertex(ref byteIndex, fileBytes), GetVertex(ref byteIndex, fileBytes),
                            GetVertex(ref byteIndex, fileBytes), GetNormalVector(ref byteIndex, fileBytes)));

                        byteIndex += 2; // Attribute byte count
                    }
                }
                else
                {
                    // nitentionally left blank
                }
                return new Mesh(triangles);
            }
            catch (Exception ex)
            {
                throw new StlFileIsInvalidException(ex);
            }
        }

        private static Vector GetNormalVector(ref int byteIndex, byte[] fileBytes)
        {
            return GetPoint(ref byteIndex, fileBytes).ToVector();
        }

        private static Vertex GetVertex(ref int byteIndex, byte[] fileBytes)
        {
            return new Vertex(GetPoint(ref byteIndex, fileBytes));
        }

        private static Point GetPoint(ref int byteIndex, byte[] fileBytes)
        {
            double vertexX = BitConverter.ToSingle(new byte[] { fileBytes[byteIndex], fileBytes[byteIndex + 1],
                fileBytes[byteIndex + 2], fileBytes[byteIndex + 3] }, 0);
            byteIndex += 4;
            double vertexY = BitConverter.ToSingle(new byte[] { fileBytes[byteIndex], fileBytes[byteIndex + 1],
                fileBytes[byteIndex + 2], fileBytes[byteIndex + 3] }, 0);
            byteIndex += 4;
            double vertexZ = BitConverter.ToSingle(new byte[] { fileBytes[byteIndex], fileBytes[byteIndex + 1],
                fileBytes[byteIndex + 2], fileBytes[byteIndex + 3] }, 0);
            byteIndex += 4;

            return new Point(vertexX, vertexY, vertexZ);
        }
    }
}
