using Colorado.Documents;
using Colorado.OpenGLWinForm;

namespace Colorado.Framework
{
    public class Application
    {
        private readonly IRenderingControl renderingControl;

        public Application(IRenderingControl renderingControl, DocumentsManager documentsManager)
        {
            this.renderingControl = renderingControl;
            DocumentsManager = documentsManager;
        }

        public DocumentsManager DocumentsManager { get; }

        public void AddDocument(Document document)
        {
            DocumentsManager.AddDocument(document);
        }

        public void Refresh()
        {
            renderingControl.RefreshView();
        }
    }
}
