using Colorado.GeometryDataStructures.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.Documents.EventArgs
{
    public class DocumentOpenedEventArgs : System.EventArgs
    {
        public DocumentOpenedEventArgs(Document openedDocument)
        {
            OpenedDocument = openedDocument;
        }

        public Document OpenedDocument { get; }
    }
}
