namespace Evaluator
{
    public record Token(TokenType TokenType, double Number)
    {
        public static Token Unknown { get; } = new Token(TokenType.Unknown, 0);
    }

}