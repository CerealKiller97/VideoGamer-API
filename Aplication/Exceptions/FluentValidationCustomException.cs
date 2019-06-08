using System;
using System.Collections.Generic;

namespace Aplication.Exceptions
{
    public class FluentValidationCustomException : Exception
    {
        public FluentValidationCustomException()
        {
        }

        public FluentValidationCustomException(IEnumerable<string> message) : base(message.ToString())
        {
        }
    }
}
