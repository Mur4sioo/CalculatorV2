using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluator
{
    public readonly record struct FunctionInfo(string Name, int Arity);

    public static class FunctionList
    {
        public static readonly List<FunctionInfo> Functions = new List<FunctionInfo>()
        {
            new FunctionInfo("sqrt", 1),
            new FunctionInfo("abs", 1),
            new FunctionInfo("sin", 1),
            new FunctionInfo("cos", 1),
            new FunctionInfo("tan", 1),
            new FunctionInfo("clamp",3)
        };
    }
}
