using Colorado.Common.UI.ViewModels.Base;
using Colorado.Documents;
using Colorado.OpenGLWinForm;
using Colorado.Viewer.Controls.ViewModels.Common;
using Colorado.Viewer.Properties;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Colorado.Viewer.Controls.ViewModels.Documents
{
    public class DocumentsSettingsUserControlViewModel : ViewerBaseViewModel
    {
        private readonly BaseTreeItemViewModel documentsNode;

        public DocumentsSettingsUserControlViewModel(IRenderingControl renderingControl) : base(renderingControl)
        {
            documentsNode = new BaseTreeItemViewModel(Resources.Documents);
            documentsNode.Children.CollectionChanged += (s, args) => documentsNode.IsExpanded = true;
            Documents = new ObservableCollection<BaseTreeItemViewModel>();
            Documents.Add(documentsNode);

            renderingControl.DocumentsManager.DocumentOpened += DocumentsManager_DocumentOpened;
            renderingControl.DocumentsManager.DocumentClosed += DocumentsManager_DocumentClosed;
            renderingControl.DocumentsManager.AllDocumentsClosed += DocumentsManager_AllDocumentsClosed;
        }

        public ObservableCollection<BaseTreeItemViewModel> Documents { get; private set; }

        public bool IsDocumentsTreeEnabled
        {
            get
            {
                return !renderingControl.DocumentsManager.Documents.Any(d => d.DocumentTransformation.IsEditing);
            }
        }

        public EditDocumentUserControlViewModel EditDocumentUserControlViewModel { get; private set; }

        public Visibility IsDocumentsTreeVisible
        {
            get
            {
                return IsDocumentsTreeEnabled ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        
        public Visibility EditDocumentUserControlVisible
        {
            get
            {
                return renderingControl.DocumentsManager.Documents.Any(d => d.DocumentTransformation.IsEditing) ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private void DocumentsManager_DocumentOpened(object sender, Colorado.Documents.EventArgs.DocumentOpenedEventArgs e)
        {
            documentsNode.Children.Add(new DocumentTreeViewItemViewModel(renderingControl, e.OpenedDocument));
            e.OpenedDocument.DocumentTransformation.TransformChanged += DocumentTransformationTransformChanged;
            e.OpenedDocument.DocumentTransformation.EditingStarted += DocumentEditingStarted;
            e.OpenedDocument.DocumentTransformation.EditingFinished += DocumentEditingFinished;
        }

        private void DocumentsManager_DocumentClosed(object sender, Colorado.Documents.EventArgs.DocumentClosedEventArgs e)
        {
            documentsNode.Children.Remove(documentsNode.Children.Cast<DocumentTreeViewItemViewModel>().
                FirstOrDefault(d => d.Document.Equals(e.ClosedDocument)));
            UnsubscribeFromDocumentEvents(e.ClosedDocument);
        }

        private void DocumentsManager_AllDocumentsClosed(object sender, Colorado.Documents.EventArgs.AllDocumentsClosedEventArgs e)
        {
            documentsNode.Children.Clear();

            foreach (Document closedDocument in e.ClosedDocuments)
            {
                UnsubscribeFromDocumentEvents(closedDocument);
            }
        }

        private void UnsubscribeFromDocumentEvents(Document document)
        {
            document.DocumentTransformation.TransformChanged += DocumentTransformationTransformChanged;
            document.DocumentTransformation.EditingStarted -= DocumentEditingStarted;
            document.DocumentTransformation.EditingFinished -= DocumentEditingFinished;
        }

        private void DocumentTransformationTransformChanged(object sender, System.EventArgs e)
        {
            renderingControl.RefreshView();
        }

        private void DocumentEditingStarted(object sender, Colorado.Documents.EventArgs.DocumentEditingStartedEventArgs e)
        {
            EditDocumentUserControlViewModel = new EditDocumentUserControlViewModel(renderingControl, e.EditingDocument);
            EditingDocumentStateChanged();
        }

        private void DocumentEditingFinished(object sender, Colorado.Documents.EventArgs.DocumentEditingFinishedEventArgs e)
        {
            EditDocumentUserControlViewModel = null;
            EditingDocumentStateChanged();
        }

        private void EditingDocumentStateChanged()
        {
            OnPropertyChanged(nameof(EditDocumentUserControlViewModel));
            OnPropertyChanged(nameof(IsDocumentsTreeEnabled));
            OnPropertyChanged(nameof(IsDocumentsTreeVisible));
            OnPropertyChanged(nameof(EditDocumentUserControlVisible));
        }
    }
}
