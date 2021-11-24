using Colorado.Common.Exceptions;
using Colorado.Documents.STL.Readers;
using Colorado.Documents.STL.Utils;
using Colorado.GeometryDataStructures.GeometryStructures.Geometry3D;
using System.IO;

namespace Colorado.Documents.STL
{
    public class STLDocument : Document
    {
        #region Constructor

        public STLDocument(string pathToStlFile)
        {
            AddGeometryObject(GetMeshFromStlDocument(pathToStlFile));
        }

        #endregion Constructor

        #region Private logic

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
                    new STLASCIIFileReader(pathToStlFile).Read() : new STLBinaryFileReader(pathToStlFile).Read();
            }
        }

        #endregion Private logic
    }
}
