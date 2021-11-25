using Colorado.Common.UI.Commands;
using Colorado.Common.UI.ViewModels.Base;
using Colorado.Common.UI.ViewModels.Controls;
using Colorado.Documents.STL;
using Colorado.OpenGL.Managers;
using Colorado.Viewer.Controls.ViewModels.Documents;
using Colorado.Viewer.Controls.Views.Tabs.LightingTab;
using Colorado.Viewer.Controls.Views.Tabs.MaterialTab;
using Colorado.Viewer.Controls.Views.Tabs.RenderingTab;
using Colorado.Viewer.Controls.Views.Tabs.ViewTab;
using Colorado.Viewer.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Windows.Input;

namespace Colorado.Viewer.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private IEnumerable<TabItemViewModel> tabs;
        private readonly Framework.Application application;

        private int fps;

        public MainWindowViewModel()
        {
            application = new Framework.Application();
            OpenGLControl = new OpenGLWPF.OpenGLControl(application);
            application.OpenGLControl.DrawSceneFinished += DrawSceneFinished;
            DocumentsTreeUserControlViewModel = new DocumentsTreeUserControlViewModel(application.RenderingControl);
            Tabs = new TabItemViewModel[]
            {
                new TabItemViewModel(Resources.LightingTabHeader, new LightingTabViewUserControl(application.RenderingControl), true),
                new TabItemViewModel(Resources.MaterialTabHeader, new MaterialSettingsTabUserControl(application.RenderingControl), false),
                new TabItemViewModel(Resources.RenderingTabHeader, new RenderingSettingsTabUserControl(application.RenderingControl), false),
                new TabItemViewModel(Resources.ViewTabHeader, new ViewSettingsTabUserControl(application.RenderingControl), false),
            };

            MenuItems = new ObservableCollection<MenuItemViewModel>()
            {
                new MenuItemViewModel(Resources.File)
                {
                     MenuItems = new ObservableCollection<MenuItemViewModel>()
                    {
                        new MenuItemViewModel(Resources.Open, OpenFileCommand),
                        new MenuItemViewModel(Resources.CloseAll, CloseAllCommand),
                    }
                }
            };
        }

        #region Properties

        public ObservableCollection<MenuItemViewModel> MenuItems { get; }

        public DocumentsTreeUserControlViewModel DocumentsTreeUserControlViewModel { get; }

        public IEnumerable<TabItemViewModel> Tabs
        {
            get
            {
                return tabs;
            }
            set
            {
                tabs = value;
                OnPropertyChanged(nameof(Tabs));
            }
        }

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

        public ICommand CloseAllCommand
        {
            get { return new CommandHandler(CloseAll); }
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
                application.DocumentsManager.AddDocument(new STLDocument(openFileDialog.FileName));
                application.Refresh();
            }
        }

        private void CloseAll()
        {
            application.DocumentsManager.CloseAllDocuments();
        }

        #endregion Private logic
    }
}
