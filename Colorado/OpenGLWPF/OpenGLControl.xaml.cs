using Colorado.Documents;
using Colorado.OpenGLWinForm;
using System.Windows.Controls;

namespace Colorado.OpenGLWPF
{
    /// <summary>
    /// Interaction logic for OpenGLControl.xaml
    /// </summary>
    public partial class OpenGLControl : UserControl
    {
        public OpenGLControl(DocumentsManager documentsManager)
        {
            InitializeComponent();
            var openGlControl = new OpenGLWinForm.OpenGLControl(documentsManager);
            winFormOpenGlControlHost.Child = openGlControl;
            RenderingControl = openGlControl;
        }

        public IRenderingControl RenderingControl { get; }
    }
}
