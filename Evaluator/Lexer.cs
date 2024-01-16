using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluator
{
    public class Lexer
    {
        private readonly string text;
        private int index;
        private readonly CultureInfo culture;
        public Lexer(string text, CultureInfo? culture)
        {
            this.culture = culture ?? CultureInfo.CurrentCulture;
            this.text = text;
            Read();
        }

        private static readonly Dictionary<char, TokenType> singleCharTokens = new Dictionary<char, TokenType>()
        {
            { '(', TokenType.ParenOpen },
            { ')', TokenType.ParenClose },
            { '+', TokenType.OperatorPlus },
            { '-', TokenType.OperatorMinus },
            { '*', TokenType.OperatorMultiply },
            { '/', TokenType.OperatorDivide },
        };

        private readonly record struct TokenInfo(TokenType Type, int Lenght, double Value, string Text, FunctionInfo Function)
        {
            public TokenInfo(TokenType type, int lenght) : this(type, lenght, 0, string.Empty, default)
            {

            }
            public TokenInfo(string Value)
                : this(TokenType.Identifier, Value.Length, 0, Value, default)
            {
            }
            public TokenInfo(FunctionInfo Value)
                : this(TokenType.Function, Value.Name.Length, 0, Value.Name, Value)
            {
            }

            public TokenInfo(double Value, int lenght)
                : this(TokenType.Number, lenght, Value, string.Empty, default)
            {
            }
        }

        private static TokenInfo GetNumberTokenInfo(ReadOnlySpan<char> text, CultureInfo culture)
        {
            var lenght = CountNumberOrDecimal(text, culture.NumberFormat.NumberDecimalSeparator);
            var numberPart = text.Slice(0, lenght);
            return new TokenInfo(double.Parse(numberPart, culture), lenght);
        }
        private static int CountCharInFunction(ReadOnlySpan<char> text, string? separator = null)
        {
            var lenght = 0;
            while (text.IsEmpty is false)
            {
                if (char.IsAsciiLetter(text[0]))
                {
                    lenght += 1;
                    text = text.Slice(1);
                }
                else
                {
                    return lenght;
                }
            }
            return lenght;
        }

        public static bool IsFunction(ReadOnlySpan<char> text, out FunctionInfo function)
        {
            foreach (var func in FunctionList.Functions)
            {
                if (text.Equals(func.Name, StringComparison.Ordinal))
                { 
                function = func;
                return true;
                }
            }
            function = default;
            return false;
        }
        private static TokenInfo GeIdentifierInfo(ReadOnlySpan<char> text, CultureInfo culture)
        {
            var lenght = CountCharInFunction(text);
            var functionPart = text.Slice(0, lenght);
            if (IsFunction(functionPart, out var functuion))
                return new TokenInfo(functuion);
            return new TokenInfo(functionPart.Slice(0, 1).ToString());

        }

        public Token Current { get; private set; } = Token.Unknown;
        public bool IsFinished { get; private set; }
        public ReadOnlySpan<char> GetRemainingText() => this.text.AsSpan().Slice(index);

        private static bool IsNumberOrDecimal(ReadOnlySpan<char> text, string separator)
        {
            return text.StartsWith(separator) || char.IsAsciiDigit(text[0]);
        }

        private static int CountNumberOrDecimal(ReadOnlySpan<char> text , string separator)
        {
            var lenght = 0;
            while (text.IsEmpty is false)
            {
                if (text.StartsWith(separator))
                {
                    lenght += separator.Length;
                    text = text.Slice(separator.Length);
                }
                else if (char.IsAsciiDigit(text[0]))
                {
                    lenght += 1;
                    text = text.Slice(1);
                }
                else
                {
                    return lenght;
                }
            }

            return lenght;
        }

        private void SkipWhiteSpace()
        {
            var count = CountWhiteSpaceCharacters(GetRemainingText());
            index += count;
        }

        private static int CountWhiteSpaceCharacters(ReadOnlySpan<char> text)
        {
            for (var i = 0; i < text.Length; i++)
            {
                if (char.IsWhiteSpace(text[i]) is false)
                    return i;
            }
            return text.Length;
        }

        public bool Read()
        {
            SkipWhiteSpace();
            var remaining = GetRemainingText();
            var (tokenType, lenght, value, tokenText, function) = remaining switch
            {
                [] => new TokenInfo(TokenType.Unknown, 0),
                _ when IsNumberOrDecimal(remaining, this.culture.NumberFormat.NumberDecimalSeparator)
                    => GetNumberTokenInfo(remaining, this.culture),
                _ when char.IsAsciiLetter(remaining[0])
                    => GeIdentifierInfo(remaining,this.culture),
                ['(', ..] => new TokenInfo(TokenType.ParenOpen, 1),
                [')', ..] => new TokenInfo(TokenType.ParenClose, 1),
                ['*', '*', ..] or ['^',..] => new TokenInfo(TokenType.OperatorExponent, 2),
                ['/', ..] => new TokenInfo(TokenType.OperatorDivide, 1),
                ['*', ..] => new TokenInfo(TokenType.OperatorMultiply, 1),
                ['+', ..] => new TokenInfo(TokenType.OperatorPlus, 1),
                ['-', ..] => new TokenInfo(TokenType.OperatorMinus, 1),
                ['.',..] => new TokenInfo(TokenType.Comma, 1),
                _ => new TokenInfo(TokenType.Unknown, 1)
            };
            this.Current = new Token(tokenType, value, tokenText, function);
            this.index += lenght;
            this.IsFinished = lenght == 0;
            return lenght > 0;
        }

        public bool TryConsumeTokenType(TokenType tokenType, out double number, out string tokenText, out FunctionInfo function)
        {
            number = 0;
            tokenText = string.Empty;
            function = default;
            if (IsFinished || this.Current.TokenType != tokenType)
            {
                return false;
            }
            number = this.Current.Number;
            tokenText = this.Current.Text;
            function = this.Current.function;
            Read();
            return true;
        }
        public bool TryConsumeTokenType(TokenType tokenType)
        {
            return TryConsumeTokenType(tokenType, out _, out _, out _);
        }
        public bool TryConsumeNumber(out double number)
        {
            return TryConsumeTokenType(TokenType.Number, out number, out _, out _);
        }
        public bool TryConsumeTokenType(TokenType a, TokenType b, out TokenType found)
        {
            if (TryConsumeTokenType(a))
            {
                found = a;
                return true;
            }

            if (TryConsumeTokenType(b))
            {
                found = b;
                return true;
            }

            found = default;
            return false;
        }

        public bool TryConsumeIdentifier(out string tokenText)
        {
            return TryConsumeTokenType(TokenType.Identifier, out _, out tokenText, out _);
        }
        public bool TryConsumeFunction (out FunctionInfo function)
        {
            return TryConsumeTokenType(TokenType.Function, out _, out _, out function);
        }
    }
}
