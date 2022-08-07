using Colorado.Common.Tools.Keyboard;
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
            WPFOpenGLControl = new OpenGLWPF.OpenGLControl(OpenGLControl);
            RenderingControl = OpenGLControl;
            KeyboardToolsManager = new KeyboardToolsManager(OpenGLControl);
            KeyboardToolsManager.RegisterKeyboardToolHandler(OpenGLControl.ViewCameraKeyboardTool);
        }

        public DocumentsManager DocumentsManager { get; }

        public OpenGLControl OpenGLControl { get; }

        public OpenGLWPF.OpenGLControl WPFOpenGLControl { get; }

        public IRenderingControl RenderingControl { get; }

        public KeyboardToolsManager KeyboardToolsManager { get; }

        public void Refresh()
        {
            RenderingControl.RefreshView();
        }
    }
}
