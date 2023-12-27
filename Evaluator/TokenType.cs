using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluator
{
    public enum TokenType
    {
        Unknown,
        OperatorPlus,
        OperatorMinus,
        OperatorDivide,
        OperatorMultiply,
        Number,
        ParenOpen,
        ParenClose,
        OperatorExponent,
    }
    
}

