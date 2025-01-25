namespace JuLang;

public static class Repl {
    private const string Prompt = ">> ";

    public static void Start(TextReader input, TextWriter output) {
        while (true) {
            output.Write(Prompt);
            var line = input.ReadLine();
            if (line == null) // End of input
            {
                return;
            }

            var lexer = new Lexer(line);

            Token token;
            do {
                token = lexer.NextToken();
                if (token.Type != TokenType.Eof) {
                    output.WriteLine(token);
                }
            } while (token.Type != TokenType.Eof);
        }
    }
}