
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.core.Exceptions
{
    public class BusineesException : Exception
    {
        public BusineesException() { }

        public BusineesException(string messge):base(messge)
        {
            
        }
    }
}
