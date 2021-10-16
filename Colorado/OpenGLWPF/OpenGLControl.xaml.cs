using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Colorado.OpenGLWPF
{
    /// <summary>
    /// Interaction logic for OpenGLControl.xaml
    /// </summary>
    public partial class OpenGLControl : UserControl
    {
        public OpenGLControl()
        {
            InitializeComponent();
            Loaded += LoadedCallback;
        }

        private void LoadedCallback(object sender, RoutedEventArgs e)
        {
            var host = new WindowsFormsHost();
            host.Child = new Colorado.OpenGLWinForm.OpenGLControl() { Dock = System.Windows.Forms.DockStyle.Fill };


            // Add the interop host control to the Grid
            // control's collection of child controls.
            this.Content = host;
        }
    }
}
