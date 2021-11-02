using Colorado.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.OpenGLWinForm
{
    public interface IRenderingControl
    {
        Document ActiveDocument { get; set; }

        void Refresh();
    }
}
