using Colorado.Common.UI.Commands;
using Colorado.Common.UI.ViewModels.Base;
using Colorado.Common.UI.ViewModels.Controls;
using Colorado.Documents;
using Colorado.Documents.STL;
using Colorado.Viewer.Controls.ViewModels.Documents;
using Colorado.Viewer.Controls.Views.Tabs.LightingTab;
using Colorado.Viewer.Controls.Views.Tabs.MaterialTab;
using Colorado.Viewer.Controls.Views.Tabs.RenderingTab;
using Colorado.Viewer.Controls.Views.Tabs.ViewTab;
using Colorado.Viewer.Properties;
using Colorado.Viewer.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;

namespace Colorado.Viewer.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private IEnumerable<TabItemViewModel> tabs;
        private readonly Framework.Application application;
        private readonly SampleFilesManager sampleFilesManager;

        private int fps;

        public MainWindowViewModel()
        {
            application = new Framework.Application();
            OpenGLControl = application.WPFOpenGLControl;
            sampleFilesManager = new SampleFilesManager(application);

            application.OpenGLControl.DrawSceneFinished += DrawSceneFinished;
            DocumentsSettingsUserControlViewModel = new DocumentsSettingsUserControlViewModel(application.RenderingControl);
            DocumentsSettingsUserControlViewModel.PropertyChanged += (s, args) => OnPropertyChanged(nameof(IsMenuEnabled));

            Tabs = new TabItemViewModel[]
            {
                new TabItemViewModel(Resources.LightingTabHeader, new LightingTabViewUserControl(application.RenderingControl), true),
                new TabItemViewModel(Resources.MaterialTabHeader, new MaterialSettingsTabUserControl(application.RenderingControl), false),
                new TabItemViewModel(Resources.RenderingTabHeader, new RenderingSettingsTabUserControl(application.RenderingControl), false),
                new TabItemViewModel(Resources.ViewTabHeader, new ViewSettingsTabUserControl(application.RenderingControl), false),
            };

            MenuItems = new ObservableCollection<MenuItemViewModel>()
            {
                new MenuItemViewModel(Resources.File,
                new []
                {
                    new MenuItemViewModel(Resources.Open, OpenFileCommand),
                    new MenuItemViewModel(Resources.CloseAll, CloseAllCommand),
                    new MenuItemViewModel(Resources.SampleFiles,
                        sampleFilesManager.SampleFiles.Select(f => new MenuItemViewModel(f.Name,
                        new CommandHandler(() => sampleFilesManager.OpenFile(f.FullName)))))
                })
            };
        }

        #region Properties

        public ObservableCollection<MenuItemViewModel> MenuItems { get; }

        public DocumentsSettingsUserControlViewModel DocumentsSettingsUserControlViewModel { get; }

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

        public bool IsMenuEnabled
        {
            get
            {
                return !application.RenderingControl.DocumentsManager.Documents.Any(d => d.DocumentTransformation.IsEditing);
            }
        }

        public int TrianglesCount => application.DocumentsManager.TotalTrianglesCount;

        #endregion Properties

        #region Commands

        public ICommand OpenFileCommand
        {
            get { return new CommandHandler(OpenFile, () => DocumentsManager.RegisteredFilters.Count != 0); }
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
            OnPropertyChanged(nameof(TrianglesCount));
        }

        private void OpenFile()
        {
            string filter = string.Empty;
            foreach (string documentFilter in DocumentsManager.RegisteredFilters)
            {
                filter += $"|{documentFilter}";
            }

            filter = filter.Remove(0, 1);

            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Multiselect = false,
                Filter = filter
            };

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                application.DocumentsManager.AddDocument(
                    new STLDocument(application.KeyboardToolsManager, openFileDialog.FileName));
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
