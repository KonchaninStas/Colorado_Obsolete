using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.Common.Exceptions
{
    public class StlFileIsInvalidException : Exception
    {
        public StlFileIsInvalidException(Exception ex) : base(string.Empty, ex)
        {
        }
    }
}
