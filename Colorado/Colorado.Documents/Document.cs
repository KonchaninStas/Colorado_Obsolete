using Colorado.GeometryDataStructures.GeometryStructures.BaseGeometryStructures;
using Colorado.GeometryDataStructures.Primitives;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Colorado.Documents
{
    public abstract class Document : IEquatable<Document>
    {
        private static int lastId;

        private static int GetNextId()
        {
            return lastId++;
        }

        #region Private fields

        private readonly IList<GeometryObject> geometryObjects;

        #endregion Private fields

        #region Constructor

        public Document()
        {
            Id = GetNextId();
            geometryObjects = new List<GeometryObject>();
            BoundingBox = new BoundingBox();
            Visible = true;
        }

        #endregion Constructor

        #region Properties

        public int Id { get; }

        public abstract string PathToFile { get; }

        public bool IsFilePresent => !string.IsNullOrEmpty(PathToFile) && File.Exists(PathToFile);

        public bool IsFolderPresent => !string.IsNullOrEmpty(PathToFile) && Directory.Exists(Path.GetDirectoryName(PathToFile));

        public abstract string Name { get; }

        public IEnumerable<GeometryObject> Geometries => geometryObjects;

        public BoundingBox BoundingBox { get; private set; }

        public bool Visible { get; set; }

        #endregion Properties

        #region Public logic

        public void OpenFolder()
        {
            if (IsFolderPresent)
                Process.Start(Path.GetDirectoryName(PathToFile));
        }

        public void AddGeometryObject(GeometryObject geometryObject)
        {
            geometryObjects.Add(geometryObject);
            BoundingBox = BoundingBox.Add(geometryObject.BoundingBox);
        }

        public bool Equals(Document other)
        {
            if (other == null)
            {
                return false;
            }

            return Id == other.Id;
        }

        public override string ToString()
        {
            return Name;
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

        #endregion Public logic
    }
}
