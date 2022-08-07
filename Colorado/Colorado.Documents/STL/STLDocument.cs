using Colorado.Common.Exceptions;
using Colorado.Common.ProgressTracking;
using Colorado.Common.Tools.Keyboard;
using Colorado.Common.UI.Handlers;
using Colorado.Documents.Properties;
using Colorado.Documents.STL.Readers;
using Colorado.Documents.STL.Utils;
using Colorado.Documents.STL.Writers;
using Colorado.GeometryDataStructures.GeometryStructures.Geometry3D;
using System;
using System.IO;
using System.Linq;

namespace Colorado.Documents.STL
{
    public class STLDocument : Document
    {
        #region Constants

        private const string filter = "Stl Files(*.stl)|*.stl;";

        #endregion Constants

        #region Constructor

        static STLDocument()
        {
            DocumentsManager.RegisterFilter(filter);
        }

        public STLDocument(KeyboardToolsManager keyboardToolsManager, string pathToStlFile): base(keyboardToolsManager)
        {
            PathToFile = pathToStlFile;
            Name = Path.GetFileNameWithoutExtension(pathToStlFile);
        }

        #endregion Constructor

        #region Properties

        public override string Name { get; }

        public override string PathToFile { get; }

        public override string Filter => filter;

        #endregion Properties

        #region Public logic

        public override void ImportGeometry()
        {
            ProgressTracker.Instance.StartIndeterminate(Resources.ProcessingTriangles);
            AddMesh(GetMeshFromStlDocument(PathToFile));
        }

        public override void Save(string fileName)
        {
            STLASCIIFileWriter.Write(Meshes.SelectMany(m => m.Triangles.Select(t=>t.ApplyTransform(
                DocumentTransformation.ActiveTransform))), fileName);
        }

        #endregion Public logic

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
