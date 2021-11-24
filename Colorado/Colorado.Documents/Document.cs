using Colorado.GeometryDataStructures.GeometryStructures.BaseGeometryStructures;
using Colorado.GeometryDataStructures.Primitives;
using System.Collections.Generic;

namespace Colorado.Documents
{
    public class Document
    {
        #region Private fields

        private readonly IList<GeometryObject> geometryObjects;

        #endregion Private fields

        #region Constructor

        public Document()
        {
            geometryObjects = new List<GeometryObject>();
            BoundingBox = new BoundingBox();
        }

        #endregion Constructor

        #region Properties

        public IEnumerable<GeometryObject> Geometries => geometryObjects;

        public BoundingBox BoundingBox { get; private set; }

        #endregion Properties

        #region Public logic

        public void AddGeometryObject(GeometryObject geometryObject)
        {
            geometryObjects.Add(geometryObject);
            BoundingBox = BoundingBox.Add(geometryObject.BoundingBox);
        }

        #endregion Public logic
    }
}
