using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluator
{
    public abstract record FunctionNode(string Name) : AstNode
    {
        public static FunctionNode Create(string name, IReadOnlyList<AstNode> arguments) => (name, arguments) switch
        {
            ("sqrt", [var arg]) => new SqrtFunctionNode(arg),
            ("abs", [var arg]) => new AbsFunctionNode(arg),
            ("sin", [var arg]) => new SinFunctionNode(arg),
            ("cos", [var arg]) => new CosFunctionNode(arg),
            ("tan", [var arg]) => new TanFunctionNode(arg),
            ("clamp", [var value, var minimum, var maximum]) => new ClampFunctionNode(value, minimum, maximum),
            ("sqrt" or "clamp", _) => throw new InvalidOperationException("Incorrect number of arguments."),
            _ => throw new InvalidOperationException($"Unknown function name {name}"),
        };
    }
    public abstract record OneArgFunctionNode(string Name, AstNode Argument) : FunctionNode(Name);
    public abstract record MultipleArgFunctionNode (string Name, AstNode Value, AstNode Minimum, AstNode Maximum) : FunctionNode(Name);
    public sealed record SqrtFunctionNode(AstNode Argument) : OneArgFunctionNode("sqrt", Argument)
    {
        public override double Evaluate(IReadOnlyDictionary<string, double>? variables)
        {
            return Math.Sqrt(this.Argument.Evaluate(variables));
        }
    }

    public sealed record AbsFunctionNode(AstNode Argument) : OneArgFunctionNode("abs", Argument)
    {
        public override double Evaluate(IReadOnlyDictionary<string, double>? variables)
        {
            return Math.Abs(this.Argument.Evaluate(variables));
        }
    }
    public sealed record SinFunctionNode(AstNode Argument) : OneArgFunctionNode("sin", Argument)
    {
        public override double Evaluate(IReadOnlyDictionary<string, double>? variables)
        {
            return Math.Sin(this.Argument.Evaluate(variables));
        }
    }
    public sealed record CosFunctionNode(AstNode Argument) : OneArgFunctionNode("cos", Argument)
    {
        public override double Evaluate(IReadOnlyDictionary<string, double>? variables)
        {
            return Math.Cos(this.Argument.Evaluate(variables));
        }
    }
    public sealed record TanFunctionNode(AstNode Argument) : OneArgFunctionNode("tan", Argument)
    {
        public override double Evaluate(IReadOnlyDictionary<string, double>? variables)
        {
            return Math.Tan(this.Argument.Evaluate(variables));
        }
    }
    public sealed record ClampFunctionNode(AstNode Value, AstNode Minimum , AstNode Maximum) : MultipleArgFunctionNode("clamp", Value, Minimum, Maximum)
    {
        public override double Evaluate(IReadOnlyDictionary<string, double>? variables)
        {
            var value = Value.Evaluate(variables);
            var minimum = Minimum.Evaluate(variables);
            var maximum = Maximum.Evaluate(variables);
            return Math.Clamp(value,minimum,maximum);
        }
    }


}
