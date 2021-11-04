using Colorado.Documents;

namespace Colorado.OpenGLWinForm
{
    public interface IRenderingControl
    {
        void SetDocumentManager(DocumentsManager documentsManager);
        void RefreshView();
    }
}
