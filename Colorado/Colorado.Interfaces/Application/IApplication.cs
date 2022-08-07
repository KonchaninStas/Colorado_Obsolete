using Colorado.Interfaces.Document;
using Colorado.Interfaces.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.Interfaces.Application
{
    interface IApplication
    {
        IRenderingControl RenderingControl { get; }

        IDocument ActiveDocument { get; }
    }
}
