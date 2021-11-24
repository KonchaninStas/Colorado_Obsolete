using Colorado.Common.UI.ViewModels.Base;
using Colorado.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.Viewer.Controls.ViewModels.Documents
{
    public class DocumentTreeViewItemViewModel : BaseTreeItemViewModel
    {
        private readonly Document document;

        public DocumentTreeViewItemViewModel(Document document) : base(document.Name)
        {
            this.document = document;
        }
    }
}
