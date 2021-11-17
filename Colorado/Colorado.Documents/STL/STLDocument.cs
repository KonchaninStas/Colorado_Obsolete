using Colorado.Common.Exceptions;
using Colorado.Documents.STL.Readers;
using Colorado.Documents.STL.Utils;
using Colorado.GeometryDataStructures.GeometryStructures.Geometry3D;
using Colorado.GeometryDataStructures.Primitives;
using System.IO;

namespace Colorado.Documents.STL
{
    public class STLDocument : Document
    {
        public STLDocument(string pathToStlFile)
        {
            AddGeometryObject(GetMeshFromStlDocument(pathToStlFile));

          
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
