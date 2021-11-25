using Colorado.Common.UI.ViewModels.Base;
using Colorado.OpenGLWinForm;
using Colorado.Viewer.Properties;
using System.Collections.ObjectModel;
using System.Linq;

namespace Colorado.Viewer.Controls.ViewModels.Documents
{
    public class DocumentsTreeUserControlViewModel : ViewModelBase
    {
        private readonly BaseTreeItemViewModel documentsNode;
        private readonly IRenderingControl renderingControl;

        public DocumentsTreeUserControlViewModel(IRenderingControl renderingControl)
        {
            documentsNode = new BaseTreeItemViewModel(Resources.Documents);
            documentsNode.Children.CollectionChanged += (s, args) => documentsNode.IsExpanded = true;
            Documents = new ObservableCollection<BaseTreeItemViewModel>();
            Documents.Add(documentsNode);
            this.renderingControl = renderingControl;

            renderingControl.DocumentsManager.DocumentOpened += (s, args) => UpdateTree();
            renderingControl.DocumentsManager.DocumentClosed += (s, args) => UpdateTree();
            renderingControl.DocumentsManager.AllDocumentsClosed += (s, args) => UpdateTree();
        }

        public ObservableCollection<BaseTreeItemViewModel> Documents { get; private set; }

        private void UpdateTree()
        {
            documentsNode.Children.Clear();
            foreach (var documentNode in renderingControl.DocumentsManager.Documents.Select(
                d => new DocumentTreeViewItemViewModel(renderingControl, d)))
            {
                documentsNode.Children.Add(documentNode);
            }
        }
    }
}
