using Colorado.Documents;
using Colorado.OpenGLWinForm;

namespace Colorado.Framework
{
    public class Application
    {
        private readonly IRenderingControl renderingControl;

        public Application()
        {
            DocumentsManager = new DocumentsManager();
            OpenGLControl = new OpenGLControl(DocumentsManager);
            this.renderingControl = OpenGLControl;
        }


        public DocumentsManager DocumentsManager { get; }

        public OpenGLControl OpenGLControl { get; }

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
