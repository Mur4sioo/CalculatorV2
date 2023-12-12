using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluator
{

    public enum BinaryOperator
    {
        Plus,
        Minus,
        Multiply,
        Divide,
    }

    public abstract record AstNode;

    public sealed record NumberNode(double Value) : AstNode;

    public sealed record BinaryNode(AstNode Left, BinaryOperator Operator, AstNode Right) : AstNode;
}
