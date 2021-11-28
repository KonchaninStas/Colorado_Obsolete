using Colorado.Common.UI.Commands;
using Colorado.Documents;
using Colorado.OpenGLWinForm;
using Colorado.Viewer.Controls.ViewModels.Common;
using System.Windows.Input;

namespace Colorado.Viewer.Controls.ViewModels.Documents
{
    public class EditDocumentUserControlViewModel : ViewerBaseViewModel
    {
        private readonly Document document;

        public EditDocumentUserControlViewModel(IRenderingControl renderingControl, Document document) : base(renderingControl)
        {
            this.document = document;
        }

        #region Commands

        public ICommand FinishEditingCommand
        {
            get { return new CommandHandler(document.DocumentTransformation.FinishEditing); }
        }

        #endregion Commands
    }
}
