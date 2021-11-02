using Colorado.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Colorado.Viewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Colorado.Framework.Application application;

        public MainWindow()
        {
            InitializeComponent();
            application = new Framework.Application(OpenGLControl.RenderingControl);
        }

        private void OpenFileMenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Multiselect = false,
                Filter = "Stl Files(*.stl)|*.stl;|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                application.SetActiveDocument(new STLDocument(openFileDialog.FileName));
                application.Refresh();
            }

        }
    }
}
