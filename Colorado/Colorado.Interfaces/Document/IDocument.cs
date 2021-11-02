using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.Interfaces.Document
{
    public interface IDocument
    {
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
