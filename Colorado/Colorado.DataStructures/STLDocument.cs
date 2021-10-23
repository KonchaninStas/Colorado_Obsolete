using Colorado.Common.Exceptions;
using Colorado.DataStructures.STL;
using Colorado.DataStructures.STL.Readers;
using Colorado.DataStructures.STL.Writers;
using Colorado.GeometryDataStructures.GeometryStructures.Geometry3D;
using Colorado.GeometryDataStructures.Primitives;
using System.IO;
using System.Linq;

namespace Colorado.DataStructures
{
    public class STLDocument : Document
    {
        public STLDocument(string pathToStlFile)
        {
            var obj = GetMeshFromStlDocument(@"C:\\cube1000.stl");


            AddGeometryObject(obj);

            //for (int i = 0; i < 50; i++)
            //{
            //    for (int y = 0; y < 100; y++)
            //    {
            //        var xValue = obj.BoundingBox.Diagonal * i;
            //        var yValue = obj.BoundingBox.Diagonal * y;
            //        AddGeometryObject(obj.GetTransformed(new Transform(new Vector(xValue, yValue, 0))));
            //    }

            //}

            //STLASCIIFileWriter.Write(this.Geometries.Cast<Mesh>().SelectMany(m => m.Triangles), );
        }

        private Mesh GetMeshFromStlDocument(string pathToStlFile)
        {
            if (!File.Exists(pathToStlFile))
            {
                throw new FileNotFoundException();
            }
            else if (!STLFileUtil.IsStlFile(pathToStlFile))
            {
                throw new FileNotSupportedException();
            }
            else
            {
                STLFileType fileType = STLFileUtil.GetStlFileType(pathToStlFile);

                return fileType == STLFileType.ASCII ?
                    STLASCIIFileReader.Read(pathToStlFile) : STLBinaryFileReader.Read(pathToStlFile);
            }
        }
    }
}
