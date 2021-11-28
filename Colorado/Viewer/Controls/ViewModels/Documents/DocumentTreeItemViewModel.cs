using Colorado.Common.UI.Commands;
using Colorado.Common.UI.ViewModels.Base;
using Colorado.Documents;
using Colorado.OpenGLWinForm;
using Colorado.Viewer.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Colorado.Viewer.Controls.ViewModels.Documents
{
    public class DocumentTreeViewItemViewModel : BaseTreeItemViewModel
    {
        private readonly IRenderingControl renderingControl;

        public DocumentTreeViewItemViewModel(IRenderingControl renderingControl, Document document) : base(document.Name)
        {
            this.renderingControl = renderingControl;
            Document = document;

            MenuItems.Add(new MenuItemViewModel(Resources.Show, ShowCommand));
            MenuItems.Add(new MenuItemViewModel(Resources.Hide, HideCommand));
            MenuItems.Add(new MenuItemViewModel(Resources.Isolate, IsolateCommand));
            MenuItems.Add(new MenuItemViewModel(Resources.Close, CloseCommand));
            MenuItems.Add(new MenuItemViewModel(Resources.OpenFolder, OpenFolderCommand));
            MenuItems.Add(new MenuItemViewModel(Resources.Edit, EditCommand));
            MenuItems.Add(new MenuItemViewModel(Resources.MoveToOrigin, MoveToOriginCommand));
            MenuItems.Add(new MenuItemViewModel(Resources.RestoreDefaultPosition, RestoreDefaultPositionCommand));
            MenuItems.CollectionChanged += (s, args) => OnPropertyChanged(nameof(ContextMenuVisible));
        }

        #region Properties

        public Document Document { get; }

        #endregion Properties

        #region Commands

        public ICommand ShowCommand
        {
            get
            {
                return new CommandHandler(ShowDocument, () => !Document.Visible);
            }
        }

        public ICommand HideCommand
        {
            get
            {
                return new CommandHandler(HideDocument, () => Document.Visible);
            }
        }

        public ICommand IsolateCommand
        {
            get
            {
                return new CommandHandler(IsolateDocument, () => renderingControl.DocumentsManager.DocumentsCount > 1);
            }
        }

        public ICommand CloseCommand
        {
            get
            {
                return new CommandHandler(CloseDocument);
            }
        }

        public ICommand OpenFolderCommand
        {
            get
            {
                return new CommandHandler(Document.OpenFolder, () => Document.IsFolderPresent);
            }
        }

        public ICommand EditCommand
        {
            get
            {
                return new CommandHandler(Document.StartEditing);
            }
        }

        public ICommand MoveToOriginCommand
        {
            get
            {
                return new CommandHandler(Document.DocumentTransformation.MoveToOrigin,
                    () => !Document.DocumentTransformation.WasMovedToOrigin && Document.DocumentTransformation.CanBeMovedToOrigin);
            }
        }

        public ICommand RestoreDefaultPositionCommand
        {
            get
            {
                return new CommandHandler(Document.DocumentTransformation.RestoreDefaultPosition, 
                    () => Document.DocumentTransformation.WasMovedToOrigin && Document.DocumentTransformation.CanBeMovedToOrigin);
            }
        }

        #endregion  Commands

        private void ShowDocument()
        {
            renderingControl.DocumentsManager.ShowDocument(Document);
            renderingControl.RefreshView();
        }

        private void HideDocument()
        {
            renderingControl.DocumentsManager.HideDocument(Document);
            renderingControl.RefreshView();
        }

        private void IsolateDocument()
        {
            renderingControl.DocumentsManager.IsolateDocument(Document);
            renderingControl.RefreshView();
        }

        private void CloseDocument()
        {
            renderingControl.DocumentsManager.CloseDocument(Document);
            renderingControl.RefreshView();
        }
    }
}
