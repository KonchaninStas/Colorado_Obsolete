using Colorado.Documents;
using Colorado.OpenGLWinForm;
using Colorado.Viewer.Controls.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Colorado.Viewer.Controls.ViewModels.Documents
{
    public class EditDocumentUserControlViewModel : ViewerBaseViewModel
    {
        public EditDocumentUserControlViewModel(IRenderingControl renderingControl, Document document) : base(renderingControl)
        {
        }
    }
}
