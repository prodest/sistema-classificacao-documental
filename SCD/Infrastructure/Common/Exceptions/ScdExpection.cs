using System;
using System.Collections.Generic;
using System.Text;

namespace Prodest.Scd.Infrastructure.Common.Exceptions
{
    public class ScdExpection : Exception
    {
        public ScdExpection() : base(){}

        public ScdExpection(string message) : base(message){}

        public ScdExpection(string message, Exception innerException) : base(message, innerException) { }
    }
}
