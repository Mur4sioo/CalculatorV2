using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Evaluator
{

    public enum BinaryOperator
    {
        Add,
        Subtract,
        Multiply,
        Divide,
    }

    public abstract record AstNode
    {
        public abstract double Evaluate();
    }

    public sealed record NumberNode(double Value) : AstNode
    {
        public override double Evaluate()
        {
            return Value;
        }
    }

    public sealed record BinaryNode(AstNode Left, BinaryOperator Operator, AstNode Right) : AstNode
    {
        public override double Evaluate()
        {
            var x = Left.Evaluate();
            var y = Right.Evaluate();
            if (Operator == BinaryOperator.Add)
            {
                return x + y;
            }
            if (Operator == BinaryOperator.Subtract)
            {
                return x - y;
            }
            if (Operator == BinaryOperator.Divide)
            {
                return x / y;
            }
            if (Operator == BinaryOperator.Multiply)
            {
                return x * y;
            }
            throw new NotImplementedException();
        }

    }
}
