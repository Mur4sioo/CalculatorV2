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
            ("clamp", [var value, var minimum, var maximum]) => new ClampFunctionNode(value, minimum, maximum),
            ("sqrt" or "clamp", _) => throw new InvalidOperationException("Incorrect number of arguments."),
            _ => throw new InvalidOperationException($"Unknown function name {name}"),
        };
    }
    public abstract record OneArgFunctionNode(string Name, AstNode Argument) : FunctionNode(Name);
    public abstract record MultipleArgFunctionNode (string Name, AstNode value, AstNode minimum, AstNode maximum) : FunctionNode(Name);
    public sealed record SqrtFunctionNode(AstNode Argument) : OneArgFunctionNode("sqrt", Argument)
    {
        public override double Evaluate(IReadOnlyDictionary<string, double>? variables)
        {
            return Math.Sqrt(this.Argument.Evaluate(variables));
        }
    }
    public sealed record ClampFunctionNode(AstNode value, AstNode minimum , AstNode maximum) : MultipleArgFunctionNode("clamp", value, minimum, maximum)
    {
        public override double Evaluate(IReadOnlyDictionary<string, double>? variables)
        {
            var value = Evaluate(variables);
            var minimum = Evaluate(variables);
            var maximum = Evaluate(variables);
            return Math.Clamp(value,minimum,maximum);
        }
    }


}
