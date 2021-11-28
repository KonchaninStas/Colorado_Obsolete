using Colorado.Common.ProgressTracking;
using Colorado.Documents.EventArgs;
using Colorado.Documents.Properties;
using Colorado.GeometryDataStructures.GeometryStructures.Geometry3D;
using Colorado.GeometryDataStructures.Primitives;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Colorado.Documents
{
    public abstract class Document : IEquatable<Document>
    {
        #region Next Id logic

        private static int lastId;

        private static int GetNextId()
        {
            return lastId++;
        }

        #endregion Next Id logic

        #region Private fields

        private readonly IList<Mesh> meshes;

        #endregion Private fields

        #region Constructor

        public Document()
        {
            Id = GetNextId();
            meshes = new List<Mesh>();
            BoundingBox = new BoundingBox();
            Visible = true;
            DocumentTransformation = new DocumentTransformation(BoundingBox);
        }

        #endregion Constructor

        #region Properties

        public int Id { get; }

        public abstract string Name { get; }

        public abstract string PathToFile { get; }

        public bool IsFilePresent => !string.IsNullOrEmpty(PathToFile) && File.Exists(PathToFile);

        public bool IsFolderPresent => !string.IsNullOrEmpty(PathToFile) && Directory.Exists(Path.GetDirectoryName(PathToFile));

        public IEnumerable<Mesh> Meshes => meshes;

        public BoundingBox BoundingBox { get; }

        public bool Visible { get; set; }

        public bool IsEditing { get; private set; }

        public DocumentTransformation DocumentTransformation { get; }

        #endregion Properties

        #region Events

        public event EventHandler<DocumentEditingStartedEventArgs> EditingStarted;

        public event EventHandler<DocumentEditingFinishedEventArgs> EditingFinished;

        #endregion Events

        #region Public logic

        public abstract void ImportGeometry();

        public void StartEditing()
        {
            IsEditing = true;
            EditingStarted?.Invoke(this, new DocumentEditingStartedEventArgs(this));
        }

        public void FinishEditing()
        {
            IsEditing = false;
            EditingFinished?.Invoke(this, new DocumentEditingFinishedEventArgs(this));
        }

        public void OpenFolder()
        {
            if (IsFolderPresent)
                Process.Start(Path.GetDirectoryName(PathToFile));
        }

        public void AddMesh(Mesh mesh)
        {
            ProgressTracker.Instance.StartIndeterminate(Resources.ProcessingTriangles);
            meshes.Add(mesh);
            BoundingBox.Add(mesh.BoundingBox);
        }

        #region Equals

        public bool Equals(Document other)
        {
            if (other == null)
            {
                return false;
            }

            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (GetType() != obj.GetType())
            {
                return false;
            }

            return this.Equals((Document)obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        #endregion Equals

        public override string ToString()
        {
            return Name;
        }

        #endregion Public logic
    }
}
