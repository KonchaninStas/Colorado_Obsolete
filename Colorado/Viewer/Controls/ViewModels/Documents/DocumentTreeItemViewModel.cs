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
        private readonly Document document;

        public DocumentTreeViewItemViewModel(IRenderingControl renderingControl, Document document) : base(document.Name)
        {
            this.renderingControl = renderingControl;
            this.document = document;

            MenuItems.Add(new MenuItemViewModel(Resources.Show, ShowCommand));
            MenuItems.Add(new MenuItemViewModel(Resources.Hide, HideCommand));
            MenuItems.Add(new MenuItemViewModel(Resources.Isolate, IsolateCommand));
            MenuItems.Add(new MenuItemViewModel(Resources.Close, CloseCommand));
            MenuItems.Add(new MenuItemViewModel(Resources.OpenFolder, OpenFolderCommand));
            MenuItems.Add(new MenuItemViewModel(Resources.Edit, EditCommand));
            MenuItems.CollectionChanged += (s, args) => OnPropertyChanged(nameof(ContextMenuVisible));
        }

        #region Commands

        public ICommand ShowCommand
        {
            get
            {
                return new CommandHandler(ShowDocument, () => !document.Visible);
            }
        }

        public ICommand HideCommand
        {
            get
            {
                return new CommandHandler(HideDocument, () => document.Visible);
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
                return new CommandHandler(document.OpenFolder, () => document.IsFolderPresent);
            }
        }

        public ICommand EditCommand
        {
            get
            {
                return new CommandHandler(document.StartEditing);
            }
        }

        public bool Document { get; internal set; }

        #endregion  Commands

        private void ShowDocument()
        {
            renderingControl.DocumentsManager.ShowDocument(document);
            renderingControl.RefreshView();
        }

        private void HideDocument()
        {
            renderingControl.DocumentsManager.HideDocument(document);
            renderingControl.RefreshView();
        }

        private void IsolateDocument()
        {
            renderingControl.DocumentsManager.IsolateDocument(document);
            renderingControl.RefreshView();
        }

        private void CloseDocument()
        {
            renderingControl.DocumentsManager.CloseDocument(document);
            renderingControl.RefreshView();
        }
    }
}
