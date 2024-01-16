using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Evaluator
{
    internal class ExpressionOptions
    {
        public ExpressionOptions(char decimalPointCharacter, char argumentSeparator)
        {
            this.ArgumentSeparator = argumentSeparator;
            this.DecimalPointCharacter = decimalPointCharacter;
            Validate();
        }
        public char ArgumentSeparator { get; }
        public char DecimalPointCharacter { get; }

        private void Validate()
        {
            if (this.DecimalPointCharacter == this.ArgumentSeparator)
                throw new ArgumentException(
                    $"{nameof(this.ArgumentSeparator)} and {nameof(this.DecimalPointCharacter)} cannot be the same.");

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
