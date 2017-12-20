using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestauranteApi.Exceptions
{
    public class ListNoContentException : Exception
    {

        public ListNoContentException() : base()
        {
        }

        public ListNoContentException(string message) : base(message)
        {
        }
    }
}