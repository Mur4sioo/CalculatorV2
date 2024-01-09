using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Evaluator
{
    public class Parser
    {
        private readonly Lexer lexer;
        public Parser(Lexer lexer)
        {
            this.lexer = lexer;
        }

        public static AstNode ParseExpression(string input, CultureInfo? culture = null)
        {
            var lexer = new Lexer(input, culture);
            var parser = new Parser(lexer);
            var result = parser.ParseExpression();
            if (result is null)
                throw new ParseException();
            if (lexer.IsFinished is false)
                throw new ParseException();
            return result;
        }
        #region Parse Methods

        private AstNode? ParseExpression()
        {
            return ParseAdditive();
        }

        private AstNode? ParseAdditive()
        {
            var left = ParseMultiplicative();
            if (left is null)
                return null;
            while (lexer.TryConsumeTokenType(TokenType.OperatorPlus, TokenType.OperatorMinus, out var foundTokenType))
            {
                var right = ParseMultiplicative();
                if (right is null)
                    return null;
                switch (foundTokenType)
                {
                    case TokenType.OperatorPlus:
                        left = new BinaryNode(left, BinaryOperator.Add, right);
                        break;
                    case TokenType.OperatorMinus:
                        left = new BinaryNode(left, BinaryOperator.Subtract, right);
                        break;
                    default:
                        return left;
                }
            }
            return left;
        }
        private AstNode? ParseMultiplicative()
        {
            var left = ParseUnary();
            if (left is null)
                return null;
            while (lexer.TryConsumeTokenType(TokenType.OperatorMultiply, TokenType.OperatorDivide, out var foundTokenType))
            {
                var right = ParseUnary();
                if (right is null)
                    throw new ParseException();
                switch (foundTokenType)
                {
                    case TokenType.OperatorMultiply:
                        left = new BinaryNode(left, BinaryOperator.Multiply, right);
                        break;
                    case TokenType.OperatorDivide:
                        left = new BinaryNode(left, BinaryOperator.Divide, right);
                        break;
                    default:
                        return left;
                }
            }
            return left;
        }

        private AstNode? ParseUnary()
        {
            if (!lexer.TryConsumeTokenType(TokenType.OperatorMinus, TokenType.OperatorPlus, out var foundTokenType))
            {
                return ParseExponent();
            }
            var operators = new List<TokenType>();
            operators.Add(foundTokenType);
            while (lexer.TryConsumeTokenType(TokenType.OperatorMinus, TokenType.OperatorPlus, out foundTokenType))
            {
                operators.Add(foundTokenType);
            }

            var right = ParseExponent();
            if (right is null)
            {
                throw new ParseException();
            }

            for (var index = operators.Count - 1; index >= 0; index--)
            {
                switch (operators[index])
                {
                    case TokenType.OperatorMinus:
                        right = new UnaryNode(UnaryOperator.Negate, right);
                        break;
                    case TokenType.OperatorPlus:
                        right = new UnaryNode(UnaryOperator.Plus, right);
                        break;
                    default:
                        throw new ParseException();
                }
            }
            return right;
        }

        private AstNode? ParseExponent()
        {
            var left = ParsePrimary();
            if (left is null)
                return null;
            while (lexer.TryConsumeTokenType(TokenType.OperatorExponent))
            {
                var right = ParseExponent();
                if (right is null)
                    throw new Exception();
                left = new BinaryNode(left, BinaryOperator.Exponent, right);
            }

            return left;
        }
        private AstNode? ParsePrimary()
        {
            return ParseParenthetical()?? ParseFunction()?? ParseVariable() ?? ParseNumber();
        }

        private AstNode? ParseParenthetical()
        {
            if (lexer.TryConsumeTokenType((TokenType.ParenOpen)))
            {
                var left = ParseExpression();
                if (left is null)
                    throw new ParseException();
                if (lexer.TryConsumeTokenType(TokenType.ParenClose))
                    return left;
                throw new ParseException();
            }
            return null;
        }
        private AstNode? ParseFunction() 
        {
            if (!lexer.TryConsumeFunction(out var functionInfo))
                return null;
            var functionName = functionInfo.Name;
            var requiredArgumentCount = functionInfo.Arity;
            if (!lexer.TryConsumeTokenType(TokenType.ParenOpen))
                throw new ParseException($"'(' is required after a function name.");
            var arguments = new List<AstNode>(functionInfo.Arity);
            var argument = this.ParseExpression();
            if (argument is null) throw new ParseException("A function argument is expected.");
            arguments.Add(argument);
            while (this.lexer.TryConsumeTokenType(TokenType.Comma))
            {
                argument = this.ParseExpression();
                if (argument is null)
                    throw new ParseException("A function argument is expected.");
                arguments.Add(argument);
            }
            if (!this.lexer.TryConsumeTokenType(TokenType.ParenClose))
                throw new ParseException("A ')' is required after a function's argument list");
            return FunctionNode.Create(functionInfo.Name, arguments);
        }
        /*
        primary
        : parenthetical
        | functionCall
        | variable
        | number;

        functionCall:
        TokenType_Function
        TokenType_OpenParen
        expression
        ( TokenType_Comma expression )*
        TokenType_CloseParen
        ;
         */
        private AstNode? ParseVariable()
        {
            if (lexer.TryConsumeIdentifier(out var text))
            {

                return new VariableNode(text);
            }
            else
            {
                return null;
            }
        }
        private AstNode? ParseNumber()
        {
            if (lexer.TryConsumeNumber(out var number))
            {
                return new NumberNode(number);
            }
            else
            {
                return null;
            }
        }

        #endregion Parse Methods
    }

}
