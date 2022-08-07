namespace Colorado.OpenGLWinForm.Rendering.Settings
{
    public class MeshRenderingSettings
    {
        public MeshRenderingSettings()
        {
            DrawFillTriangles = true;
        }

        public bool DrawFillTriangles { get; set; }

        public bool EnableWireframeMode { get; set; }

        public bool DrawTrianglesVertices { get; set; }
    }
}
