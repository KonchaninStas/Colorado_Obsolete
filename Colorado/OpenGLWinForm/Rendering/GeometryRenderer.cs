using Colorado.Common.Utilities;
using Colorado.Documents;
using Colorado.GeometryDataStructures.Colors;
using Colorado.GeometryDataStructures.GeometryStructures.BaseGeometryStructures;
using Colorado.GeometryDataStructures.GeometryStructures.Geometry3D;
using Colorado.GeometryDataStructures.Primitives;
using Colorado.OpenGL.OpenGLWrappers.Geometry;
using Colorado.OpenGLWinForm.Rendering.PrimitivesRenderers;
using Colorado.OpenGLWinForm.Rendering.RenderableObjects;
using Colorado.OpenGLWinForm.Rendering.Settings;
using Colorado.OpenGLWinForm.View;

namespace Colorado.OpenGLWinForm.Rendering
{
    public class GeometryRenderer
    {
        #region Private fields

        private readonly DocumentsManager documentsManager;
        private readonly Camera viewCamera;

        #endregion Private fields

        #region Constructor

        public GeometryRenderer(DocumentsManager documentsManager, Camera viewCamera)
        {
            this.documentsManager = documentsManager;
            this.viewCamera = viewCamera;

            GlobalMaterialRenderingSettings = new GlobalMaterialRenderingSettings();
            TargetPointRenderingSettings = new TargetPointRenderingSettings();
            CoordinateSystemRenderer = new CoordinateSystemRenderer();
            MeshRenderingSettings = new MeshRenderingSettings();

            SubscribeToEvents();
            UpdateRenderingControlSettings();
        }

        #endregion Constructor 

        #region Properties

        public GridPlane GridPlane { get; private set; }

        public GlobalMaterialRenderingSettings GlobalMaterialRenderingSettings { get; }

        public TargetPointRenderingSettings TargetPointRenderingSettings { get; }

        public CoordinateSystemRenderer CoordinateSystemRenderer { get; }

        public MeshRenderingSettings MeshRenderingSettings { get; }

        #endregion Properties

        #region Public logic

        public void DrawGeometryPrimitives()
        {
            GridPlane.Draw();
            if (TargetPointRenderingSettings.DrawTargetPoint)
            {
                OpenGLGeometryWrapper.DrawPoint(viewCamera.TargetPoint.Inverse, TargetPointRenderingSettings.TargetPointColor, 20);
            }
            CoordinateSystemRenderer.Draw();
        }

        public void DrawSceneGeometry()
        {
            DrawEntities();
        }

        #endregion Public logic

        #region Private logic

        private void SubscribeToEvents()
        {
            documentsManager.DocumentOpened += (s, e) => UpdateRenderingControlSettings();
            documentsManager.DocumentClosed += (s, e) => UpdateRenderingControlSettings();
            documentsManager.AllDocumentsClosed += (s, e) => UpdateRenderingControlSettings();
        }

        private void UpdateRenderingControlSettings()
        {
            bool visible = GridPlane != null ? GridPlane.Visible : true;
            GridPlane = documentsManager.TotalBoundingBox.IsEmpty ? new GridPlane()
               : new GridPlane(5, documentsManager.TotalBoundingBox.Diagonal * 2, documentsManager.TotalBoundingBox.MinPoint.Z);
            GridPlane.Visible = visible;
        }

        private void DrawEntities()
        {
            foreach (Document document in documentsManager.DocumentsToRender)
            {
                foreach (Mesh mesh in document.Meshes)
                {
                    if (MeshRenderingSettings.DrawFillTriangles)
                    {
                        OpenGLFastRenderer.DrawMesh(mesh, GlobalMaterialRenderingSettings.UseGlobalMaterial ?
                           GlobalMaterialRenderingSettings.GlobalMaterial : null, document.DocumentTransformation.ActiveTransform);
                    }

                    if (MeshRenderingSettings.EnableWireframeMode)
                    {
                        OpenGLFastRenderer.DrawMeshLines(mesh, Material.Black, document.DocumentTransformation.ActiveTransform);
                    }

                    if (MeshRenderingSettings.DrawTrianglesVertices)
                    {
                        OpenGLFastRenderer.DrawMeshVertices(mesh, Material.Black, document.DocumentTransformation.ActiveTransform);
                    }
                }
            }
        }

        #endregion Private logic
    }
}
