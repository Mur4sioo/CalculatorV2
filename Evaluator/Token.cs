namespace Evaluator
{
    public record Token(TokenType TokenType, double Number, string Text = "", FunctionInfo function = default)
    {
        public static Token Unknown { get; } = new Token(TokenType.Unknown, 0, string.Empty);
    }

}