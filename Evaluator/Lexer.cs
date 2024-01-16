using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Evaluator
{
    public class Lexer
    {
        private readonly string text;
        private int index;
        private readonly ExpressionOptions options;
        public Lexer(string text, ExpressionOptions options)
        {
            this.options = options;
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

        private static TokenInfo GetNumberTokenInfo(ReadOnlySpan<char> text, ExpressionOptions options)
        {
            var lenght = CountNumberOrDecimal(text, options.DecimalPointCharacter);
            var numberPart = text.Slice(0, lenght);
            return new TokenInfo(ParseDouble(numberPart, options.DecimalPointCharacter), lenght);
        }

        private static double ParseDouble(ReadOnlySpan<char> text, char decimalPointCharacter)
        {
            if (decimalPointCharacter != '.' && text.Contains('.'))
            {
                throw new FormatException(
                    $"{text} mixes the use of invariant decimal '.' and user-specified decimal {decimalPointCharacter}");
            }

            char[]? charArray = null;
            try
            {
                charArray = ArrayPool<char>.Shared.Rent(text.Length);
                var charSpan = charArray.AsSpan();
                charSpan = charSpan.Slice(0, text.Length);
                for (var i = 0; i < text.Length; i++)
                {
                    var toCopy = text[i];
                    if (toCopy == decimalPointCharacter)
                        toCopy = '.';
                    charSpan[i] = toCopy;
                }

                return double.Parse(charSpan, CultureInfo.InvariantCulture);
            }
            finally
            {
                if (charArray is not null)
                    ArrayPool<char>.Shared.Return(charArray);
            }
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
        private static TokenInfo GeIdentifierInfo(ReadOnlySpan<char> text, ExpressionOptions options)
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

        private static bool IsNumberOrDecimal(ReadOnlySpan<char> text, char separator)
        {
            return text[0] == separator || char.IsAsciiDigit(text[0]);
        }

        private static int CountNumberOrDecimal(ReadOnlySpan<char> text , char separator)
        {
            var lenght = 0;
            while (text.IsEmpty is false)
            {
                if (text[0] == separator)
                {
                    lenght += 1;
                    text = text.Slice(1);
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
            var argumentSeparator = this.options.ArgumentSeparator;
            var (tokenType, lenght, value, tokenText, function) = remaining switch
            {
                [] => new TokenInfo(TokenType.Unknown, 0),
                _ when IsNumberOrDecimal(remaining, this.options.DecimalPointCharacter)
                    => GetNumberTokenInfo(remaining, this.options),
                _ when char.IsAsciiLetter(remaining[0])
                    => GeIdentifierInfo(remaining,this.options),
                ['(', ..] => new TokenInfo(TokenType.ParenOpen, 1),
                [')', ..] => new TokenInfo(TokenType.ParenClose, 1),
                ['*', '*', ..] or ['^',..] => new TokenInfo(TokenType.OperatorExponent, 2),
                ['/', ..] => new TokenInfo(TokenType.OperatorDivide, 1),
                ['*', ..] => new TokenInfo(TokenType.OperatorMultiply, 1),
                ['+', ..] => new TokenInfo(TokenType.OperatorPlus, 1),
                ['-', ..] => new TokenInfo(TokenType.OperatorMinus, 1),
                [',',..] => new TokenInfo(TokenType.Comma, 1),
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
