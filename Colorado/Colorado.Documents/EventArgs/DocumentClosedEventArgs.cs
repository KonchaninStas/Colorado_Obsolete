using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.Documents.EventArgs
{
    public class DocumentClosedEventArgs : System.EventArgs
    {
        public DocumentClosedEventArgs(Document closedDocument)
        {
            ClosedDocument = closedDocument;
        }

        public Document ClosedDocument { get; }
    }
}
