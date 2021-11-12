using Colorado.Documents;
using Colorado.OpenGLWinForm;

namespace Colorado.Framework
{
    public class Application
    {
        private readonly DocumentsManager documentsManager;
        private readonly IRenderingControl renderingControl;

        public Application()
        {
            documentsManager = new DocumentsManager();
            OpenGLControl = new OpenGLControl(documentsManager) { Dock = System.Windows.Forms.DockStyle.Fill };
            this.renderingControl = OpenGLControl;
        }

        public OpenGLControl OpenGLControl { get; }

        public void AddDocument(Document document)
        {
            documentsManager.AddDocument(document);
        }

        public void CloseAllDocuments( )
        {
            documentsManager.CloseAllDocuments();
        }

        public void Refresh()
        {
            renderingControl.RefreshView();
        }
    }
}
