﻿using Colorado.Documents;
using Colorado.Documents.STL;
using System.Windows;
using System.Windows.Forms;

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
            var documentsManager = new DocumentsManager();
            var openGLControl = new OpenGLWPF.OpenGLControl(documentsManager);
            openGLControlWrapper = openGLControl;
            application = new Framework.Application(openGLControl.RenderingControl, documentsManager);
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
                application.AddDocument(new STLDocument(openFileDialog.FileName));
                application.Refresh();
            }

        }
    }
}
