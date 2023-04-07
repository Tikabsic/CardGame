using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class ForbiddenMoveException : Exception
    {
        public ForbiddenMoveException( string message) : base(message)
        {

        }
    }
}
