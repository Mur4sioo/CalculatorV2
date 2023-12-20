using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluator
{
    internal class ParseException : Exception
    {
        public ParseException()
        {

        }

        public ParseException(string? message) : base(message)
        {

        }

        public ParseException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
        [DoesNotReturn]
        private static void ThrowBadFormulaException(string? message = null)
            => throw new ParseException(message?? "Bad formula");
    }
}
