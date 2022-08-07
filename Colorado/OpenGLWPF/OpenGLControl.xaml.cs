using System.Windows.Controls;

namespace Colorado.OpenGLWPF
{
    /// <summary>
    /// Interaction logic for OpenGLControl.xaml
    /// </summary>
    public partial class OpenGLControl : UserControl
    {
        public OpenGLControl(Colorado.OpenGLWinForm.OpenGLControl openGLControl)
        {
            InitializeComponent();
            winFormOpenGlControlHost.Child = openGLControl;
        }
    }
}
