using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestauranteApi.Exceptions
{
    public class BusinessException : Exception
    {

        public BusinessException() : base()
        {
        }

        public BusinessException(string message) : base(message)
        {
        }
    }
}