using Colorado.GeometryDataStructures.GeometryStructures.BaseGeometryStructures;
using Colorado.GeometryDataStructures.GeometryStructures.Geometry3D;
using Colorado.GeometryDataStructures.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.Documents
{
    public class Document
    {
        private readonly IList<GeometryObject> geometryObjects;
        private readonly IList<Mesh> meshes;

        public Document()
        {
            geometryObjects = new List<GeometryObject>();
            meshes = new List<Mesh>();
            BoundingBox = new BoundingBox();
        }

        public IEnumerable<GeometryObject> Geometries => geometryObjects;

        public IEnumerable<Mesh> Meshes => meshes;

        public BoundingBox BoundingBox { get; private set; }

        public void AddMeshGeometryObject(Mesh mesh)
        {
            meshes.Add(mesh);
            AddGeometryObject(mesh);
        }

        public void AddGeometryObject(GeometryObject geometryObject)
        {
            geometryObjects.Add(geometryObject);
            BoundingBox = BoundingBox.Add(geometryObject.BoundingBox);
        }
    }
}
