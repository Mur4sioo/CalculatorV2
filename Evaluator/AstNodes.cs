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
        Exponent,
        Variable,
    }
    public enum UnaryOperator
    {
        Plus,
        Negate,
    }

    public abstract record AstNode
    {
        public abstract double Evaluate(IReadOnlyDictionary<string, char>? variables);
    }

    public record VariableNode(string name) : AstNode
    {
        public override double Evaluate(IReadOnlyDictionary<string, char>? variables)
        {
            throw new NotImplementedException();
        }
    }

    public sealed record NumberNode(double Value) : AstNode
    {
        public override double Evaluate(IReadOnlyDictionary<string, char>? variables)
        {
            return Value;
        }
    }

    public sealed record UnaryNode(UnaryOperator Operator, AstNode Value) : AstNode
    {
        public override double Evaluate(IReadOnlyDictionary<string, char>? variables)
        {
            var result = Value.Evaluate(variables);
            switch (Operator)
            {
                case UnaryOperator.Negate:
                    return -result;
                case UnaryOperator.Plus:
                    return result;
                default:
                    throw new NotImplementedException($"Unhandled unary operator {Operator}");
            }
        }
    }
    
    public sealed record BinaryNode(AstNode Left, BinaryOperator Operator, AstNode Right) : AstNode
    {
        public override double Evaluate(IReadOnlyDictionary<string, char>? variables)
        {
            var x = Left.Evaluate(variables);
            var y = Right.Evaluate(variables);
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

            if (Operator == BinaryOperator.Exponent)
            {
                return Math.Pow(x, y);
            }
            throw new NotImplementedException();
        }

    }
}
