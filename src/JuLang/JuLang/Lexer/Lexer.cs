namespace JuLang;

public class Lexer {
    private readonly string _input;
    private int _position; // current position in input (points to current char)
    private int _readPosition; // current reading position in input (after current char)
    private char _ch; // current char under examination

    public Lexer(string input) {
        _input = input;
        ReadChar();
    }

    public Token NextToken() {
        Token tok;

        SkipWhitespace();

        switch (_ch) {
            case '=':
                if (PeekChar() == '=') {
                    var ch = _ch;
                    ReadChar();
                    var literal = $"{ch}{_ch}";
                    tok = new Token(TokenType.Eq, literal);
                }
                else {
                    tok = NewToken(TokenType.Assign, _ch);
                }

                break;
            case '+':
                tok = NewToken(TokenType.Plus, _ch);
                break;
            case '-':
                tok = NewToken(TokenType.Minus, _ch);
                break;
            case '!':
                if (PeekChar() == '=') {
                    var ch = _ch;
                    ReadChar();
                    var literal = $"{ch}{_ch}";
                    tok = new Token(TokenType.NotEq, literal);
                }
                else {
                    tok = NewToken(TokenType.Bang, _ch);
                }

                break;
            case '/':
                tok = NewToken(TokenType.Slash, _ch);
                break;
            case '*':
                tok = NewToken(TokenType.Asterisk, _ch);
                break;
            case '<':
                tok = NewToken(TokenType.Lt, _ch);
                break;
            case '>':
                tok = NewToken(TokenType.Gt, _ch);
                break;
            case ';':
                tok = NewToken(TokenType.Semicolon, _ch);
                break;
            case ',':
                tok = NewToken(TokenType.Comma, _ch);
                break;
            case '{':
                tok = NewToken(TokenType.Lbrace, _ch);
                break;
            case '}':
                tok = NewToken(TokenType.Rbrace, _ch);
                break;
            case '(':
                tok = NewToken(TokenType.Lparen, _ch);
                break;
            case ')':
                tok = NewToken(TokenType.Rparen, _ch);
                break;
            case '\0':
                tok = new Token(TokenType.Eof, "");
                break;
            default:
                if (IsLetter(_ch)) {
                    var literal = ReadIdentifier();
                    var type = Keywords.LookupIdent(literal);
                    return new Token(type, literal);
                }

                if (IsDigit(_ch)) {
                    var literal = ReadNumber();
                    return new Token(TokenType.Int, literal);
                }

                tok = NewToken(TokenType.Illegal, _ch);

                break;
        }

        ReadChar();
        return tok;
    }

    private void SkipWhitespace() {
        while (_ch == ' ' || _ch == '\t' || _ch == '\n' || _ch == '\r') {
            ReadChar();
        }
    }

    private void ReadChar() {
        _ch = _readPosition >= _input.Length ? '\0' : _input[_readPosition];
        _position = _readPosition;
        _readPosition++;
    }

    private char PeekChar() {
        return _readPosition >= _input.Length ? '\0' : _input[_readPosition];
    }

    private string ReadIdentifier() {
        var startPosition = _position;
        while (IsLetter(_ch)) {
            ReadChar();
        }

        return _input.Substring(startPosition, _position - startPosition);
    }

    private string ReadNumber() {
        var startPosition = _position;
        while (IsDigit(_ch)) {
            ReadChar();
        }

        return _input.Substring(startPosition, _position - startPosition);
    }

    private static bool IsLetter(char ch) {
        return char.IsLetter(ch) || ch == '_';
    }

    private static bool IsDigit(char ch) {
        return char.IsDigit(ch);
    }

    private static Token NewToken(TokenType tokenType, char ch) {
        return new Token(tokenType, ch.ToString());
    }
}