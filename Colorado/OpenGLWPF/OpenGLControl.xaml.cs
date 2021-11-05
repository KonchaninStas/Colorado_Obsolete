using Colorado.Framework;
using System.Windows.Controls;

namespace Colorado.OpenGLWPF
{
    /// <summary>
    /// Interaction logic for OpenGLControl.xaml
    /// </summary>
    public partial class OpenGLControl : UserControl
    {
        public OpenGLControl(Application application)
        {
            InitializeComponent();
            winFormOpenGlControlHost.Child = application.OpenGLControl;
        }
    }
}
