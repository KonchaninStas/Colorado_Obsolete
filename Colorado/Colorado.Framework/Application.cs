using Colorado.DataStructures;
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

        public Application(IRenderingControl renderingControl)
        {
            this.renderingControl = renderingControl;
        }

        public void SetActiveDocument(Document document)
        {
            renderingControl.SetActiveDocument(document);
        }

        public void Refresh()
        {
            renderingControl.Refresh();
        }
    }
}
