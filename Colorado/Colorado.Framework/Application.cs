using Colorado.Documents;
using Colorado.Documents.STL;
using Colorado.OpenGLWinForm;

namespace Colorado.Framework
{
    public class Application
    {
        private readonly DocumentsManager documentsManager;

        public Application()
        {
            documentsManager = new DocumentsManager();
            OpenGLControl = new OpenGLControl(documentsManager) { Dock = System.Windows.Forms.DockStyle.Fill };
            RenderingControl = OpenGLControl;

            documentsManager.AddDocument(new STLDocument(@"C:\Users\skonchanin\Downloads\Cube (2).stl"));
        }

        public OpenGLControl OpenGLControl { get; }

        public IRenderingControl RenderingControl { get; }

        public void AddDocument(Document document)
        {
            documentsManager.AddDocument(document);
        }

        public void CloseAllDocuments()
        {
            documentsManager.CloseAllDocuments();
        }

        public void Refresh()
        {
            RenderingControl.RefreshView();
        }
    }
}
