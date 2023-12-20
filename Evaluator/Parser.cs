using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluator
{
    public class Parser
    {
        private readonly Lexer lexer;
        public Parser(Lexer lexer)
        {
            this.lexer = lexer;
        }

        public static AstNode ParseExpression(string input)
        {
            var lexer = new Lexer(input);
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
            var left = ParsePrimary();
            if (left is null)
                return null;
            while (lexer.TryConsumeTokenType(TokenType.OperatorMultiply, TokenType.OperatorDivide, out var foundTokenType))
            {
                var right = ParsePrimary();
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
                return ParsePrimary();
            }
            var operators = new List<TokenType>();
            operators.Add(foundTokenType);
            while (lexer.TryConsumeTokenType(TokenType.OperatorMinus, TokenType.OperatorPlus, out foundTokenType))
            {
                operators.Add(foundTokenType);
            }

            var right = ParsePrimary();
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
        private AstNode? ParsePrimary()
        {
            return ParseParenthetical() ?? ParseNumber();
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
