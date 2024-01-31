using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Evaluator
{
    public sealed class ExpressionOptions
    {
        public static void ChangeDecimalPoint(ReadOnlySpan<char> input, Span<char> output, char convertFrom, char convertTo)
        {
            if (convertFrom == convertTo || !input.Contains(convertFrom))
            {
                input.CopyTo(output);
                return;
            }
            input.CopyTo(output);
            for (var i = 0; i < output.Length; i++)
            {
                if (output[i] == convertFrom)
                    output[i] = convertTo;
            }
        }

        public static string ChangeDecimalPoint(string input, char convertFrom, char convertTo)
        {
            return String.Create(
                input.Length,
                (input, convertFrom, convertTo),
                static (outputSpan, args) => ChangeDecimalPoint(
                    args.input,
                    outputSpan,
                    args.convertFrom,
                    args.convertTo
                )
            );
        }
        public ExpressionOptions(char decimalPointCharacter, char argumentSeparator)
        {
            this.ArgumentSeparator = argumentSeparator;
            this.DecimalPointCharacter = decimalPointCharacter;
            Validate();
        }

        public static ExpressionOptions Default { get; } = new(
            decimalPointCharacter: ',',
            argumentSeparator: '.'
        );
        public char ArgumentSeparator { get;  }
        public char DecimalPointCharacter { get; }


        private void Validate()
        {
            if (this.DecimalPointCharacter == this.ArgumentSeparator)
                throw new ArgumentException(
                    $"{nameof(this.ArgumentSeparator)} and {nameof(this.DecimalPointCharacter)} cannot be the same.");
            ValidateCharacterIsValid(this.DecimalPointCharacter);
            ValidateCharacterIsValid(this.ArgumentSeparator);

        }

        private static void ValidateCharacterIsValid(char character,
            [CallerArgumentExpression(nameof(character))] string argumentName = "")
        {
            if (char.IsLetterOrDigit(character) is false && AllowedCharacters.Contains(character))
                return;
            if (argumentName.StartsWith("this."))
                argumentName = argumentName.Substring(5);
            throw new ArgumentException($"{argumentName} must be one of {AllowedCharacters}", argumentName);
        }

        private const string AllowedCharacters = ".,";
    }
}
