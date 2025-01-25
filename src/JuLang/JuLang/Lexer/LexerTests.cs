using Xunit;

namespace JuLang;

public class LexerTests {
    [Fact]
    public void TestNextToken() {
        const string input = """
                             let five = 5;
                             let ten = 10;

                             let add = fn(x, y) {
                               x + y;
                             };

                             let result = add(five, ten);
                             !-/*5;
                             5 < 10 > 5;

                             if (5 < 10) {
                                 return true;
                             } else {
                                 return false;
                             }

                             10 == 10;
                             10 != 9;

                             """;
        var tests = new List<(TokenType ExpectedType, string ExpectedLiteral)> {
            (TokenType.Let, "let"),
            (TokenType.Ident, "five"),
            (TokenType.Assign, "="),
            (TokenType.Int, "5"),
            (TokenType.Semicolon, ";"),
            (TokenType.Let, "let"),
            (TokenType.Ident, "ten"),
            (TokenType.Assign, "="),
            (TokenType.Int, "10"),
            (TokenType.Semicolon, ";"),
            (TokenType.Let, "let"),
            (TokenType.Ident, "add"),
            (TokenType.Assign, "="),
            (TokenType.Function, "fn"),
            (TokenType.Lparen, "("),
            (TokenType.Ident, "x"),
            (TokenType.Comma, ","),
            (TokenType.Ident, "y"),
            (TokenType.Rparen, ")"),
            (TokenType.Lbrace, "{"),
            (TokenType.Ident, "x"),
            (TokenType.Plus, "+"),
            (TokenType.Ident, "y"),
            (TokenType.Semicolon, ";"),
            (TokenType.Rbrace, "}"),
            (TokenType.Semicolon, ";"),
            (TokenType.Let, "let"),
            (TokenType.Ident, "result"),
            (TokenType.Assign, "="),
            (TokenType.Ident, "add"),
            (TokenType.Lparen, "("),
            (TokenType.Ident, "five"),
            (TokenType.Comma, ","),
            (TokenType.Ident, "ten"),
            (TokenType.Rparen, ")"),
            (TokenType.Semicolon, ";"),
            (TokenType.Bang, "!"),
            (TokenType.Minus, "-"),
            (TokenType.Slash, "/"),
            (TokenType.Asterisk, "*"),
            (TokenType.Int, "5"),
            (TokenType.Semicolon, ";"),
            (TokenType.Int, "5"),
            (TokenType.Lt, "<"),
            (TokenType.Int, "10"),
            (TokenType.Gt, ">"),
            (TokenType.Int, "5"),
            (TokenType.Semicolon, ";"),
            (TokenType.If, "if"),
            (TokenType.Lparen, "("),
            (TokenType.Int, "5"),
            (TokenType.Lt, "<"),
            (TokenType.Int, "10"),
            (TokenType.Rparen, ")"),
            (TokenType.Lbrace, "{"),
            (TokenType.Return, "return"),
            (TokenType.True, "true"),
            (TokenType.Semicolon, ";"),
            (TokenType.Rbrace, "}"),
            (TokenType.Else, "else"),
            (TokenType.Lbrace, "{"),
            (TokenType.Return, "return"),
            (TokenType.False, "false"),
            (TokenType.Semicolon, ";"),
            (TokenType.Rbrace, "}"),
            (TokenType.Int, "10"),
            (TokenType.Eq, "=="),
            (TokenType.Int, "10"),
            (TokenType.Semicolon, ";"),
            (TokenType.Int, "10"),
            (TokenType.NotEq, "!="),
            (TokenType.Int, "9"),
            (TokenType.Semicolon, ";"),
            (TokenType.Eof, "")
        };

        var lexer = new Lexer(input);

        foreach (var test in tests) {
            var token = lexer.NextToken();
            Assert.Equal(test.ExpectedType, token.Type);
            Assert.Equal(test.ExpectedLiteral, token.Literal);
        }
    }

    [Fact]
    public void TestSingleCharacterTokens() {
        var input = "=+-*/(){}";
        var expectedTokens = new List<Token> {
            new(TokenType.Assign, "="),
            new(TokenType.Plus, "+"),
            new(TokenType.Minus, "-"),
            new(TokenType.Asterisk, "*"),
            new(TokenType.Slash, "/"),
            new(TokenType.Lparen, "("),
            new(TokenType.Rparen, ")"),
            new(TokenType.Lbrace, "{"),
            new(TokenType.Rbrace, "}")
        };

        var lexer = new Lexer(input);
        foreach (var expectedToken in expectedTokens) {
            var token = lexer.NextToken();
            Assert.Equal(expectedToken.Type, token.Type);
            Assert.Equal(expectedToken.Literal, token.Literal);
        }
    }

    [Fact]
    public void TestKeywords() {
        var input = "fn let true false if else return";
        var expectedTokens = new List<Token> {
            new(TokenType.Function, "fn"),
            new(TokenType.Let, "let"),
            new(TokenType.True, "true"),
            new(TokenType.False, "false"),
            new(TokenType.If, "if"),
            new(TokenType.Else, "else"),
            new(TokenType.Return, "return")
        };

        var lexer = new Lexer(input);
        foreach (var expectedToken in expectedTokens) {
            var token = lexer.NextToken();
            Assert.Equal(expectedToken.Type, token.Type);
            Assert.Equal(expectedToken.Literal, token.Literal);
        }
    }

    [Fact]
    public void TestIdentifiersAndLiterals() {
        var input = "add 12345";
        var expectedTokens = new List<Token> {
            new(TokenType.Ident, "add"),
            new(TokenType.Int, "12345")
        };

        var lexer = new Lexer(input);
        foreach (var expectedToken in expectedTokens) {
            var token = lexer.NextToken();
            Assert.Equal(expectedToken.Type, token.Type);
            Assert.Equal(expectedToken.Literal, token.Literal);
        }
    }

    [Fact]
    public void TestOperators() {
        var input = "== != < >";
        var expectedTokens = new List<Token> {
            new(TokenType.Eq, "=="),
            new(TokenType.NotEq, "!="),
            new(TokenType.Lt, "<"),
            new(TokenType.Gt, ">")
        };

        var lexer = new Lexer(input);
        foreach (var expectedToken in expectedTokens) {
            var token = lexer.NextToken();
            Assert.Equal(expectedToken.Type, token.Type);
            Assert.Equal(expectedToken.Literal, token.Literal);
        }
    }

    [Fact]
    public void TestDelimiters() {
        var input = ",;";
        var expectedTokens = new List<Token> {
            new(TokenType.Comma, ","),
            new(TokenType.Semicolon, ";")
        };

        var lexer = new Lexer(input);
        foreach (var expectedToken in expectedTokens) {
            var token = lexer.NextToken();
            Assert.Equal(expectedToken.Type, token.Type);
            Assert.Equal(expectedToken.Literal, token.Literal);
        }
    }
}