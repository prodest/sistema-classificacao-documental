using System;
using System.Collections.Generic;
using System.Text;

namespace Prodest.Scd.Business.Common.Exceptions
{
    public class ScdException : Exception
    {
        public ScdException() : base(){}

        public ScdException(string message) : base(message){}

        public ScdException(string message, Exception innerException) : base(message, innerException) { }
    }
}
