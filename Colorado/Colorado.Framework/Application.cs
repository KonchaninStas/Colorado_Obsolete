using Colorado.Documents;
using Colorado.Documents.STL;
using Colorado.OpenGLWinForm;

namespace Colorado.Framework
{
    public class Application
    {
        public Application()
        {
            DocumentsManager = new DocumentsManager();
            OpenGLControl = new OpenGLControl(DocumentsManager) { Dock = System.Windows.Forms.DockStyle.Fill };
            RenderingControl = OpenGLControl;

            //documentsManager.AddDocument(new STLDocument(@"C:\Users\skonchanin\Downloads\Rhino.stl"));
        }

        public DocumentsManager DocumentsManager { get; }

        public OpenGLControl OpenGLControl { get; }

        public IRenderingControl RenderingControl { get; }

        public void Refresh()
        {
            RenderingControl.RefreshView();
        }
    }
}
