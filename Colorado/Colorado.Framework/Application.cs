using Colorado.Documents;
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
