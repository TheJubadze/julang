namespace JuLang;

public class Token(TokenType type, string literal) {
    public TokenType Type { get; } = type;
    public string Literal { get; } = literal;

    public override string ToString() => $"{{ Type: {Type}, Literal: \"{Literal}\" }}";
}

public enum TokenType {
    // General
    Illegal,
    Eof,

    // Identifiers + literals
    Ident, // e.g., add, foobar, x, y, ...
    Int, // e.g., 1343456

    // Operators
    Assign,
    Plus,
    Minus,
    Bang,
    Asterisk,
    Slash,

    Lt,
    Gt,

    Eq,
    NotEq,

    // Delimiters
    Comma,
    Semicolon,

    Lparen,
    Rparen,
    Lbrace,
    Rbrace,

    // Keywords
    Function,
    Let,
    True,
    False,
    If,
    Else,
    Return
}

public static class Keywords {
    private static readonly Dictionary<string, TokenType> _keywords = new() {
        { "fn", TokenType.Function },
        { "let", TokenType.Let },
        { "true", TokenType.True },
        { "false", TokenType.False },
        { "if", TokenType.If },
        { "else", TokenType.Else },
        { "return", TokenType.Return }
    };

    public static TokenType LookupIdent(string ident) {
        return _keywords.GetValueOrDefault(ident, TokenType.Ident);
    }

    public static TokenType Get(string key) {
        return _keywords[key];
    }
}