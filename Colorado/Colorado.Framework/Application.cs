using Colorado.DataStructures;
using Colorado.Documents;
using Colorado.OpenGLWinForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.Framework
{
    public class Application
    {
        private readonly IRenderingControl renderingControl;

        public Application(IRenderingControl renderingControl, DocumentsManager documentsManager)
        {
            this.renderingControl = renderingControl;
        }

        public DocumentsManager DocumentsManager { get; }

        public void AddDocument(Document document)
        {
            renderingControl.SetActiveDocument(document);
        }

        public void Refresh()
        {
            renderingControl.Refresh();
        }
    }
}
