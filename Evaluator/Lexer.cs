using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluator
{
    public class Lexer
    {
        private readonly string text;
        private int index;
        public Lexer(string text)
        {
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
        public Token Current { get; private set; } = Token.Unknown;
        public bool IsFinished { get; private set; }
        public ReadOnlySpan<char> GetRemainingText() => this.text.AsSpan().Slice(index);
        private static bool IsNumberOrDecimal(char ch) => ch == '.' || char.IsAsciiDigit(ch);

        private static int CountNumberOrDecimal(ReadOnlySpan<char> text)
        {
            for (var i = 0; i < text.Length; i++)
            {
                if (IsNumberOrDecimal(text[i]) is false)
                    return i;
            }
            return text.Length;
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
            if (IsFinished)
            {
                this.Current = Token.Unknown;
                return false;
            }

            SkipWhiteSpace();
            var remaining = GetRemainingText();
            if (remaining.IsEmpty)
            {
                this.IsFinished = true;
                this.Current = Token.Unknown;
                return false;
            }
            var firstCharacter = remaining[0];
            if (singleCharTokens.TryGetValue(firstCharacter, out var tokenType) is true)
            {
                this.Current = new Token(tokenType, 0);
                index++;
                return true;
            }

            if (IsNumberOrDecimal(firstCharacter))
            {
                var length = CountNumberOrDecimal(remaining);
                var numberPart = remaining.Slice(0, length);
                this.Current = new Token(TokenType.Number, double.Parse(numberPart));
                index += length;
                return true;
            }

            return false;
        }
        public bool TryConsumeTokenType(TokenType tokenType, out double number)
        {
            number = 0;
            if (IsFinished || this.Current.TokenType != tokenType)
            {
                return false;
            }
            number = this.Current.Number;
            Read();
            return true;
        }
        public bool TryConsumeTokenType(TokenType tokenType)
        {
            return TryConsumeTokenType(tokenType, out _);
        }
        public bool TryConsumeNumber(out double number)
        {
            return TryConsumeTokenType(TokenType.Number, out number);
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
    }
}
