using Colorado.Common.UI.ViewModels.Base;
using Colorado.Documents;
using Colorado.OpenGLWinForm;
using Colorado.Viewer.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Colorado.Viewer.Controls.ViewModels.Documents
{
    public class DocumentsTreeUserControlViewModel : ViewModelBase
    {
        private readonly BaseTreeItemViewModel documentsNode;
        private readonly DocumentsManager documentsManager;
        private readonly IRenderingControl renderingControl;

        public DocumentsTreeUserControlViewModel(DocumentsManager documentsManager, IRenderingControl renderingControl)
        {
            documentsNode = new BaseTreeItemViewModel(Resources.Documents);
            Documents = new ObservableCollection<BaseTreeItemViewModel>();
            Documents.Add(documentsNode);
            this.documentsManager = documentsManager;
            this.renderingControl = renderingControl;

            documentsManager.DocumentOpened += (s, args) => UpdateTree();
            documentsManager.DocumentClosed += (s, args) => UpdateTree();
            documentsManager.AllDocumentsClosed += (s, args) => UpdateTree();
        }

        public ObservableCollection<BaseTreeItemViewModel> Documents { get; private set; }

        private void UpdateTree()
        {
            documentsNode.Children.Clear();
            foreach (var documentNode in documentsManager.Documents.Select(d => new DocumentTreeViewItemViewModel(d)))
            {
                documentsNode.Children.Add(documentNode);
            }
        }
    }
}
