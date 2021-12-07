using Colorado.Common.Exceptions;
using Colorado.Common.Extensions;
using Colorado.Common.ProgressTracking;
using Colorado.Common.Tools.Keyboard;
using Colorado.Common.UI.Handlers;
using Colorado.Documents.Properties;
using Colorado.GeometryDataStructures.GeometryStructures.Geometry3D;
using Colorado.GeometryDataStructures.Primitives;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

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

        private readonly List<Mesh> meshes;

        #endregion Private fields

        #region Constructor

        public Document(KeyboardToolsManager keyboardToolsManager)
        {
            Id = GetNextId();
            meshes = new List<Mesh>();
            BoundingBox = new BoundingBox();
            Visible = true;
            DocumentTransformation = new DocumentTransformation(this, keyboardToolsManager);
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

        public DocumentTransformation DocumentTransformation { get; private set; }

        public abstract string Filter { get; }

        #endregion Properties

        #region Public logic

        public abstract void ImportGeometry();

        public void PrepareLocalTransform()
        {
            ProgressTracker.Instance.Init(meshes.Count);

            if (!BoundingBox.Center.IsZero)
            {
                Transform transform = Transform.CreateTranslation(BoundingBox.Center.ToVector().Inverse);
                IEnumerable<Mesh> tempMeshes = meshes.Select(m => m.GetTransformed(transform)).ToList();
                meshes.Clear();
                BoundingBox.ResetToDefault();
                tempMeshes.ForEach(m => AddMesh(m));

                DocumentTransformation.ApplyTransform(transform.GetInverted());
                DocumentTransformation.InitialTransform = transform.GetInverted();
            }
        }

        public void OpenFolder()
        {
            if (IsFolderPresent)
                Process.Start(Path.GetDirectoryName(PathToFile));
        }

        public void AddMesh(Mesh mesh)
        {
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

        public void Save()
        {
            var saveFileDialog = new SaveFileDialog()
            {
                Filter = Filter
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ProgressHandler progressHandler = new ProgressHandler(string.Format(Resources.SavingDocument,
                    Path.GetFileNameWithoutExtension(saveFileDialog.FileName), saveFileDialog.FileName));
                try
                {
                    ProgressTracker.Instance.ShowWindow();

                    Save(saveFileDialog.FileName);
                }
                catch (OperationAbortException)
                {
                    DeleteFile(saveFileDialog);
                }
                catch (Exception ex)
                {
                    DeleteFile(saveFileDialog);
                    MessageViewHandler.ShowExceptionMessage(Resources.OpeningDocument, ex);
                }
                finally
                {
                    progressHandler.Abort();
                }
            }
        }

        private static void DeleteFile(SaveFileDialog saveFileDialog)
        {
            if (File.Exists(saveFileDialog.FileName))
            {
                File.Delete(saveFileDialog.FileName);
            }
        }

        public abstract void Save(string fileName);

        #endregion Public logic
    }
}
