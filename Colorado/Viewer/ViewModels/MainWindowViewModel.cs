using Colorado.Common.UI.Commands;
using Colorado.Common.UI.ViewModels.Base;
using Colorado.Documents.STL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Colorado.Viewer.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private readonly Colorado.Framework.Application application;

        private int fps;

        public MainWindowViewModel()
        {
            application = new Framework.Application();
            OpenGLControl = new OpenGLWPF.OpenGLControl(application);
            application.OpenGLControl.DrawSceneFinished += DrawSceneFinished;
        }

        #region Properties

        public OpenGLWPF.OpenGLControl OpenGLControl { get; }

        public int FPS
        {
            get
            {
                return fps;
            }
            set
            {
                fps = value;
                OnPropertyChanged(nameof(FPS));
            }
        }

        #endregion Properties

        #region Commands

        public ICommand OpenFileCommand
        {
            get { return new CommandHandler(OpenFile); }
        }

        public ICommand CloseFileCommand
        {
            get { return new CommandHandler(CloseFile); }
        }

        #endregion  Commands

        #region Private logic

        private void DrawSceneFinished(object sender, EventArgs e)
        {
            FPS = application.OpenGLControl.FpsCalculator.FramesPerSecond;
        }

        private void OpenFile()
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

        private void CloseFile()
        {
            application.CloseAllDocuments();
        }

        #endregion Private logic
    }
}
