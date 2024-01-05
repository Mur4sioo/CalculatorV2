namespace Evaluator
{
    public record Token(TokenType TokenType, double Number, string Text = "")
    {
        public static Token Unknown { get; } = new Token(TokenType.Unknown, 0, string.Empty);
    }

}